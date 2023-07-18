package com.example.diplom_mob.classes;

import android.app.Activity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

import com.example.diplom_mob.R;
import com.example.diplom_mob.models.Ticket;

import java.util.ArrayList;

public class TicketAdapter extends BaseAdapter
{
    Activity ctx;
    ArrayList<Ticket> tickets;

    public TicketAdapter(Activity ctx, ArrayList<Ticket> tickets)
    {
        this.ctx = ctx;
        this.tickets = tickets;
    }

    @Override
    public int getCount() {
        return tickets.size();
    }

    @Override
    public Object getItem(int i) {
        return tickets.get(i);
    }

    @Override
    public long getItemId(int i) {
        return i;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent)
    {
        Ticket t = tickets.get(position);
        convertView = LayoutInflater.from(ctx.getApplicationContext()).inflate(R.layout.item_ticket, parent, false);

        TextView tvFilmName = convertView.findViewById(R.id.tvTicketFilmName);
        TextView tvDate = convertView.findViewById(R.id.tvTicketSeanceDate);
        TextView tvTime = convertView.findViewById(R.id.tvTicketSeanceTime);
        TextView tvHall = convertView.findViewById(R.id.tvTicketHall);
        TextView tvRow = convertView.findViewById(R.id.tvTicketRow);
        TextView tvNum = convertView.findViewById(R.id.tvTicketPlace);

        tvFilmName.setText(t.filmName);
        tvDate.setText(t.seanceDate.toLocaleString().substring(0, t.seanceDate.toLocaleString().length()-9));
        tvTime.setText(t.seanceTime.toString());
        tvHall.setText(t.hall);
        tvRow.setText("" + t.placeRow);
        tvNum.setText("" + t.placeNum);

        return convertView;
    }
}
