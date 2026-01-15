package com.example.list_nguoi;

import android.os.Bundle;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.RadioButton;
import android.widget.Spinner;

import androidx.activity.EdgeToEdge;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.graphics.Insets;
import androidx.core.view.ViewCompat;
import androidx.core.view.WindowInsetsCompat;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;

public class MainActivity extends AppCompatActivity {

    EditText editTextHoTen, editTextSDT;

    Button btnAdd, btnSort;

    RadioButton rdbNam, rdbNu;

    ListView listViewDanhSach;

    Spinner spinnerQueQuan;

    ArrayList<String> arrayListDs, arrayListQq;

    ArrayAdapter<String> adapterDs, adapterQq;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        EdgeToEdge.enable(this);
        setContentView(R.layout.activity_main);

        InitWidget();

        arrayListDs = new ArrayList<String>();

        adapterDs = new ArrayAdapter<>(
                MainActivity.this,
                android.R.layout.simple_list_item_1,
                arrayListDs
        );

        listViewDanhSach.setAdapter(adapterDs);

        arrayListQq = new ArrayList<>(
                Arrays.asList(
                        "Hà Nội",
                        "Hồ Chí Minh",
                        "Đà Nẵng",
                        "Hải Phòng",
                        "Cần Thơ",
                        "Cao Bằng",
                        "Thái Bình"
                )
        );

        adapterQq = new ArrayAdapter<>(
                MainActivity.this,
                android.R.layout.simple_list_item_1,
                arrayListQq
        );

        spinnerQueQuan.setAdapter(adapterQq);

        btnAdd.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                StringBuilder thongTin = new StringBuilder();

                thongTin.append(editTextHoTen.getText().toString())
                        .append(" - ");

                if (rdbNam.isChecked()) {
                    thongTin.append(rdbNam.getText().toString());
                } else {
                    thongTin.append(rdbNu.getText().toString());
                }
                thongTin.append("\n");

                thongTin.append(editTextSDT.getText().toString())
                        .append(" - ");

                String queQuan = spinnerQueQuan.getSelectedItem().toString();
                thongTin.append(queQuan);

                arrayListDs.add(thongTin.toString());
                adapterDs.notifyDataSetChanged();
            }
        });

        btnSort.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Collections.sort(arrayListDs);
                adapterDs.notifyDataSetChanged();
            }
        });

        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.main), (v, insets) -> {
            Insets systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars());
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom);
            return insets;
        });
    }

    private void InitWidget() {
        editTextHoTen = (EditText) findViewById(R.id.editTextHoTen);
        editTextSDT = (EditText) findViewById(R.id.editTextSDT);
        btnAdd = (Button) findViewById(R.id.btnAdd);
        btnSort = (Button) findViewById(R.id.btnSort);
        rdbNam = (RadioButton) findViewById(R.id.rdbNam);
        rdbNu = (RadioButton) findViewById(R.id.rdbNu);
        listViewDanhSach = (ListView) findViewById(R.id.listView);
        spinnerQueQuan = (Spinner) findViewById(R.id.spinner);

    }
}