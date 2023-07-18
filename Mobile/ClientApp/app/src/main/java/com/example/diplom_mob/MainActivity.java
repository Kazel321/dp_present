package com.example.diplom_mob;

import androidx.annotation.NonNull;
import androidx.annotation.RequiresApi;
import androidx.appcompat.app.ActionBar;
import androidx.appcompat.app.AppCompatActivity;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.app.AlertDialog;
import android.app.DatePickerDialog;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Color;
import android.os.Build;
import android.os.Bundle;
import android.text.Html;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.Window;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import com.example.diplom_mob.classes.ApiHelper;
import com.example.diplom_mob.classes.DB;
import com.example.diplom_mob.classes.FilmSeanceAdapter;
import com.example.diplom_mob.models.Film;
import com.example.diplom_mob.models.FilmSeance;
import com.example.diplom_mob.models.Genre;
import com.example.diplom_mob.models.PlaceType;
import com.example.diplom_mob.models.Seance;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.text.SimpleDateFormat;
import java.time.LocalTime;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.TimeZone;

public class MainActivity extends AppCompatActivity {

    SimpleDateFormat parseFormat = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
    SimpleDateFormat newParseFormat = new SimpleDateFormat("yyyy-MM-dd");
    SimpleDateFormat stringFormat = new SimpleDateFormat("dd-MM-yyyy");

    Button btnDate;

    TextView tv;
    ListView lst;
    ArrayList<FilmSeance> filmSeances = new ArrayList<>();
    FilmSeanceAdapter adp;

    Date date = null;
    boolean isNowDate = false;

    Activity ctx;
    ApiHelper req;

    AlertDialog alertDialogApi;
    View dialogApiView;
    EditText txtApi;
    Button btnApi;

    Intent i;

    TextView txtEmpty;

    public void onTicket(View v)
    {
        i = new Intent(ctx, TicketActivity.class);
        startActivityForResult(i, 1);
    }

    @SuppressLint("MissingInflatedId")
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        ActionBar actionBar = getSupportActionBar();
        actionBar.setDisplayShowHomeEnabled(true);
        actionBar.setIcon(R.drawable.logo);
        actionBar.setTitle(Html.fromHtml("<font color=\"black\">" + getString(R.string.app_name) + "</font>"));

        ctx = this;

        g.db = new DB(this, "db.db", null, 1);

        parseFormat.setTimeZone(TimeZone.getTimeZone("GMT"));

        tv = findViewById(R.id.textView);
        txtEmpty = findViewById(R.id.tvEmptySeances);
        btnDate = findViewById(R.id.btnDate);
        btnDate.setEnabled(false);

        tv.setVisibility(View.GONE);

        lst = findViewById(R.id.listFilmSeances);
        adp = new FilmSeanceAdapter(ctx, filmSeances);
        lst.setAdapter(adp);
        tv.setText("123123");
        /*
        req = new ApiHelper(ctx)
        {
            @RequiresApi(api = Build.VERSION_CODES.O)
            @Override
            public void on_ready(String res)
            {
                tv.setText("GET: " + res);
                //tv.setVisibility(View.GONE);
                try {

                    JSONObject obj = new JSONObject(res);
                    date = parseFormat.parse(obj.getString("date").toString());

                    checkDateNow(date);

                    btnDate.setEnabled(true);
                    Calendar cal = Calendar.getInstance();
                    cal.setTime(date);
                    int year = cal.get(Calendar.YEAR);
                    int month = cal.get(Calendar.MONTH);
                    int day = cal.get(Calendar.DAY_OF_MONTH);
                    btnDate.setText(makeDateString(day, month + 1, year));
                    initDatePicker();

                    update();
                }
                catch (Exception e)
                {
                    e.printStackTrace();
                }
            }

            @Override
            public void on_fail()
            {
                txtEmpty.setText("Не удалось подключиться к серверу");
            }
        };
        req.send("/min_date", "GET", "{}");
        */

        updateDate();

