package com.example.barcodescanner;

import androidx.annotation.NonNull;
import androidx.annotation.RequiresApi;
import androidx.appcompat.app.ActionBar;
import androidx.appcompat.app.AppCompatActivity;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.app.AlertDialog;
import android.content.Intent;
import android.os.Build;
import android.os.Bundle;
import android.text.Html;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.Toast;

import com.google.zxing.integration.android.IntentIntegrator;
import com.google.zxing.integration.android.IntentResult;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.TimeZone;

public class MainActivity extends AppCompatActivity {

    Activity ctx;
    AlertDialog alertDialogApi;
    View dialogApiView;
    EditText txtApi;
    Button btnApi;

    SimpleDateFormat parseFormat = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
    SimpleDateFormat stringFormat = new SimpleDateFormat("dd-MM-yyyy");

    ArrayList<Record> lst = new ArrayList<>();
    RecordAdapter adp;
    ListView lstctl;

    @RequiresApi(api = Build.VERSION_CODES.O)
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

        lstctl = findViewById(R.id.listView);
        adp = new RecordAdapter(ctx, lst);
        lstctl.setAdapter(adp);
        update();

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
            }
        });

        connectDialog = new AlertDialog.Builder(ctx)
                .setTitle("Ошибка")
                .setMessage("Нет подключения к серверу")
                .setIcon(android.R.drawable.ic_dialog_alert);

        errDialog = new AlertDialog.Builder(ctx)
                .setTitle("Провал")
                .setMessage("Билет не рабочий")
                .setIcon(android.R.drawable.ic_delete);

        succDialog = new AlertDialog.Builder(ctx)
                .setTitle("Успех")
                .setMessage("Билет рабочий")
                .setIcon(android.R.drawable.presence_online);
    }

    AlertDialog.Builder succDialog;
    AlertDialog.Builder errDialog;
    AlertDialog.Builder connectDialog;

    public void initScat(View v)
    {
        IntentIntegrator integrator = new IntentIntegrator(this);
        integrator.setDesiredBarcodeFormats(IntentIntegrator.CODE_128);
        integrator.setPrompt("Поднесите штрих-код");
        //integrator.setCameraId(0);  // используем заднюю камеру
        //integrator.setBeepEnabled(true);
        integrator.setOrientationLocked(true);
        integrator.setBeepEnabled(true);
        integrator.setCaptureActivity(Portrait.class);
        //integrator.setBarcodeImageEnabled(true);
        integrator.initiateScan();
    }

    @RequiresApi(api = Build.VERSION_CODES.O)
    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        IntentResult result = IntentIntegrator.parseActivityResult(requestCode, resultCode, data);
        if (result != null)
        {
            String contents = result.getContents();
            if (contents != null)
            {
                // здесь можно обработать полученное значение штрих-кода
                Toast.makeText(this, "Сканировано: " + contents, Toast.LENGTH_LONG).show();
                String code = contents.substring(contents.length()-6);
                int cd = Integer.parseInt(code);
                ApiHelper api = new ApiHelper(ctx)
                {
                    @Override
                    public void on_ready(String res)
                    {
                        if (res.equals("true"))
                        {
                            succDialog.show();
                            g.db.addRecord(code);
                            update();
                        }
                        else if (res.equals("false"))
                        {
                            errDialog.show();
                        }
                    }

                    @Override
                    public void on_fail() {
                        connectDialog.show();
                    }
                };
                api.send("/ticket_control", "POST", "{ \"code\": " + cd + "}");
            }
        } else {
            super.onActivityResult(requestCode, resultCode, data);
        }
    }

    @RequiresApi(api = Build.VERSION_CODES.O)
    void update()
    {
        lst.clear();
        try
        {
            g.db.getAllRecords(lst);
        }
        catch (Exception e)
        {
            e.printStackTrace();
        }
        adp.notifyDataSetChanged();
    }

    @RequiresApi(api = Build.VERSION_CODES.O)
    public void onClear(View v)
    {
        g.db.delAllRecords();
        update();
    }

    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.menu, menu);
        return true;
    }

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
        }

        return super.onOptionsItemSelected(item);
    }
}