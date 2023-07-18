package com.example.barcodescanner;

import android.app.Activity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

import java.util.ArrayList;

public class RecordAdapter extends BaseAdapter
{
    Activity ctx;
    ArrayList<Record> records;

    public RecordAdapter(Activity ctx, ArrayList<Record> records)
    {
        this.ctx = ctx;
        this.records = records;
    }

    @Override
    public int getCount() {
        return records.size();
    }

    @Override
    public Object getItem(int i) {
        return records.get(i);
    }

    @Override
    public long getItemId(int i) {
        return records.get(i).id;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent)
    {
        Record r = records.get(position);
        convertView = LayoutInflater.from(ctx.getApplicationContext()).inflate(R.layout.item_record, parent, false);

        TextView tvDate = convertView.findViewById(R.id.txtDate);
        TextView tvTime = convertView.findViewById(R.id.txtTime);
        TextView tvCode = convertView.findViewById(R.id.txtCode);

        tvDate.setText(r.date.toLocaleString().substring(0, r.date.toLocaleString().length()-9));
        tvTime.setText(r.time.toString());
        tvCode.setText(r.code);

        return convertView;
    }
}
