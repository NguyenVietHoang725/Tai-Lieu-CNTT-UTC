package com.example.bmicalculator;

import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.RelativeLayout;
import android.widget.TextView;

import androidx.activity.EdgeToEdge;
import androidx.appcompat.app.AppCompatActivity;
import androidx.constraintlayout.widget.ConstraintLayout;
import androidx.core.graphics.Insets;
import androidx.core.view.ViewCompat;
import androidx.core.view.WindowInsetsCompat;

import java.text.DecimalFormat;

public class MainActivity extends AppCompatActivity {

    TextView txtViewCanNang, txtViewChieuCao, txtViewTheTrang, txtViewKetQuaTheTrang, txtViewKetQuaBMI;
    EditText editTextCanNang, editTextChieuCao;
    Button btnTinhToan;

    ImageView imgViewHinhAnh;

    ConstraintLayout manHinh;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        EdgeToEdge.enable(this);
        setContentView(R.layout.activity_main);

        InitWidget();

        manHinh.setBackgroundResource(R.drawable.background);

        btnTinhToan.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                double canNang = Double.parseDouble(editTextCanNang.getText().toString());
                double chieuCao = Double.parseDouble(editTextChieuCao.getText().toString());

                double bmi = canNang / (chieuCao * chieuCao);
                DecimalFormat df;
                df = new DecimalFormat("#.#");

                txtViewKetQuaBMI.setText("" + df.format(bmi));

                String s = "";

                if (bmi < 18.5) {
                    s = "Gầy";
                    imgViewHinhAnh.setImageResource(R.drawable.gay);
                } else if (bmi >= 18.5 && bmi <= 24.9) {
                    s = "Bình thường";
                    imgViewHinhAnh.setImageResource(R.drawable.binhthuong);
                } else if (bmi >= 25 && bmi <= 29.9) {
                    s = "Tiền béo phì";
                    imgViewHinhAnh.setImageResource(R.drawable.tienbeophi);
                } else if (bmi >= 30 && bmi <= 34.9) {
                    s = "Béo phì độ 1";
                    imgViewHinhAnh.setImageResource(R.drawable.beophi1);
                } else if (bmi >= 35 && bmi <= 39.9) {
                    s = "Béo phì độ 2";
                    imgViewHinhAnh.setImageResource(R.drawable.beophi2);
                } else if (bmi >= 40) {
                    s = "Béo phì độ 3";
                    imgViewHinhAnh.setImageResource(R.drawable.beophi3);
                }

                txtViewKetQuaTheTrang.setText(s);
            }
        });

        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.manHinh), (v, insets) -> {
            Insets systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars());
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom);
            return insets;
        });
    }

    private void InitWidget() {
        editTextCanNang = (EditText) findViewById(R.id.editTextCanNang);
        editTextChieuCao = (EditText) findViewById(R.id.editTextChieuCao);

        txtViewKetQuaTheTrang = (TextView) findViewById(R.id.textViewKetQuaTheTrang);
        txtViewKetQuaBMI = (TextView) findViewById(R.id.textViewKetQuaBMI);

        btnTinhToan = (Button) findViewById(R.id.buttonTinhToan);

        imgViewHinhAnh = (ImageView) findViewById(R.id.imageViewHinhAnh);

        manHinh = (ConstraintLayout) findViewById(R.id.manHinh);
    }
}