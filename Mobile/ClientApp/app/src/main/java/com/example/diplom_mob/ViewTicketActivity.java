package com.example.diplom_mob;

import androidx.appcompat.app.ActionBar;
import androidx.appcompat.app.AppCompatActivity;

import android.graphics.Bitmap;
import android.graphics.Color;
import android.os.Bundle;
import android.text.Html;
import android.view.View;
import android.widget.ImageView;
import android.widget.TextView;

import com.google.zxing.BarcodeFormat;
import com.google.zxing.EncodeHintType;
import com.google.zxing.Writer;
import com.google.zxing.common.BitMatrix;
import com.google.zxing.oned.Code128Writer;
import com.google.zxing.qrcode.decoder.ErrorCorrectionLevel;

import java.text.DecimalFormat;
import java.util.Hashtable;

public class ViewTicketActivity extends AppCompatActivity {

    TextView tvName;
    TextView tvSeanceDate;
    TextView tvSeanceTime;
    TextView tvHall;
    TextView tvRow;
    TextView tvPlace;

    TextView tvDesc;

    ImageView img;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_view_ticket);

        ActionBar actionBar = getSupportActionBar();
        actionBar.setDisplayShowHomeEnabled(true);
        actionBar.setIcon(R.drawable.logo);
        actionBar.setTitle(Html.fromHtml("<font color=\"black\">" + getString(R.string.app_name) + "</font>"));

        tvName = findViewById(R.id.tvViewTicketFilmName);
        tvSeanceDate = findViewById(R.id.tvViewTicketSeanceDate);
        tvSeanceTime = findViewById(R.id.tvViewTicketSeanceTime);
        tvHall = findViewById(R.id.tvViewTicketHallName);
        tvRow = findViewById(R.id.tvViewTicketPlaceRow);
        tvPlace = findViewById(R.id.tvViewTicketPlaceNum);

        tvDesc = findViewById(R.id.tvViewTicketDesc);

        img = findViewById(R.id.imgBarCode);

        tvName.setText(g.t.filmName);
        tvSeanceDate.setText(g.t.seanceDate.toLocaleString().split(" 00:00:00")[0]);
        tvSeanceTime.setText(g.t.seanceTime.toString());
        tvHall.setText(g.t.hall);
        tvRow.setText("" + g.t.placeRow);
        tvPlace.setText("" + g.t.placeNum);

        String desc = "Продажа: " + g.t.dateTime.toLocaleString() + "\n";
        desc += "Заказ №: " + g.t.id + "\n";
        desc += "Цена: " + String.format("%.2f", g.t.cost) + " руб.";
        tvDesc.setText(desc);

        String code = g.t.img;
        Writer codeWriter = new Code128Writer();
        try
        {
            int width = 1000;
            int height = 1100;
            Hashtable<EncodeHintType, ErrorCorrectionLevel> hintMap = new Hashtable<EncodeHintType, ErrorCorrectionLevel>();
            hintMap.put(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.L);
            BitMatrix bitMatrix = codeWriter.encode(code, BarcodeFormat.CODE_128, width, height, hintMap);
            Bitmap bmp = Bitmap.createBitmap(width, height, Bitmap.Config.ARGB_8888);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    bmp.setPixel(i,j,bitMatrix.get(i,j) ? Color.BLACK : Color.WHITE);
                }
            }
            img.setImageBitmap(bmp);
        }
        catch (Exception e)
        {
            e.printStackTrace();
        }
    }

    public void onDeleteTicket(View v)
    {
        g.db.delTicket(g.t.id);
        finish();
    }
}