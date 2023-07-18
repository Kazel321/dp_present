package com.example.diplom_mob.classes;

import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;
import android.os.Build;
import android.util.Log;

import androidx.annotation.Nullable;
import androidx.annotation.RequiresApi;

import com.example.diplom_mob.models.Ticket;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.time.LocalTime;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;

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
        sql = "CREATE TABLE Tickets (id INT, seanceId INT, datetime TEXT, placeId INT, cost REAL, code INT, active INT, " +
                "filmName TEXT, seanceDate TEXT, seanceTime TEXT, hall TEXT, placeRow INT, placeNum INT, image TEXT);";
        db.execSQL(sql);
    }

    @RequiresApi(api = Build.VERSION_CODES.O)
    public void addTicket(Ticket t)
    {
        SQLiteDatabase db = getWritableDatabase();
        sql = "INSERT INTO Tickets VALUES (" + t.id +
                ", " + t.seanceId +
                ", '" + parseFormat.format(t.dateTime) +
                "', " + t.placeId +
                ", " + t.cost +
                ", " + t.code +
                ", " + (t.active ? "1" : "0") +
                ", '" + t.filmName + "'" +
                ", '" + parseFormat.format(t.seanceDate) + "'" +
                ", '" + DateTimeFormatter.ofPattern("HH:mm:ss").format(t.seanceTime) + "'" +
                ", '" + t.hall + "'" +
                ", " + t.placeRow +
                ", " + t.placeNum +
                ", '" + t.img + "'" +
                ");";
        db.execSQL(sql);
    }

    @RequiresApi(api = Build.VERSION_CODES.O)
    public Ticket getTicket(int id) throws ParseException {
        SQLiteDatabase db = getReadableDatabase();
        Ticket t = null;
        sql = "SELECT * FROM Tickets WHERE id = " + id + ";";
        Cursor cur = db.rawQuery(sql, null);
        if (cur.moveToFirst())
        {
            t = new Ticket();
            t.id = cur.getInt(0);
            t.seanceId = cur.getInt(1);
            t.dateTime = parseFormat.parse(cur.getString(2));
            t.placeId = cur.getInt(3);
            t.cost = cur.getFloat(4);
            t.code = cur.getInt(5);
            int active = cur.getInt(6);
            if (active == 1)
                t.active = true;
            else
                t.active = false;
            t.filmName = cur.getString(7);
            t.seanceDate = parseFormat.parse(cur.getString(8));
            t.seanceTime = LocalTime.parse(cur.getString(9), DateTimeFormatter.ofPattern("HH:mm:ss"));
            t.hall = cur.getString(10);
            t.placeRow = cur.getInt(11);
            t.placeNum = cur.getInt(12);
            t.img = cur.getString(13);
        }
        return t;
    }

    @RequiresApi(api = Build.VERSION_CODES.O)
    public void getAllTickets(ArrayList<Ticket> lst) throws ParseException {
        SQLiteDatabase db = getReadableDatabase();
        sql = "SELECT * FROM Tickets";
        Cursor cur = db.rawQuery(sql, null);

        while (cur.moveToNext())
        {
            Ticket t = new Ticket();
            t.id = cur.getInt(0);
            t.seanceId = cur.getInt(1);
            t.dateTime = parseFormat.parse(cur.getString(2));
            t.placeId = cur.getInt(3);
            t.cost = cur.getFloat(4);
            t.code = cur.getInt(5);
            int active = cur.getInt(6);
            if (active == 1)
                t.active = true;
            else
                t.active = false;

            t.filmName = cur.getString(7);
            t.seanceDate = parseFormat.parse(cur.getString(8));
            t.seanceTime = LocalTime.parse(cur.getString(9), DateTimeFormatter.ofPattern("HH:mm:ss"));
            t.hall = cur.getString(10);
            t.placeRow = cur.getInt(11);
            t.placeNum = cur.getInt(12);
            t.img = cur.getString(13);
            lst.add(t);
        }
    }

    public void delTicket(int id)
    {
        SQLiteDatabase db = getWritableDatabase();
        sql = "DELETE FROM Tickets WHERE id = " + id + ";";
        db.execSQL(sql);
    }

    public void delAllTickets()
    {
        SQLiteDatabase db = getWritableDatabase();
        sql = "DELETE FROM Tickets;";
        db.execSQL(sql);
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
