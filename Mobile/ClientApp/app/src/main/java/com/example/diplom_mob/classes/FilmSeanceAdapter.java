package com.example.diplom_mob.classes;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.app.AlertDialog;
import android.content.Intent;
import android.os.Build;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.HorizontalScrollView;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.RequiresApi;

import com.example.diplom_mob.FilmActivity;
import com.example.diplom_mob.R;
import com.example.diplom_mob.g;
import com.example.diplom_mob.models.Film;
import com.example.diplom_mob.models.FilmSeance;
import com.example.diplom_mob.models.Genre;
import com.example.diplom_mob.models.Place;
import com.example.diplom_mob.models.Seance;
import com.example.diplom_mob.models.Ticket;

import org.json.JSONArray;
import org.json.JSONObject;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;

public class FilmSeanceAdapter extends BaseAdapter
{
    Activity ctx;
    ArrayList<FilmSeance> filmSeances;
    SimpleDateFormat parseFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");

    SimpleDateFormat parseDateFormat = new SimpleDateFormat("dd.MM.yyyy");
    SimpleDateFormat parseTimeFormat = new SimpleDateFormat("HH:mm");

    public FilmSeanceAdapter(Activity ctx, ArrayList<FilmSeance> filmSeances)
    {
        this.ctx = ctx;
        this.filmSeances = filmSeances;
    }

    @Override
    public int getCount() {
        return filmSeances.size();
    }

    @Override
    public Object getItem(int i) {
        return filmSeances.get(i);
    }

    @Override
    public long getItemId(int i) {
        return i;
    }

