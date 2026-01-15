package com.example.basicform;

import android.os.Bundle;
import android.provider.MediaStore;
import android.view.View;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.RadioButton;
import android.widget.TextView;
import android.widget.Toast;

import androidx.activity.EdgeToEdge;
import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.graphics.Insets;
import androidx.core.view.ViewCompat;
import androidx.core.view.WindowInsetsCompat;

public class MainActivity extends AppCompatActivity {

    Button btnGui;

    CheckBox chbTheThao, chbDuLich, chbDocSach;

    RadioButton rdbNam, rdbNu;

    TextView textViewHoTen, textViewNgaySinh, textViewGioiTinh, textViewSDT, textViewSoThich;

    EditText editTextHoTen, editTextNgaySinh, editTextSDT;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        EdgeToEdge.enable(this);
        setContentView(R.layout.activity_main);

        InitWidget();

        btnGui.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                StringBuilder thongTin = new StringBuilder();

                thongTin.append("Họ tên: ")
                        .append(editTextHoTen.getText().toString())
                        .append("\n");

                thongTin.append("Ngày sinh: ")
                        .append(editTextNgaySinh.getText().toString())
                        .append("\n");

                thongTin.append("Giới tính: ");
                if (rdbNam.isChecked()) {
                    thongTin.append(rdbNam.getText().toString());
                } else {
                    thongTin.append(rdbNu.getText().toString());
                }
                thongTin.append("\n");

                thongTin.append("Số điện thoại: ")
                        .append(editTextSDT.getText().toString())
                        .append("\n");

                thongTin.append("Sở thích:");
                if (chbDuLich.isChecked()) {
                    thongTin.append(" ").append(chbDuLich.getText().toString());
                }
                if (chbTheThao.isChecked()) {
                    thongTin.append(" ").append(chbTheThao.getText().toString());
                }
                if (chbDocSach.isChecked()) {
                    thongTin.append(" ").append(chbDocSach.getText().toString());
                }

                AlertDialog dialog = new AlertDialog.Builder(MainActivity.this)
                        .setTitle("Thông tin người dùng")
                        .setMessage(thongTin.toString())
                        .setCancelable(false)
                        .setPositiveButton("OK", (dialogInterface, which) -> dialogInterface.dismiss())
                        .create();

                dialog.show();

            }
        });

        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.main), (v, insets) -> {
            Insets systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars());
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom);
            return insets;
        });
    }

    private void InitWidget() {
        chbTheThao = (CheckBox) findViewById(R.id.chbTheThao);
        chbDuLich = (CheckBox) findViewById(R.id.chbDuLich);
        chbDocSach = (CheckBox) findViewById(R.id.chbDocSach);

        rdbNam = (RadioButton) findViewById(R.id.rdbNam);
        rdbNu = (RadioButton) findViewById(R.id.rdbNu);

        btnGui = (Button) findViewById(R.id.btnGui);

        editTextHoTen = (EditText) findViewById(R.id.editTextHoTen);
        editTextNgaySinh = (EditText) findViewById(R.id.editTextNgaySinh);
        editTextSDT = (EditText) findViewById(R.id.editTextSDT);
    }
}