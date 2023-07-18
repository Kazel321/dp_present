package com.example.diplom_mob.models;

import android.graphics.Bitmap;

import java.time.LocalTime;
import java.util.ArrayList;

public class Film
{
    public Integer id;
    public String name;
    public String country;
    public String minAge;
    public LocalTime duration;
    public String desc;
    public Integer year;
    public Bitmap cover;
    public ArrayList<Genre> genres = new ArrayList<>();

    public String trailer;
}
