using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeWork7
{
    public partial class Form1 : Form
    {
        private Cell[][] cells;
        private Graphics graphics;
        private int cellHeight;
        private int cellWidth;
        private int FIELD_SIZE = 8;
        private bool isClickable;
        Timer timer;

        public Form1()
        {
            InitializeComponent();
            InitializeCells();
            graphics = pictureBoxField.CreateGraphics();
            cellHeight = pictureBoxField.Height / FIELD_SIZE;
            cellWidth = pictureBoxField.Width / FIELD_SIZE;
            DrawField();
            timer = new Timer();
        }

        private void DrawField()
        {
            Pen pen = new Pen(Color.Black);
            pen.Width = 2;
            Brush brush = new SolidBrush(Color.Gray);
            graphics.FillRectangle(brush, new Rectangle(0, 0, pictureBoxField.Width - 1, pictureBoxField.Height - 1));
            graphics.DrawRectangle(pen, new Rectangle(0, 0, pictureBoxField.Width - 1, pictureBoxField.Height - 1));
            
            for (int i = 0; i < FIELD_SIZE; i++)
                graphics.DrawLine(pen, new Point(0, i * cellHeight), new Point(pictureBoxField.Height - 1, i * cellHeight));
            for (int i = 0; i < FIELD_SIZE; i++)
                graphics.DrawLine(pen, new Point(i * cellWidth, 0), new Point(i * cellWidth, pictureBoxField.Width - 1));
        }

        private void DrawCell(Brush brush, int x, int y, int minesCounter)
        {
            Rectangle rect = new Rectangle(x * cellWidth + 1, y * cellHeight + 1, cellHeight - 2, cellHeight - 2);
            graphics.FillRectangle(brush, rect);
            Brush brushF = new SolidBrush(Color.Black);
            graphics.DrawString(minesCounter.ToString(), new Font("Calibri", 24), brushF, x * cellWidth + 9, y * cellHeight + 3);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            isClickable = true;
            DrawField();
            InitializeCells();
            timer.Stop();
            try
            {
                timer.Tick -= TickerHandler;
            } catch (Exception exc)
            {
                label1.Text = "Ошибка сборса таймера";
            }
            labelTime.Text = "000";
            timer.Enabled = true;
            timer.Interval = 1000;
            timer.Start();
            timer.Tick += TickerHandler;
        }

        private void TickerHandler(object sender, EventArgs e)
        {
            labelTime.Text = (int.Parse(labelTime.Text) + 1).ToString();
            if (labelTime.Text.Length < 2) labelTime.Text = "00" + labelTime.Text;
            if (labelTime.Text.Length < 3) labelTime.Text = "0" + labelTime.Text;
        }

        private struct Cell
        {
            public bool isMine;
            public bool isFlag;
            public int x;
            public int y;
            public bool isOpened;
        }

        private void InitializeCells()
        {
            cells = new Cell[FIELD_SIZE][];
            for (int i = 0; i < cells.Length; i++)
            {
                cells[i] = new Cell[FIELD_SIZE];
            }
            for (int i = 0; i < FIELD_SIZE; i++)
            {
                for (int j = 0; j < FIELD_SIZE; j++)
                {
                    Cell cell = cells[i][j];
                    cell.x = j;
                    cell.y = i;
                    cell.isMine = false;
                    cell.isFlag = false;
                    
                }
            }
            Random r = new Random(); // Расставляем мины
            for (int i = 0; i < 7; i++)
            {
                cells[r.Next(8)][r.Next(8)].isMine = true;
            }
        }

        private void pictureBoxField_Click(object sender, EventArgs e)
        {
            if (isClickable)
            {
                Point point = pictureBoxField.PointToClient(Cursor.Position); // получили координаты клика
                int x, y;
                x = point.X / cellWidth;
                y = point.Y / cellHeight;
                label1.Text = "Ячейка: (" + x + ", " + y + ")";
                OpenCell(x, y);
            }

        }

        private void OpenCell(int x, int y)
        {
            Color color;
            cells[x][y].isOpened = true;
            if (cells[x][y].isMine)
                color = Color.Red;
            else
                color = Color.LightGray;

            int minesCounter = 0;
            // Считаем количество мин в ячейках вокруг
            checkCell(x - 1, y - 1, ref minesCounter);
            checkCell(x, y - 1, ref minesCounter);
            checkCell(x + 1, y - 1, ref minesCounter);
            checkCell(x + 1, y, ref minesCounter);
            checkCell(x + 1, y + 1, ref minesCounter);
            checkCell(x, y + 1, ref minesCounter);
            checkCell(x - 1, y + 1, ref minesCounter);
            checkCell(x - 1, y, ref minesCounter);

            DrawCell(new SolidBrush(color), x, y, minesCounter);
        }

        private void checkCell(int x, int y, ref int counter)
        {
            if (x > -1 && x < FIELD_SIZE && y > -1 && y < FIELD_SIZE)
            {
                if (cells[x][y].isMine)
                    counter++;
            }
        }
    }
}
