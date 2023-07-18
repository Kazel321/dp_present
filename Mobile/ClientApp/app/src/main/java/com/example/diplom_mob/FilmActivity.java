package com.example.diplom_mob;

import androidx.appcompat.app.ActionBar;
import androidx.appcompat.app.AppCompatActivity;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.app.AlertDialog;
import android.content.Intent;
import android.content.res.Configuration;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.Bundle;
import android.text.Html;
import android.view.LayoutInflater;
import android.view.View;
import android.webkit.WebChromeClient;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.Button;
import android.widget.FrameLayout;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;

import com.example.diplom_mob.classes.ApiHelper;
import com.example.diplom_mob.models.Film;

import org.json.JSONArray;
import org.json.JSONObject;
import org.w3c.dom.Text;

import java.util.ArrayList;

public class FilmActivity extends AppCompatActivity {

    Intent i;
    Activity ctx;
    ApiHelper api;
    Film f;

    ImageView img;
    TextView tvName;
    TextView tvCountry;
    TextView tvGenres;
    TextView tvAge;
    TextView tvDuration;
    TextView tvYear;
    TextView tvDesc;
    Button btnScreens;

    TextView tvScreenFilmName;
    Button btnPrev;
    Button btnNext;
    ImageView imgScreens;

    Button btnTrailer;

