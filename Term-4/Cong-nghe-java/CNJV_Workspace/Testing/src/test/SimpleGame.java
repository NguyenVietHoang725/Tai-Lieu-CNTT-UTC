package test;

import javax.swing.*;
import java.awt.*;
import java.awt.event.*;

public class SimpleGame extends JFrame {
    private MyPanel panel;
    private JButton moveButton;
    private int rectX = 50;

    public SimpleGame() {
        setTitle("Demo JFrame Game");
        setSize(400, 300);
        setDefaultCloseOperation(EXIT_ON_CLOSE);
        setLayout(new BorderLayout());

        // Khởi tạo panel custom để vẽ
        panel = new MyPanel();
        add(panel, BorderLayout.CENTER);

        // Tạo nút để di chuyển hình chữ nhật
        moveButton = new JButton("Move Rectangle");
        moveButton.addActionListener(e -> {
            rectX += 10;
            panel.repaint();  // Vẽ lại panel sau khi thay đổi
        });

        add(moveButton, BorderLayout.SOUTH);

        setVisible(true);
    }

    // Panel tùy chỉnh để vẽ
    class MyPanel extends JPanel {
        @Override
        protected void paintComponent(Graphics g) {
            super.paintComponent(g);
            g.setColor(Color.BLUE);
            g.fillRect(rectX, 100, 100, 50);  // Hình chữ nhật màu xanh
        }
    }

    public static void main(String[] args) {
        SwingUtilities.invokeLater(SimpleGame::new);
    }
}
