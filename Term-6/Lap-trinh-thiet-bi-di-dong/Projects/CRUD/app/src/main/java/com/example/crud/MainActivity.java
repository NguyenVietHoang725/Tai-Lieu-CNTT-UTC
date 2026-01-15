package com.example.crud;

import android.content.DialogInterface;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.Toast;

import androidx.activity.EdgeToEdge;
import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.graphics.Insets;
import androidx.core.view.ViewCompat;
import androidx.core.view.WindowInsetsCompat;

import java.util.ArrayList;

public class MainActivity extends AppCompatActivity {

    EditText editTextDisplay;
    Button btnCreate, btnEdit, btnDelete, btnSearch;
    ListView listView;

    int pos = -1;

    ArrayList<String> arrayListDs;

    ArrayAdapter<String> adapter;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        EdgeToEdge.enable(this);
        setContentView(R.layout.activity_main);
        
        InitWidget();

        arrayListDs = new ArrayList<String>();

        arrayListDs.add("Android Programming");
        arrayListDs.add("C Programming");
        arrayListDs.add("Computer Network");
        arrayListDs.add("Data Structure");

        adapter = new ArrayAdapter<>(
                MainActivity.this,
                android.R.layout.simple_list_item_1,
                arrayListDs
        );

        listView.setAdapter(adapter);

        listView.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                editTextDisplay.setText(arrayListDs.get(position));
                pos = position;
            }
        });

        btnCreate.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                String s = editTextDisplay.getText().toString();
                arrayListDs.add(s);
                adapter.notifyDataSetChanged();
            }
        });

        btnEdit.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (pos == -1) {
                    Toast.makeText(MainActivity.this, "Chưa chọn mục nào", Toast.LENGTH_SHORT).show();
                    return;
                }
                String s = editTextDisplay.getText().toString();
                arrayListDs.set(pos, s);
                adapter.notifyDataSetChanged();
                pos = -1;
            }
        });

        btnDelete.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (pos == -1) {
                    Toast.makeText(MainActivity.this, "Chưa chọn mục nào", Toast.LENGTH_SHORT).show();
                    return;
                }
                acceptDelete(pos);
                editTextDisplay.setText("");
            }
        });

        btnSearch.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                String s = editTextDisplay.getText().toString();
                for (int i = 0; i < arrayListDs.size(); i++) {
                    if (arrayListDs.get(i).equals(s)) {
                        Toast.makeText(MainActivity.this,
                                "Tim thay: " + s, Toast.LENGTH_SHORT).show();
                    }
                }
            }
        });

        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.main), (v, insets) -> {
            Insets systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars());
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom);
            return insets;
        });
    }

    private void acceptDelete(int position) {
        AlertDialog.Builder alertDialog = new AlertDialog.Builder(MainActivity.this);
        alertDialog.setTitle("Thong bao");
        alertDialog.setIcon(R.mipmap.ic_launcher);
        alertDialog.setMessage("Ban co chac chan xoa khong ?");
        alertDialog.setPositiveButton("Co", new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                arrayListDs.remove(position);
                adapter.notifyDataSetChanged();
                pos = -1;
            }
        });
        alertDialog.setNegativeButton("Khong", new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {

            }
        });
        alertDialog.show();
    }

    private void InitWidget() {
        editTextDisplay = (EditText) findViewById(R.id.editTextDisplay);
        btnCreate = (Button) findViewById(R.id.btnCreate);
        btnEdit = (Button) findViewById(R.id.btnEdit);
        btnDelete = (Button) findViewById(R.id.btnDelete);
        btnSearch = (Button) findViewById(R.id.btnSearch);
        listView = (ListView) findViewById(R.id.listView);
    }
}