    @Override
    public View getView(int i, View convertView, ViewGroup parent)
    {
        FilmSeance filmSeance = filmSeances.get(i);
        Film film = filmSeance.film;
        ArrayList<Seance> seances = filmSeance.seances;
        ArrayList<Genre> genres = film.genres;
        convertView = LayoutInflater.from(ctx.getApplicationContext()).inflate(R.layout.item_film_seance, parent, false);

        ImageView cover = convertView.findViewById(R.id.imgCover);
        TextView filmName = convertView.findViewById(R.id.tvFilmName);
        TextView filmMinAge = convertView.findViewById(R.id.tvMinAge);
        TextView filmDesc = convertView.findViewById(R.id.tvDesc);
        TextView filmGenres = convertView.findViewById(R.id.tvGenre);
        LinearLayout btnLayout = convertView.findViewById(R.id.btnLayout);

        filmName.setText(film.name + " (" + film.year + ")");
        filmMinAge.setText(film.minAge);
        filmDesc.setText(film.desc);
        cover.setImageBitmap(film.cover);

        View.OnClickListener filmClick = new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                g.f = film;
                startFilm();
            }
        };

        cover.setOnClickListener(filmClick);
        filmName.setOnClickListener(filmClick);

        String lineGenre = "Жанры: ";
        for (int j = 0; j < genres.size(); j++) {
            lineGenre += genres.get(j).name;
            if (j > 0) break;
            if (j != (genres.size() - 1)) lineGenre += ", ";
        }
        filmGenres.setText(lineGenre);

        for (int j = seances.size() - 1; j >= 0; j--)
        {
            Button btn = new Button(ctx);
            LinearLayout.LayoutParams params = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WRAP_CONTENT, LinearLayout.LayoutParams.WRAP_CONTENT);
            params.setMargins(5, 0, 5, 0);
            btn.setTag(seances.get(j).id);
            btn.setLayoutParams(params);
            btn.setText(seances.get(j).time.toString());
            btn.setTextSize(10);
            int finalJ = j;
            btn.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View view) {
                    g.s = seances.get(finalJ);
                    g.f = film;
                    startDialog();
                }
            });
            btnLayout.addView(btn);
        }
        return convertView;
    }

    void startFilm()
    {
        Intent i = new Intent(ctx, FilmActivity.class);
        ctx.startActivityForResult(i, 1);
    }

    @SuppressLint("MissingInflatedId")
    void startDialog()
    {
        LayoutInflater dialogLayout = LayoutInflater.from(ctx);
        View dialogView = dialogLayout.inflate(R.layout.dialog_places, null);
        AlertDialog alertDialog = new AlertDialog.Builder(ctx).create();
        alertDialog.setView(dialogView);

        TextView tvFilm = dialogView.findViewById(R.id.tvFilm);
        tvFilm.setText(g.f.name + " " + g.s.date.toLocaleString().split(" " + (g.s.date.getYear() + 1900))[0] + " " + g.s.time.toString());

        HorizontalScrollView scrPlaces = dialogView.findViewById(R.id.scrPlaces);
        scrPlaces.post(new Runnable() {
            @Override
            public void run() {
                int x = (scrPlaces.getChildAt(0).getWidth() - dialogView.getWidth()) / 2;
                scrPlaces.scrollTo(x, 0);
            }
        });

        ArrayList<PlaceItem> places = new ArrayList<>();
        ArrayList<Integer> busyPlaces = new ArrayList<Integer>();
        final PlaceAdapter[] adp = new PlaceAdapter[1];
        ListView lst = dialogView.findViewById(R.id.lstPlaces);

        TextView tvPlaceCost = dialogView.findViewById(R.id.tvPlaceCost);

        ApiHelper req = new ApiHelper(ctx)
        {
            @Override
            public void on_ready(String res)
            {
                try
                {
                    JSONObject all = new JSONObject(res);
                    int maxNum = all.getInt("maxNumber");
                    int maxRow = all.getInt("rows");
                    for (int i = 0; i < maxRow; i++)
                    {
                        PlaceItem itm = new PlaceItem();
                        places.add(itm);
                    }
                    JSONArray arr = all.getJSONArray("all");
                    for (int i = 0; i < arr.length(); i++)
                    {
                        JSONObject obj = arr.getJSONObject(i);
                        Place p = new Place();
                        p.id = obj.getInt("PlaceId");
                        p.hallId = obj.getInt("HallId");
                        p.number = obj.getInt("PlaceNumber");
                        p.row = obj.getInt("PlaceRow");
                        p.placeType = obj.getInt("PlaceTypeId");

                        places.get(p.row - 1).places.add(p);
                    }
                    arr = all.getJSONArray("busy");
                    for (int i = 0; i < arr.length(); i++)
                    {
                        JSONObject obj = arr.getJSONObject(i);
                        int id = obj.getInt("PlaceId");
                        busyPlaces.add(id);
                    }

                    adp[0] = new PlaceAdapter(ctx, places, busyPlaces, maxNum, maxRow, tvPlaceCost);
                    lst.setAdapter(adp[0]);
                    adp[0].notifyDataSetChanged();

                    alertDialog.show();
                }
                catch (Exception e)
                {
                    e.printStackTrace();
                }
            }
        };
        req.send("/places", "POST", "{\"seanceId\": \"" + g.s.id + "\"}");

        Button btnBuy = dialogView.findViewById(R.id.btnCreateTicket);
        btnBuy.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view)
            {

                String checkPlaces = adp[0].selPlaces.toString().replace("[", "{").replace("]", "}");
                ApiHelper checkApi = new ApiHelper(ctx)
                {
                    @Override
                    public void on_ready(String res)
                    {
                        int count = Integer.parseInt(res.substring(1, res.length()-1));
                        if (count > 0)
                        {
                            Toast.makeText(ctx, "Одно из мест уже заняли", Toast.LENGTH_SHORT).show();
                            alertDialog.cancel();
                            return;
                        }
                        else
                        {
                            ArrayList<Integer> places = adp[0].selPlaces;
                            for (int i = 0; i < places.size(); i++)
                            {
                                ApiHelper api = new ApiHelper(ctx)
                                {
                                    @RequiresApi(api = Build.VERSION_CODES.O)
                                    @Override
                                    public void on_ready(String res)
                                    {
                                        try
                                        {
                                            JSONObject obj = new JSONObject(res);

                                            JSONObject tick = obj.getJSONObject("t");
                                            Ticket t = new Ticket();
                                            t.id = tick.getInt("TicketId");
                                            t.seanceId = tick.getInt("SeanceId");
                                            t.dateTime = parseFormat.parse(tick.getString("TicketDateTime").replace("T", " "));
                                            t.placeId = tick.getInt("PlaceId");
                                            t.cost = Float.parseFloat(tick.get("TicketCost").toString());
                                            t.code = Integer.parseInt(tick.getString("TicketCode"));
                                            t.active = tick.getBoolean("TicketActive");

                                            t.filmName = g.f.name + " (" + g.f.minAge + ")";
                                            t.seanceDate = g.s.date;
                                            t.seanceTime = g.s.time;

                                            JSONObject h = obj.getJSONObject("h");
                                            t.hall = h.getString("HallName");

                                            JSONObject p = obj.getJSONObject("p");
                                            t.placeRow = p.getInt("PlaceRow");
                                            t.placeNum = p.getInt("PlaceNumber");
                                            t.dateTime = addHoursToJavaUtilDate(t.dateTime, 3);

                                            String barcode = parseDateFormat.format(addHoursToJavaUtilDate(t.dateTime, 3)).replace(".", "") +
                                                    parseTimeFormat.format(t.dateTime).replace(":", "") +
                                                    t.code;
                                            t.img = barcode;

                                            g.db.addTicket(t);
                                            Toast.makeText(ctx, "Покупка успешна", Toast.LENGTH_SHORT).show();
                                        }
                                        catch (Exception e)
                                        {
                                            e.printStackTrace();
                                        }
                                        finally
                                        {
                                            alertDialog.cancel();
                                        }
                                    }
                                };
                                api.send("/ticket", "POST", "{\"seanceId\": " + g.s.id + ", \"placeId\": " + places.get(i).intValue() + "}");
                            }
                        }
                    }
                };
                checkApi.send("/check_ticket", "POST", "{\"seanceId\": " + g.s.id + ", \"places\": \"" + checkPlaces + "\"}");
            }
        });
    }

    public Date addHoursToJavaUtilDate(Date date, int hours) {
        Calendar calendar = Calendar.getInstance();
        calendar.setTime(date);
        calendar.add(Calendar.HOUR_OF_DAY, hours);
        return calendar.getTime();
    }
}
