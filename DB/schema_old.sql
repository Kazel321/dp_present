CREATE TABLE "Country"
(
    "CountryId" SERIAL PRIMARY KEY NOT NULL,
    "CountryName" VARCHAR NOT NULL
);

CREATE TABLE "MinAge"
(
    "MinAgeId" SERIAL PRIMARY KEY NOT NULL,
    "MinAgeValue" INTEGER NOT NULL
);

CREATE TABLE "Genre"
(
    "GenreId" SERIAL PRIMARY KEY NOT NULL,
    "GenreName" VARCHAR NOT NULL
);

CREATE TABLE "Film"
(
    "FilmId" SERIAL PRIMARY KEY NOT NULL,
    "FilmName" VARCHAR NOT NULL,
    "CountryId" INTEGER NOT NULL,
    "MinAgeId" INTEGER NOT NULL,
    "FilmDuration" TIME NOT NULL,
    "FilmDescription" VARCHAR NULL,
    "FilmYear" INTEGER NOT NULL,
    "FilmCover" BYTEA NULL,
    FOREIGN KEY ("CountryId") REFERENCES "Country"("CountryId"),
    FOREIGN KEY ("MinAgeId") REFERENCES "MinAge"("MinAgeId")
);

CREATE TABLE "FilmGenre"
(
    "FilmId" INTEGER NOT NULL,
    "GenreId" INTEGER NOT NULL,
    PRIMARY KEY ("FilmId", "GenreId"),
    FOREIGN KEY ("FilmId") REFERENCES "Film"("FilmId"),
    FOREIGN KEY ("GenreId") REFERENCES "Genre"("GenreId")
);

CREATE TABLE "Hall"
(
    "HallId" SERIAL PRIMARY KEY NOT NULL,
    "HallName" VARCHAR NOT NULL
);

CREATE TABLE "PlaceType"
(
    "PlaceTypeId" SERIAL PRIMARY KEY NOT NULL,
    "PlaceTypeName" VARCHAR NOT NULL,
    "PlaceTypeCost" REAL NOT NULL
);

CREATE TABLE "Place"
(
    "PlaceId" SERIAL PRIMARY KEY NOT NULL,
    "HallId" INTEGER NOT NULL,
    "PlaceNumber" INTEGER NOT NULL,
    "PlaceRow" INTEGER NOT NULL,
    "PlaceTypeId" INTEGER NOT NULL,
    FOREIGN KEY ("HallId") REFERENCES "Hall"("HallId"),
    FOREIGN KEY ("PlaceTypeId") REFERENCES "PlaceType"("PlaceTypeId")
);

CREATE TABLE "Role"
(
    "RoleId" SERIAL PRIMARY KEY NOT NULL,
    "RoleName" VARCHAR NOT NULL
);

CREATE TABLE "Screenshot"
(
    "ScreenshotId" SERIAL PRIMARY KEY NOT NULL,
    "FilmId" INTEGER NOT NULL,
    "ScreenshotImage" BYTEA NOT NULL,
    FOREIGN KEY ("FilmId") REFERENCES "Film"("FilmId")
);

CREATE TABLE "Seance"
(
    "SeanceId" SERIAL PRIMARY KEY NOT NULL,
    "FilmId" INTEGER NOT NULL,
    "HallId" INTEGER NOT NULL,
    "SeanceDate" DATE NOT NULL,
    "SeanceTime" TIME NOT NULL,
    "SeanceCost" REAL NOT NULL,
    FOREIGN KEY ("FilmId") REFERENCES "Film"("FilmId"),
    FOREIGN KEY ("HallId") REFERENCES "Hall"("HallId")
);

CREATE TABLE "Ticket"
(
    "TicketId" SERIAL PRIMARY KEY NOT NULL,
    "SeanceId" INTEGER NOT NULL,
    "TicketDateTime" TIMESTAMP NOT NULL,
    "PlaceId" INTEGER NOT NULL,
    "TicketCost" REAL NOT NULL,
    "TicketCode" CHAR(6) NOT NULL,
    "TicketActive" BOOLEAN NOT NULL,
    FOREIGN KEY ("SeanceId") REFERENCES "Seance"("SeanceId"),
    FOREIGN KEY ("PlaceId") REFERENCES "Place"("PlaceId")
);

CREATE TABLE "User"
(
    "UserId" SERIAL PRIMARY KEY NOT NULL,
    "RoleId" INTEGER NOT NULL,
    "UserEmail" VARCHAR NOT NULL,
    "UserLogin" VARCHAR NOT NULL,
    "UserPassword" VARCHAR NOT NULL,
    "UserLastName" VARCHAR NOT NULL,
    "UserFirstName" VARCHAR NOT NULL,
    "UserPatronymic" VARCHAR NOT NULL,
    "UserPhoneNumber" CHAR(15) NOT NULL,
    "UserBirthDate" DATE NOT NULL,
    "UserActive" BOOLEAN NOT NULL,
    FOREIGN KEY ("RoleId") REFERENCES "Role"("RoleId")
);

CREATE TABLE "Login"
(
    "LoginId" SERIAL PRIMARY KEY NOT NULL,
    "UserId" INTEGER NOT NULL,
    "LoginDateTime" TIMESTAMP NOT NULL,
    FOREIGN KEY ("UserId") REFERENCES "User"("UserId")
);

copy "Country" FROM 'D:\1study\DiplomAPI\db\import\country.csv' DELIMITER ';';
copy "MinAge" FROM 'D:\1study\DiplomAPI\db\import\minAge.csv' DELIMITER ';';
--вручную
--copy "Film" FROM 'D:\1study\DiplomAPI\db\import\film.csv' DELIMITER ';';
copy "Genre" FROM 'D:\1study\DiplomAPI\db\import\genre.csv' DELIMITER ';';
copy "FilmGenre" FROM 'D:\1study\DiplomAPI\db\import\filmGenre.csv' DELIMITER ';';
copy "Hall" FROM 'D:\1study\DiplomAPI\db\import\hall.csv' DELIMITER ';';
copy "PlaceType" FROM 'D:\1study\DiplomAPI\db\import\placeType.csv' DELIMITER ';';
copy "Place" FROM 'D:\1study\DiplomAPI\db\import\place.csv' DELIMITER ';';
copy "Seance" FROM 'D:\1study\DiplomAPI\db\import\seance.csv' DELIMITER ';';
copy "Role" FROM 'D:\1study\DiplomAPI\db\import\role.csv' DELIMITER ';';
copy "User" FROM 'D:\1study\DiplomAPI\db\import\user.csv' DELIMITER ';';
--вручную
--copy "Screenshot" FROM 'D:\1study\DiplomAPI\db\import\screenshot.csv' DELIMITER ';';
copy "Ticket" FROM 'D:\1study\DiplomAPI\db\import\ticket.csv' DELIMITER ';';