const db = require('../db')
class Controller{
    async getMinDate(req, res)
    {
        const minDate = await db.query('select MIN("SeanceDate") AS "date" FROM "Seance" WHERE "SeanceDate" > CURRENT_DATE OR ("SeanceDate" = CURRENT_DATE AND "SeanceTime" > CURRENT_TIME)')
        res.json(minDate.rows[0])
    }

    async getFilmSeances(req, res)
    {
        const {date} = req.body
        const films = await db.query('select \n' +
        'f."FilmId", f."FilmName", MAX(cc."CountryName") "CountryName", MAX(mm."MinAgeValue") "MinAgeValue", "f"."FilmDuration", "f"."FilmDescription", "f"."FilmYear", "f"."FilmCover", "f"."FilmTrailerLink", \n' +
        'ARRAY_AGG(DISTINCT gg."GenreId" || \';\' || gg."GenreName") genres, \n' +
	'COUNT(DISTINCT gg."GenreId"), \n' +
        'ARRAY_AGG(s."SeanceId" || \';\' || s."SeanceDate" || \';\' || s."SeanceCost" || \';\' || s."SeanceTime" ORDER BY s."SeanceTime" DESC) seances\n' +
        'FROM\n' +
        '"Film" f\n' +
        'LEFT JOIN "Seance" s on f."FilmId" = s."FilmId"\n' +
        'LEFT JOIN "FilmGenre" fg on f."FilmId" = fg."FilmId"\n' +
        'LEFT JOIN "Genre" gg on fg."GenreId" = gg."GenreId"\n' +
        ',"Country" cc\n' +
        ',"MinAge" mm\n' +
        'WHERE s."SeanceDate" = $1\n' +
	'AND ((s."SeanceDate" = CURRENT_DATE AND s."SeanceTime" > CURRENT_TIME) OR (s."SeanceDate" != CURRENT_DATE))\n' +
        'AND cc."CountryId" = f."CountryId"\n' +
        'AND mm."MinAgeId" = f."MinAgeId"\n' +
        'GROUP BY f."FilmId"\n' +
        'ORDER BY f."FilmYear" DESC', [date])
        res.json(films.rows)
    }
    async getFilms(req, res){
        const {date} = req.body
        const films = await db.query('select DISTINCT ON ("Seance"."FilmId") "Film"."FilmId", "Film"."FilmName", "Country"."CountryName", "MinAge"."MinAgeValue", "Film"."FilmDuration", "Film"."FilmDescription", "Film"."FilmYear", "Film"."FilmCover", "Film"."FilmTrailerLink"\n' +
            'FROM "Film" inner join "Seance" on "Film"."FilmId" = "Seance"."FilmId"\n' +
            'inner join "Country" on "Film"."CountryId" = "Country"."CountryId"\n' +
            'inner join "MinAge" on "Film"."MinAgeId" = "MinAge"."MinAgeId"\n' +
            'WHERE "Seance"."SeanceDate" = $1', [date])
        res.json(films.rows)
    }

    async getGenres(req, res){
        const {filmId} = req.body
        const genres = await db.query('select "Genre"."GenreId", "Genre"."GenreName" from "Genre"\n' +
            'inner join "FilmGenre" on "FilmGenre"."GenreId" = "Genre"."GenreId"\n' +
            'WHERE "FilmGenre"."FilmId" = $1', [filmId])
            res.json(genres.rows)
    }

    async getSeances(req, res){
        const {filmId, date} = req.body
        const seances = await db.query('SELECT * FROM "Seance" WHERE "FilmId" = $1 AND "SeanceDate" = $2 ORDER BY "SeanceTime" DESC', [filmId, date])
        res.json(seances.rows)
    }

    /*async getPlaces(req, res){
        const {hallId} = req.body
        const places = await db.query('SELECT * FROM "Place" WHERE "HallId" = $1', [hallId])
        res.json(places.rows)
    }*/

