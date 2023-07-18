package com.example.diplom_mob.classes;

import android.content.Context;
import android.view.Gravity;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.CheckBox;
import android.widget.LinearLayout;
import android.widget.TextView;

public class PlaceLayout extends LinearLayout
{
    Context ctx;

    public TextView tv;
    public CheckBox chk;

    public PlaceLayout(Context context)
    {
        super(context);

        chk = new CheckBox(context);
        tv = new TextView(context);

        ctx = context;

        this.setOrientation(VERTICAL);
        this.setLayoutParams(new LayoutParams(LinearLayout.LayoutParams.MATCH_PARENT, LinearLayout.LayoutParams.MATCH_PARENT, 1.0f));


        chk.setLayoutParams(new LayoutParams(LinearLayout.LayoutParams.MATCH_PARENT, LinearLayout.LayoutParams.MATCH_PARENT, 0.7f));

        //LayoutParams params = new LayoutParams(LayoutParams.MATCH_PARENT, LayoutParams.MATCH_PARENT, 1.0f);
        LayoutParams params = new LayoutParams(LayoutParams.MATCH_PARENT, LayoutParams.MATCH_PARENT, 1.0f);
        params.setMargins(0,0, 0, 0);
        tv.setLayoutParams(params);

        chk.setVisibility(INVISIBLE);
        tv.setVisibility(INVISIBLE);

        chk.setEnabled(false);

        chk.setText("");
        chk.setGravity(Gravity.CENTER);
        tv.setTextAlignment(TEXT_ALIGNMENT_CENTER);
        tv.setGravity(Gravity.CENTER);

        this.addView(chk);
        this.addView(tv);
    }
}