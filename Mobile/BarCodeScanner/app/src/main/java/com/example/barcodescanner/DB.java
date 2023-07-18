package com.example.barcodescanner;

import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;
import android.os.Build;

import androidx.annotation.Nullable;
import androidx.annotation.RequiresApi;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.time.LocalTime;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.Date;

public class DB extends SQLiteOpenHelper
{
    String sql;
    SimpleDateFormat parseFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");

    public DB(@Nullable Context context, @Nullable String name, @Nullable SQLiteDatabase.CursorFactory factory, int version) {
        super(context, name, factory, version);
    }

    @Override
    public void onCreate(SQLiteDatabase db) {
        sql = "CREATE TABLE APIEndPoint (endpoint TEXT);";
        db.execSQL(sql);
        sql = "CREATE TABLE history (id INT, date TEXT, time TEXT, code TEXT);";
        db.execSQL(sql);
    }

    @RequiresApi(api = Build.VERSION_CODES.O)
    public void addRecord(String code)
    {
        SQLiteDatabase db = getWritableDatabase();
        int id = getMaxId() + 1;
        Date now = new Date();
        LocalTime time = LocalTime.now();
        sql = "INSERT INTO history VALUES (" + id + ", '" + parseFormat.format(now) + "', '" + DateTimeFormatter.ofPattern("HH:mm:ss").format(time) + "', '" + code + "');";
        db.execSQL(sql);
    }

    @RequiresApi(api = Build.VERSION_CODES.O)
    public void getAllRecords(ArrayList<Record> lst) throws ParseException {
        SQLiteDatabase db = getReadableDatabase();
        sql = "SELECT * FROM history";
        Cursor cur = db.rawQuery(sql, null);

        while (cur.moveToNext())
        {
            Record r = new Record();
            r.id = cur.getInt(0);
            r.date = parseFormat.parse(cur.getString(1));
            r.time = LocalTime.parse(cur.getString(2), DateTimeFormatter.ofPattern("HH:mm:ss"));
            r.code = cur.getString(3);
            lst.add(r);
        }
    }

    public void delAllRecords()
    {
        SQLiteDatabase db =getWritableDatabase();
        sql = "DELETE FROM history";
        db.execSQL(sql);
    }

    public int getMaxId()
    {
        int id = 0;
        SQLiteDatabase db = getReadableDatabase();
        sql = "SELECT MAX(id) FROM history";
        Cursor cur =db.rawQuery(sql, null);
        if (cur.moveToFirst())
        {
            id = cur.getInt(0);
        }
        return id;
    }

    public void saveEndPoint(String endpoint)
    {
        SQLiteDatabase db = getWritableDatabase();
        String sql = "DELETE FROM APIEndPoint;";
        db.execSQL(sql);
        sql = "INSERT INTO APIEndPoint VALUES ('" + endpoint + "');";
        db.execSQL(sql);
    }

    public String getEndPoint()
    {
        SQLiteDatabase db = getReadableDatabase();
        String sql = "SELECT * FROM APIEndPoint;";
        Cursor cur = db.rawQuery(sql, null);
        if (cur.moveToFirst())
        {
            String endpoint = cur.getString(0);
            return endpoint;
        }
        else return null;
    }

    @Override
    public void onUpgrade(SQLiteDatabase sqLiteDatabase, int i, int i1) {

    }
}