    async getPlaces(req, res){
        const {seanceId} = req.body
        const hall = await db.query('SELECT "HallId" FROM "Seance" WHERE "SeanceId" = $1', [seanceId])
        try {
            var hallId = hall.rows[0].HallId
        }
        catch
        {
            return
        }
        const places = await db.query('SELECT * FROM "Place" WHERE "HallId" = $1 ORDER BY "PlaceRow", "PlaceNumber"', [hallId])
        const busyPlaces = await db.query('SELECT "PlaceId" FROM "Ticket" WHERE "SeanceId" = $1', [seanceId])
        const maxNumber = await db.query('SELECT MAX("PlaceNumber") FROM "Place" WHERE "HallId" = $1', [hallId])
        const rows = await db.query('SELECT MAX("PlaceRow") FROM "Place" WHERE "HallId" = $1', [hallId])
        res.json({
            all: places.rows,
            busy: busyPlaces.rows,
            maxNumber: maxNumber.rows[0].max,
            rows: rows.rows[0].max
        })
    }

    async getBusyPlaces(req, res){
        const {seanceId} = req.body
        const places = await db.query('SELECT "PlaceId" FROM "Ticket" WHERE "SeanceId" = $1', [seanceId])
        res.json(places.rows)
    }

    async getPlaceTypes(req, res){
        const placeTypes = await db.query('SELECT * FROM "PlaceType"')
        res.json(placeTypes.rows)
    }

    async createTicket(req, res){
        const {seanceId, placeId} = req.body
        let seanceCost = await db.query('SELECT "SeanceCost" FROM "Seance" WHERE "SeanceId" = $1', [seanceId])
        let placeCost = await db.query('select "PlaceTypeCost" from "PlaceType" inner join "Place" on "PlaceType"."PlaceTypeId" = "Place"."PlaceTypeId"\n' +
            'WHERE "Place"."PlaceId" = $1', [placeId])

        seanceCost = seanceCost.rows[0].SeanceCost
        placeCost = placeCost.rows[0].PlaceTypeCost

        const ticketCost = (seanceCost + placeCost) * 0.8

        let ticketCode = 100000
        let isExist = null

        do {
            ticketCode = between(100000, 1000000)
            isExist = await db.query('SELECT * FROM "Ticket" WHERE "TicketCode" = $1', [ticketCode])
        } while (isExist.rows[0] != undefined)

        const ticket = await db.query('INSERT INTO "Ticket" ("SeanceId", "TicketDateTime", "PlaceId", "TicketCost", "TicketCode", "TicketActive")\n' +
            'VALUES ($1, NOW(), $2, $3, $4, true) RETURNING *', [seanceId, placeId, ticketCost, ticketCode])
        const seance = await db.query('SELECT * FROM "Seance" WHERE "SeanceId" = $1', [seanceId])
        const hallId = seance.rows[0].HallId
        const place = await db.query('SELECT * FROM "Place" WHERE "PlaceId" = $1', [placeId])
        const hall = await db.query('SELECT * FROM "Hall" WHERE "HallId" = $1', [hallId])
        res.json({
            t: ticket.rows[0],
            p: place.rows[0],
            h: hall.rows[0]
        })
    }

    async checkTicket(req, res){
        const {seanceId, places} = req.body
        let count = await db.query('SELECT COUNT(*) FROM "Ticket" WHERE "SeanceId" = $1 AND "PlaceId" = ANY($2::int[])', [seanceId, places])
        res.json(count.rows[0].count)
    }

    async getFilm(req, res){
        const {filmId} = req.body
        let film = await db.query('SELECT * FROM "Film" WHERE "FilmId" = $1', [filmId])
        res.json(film.rows[0])
    }

    async getScreenshots(req, res){
        const {filmId} = req.body
        let screens = await db.query('SELECT * FROM "Screenshot" WHERE "FilmId" = $1', [filmId])
        res.json(screens.rows)
    }

    async getScreenshotsCount(req, res){
        const {filmId} = req.body
        let screensCount = await db.query('SELECT COUNT("ScreenshotId") FROM "Screenshot" WHERE "FilmId" = $1', [filmId])
        res.json(screensCount.rows[0].count)
    }

    async ticketControl(req, res)
    {
        const {code} = req.body
        let TicketActive = await db.query('SELECT "TicketActive" FROM "Ticket" WHERE "TicketCode" = $1', [code])
        try {
            var isActive = TicketActive.rows[0].TicketActive
        }
        catch
        {
            res.json(false)
            return
        }
        if (isActive)
        {
            await db.query(`UPDATE "Ticket" SET "TicketActive" = false WHERE "TicketCode" = '${code}'`)
        }
        res.json(isActive)
    }
}

function between(min, max) {
    return Math.floor(
        Math.random() * (max - min) + min
    )
}

module.exports = new Controller()