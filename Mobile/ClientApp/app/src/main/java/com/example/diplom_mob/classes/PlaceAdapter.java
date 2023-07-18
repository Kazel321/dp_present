package com.example.diplom_mob.classes;

import android.app.Activity;
import android.content.res.ColorStateList;
import android.graphics.Color;
import android.os.Build;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.CompoundButton;
import android.widget.HorizontalScrollView;
import android.widget.LinearLayout;
import android.widget.ScrollView;
import android.widget.TextView;

import com.example.diplom_mob.R;
import com.example.diplom_mob.g;
import com.example.diplom_mob.models.Place;

import java.text.NumberFormat;
import java.util.ArrayList;
import java.util.Currency;
import java.util.Locale;

public class PlaceAdapter extends BaseAdapter
{
    Activity ctx;
    ArrayList<PlaceItem> places;
    ArrayList<Integer> busyPlaces;
    public ArrayList<Integer> selPlaces = new ArrayList<>();

    int maxNumber;
    int rowCount;

    TextView tvCost;

    public PlaceAdapter(Activity ctx, ArrayList<PlaceItem> places, ArrayList<Integer> busyPlaces, int maxNumber, int rowCount, TextView tv)
    {
        this.ctx = ctx;
        this.places = places;
        this.busyPlaces = busyPlaces;
        this.maxNumber = maxNumber;
        this.rowCount = rowCount;
        this.tvCost = tv;
    }

    @Override
    public int getCount()
    {
        return places.size();
    }

    @Override
    public Object getItem(int i) {
        return places.get(i);
    }

    @Override
    public long getItemId(int i) {
        return i;
    }

    @Override
    public View getView(int i, View convertView, ViewGroup parent)
    {
        convertView = LayoutInflater.from(ctx.getApplicationContext()).inflate(R.layout.item_place_row, parent, false);
        TextView tvRow = convertView.findViewById(R.id.tvRow);
        LinearLayout ltPlaces = convertView.findViewById(R.id.ltPlaces);


        PlaceItem row = places.get(i);
        ArrayList<Place> places = row.places;
        PlaceLayout[] number = new PlaceLayout[maxNumber];

        for (int j = 0; j < number.length; j++)
        {
            number[j] = new PlaceLayout(ctx.getApplicationContext());
            ltPlaces.addView(number[j]);
        }

        int start = (maxNumber - row.places.size()) / 2;
        for (int j = 0; j < places.size(); j++)
        {
            Place p = places.get(j);
            if (!isBusy(p.id)) {
                number[j + start].chk.setTag(p.id);
                number[j + start].chk.setEnabled(true);
                number[j + start].chk.setVisibility(View.VISIBLE);
                number[j + start].tv.setVisibility(View.VISIBLE);
                number[j + start].tv.setText("" + p.number);
                int finalJ = j;
                number[j + start].chk.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
                    @Override
                    public void onCheckedChanged(CompoundButton compoundButton, boolean b)
                    {
                        float cost = (g.findPlaceType(p.placeType).cost + g.s.cost) * 0.8f;
                        int oper;
                        if (number[finalJ + start].chk.isChecked())
                        {
                            selPlaces.add(p.id);
                            oper = 1;
                        }
                        else
                        {
                            selPlaces.remove(p.id);
                            oper = 2;
                        }
                        updateCost(oper, cost);
                    }
                });
                switch (p.placeType)
                {
                    case 1:
                        break;
                    case 2:
                        number[j + start].chk.setButtonTintList(ColorStateList.valueOf(Color.YELLOW));
                        break;
                    case 3:
                        number[j + start].chk.setVisibility(View.INVISIBLE);
                        number[j + start].tv.setVisibility(View.INVISIBLE);
                        break;
                    default:
                        number[j + start].chk.setButtonTintList(ColorStateList.valueOf(Color.BLUE));
                        break;
                }
            }
            else
            {
                number[j + start].chk.setChecked(true);
                number[j + start].chk.setVisibility(View.VISIBLE);
                number[j + start].tv.setVisibility(View.VISIBLE);
                number[j + start].tv.setText("x");
                number[j + start].chk.setButtonTintList(ColorStateList.valueOf(Color.RED));
            }
        }
        tvRow.setText("Ряд " + (i+1));

        return convertView;
    }

    boolean isBusy(int placeId)
    {
        for (int i = 0; i < busyPlaces.size(); i++)
        {
            if (placeId == busyPlaces.get(i)) return true;
        }
        return false;
    }

    float cost = 0;
    Locale ru = new Locale("ru", "RU");
    Currency rub = Currency.getInstance(ru);
    NumberFormat rubFormat = NumberFormat.getCurrencyInstance(ru);

    void updateCost(int oper, float cost)
    {
        switch (oper)
        {
            case 1:
                this.cost+=cost;
                break;
            case 2:
                this.cost-=cost;
                break;
        }
        tvCost.setText("Стоимость: " + rubFormat.format(this.cost));
    }
}
