package com.example.b11_2026;

import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

import androidx.activity.EdgeToEdge;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.graphics.Insets;
import androidx.core.view.ViewCompat;
import androidx.core.view.WindowInsetsCompat;

public class MainActivity extends AppCompatActivity {
    TextView txtViewA, txtViewB, txtViewPt, txtViewKq;
    EditText editTextA, editTextB, editTextPt;
    Button btnOk;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        EdgeToEdge.enable(this);
        setContentView(R.layout.activity_main);

        InitWidget();

        btnOk.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Double a = Double.parseDouble(editTextA.getText().toString());
                Double b = Double.parseDouble(editTextB.getText().toString());

                String pt = editTextPt.getText().toString();

                Double kq = 0.0;
                if (pt.equals("+")) {
                    kq = a + b;
                } else if (pt.equals("-")) {
                    kq = a - b;
                } else if (pt.equals("*")) {
                    kq = a * b;
                } else if (pt.equals("/")) {
                    if (b == 0) {
                        txtViewKq.setText("Không tính được do b bằng 0.");
                        return;
                    }
                    kq = a / b;
                }
                txtViewKq.setText("Kết quả: " + kq);
            }
        });

        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.main), (v, insets) -> {
            Insets systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars());
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom);
            return insets;
        });
    }

    private void InitWidget() {
        editTextA = (EditText) findViewById(R.id.editTextA);
        editTextB = (EditText) findViewById(R.id.editTextB);
        editTextPt = (EditText) findViewById(R.id.editTextPt);
        txtViewKq = (TextView) findViewById(R.id.txtViewKq);
        btnOk = (Button) findViewById(R.id.btnOk);
    }
}