    @SuppressLint("MissingInflatedId")
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_film);

        ActionBar actionBar = getSupportActionBar();
        actionBar.setDisplayShowHomeEnabled(true);
        actionBar.setIcon(R.drawable.logo);
        actionBar.setTitle(Html.fromHtml("<font color=\"black\">" + getString(R.string.app_name) + "</font>"));

        f = g.f;
        ctx = this;

        img = findViewById(R.id.imageView);
        tvName = findViewById(R.id.tvName);
        tvCountry = findViewById(R.id.tvCountry);
        tvAge = findViewById(R.id.tvMinAge);
        tvDuration = findViewById(R.id.tvDuration);
        tvYear = findViewById(R.id.tvYear);
        tvDesc = findViewById(R.id.tvDesc);

        btnScreens = findViewById(R.id.btnScreens);
        btnTrailer = findViewById(R.id.btnTrailer);

        img.setImageBitmap(f.cover);
        tvName.setText(f.name);
        tvCountry.setText(f.country);
        tvAge.setText(f.minAge);
        tvDuration.setText(f.duration.toString());
        tvYear.setText("" + f.year);
        tvDesc.setText(f.desc);

        String genres = "";
        for (int j = 0; j < f.genres.size(); j++) {
            genres += f.genres.get(j).name;
            if (j != (f.genres.size()) - 1) genres += ", ";
        }
        tvGenres = findViewById(R.id.tvGenres);
        tvGenres.setText(genres);

        api = new ApiHelper(ctx){
            @Override
            public void on_ready(String res)
            {
                Integer count = Integer.parseInt(res.substring(1, res.length()-1));
                if (count == 0)
                    btnScreens.setVisibility(View.GONE);
            }
        };
        api.send("/screenshotsCount", "POST", "{\"filmId\": " + f.id + "}");

        LayoutInflater dialogLayout = LayoutInflater.from(ctx);
        View dialogView = dialogLayout.inflate(R.layout.dialog_screens, null);
        alertDialog = new AlertDialog.Builder(ctx).create();
        alertDialog.setView(dialogView);

        tvScreenFilmName = dialogView.findViewById(R.id.tvScreenFilmName);
        tvScreenFilmName.setText("Скриншоты из: " + f.name);

        imgScreens = dialogView.findViewById(R.id.imgScreen);

        btnPrev = dialogView.findViewById(R.id.btnPrev);
        btnPrev.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if (selScreen == 0) return;
                selScreen--;
                imgScreens.setImageBitmap(screens.get(selScreen));
            }
        });

        btnNext = dialogView.findViewById(R.id.btnNext);
        btnNext.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if (selScreen == screens.size()-1) return;
                selScreen++;
                imgScreens.setImageBitmap(screens.get(selScreen));
            }
        });

        if (f.trailer == null || f.trailer.isEmpty())
        {
            btnTrailer.setVisibility(View.GONE);
        }
        else
        {

            LayoutInflater trailerLayout = LayoutInflater.from(ctx);
            View trailerView = trailerLayout.inflate(R.layout.dialog_trailer, null);
            trailerDialog = new AlertDialog.Builder(ctx).create();
            trailerDialog.setView(trailerView);

            TextView tvTrailerFilmName = trailerView.findViewById(R.id.tvTrailerFilmName);
            tvTrailerFilmName.setText("Трейлер фильма: " + f.name);

            Button btnCloseTrailer = trailerView.findViewById(R.id.btnCloseTrailer);
            btnCloseTrailer.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View view) {
                    trailerDialog.cancel();
                }
            });

            webView = trailerView.findViewById(R.id.webView);
            webView.setWebViewClient(new WebViewClient());
            webView.setWebChromeClient(new MyChrome());
            webView.getSettings().setJavaScriptEnabled(true);
            webView.loadUrl(f.trailer);
        }
    }

    WebView webView;

    ArrayList<Bitmap> screens = new ArrayList<>();
    int selScreen;
    AlertDialog alertDialog;

    AlertDialog trailerDialog;

    public void showScreens(View v)
    {
        screens.clear();
        api = new ApiHelper(ctx)
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
                        Bitmap bmp = getBitmapFromJSON(obj.getJSONObject("ScreenshotImage"));
                        screens.add(bmp);
                    }
                    selScreen = 0;
                    imgScreens.setImageBitmap(screens.get(selScreen));
                    alertDialog.show();
                }
                catch (Exception e)
                {
                    e.printStackTrace();
                }
            }
        };
        api.send("/screenshots", "POST", "{\"filmId\": " + f.id + "}");
    }

    public void showTrailer(View v)
    {
        trailerDialog.show();
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

    private class MyChrome extends WebChromeClient {
        private View mCustomView;
        private WebChromeClient.CustomViewCallback mCustomViewCallback;
        protected FrameLayout mFullscreenContainer;
        private int mOriginalOrientation;
        private int mOriginalSystemUiVisibility;

        MyChrome() {}

        public Bitmap getDefaultVideoPoster()
        {
            if (mCustomView == null) {
                return null;
            }
            return BitmapFactory.decodeResource(getApplicationContext().getResources(), 2130837573);
        }

        public void onHideCustomView()
        {
            ((FrameLayout)getWindow().getDecorView()).removeView(this.mCustomView);
            this.mCustomView = null;
            getWindow().getDecorView().setSystemUiVisibility(this.mOriginalSystemUiVisibility);
            setRequestedOrientation(this.mOriginalOrientation);
            this.mCustomViewCallback.onCustomViewHidden();
            this.mCustomViewCallback = null;
        }

        public void onShowCustomView(View paramView, WebChromeClient.CustomViewCallback paramCustomViewCallback)
        {
            trailerDialog.cancel();
            if (this.mCustomView != null)
            {
                onHideCustomView();
                return;
            }
            this.mCustomView = paramView;
            this.mOriginalSystemUiVisibility = getWindow().getDecorView().getSystemUiVisibility();
            this.mOriginalOrientation = getRequestedOrientation();
            this.mCustomViewCallback = paramCustomViewCallback;
            ((FrameLayout)getWindow().getDecorView()).addView(this.mCustomView, new FrameLayout.LayoutParams(-1, -1));
            getWindow().getDecorView().setSystemUiVisibility(3846 | View.SYSTEM_UI_FLAG_LAYOUT_STABLE);
        }
    }

    @Override
    public void onConfigurationChanged(Configuration newConfig){
        super.onConfigurationChanged(newConfig);
    }

    @Override
    protected void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        webView.saveState(outState);
    }

    @Override
    protected void onRestoreInstanceState(Bundle savedInstanceState) {
        super.onRestoreInstanceState(savedInstanceState);
        webView.restoreState(savedInstanceState);
    }
}