package com.example.diplom_mob.classes;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.graphics.Canvas;

import com.example.diplom_mob.g;

import java.io.BufferedInputStream;
import java.io.BufferedOutputStream;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;
import java.nio.charset.Charset;
import java.nio.charset.StandardCharsets;

public class ApiHelper {
    public String base = "http://192.168.0.111:8080/api";
    //public String base = "https://biba.loca.lt/api";
    Activity ctx;

    @SuppressLint("SuspiciousIndentation")
    public ApiHelper(Activity ctx)
    {
        this.ctx = ctx;
        if (g.db.getEndPoint() != null)
            base = g.db.getEndPoint();
    }

    public void on_ready(String res)
    {

    }

    public void on_fail()
    {

    }

    String http_get(String req, String method, String body) throws IOException
    {
        URL url = new URL(req);
        HttpURLConnection con = (HttpURLConnection) url.openConnection();

        con.setRequestMethod(method);

        if (method.equals("POST"))
        {
            con.setRequestProperty("Content-Type", "application/json");
            con.setRequestProperty("User-Agent","Mozilla/5.0 ( compatible ) ");
            con.setRequestProperty("accept","application/json");
            con.setDoOutput(true);

            BufferedOutputStream out = new BufferedOutputStream(con.getOutputStream());
            out.write(body.getBytes());
            out.flush();
        }

        BufferedInputStream inp = new BufferedInputStream(con.getInputStream());


        //byte[] buf = new byte[16384];
        /*
        byte[] buf = new byte[512];
        String res = "";

        while (true)
        {
            int num = inp.read(buf);
            if(num < 0) break;

            //res += new String(buf, 0, num);
            res += new String(buf, 0, num, Charset.forName("UTF-8"));
        }

         */

        String res = "";
        BufferedReader reader = new BufferedReader(new InputStreamReader(inp, StandardCharsets.UTF_8));
        String line = "";
        while ((line = reader.readLine()) != null) {
            res += line;
        }

        con.disconnect();

        return res;
    }

    public class NetOp implements Runnable
    {
        public String req;
        public String body;
        public String method;

        public void run()
        {
            try
            {
                final String res = http_get(base + req, method, body);

                ctx.runOnUiThread(new Runnable() {
                    @Override
                    public void run()
                    {
                        on_ready(res);
                    }
                });
            }
            catch (IOException ex)
            {
                ctx.runOnUiThread(new Runnable() {
                    @Override
                    public void run()
                    {
                        on_fail();
                    }
                });
                ex.printStackTrace();
            }
        }
    }

    public void send(String req, String method, String body)
    {
        NetOp nop = new NetOp();
        nop.body = body;
        nop.req = req;
        nop.method = method;

        Thread th = new Thread(nop);
        th.start();

        if (req == "/min_date" || req == "/place_types") {
            Runnable r = new Runnable() {
                @Override
                public void run() {
                    try {
                        Thread.sleep(5000);
                        if (th.isAlive()) {
                            th.interrupt();
                            ctx.runOnUiThread(new Runnable() {
                                @Override
                                public void run() {
                                    on_fail();
                                }
                            });
                        }
                    } catch (InterruptedException e) {
                        throw new RuntimeException(e);
                    }

                }
            };
            Thread chk = new Thread(r);
            chk.start();
        }
    }
}

