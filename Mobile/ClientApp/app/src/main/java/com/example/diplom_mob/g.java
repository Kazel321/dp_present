package com.example.diplom_mob;

import com.example.diplom_mob.classes.DB;
import com.example.diplom_mob.models.Film;
import com.example.diplom_mob.models.PlaceType;
import com.example.diplom_mob.models.Seance;
import com.example.diplom_mob.models.Ticket;

import java.util.ArrayList;
import java.util.List;

public final class g
{
    public static DB db;
    public static Seance s;
    public static Film f;
    public static Ticket t;
    public static ArrayList<PlaceType> pt = new ArrayList<>();

    public static PlaceType findPlaceType(int id)
    {
        PlaceType t = null;
        for (int i = 0; i < pt.size(); i++)
        {
            if (pt.get(i).id == id) {
                t = pt.get(i);
                break;
            }
        }
        return t;
    }
}