        LayoutInflater dialogLayout = LayoutInflater.from(ctx);
        dialogApiView = dialogLayout.inflate(R.layout.dialog_api, null);
        alertDialogApi = new AlertDialog.Builder(this).create();
        alertDialogApi.setView(dialogApiView);
        txtApi = dialogApiView.findViewById(R.id.txtApi);
        btnApi = dialogApiView.findViewById(R.id.btnApi);
        btnApi.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view)
            {
                if (txtApi.getText().toString().isEmpty())
                {
                    Toast.makeText(dialogApiView.getContext(), "Поле для ввода пустое", Toast.LENGTH_SHORT).show();
                    return;
                }
                String endpoint = txtApi.getText().toString();
                endpoint = "http://" + endpoint + ":8080/api";
                g.db.saveEndPoint(endpoint);
                alertDialogApi.cancel();
                updateDate();
            }
        });
    }

    Bitmap getBitmapFromJSON(JSONObject obj)
    {
        try {
            Bitmap bitmap = null;
            byte[] tmp = new byte[obj.getJSONArray("data").length()];
            for (int i = 0; i < obj.getJSONArray("data").length(); i++)
            {
                tmp[i] = (byte) (((int) obj.getJSONArray("data").get(i)) & 0xFF);
            }
            bitmap = BitmapFactory.decodeByteArray(tmp, 0, tmp.length);
            return bitmap;
        }
        catch (Exception e)
        {
            e.printStackTrace();
            return null;
        }
    }

    void checkDateNow(Date date)
    {
        Date nowDate = new Date();
        if (date.getYear() == nowDate.getYear() && date.getMonth() == nowDate.getMonth() && date.getDate() == nowDate.getDate())
            isNowDate = true;
    }

    DatePickerDialog datePickerDialog;

    void initDatePicker()
    {
        DatePickerDialog.OnDateSetListener dateSetListener = new DatePickerDialog.OnDateSetListener() {
            @Override
            public void onDateSet(DatePicker datePicker, int year, int month, int day) {
                month++;
                date.setYear(year-1900); date.setMonth(month-1); date.setDate(day);
                checkDateNow(date);
                String stringDate = makeDateString(day, month, year);
                btnDate.setText(stringDate);
                update();
            }
        };

        Calendar cal = Calendar.getInstance();
        cal.setTime(date);
        int year = cal.get(Calendar.YEAR);
        int month = cal.get(Calendar.MONTH);
        int day = cal.get(Calendar.DAY_OF_MONTH);

        int style = AlertDialog.THEME_HOLO_LIGHT;

        datePickerDialog = new DatePickerDialog(this, style, dateSetListener, year, month, day);
        datePickerDialog.getDatePicker().setMinDate(new Date().getTime());
        //datePickerDialog.getDatePicker().setMinDate(date.getTime());
    }

    String makeDateString(int day, int month, int year)
    {
        return getMonthFormat(month) + " " + day + " " + year;
    }

    String getMonthFormat(int month)
    {
        switch (month)
        {
            case 1: return "ЯНВ";
            case 2: return "ФЕВ";
            case 3: return "МАР";
            case 4: return "АПР";
            case 5: return "МАЙ";
            case 6: return "ИЮН";
            case 7: return "ИЮЛ";
            case 8: return "АВГ";
            case 9: return "СЕН";
            case 10: return "ОКТ";
            case 11: return "НОЯ";
            case 12: return "ДЕК";
        }
        return null;
    }

    public void onSelectDate(View v)
    {
        datePickerDialog.show();
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.menu, menu);
        return true;
    }

    void updateDate()
    {
        if (date == null)
        {
            txtEmpty.setText("Подключение...");
            req = new ApiHelper(ctx)
            {
                @RequiresApi(api = Build.VERSION_CODES.O)
                @Override
                public void on_ready(String res)
                {
                    tv.setText("GET: " + res);
                    tv.setVisibility(View.GONE);
                    try {

                        JSONObject obj = new JSONObject(res);
                        date = parseFormat.parse(obj.getString("date").toString());

                        checkDateNow(date);

                        btnDate.setEnabled(true);
                        Calendar cal = Calendar.getInstance();
                        cal.setTime(date);
                        int year = cal.get(Calendar.YEAR);
                        int month = cal.get(Calendar.MONTH);
                        int day = cal.get(Calendar.DAY_OF_MONTH);
                        btnDate.setText(makeDateString(day, month + 1, year));
                        initDatePicker();

                        update();
                    }
                    catch (Exception e)
                    {
                        e.printStackTrace();
                    }
                }

                @Override
                public void on_fail()
                {
                    txtEmpty.setText("Не удалось подключиться к серверу");
                }
            };
            req.send("/min_date", "GET", "{}");

            ApiHelper pt = new ApiHelper(ctx)
            {
                @Override
                public void on_ready(String res)
                {
                    try
                    {
                        g.pt.clear();
                        JSONArray arr = new JSONArray(res);
                        for (int i = 0; i < arr.length(); i++)
                        {
                            PlaceType pt = new PlaceType();
                            JSONObject obj = arr.getJSONObject(i);

                            pt.id = obj.getInt("PlaceTypeId");
                            pt.name = obj.getString("PlaceTypeName");
                            pt.cost = Float.parseFloat(obj.get("PlaceTypeCost").toString());

                            g.pt.add(pt);
                        }
                    }
                    catch (Exception e)
                    {
                        e.printStackTrace();
                    }
                }

                @Override
                public void on_fail()
                {
                    txtEmpty.setText("Не удалось подключиться к серверу");
                }
            };
            pt.send("/place_types", "POST", "{}");
        }
        else
            update();
    }

    @Override
    public boolean onOptionsItemSelected(@NonNull MenuItem item) {
        int id = item.getItemId();

        switch (id)
        {
            case R.id.itm_api:
            {
                if (g.db.getEndPoint() != null)
                {
                    String ip = g.db.getEndPoint();
                    ip = ip.split(":8080")[0].split("//")[1];
                    txtApi.setText(ip);
                }
                alertDialogApi.show();
                break;
            }
            case R.id.itm_upd:
            {
                updateDate();
            }
        }

        return super.onOptionsItemSelected(item);
    }


    void update()
    {
        filmSeances.clear();
        adp.notifyDataSetChanged();
        txtEmpty.setVisibility(View.VISIBLE);
        txtEmpty.setText("Загрузка...");
        req = new ApiHelper(ctx)
        {
            @RequiresApi(api = Build.VERSION_CODES.O)
            @Override
            public void on_ready(String res)
            {
                try
                {
                    JSONArray arr = new JSONArray(res);
                    for (int i = 0; i < arr.length(); i++)
                    {
                        FilmSeance fs = new FilmSeance();
                        Film f = new Film();
                        JSONObject obj = arr.getJSONObject(i);

                        f.id = obj.getInt("FilmId");
                        f.name = obj.getString("FilmName");
                        f.country = obj.getString("CountryName");
                        f.minAge = obj.getInt("MinAgeValue") + "+";
                        f.duration = LocalTime.parse(obj.getString("FilmDuration"), DateTimeFormatter.ofPattern("HH:mm:ss"));
                        f.desc = obj.getString("FilmDescription");
                        f.year = obj.getInt("FilmYear");

                        JSONArray genres = obj.getJSONArray("genres");
                        for (int j = 0; j < genres.length(); j++)
                        {
                            Genre g = new Genre();
                            String[] data = genres.getString(j).split(";");
                            g.id = Integer.parseInt(data[0]);
                            g.name = data[1];

                            f.genres.add(g);
                        }

                        f.cover = getBitmapFromJSON(obj.getJSONObject("FilmCover"));
                        if (!obj.getString("FilmTrailerLink").isEmpty())
                            f.trailer = obj.getString("FilmTrailerLink");


                        fs.film = f;

                        JSONArray seances = obj.getJSONArray("seances");
                        int count = Integer.parseInt(obj.getString("count"));
                        for (int j = 0; j < seances.length(); j += count)
                        {
                            Seance s = new Seance();
                            String[] data = seances.getString(j).split(";");
                            s.id = Integer.parseInt(data[0]);
                            s.date = newParseFormat.parse(data[1]);
                            s.cost = Float.parseFloat(data[2]);
                            s.time = LocalTime.parse(data[3], DateTimeFormatter.ofPattern("HH:mm:ss"));

                            fs.seances.add(s);

                            if (isNowDate)
                            {
                                int a = s.time.toSecondOfDay();
                                int b = LocalTime.now().toSecondOfDay();
                                if (a < b)
                                {
                                    fs.seances.remove(s);
                                }
                            }
                        }

                        if (fs.seances.size() > 0)
                            filmSeances.add(fs);
                        adp.notifyDataSetChanged();
                    }
                    adp.notifyDataSetChanged();
                    if (filmSeances.size() == 0)
                    {
                        txtEmpty.setVisibility(View.VISIBLE);
                        txtEmpty.setText("Нет сеансов на эту дату");
                        lst.setVisibility(View.GONE);
                    }
                    else
                    {
                        txtEmpty.setVisibility(View.GONE);
                        lst.setVisibility(View.VISIBLE);
                    }
                }
                catch (Exception e)
                {
                    e.printStackTrace();
                }

            }
        };
        req.send("/film_seances", "POST", "{\"date\": \"" + stringFormat.format(date) + "\"}");
    }

    void update1()
    {
        filmSeances.clear();
        adp.notifyDataSetChanged();
        req = new ApiHelper(ctx)
        {
            @RequiresApi(api = Build.VERSION_CODES.O)
            @Override
            public void on_ready(String res)
            {
                try
                {
                    JSONArray arr = new JSONArray(res);
                    ArrayList<Film> films = new ArrayList<>();
                    for (int i = 0; i < arr.length(); i++)
                    {
                        Film f = new Film();
                        JSONObject obj = arr.getJSONObject(i);

                        f.id = obj.getInt("FilmId");
                        f.name = obj.getString("FilmName");
                        f.country = obj.getString("CountryName");
                        f.minAge = obj.getInt("MinAgeValue") + "+";
                        f.duration = LocalTime.parse(obj.getString("FilmDuration"), DateTimeFormatter.ofPattern("HH:mm:ss"));
                        f.desc = obj.getString("FilmDescription");
                        f.year = obj.getInt("FilmYear");
                        f.cover = getBitmapFromJSON(obj.getJSONObject("FilmCover"));
                        if (!obj.getString("FilmTrailerLink").isEmpty())
                            f.trailer = obj.getString("FilmTrailerLink");
                        films.add(f);
                    }

                    for (int i = 0; i < films.size(); i++)
                    {
                        int finalI = i;
                        ApiHelper genrReq = new ApiHelper(ctx)
                        {
                            @Override
                            public void on_ready(String res)
                            {
                                try
                                {
                                    JSONArray arr = new JSONArray(res);
                                    for (int j = 0; j < arr.length(); j++)
                                    {
                                        JSONObject obj = arr.getJSONObject(j);

                                        Genre g = new Genre();
                                        g.id = obj.getInt("GenreId");
                                        g.name = obj.getString("GenreName");

                                        films.get(finalI).genres.add(g);
                                    }
                                    FilmSeance fs = new FilmSeance();
                                    fs.film = films.get(finalI);
                                    ApiHelper seanceReq = new ApiHelper(ctx)
                                    {
                                        @Override
                                        public void on_ready(String res)
                                        {
                                            try
                                            {
                                                JSONArray arr = new JSONArray(res);
                                                for (int i = 0; i < arr.length(); i++)
                                                {
                                                    JSONObject obj = arr.getJSONObject(i);

                                                    Seance s = new Seance();
                                                    s.id = obj.getInt("SeanceId");
                                                    s.filmId = obj.getInt("FilmId");
                                                    s.hallId = obj.getInt("HallId");
                                                    s.date = parseFormat.parse(obj.getString("SeanceDate").toString());
                                                    s.time = LocalTime.parse(obj.getString("SeanceTime"), DateTimeFormatter.ofPattern("HH:mm:ss"));
                                                    s.cost = Float.parseFloat(obj.get("SeanceCost").toString());

                                                    fs.seances.add(s);

                                                    if (isNowDate)
                                                    {
                                                        if (s.time.getSecond() < LocalTime.now().getSecond())
                                                        {
                                                            fs.seances.remove(s);
                                                        }
                                                    }
                                                }
                                                if (fs.seances.size() > 0)
                                                    filmSeances.add(fs);
                                                adp.notifyDataSetChanged();
                                            }
                                            catch (Exception e)
                                            {
                                                e.printStackTrace();
                                            }
                                        }
                                    };
                                    seanceReq.send("/seances", "POST", "{\"filmId\": " + fs.film.id + "," +
                                            " \"date\": \"" + stringFormat.format(date) + "\"}");
                                }
                                catch (Exception ex)
                                {
                                    ex.printStackTrace();
                                }
                            }
                        };
                        genrReq.send("/genres", "POST", "{\"filmId\": " + films.get(i).id + "}");
                    }
                }
                catch (Exception e)
                {
                    e.printStackTrace();
                }
            }
        };
        req.send("/films", "POST", "{\"date\": \"" + stringFormat.format(date) + "\"}");
    }
}