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
        private GameObject game = null;
        private Graphics graphics;
        private int cellHeight; // высота одной ячейки в пикселях
        private int cellWidth; // ширина одной ячейки в пикселях
        private int FIELD_SIZE = 8; // размер минного поля 8 на 8 ячеек
        private int flagsCounter;

        private bool isClickable; // активирует или деактивирует возможность кликать по ячейкам
        Timer timer;

        public Form1()
        {
            InitializeComponent();
            game = new GameObject(FIELD_SIZE); // создаём экземпляр класса игры
            game.InitializeCells(); // расставляем мины случайным образом
            graphics = pictureBoxField.CreateGraphics();
            cellHeight = pictureBoxField.Height / FIELD_SIZE;
            cellWidth = pictureBoxField.Width / FIELD_SIZE;
            DrawField();
            flagsCounter = 0;
            timer = new Timer();
        }

        // рисуем сетку на поле в соответствии с установленным размером минного поля FIELD_SIZE
        private void DrawField()
        {
            Pen pen = new Pen(Color.Black);
            pen.Width = 2;
            Brush brush = new SolidBrush(Color.Gray);
            graphics.FillRectangle(brush, new Rectangle(0, 0, pictureBoxField.Width - 1, pictureBoxField.Height - 1)); // заполняем сплошным цветом
            graphics.DrawRectangle(pen, new Rectangle(0, 0, pictureBoxField.Width - 1, pictureBoxField.Height - 1)); // рисуем рамку по периметру
            
            // рисуем вертикальные и горизонтальные линии
            for (int i = 0; i < FIELD_SIZE; i++)
                graphics.DrawLine(pen, new Point(0, i * cellHeight), new Point(pictureBoxField.Height - 1, i * cellHeight)); 
            for (int i = 0; i < FIELD_SIZE; i++)
                graphics.DrawLine(pen, new Point(i * cellWidth, 0), new Point(i * cellWidth, pictureBoxField.Width - 1));
        }

        // отрисовываем открытие ячейки
        private void DrawCell(Brush brush, int x, int y, int minesCounter)
        {
            DrawCell(brush, x, y);
            Brush brushF = new SolidBrush(Color.Black);
            if (minesCounter > 0)
                graphics.DrawString(minesCounter.ToString(), new Font("Calibri", 24), brushF, x * cellWidth + 9, y * cellHeight + 3);
        }

        private void DrawCell(Brush brush, int x, int y)
        {
            Rectangle rect = new Rectangle(x * cellWidth + 1, y * cellHeight + 1, cellHeight - 2, cellHeight - 2);
            graphics.FillRectangle(brush, rect);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            isClickable = true;
            DrawField();
            game = new GameObject(FIELD_SIZE);
            game.InitializeCells();
            timer.Stop();
            try
            {
                timer.Tick -= TickerHandler;
            } catch (Exception exc)
            {
                label1.Text = "Ошибка сборса таймера";
            }
            flagsCounter = game.MinesCount;
            SetMinesCounter(flagsCounter);
            
            labelTime.Text = "000";
            timer.Enabled = true;
            timer.Interval = 1000;
            timer.Start();
            timer.Tick += TickerHandler;

        }

        private void SetMinesCounter(int count)
        {
            labelFlagsCount.Text = count.ToString();
            if (labelFlagsCount.Text.Length < 2) labelFlagsCount.Text = "00" + labelFlagsCount.Text;
            else if (labelFlagsCount.Text.Length < 3) labelFlagsCount.Text = "0" + labelFlagsCount.Text;
        }

        private void TickerHandler(object sender, EventArgs e)
        {
            labelTime.Text = (int.Parse(labelTime.Text) + 1).ToString();
            if (labelTime.Text.Length < 2) labelTime.Text = "00" + labelTime.Text;
            if (labelTime.Text.Length < 3) labelTime.Text = "0" + labelTime.Text;
        }

        private void pictureBoxField_Click(object sender, EventArgs e)
        {
            if (isClickable)
            {
                Point point = pictureBoxField.PointToClient(Cursor.Position); // получили координаты клика
                int x, y;
                // переводим координаты клика в номера ячеек
                x = point.X / cellWidth;
                y = point.Y / cellHeight;
                // Проверяем, какой кнопкой мыши сделан щелчок
                MouseEventArgs me = (MouseEventArgs)e;
                switch (me.Button)
                {
                    case MouseButtons.Left:
                        label1.Text = "Ячейка: (" + x + ", " + y + ")";
                        OpenCell(x, y);
                        break;
                    case MouseButtons.Right:
                        SetFlag(x, y);
                        break;
                }
            }
        }

        private void OpenCell(int x, int y)
        {
            // открываем ячейку и узнаём что там
            if (x >= 0 && x < FIELD_SIZE && y >= 0 && y < FIELD_SIZE)
            {
                int openCellResult = game.OpenCell(x, y);
                if (openCellResult == 0)
                {
                    OpenCell(x, y - 1);
                    OpenCell(x, y + 1);
                    OpenCell(x - 1, y);
                    OpenCell(x + 1, y);
                }
                if (openCellResult >= 0) // успех
                {
                    DrawCell(new SolidBrush(Color.LightGray), x, y, openCellResult);
                }
                else if (openCellResult == -1) // проигрыш
                {
                    DrawCell(new SolidBrush(Color.Red), x, y, openCellResult);
                    isClickable = false;
                    timer.Stop();
                    MessageBox.Show("БАБАХ!!!");
                }
                if (game.CheckGameFinished())
                {
                    FinishGame();
                }
            }
        }

        private void SetFlag(int x, int y)
        {
            if (x >= 0 && x < FIELD_SIZE && y >= 0 && y < FIELD_SIZE)
            {
                if (game.SetFlag(x, y))
                {
                    DrawCell(new SolidBrush(Color.Green), x, y);
                    SetMinesCounter(--flagsCounter);
                }
                else
                {
                    DrawCell(new SolidBrush(Color.Gray), x, y);
                    SetMinesCounter(++flagsCounter);
                }
            }
        }

        public void FinishGame()
        {
            isClickable = false;
            timer.Stop();
            MessageBox.Show("Поздравляем, Вы выиграли!");
        }

        private void оРазработчикеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Реализация классический игры сапёр для Windows в качестве домашнего задания\n" +
                "Разработчик: Змеевский Михаил");
        }

        private void правилаИгрыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Для начала игры нажмите Старт.\n" +
                "- Номер слева от кнопки Старт - количество непомеченных мин, справа - таймер\n" +
                "- Необходимо последовательно открывать ячейки, кликая по ним левой кнопкой мыши\n" +
                "- Номер в открытой ячейке показывает количество мин вокруг неё\n" +
                "- Считаются мины мины в ближайшких ячейках сверху, снизу,\n" +
                "  по бокам и по диагоналям от открытой ячейки\n" +
                "- Если Вы считаете, что в ячейке мина, вы можете пометить её, кликнув правой кнопкой мыши по ней\n" +
                "- Если в открытой ячейке оказывается мина, игра заканчивается, при этом ячейка светится красным\n" +
                "- Игра считается выигранной, когда открыты все ячейки, кроме заминированных");
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
