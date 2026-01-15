package com.example.listview;

import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.Spinner;
import android.widget.Toast;

import androidx.activity.EdgeToEdge;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.graphics.Insets;
import androidx.core.view.ViewCompat;
import androidx.core.view.WindowInsetsCompat;

import java.util.ArrayList;

public class MainActivity extends AppCompatActivity {
   ListView listViewDanhSach;
   Spinner spinnerDanhSach;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        EdgeToEdge.enable(this);
        setContentView(R.layout.activity_main);

        EditText editTextThemMoi;
        Button btnThem;

        listViewDanhSach = (ListView) findViewById(R.id.listViewDanhSach);


        // Tạo nguồn dữ liệu (array list)
        ArrayList<String> arrayListDs = new ArrayList<String>();
        arrayListDs.add("Android Programming");
        arrayListDs.add("C Programming");
        arrayListDs.add("Computer Network");
        arrayListDs.add("Data Structure");

        // Tạo cầu nối Adapter
        ArrayAdapter<String> adapter = new ArrayAdapter<String>(this, android.R.layout.simple_list_item_1, arrayListDs);
        listViewDanhSach.setAdapter(adapter);

        // Thêm sự kiện
        listViewDanhSach.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                Toast.makeText(MainActivity.this,
                        arrayListDs.get(position), Toast.LENGTH_SHORT).show();
            }
        });

        spinnerDanhSach = (Spinner) findViewById(R.id.spinnerDanhSach);

        ArrayAdapter<String> adapter1 = new ArrayAdapter<String>(this, android.R.layout.simple_spinner_item, arrayListDs);
        spinnerDanhSach.setAdapter(adapter1);

        spinnerDanhSach.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                Toast.makeText(MainActivity.this,
                        arrayListDs.get(position), Toast.LENGTH_SHORT).show();
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {

            }
        });

        editTextThemMoi = (EditText) findViewById(R.id.editTextThemMoi);
        btnThem = (Button) findViewById(R.id.btnThem);

        btnThem.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                String s = editTextThemMoi.getText().toString();
                arrayListDs.add(s);
                adapter.notifyDataSetChanged();
                adapter1.notifyDataSetChanged();
            }
        });


        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.main), (v, insets) -> {
            Insets systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars());
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom);
            return insets;
        });
    }
}