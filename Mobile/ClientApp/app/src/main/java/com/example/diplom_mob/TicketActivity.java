package com.example.diplom_mob;

import androidx.annotation.GravityInt;
import androidx.annotation.Nullable;
import androidx.annotation.RequiresApi;
import androidx.appcompat.app.ActionBar;
import androidx.appcompat.app.AppCompatActivity;

import android.annotation.SuppressLint;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.Color;
import android.os.Build;
import android.os.Bundle;
import android.text.Html;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.TextView;

import com.example.diplom_mob.classes.TicketAdapter;
import com.example.diplom_mob.models.Ticket;
import com.google.zxing.BarcodeFormat;
import com.google.zxing.EncodeHintType;
import com.google.zxing.Writer;
import com.google.zxing.WriterException;
import com.google.zxing.common.BitMatrix;
import com.google.zxing.oned.Code128Writer;
import com.google.zxing.qrcode.decoder.ErrorCorrectionLevel;

import java.text.ParseException;
import java.util.ArrayList;
import java.util.Hashtable;

public class TicketActivity extends AppCompatActivity {

    ListView lstctl;
    ArrayList<Ticket> lst = new ArrayList<Ticket>();
    TicketAdapter adp;

    TextView tvTicketCount;

    Button btnClear;

    @SuppressLint("MissingInflatedId")
    @RequiresApi(api = Build.VERSION_CODES.O)
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_ticket);

        ActionBar actionBar = getSupportActionBar();
        actionBar.setDisplayShowHomeEnabled(true);
        actionBar.setIcon(R.drawable.logo);
        actionBar.setTitle(Html.fromHtml("<font color=\"black\">" + getString(R.string.app_name) + "</font>"));

        btnClear = findViewById(R.id.btnClearTickets);

        tvTicketCount = findViewById(R.id.tvEmptyTickets);

        lstctl = findViewById(R.id.lstTicket);
        adp = new TicketAdapter(this, lst);
        lstctl.setAdapter(adp);
        lstctl.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, int i, long l)
            {
                g.t = lst.get(i);
                onViewTicket();
            }
        });

        update();
    }

    public void onViewTicket()
    {
        Intent i = new Intent(this, ViewTicketActivity.class);
        startActivityForResult(i, 1);
    }

    @RequiresApi(api = Build.VERSION_CODES.O)
    public void onClear(View v)
    {
        g.db.delAllTickets();
        update();
    }

    @RequiresApi(api = Build.VERSION_CODES.O)
    void update()
    {
        lst.clear();
        try
        {
            g.db.getAllTickets(lst);
        }
        catch (ParseException e)
        {
            e.printStackTrace();
        }
        if (lst.size() == 0)
        {
            tvTicketCount.setVisibility(View.VISIBLE);
            lstctl.setVisibility(View.GONE);
            btnClear.setVisibility(View.GONE);
        }
        else
        {
            tvTicketCount.setVisibility(View.GONE);
            lstctl.setVisibility(View.VISIBLE);
            btnClear.setVisibility(View.VISIBLE);
        }
        adp.notifyDataSetChanged();
    }

    @RequiresApi(api = Build.VERSION_CODES.O)
    @Override
    protected void onActivityResult(int requestCode, int resultCode, @Nullable Intent data) {
        update();
        super.onActivityResult(requestCode, resultCode, data);
    }
}