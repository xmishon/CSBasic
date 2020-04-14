using System;

// Разработчик: Змеевский Михаил
namespace HomeWork7
{
    class GameObject
    {
        private Cell[][] cells;
        private int fieldSize = 8;
        public delegate void FinishGame();
        public int MinesCount { get; private set; }

        public GameObject(int fieldSize)
        {
            this.fieldSize = fieldSize;
            MinesCount = 0;
        }

        private struct Cell
        {
            public bool isMine;
            public bool isFlag;
            public int x;
            public int y;
            public bool isOpened;
        }

        public bool CheckGameFinished()
        {
            int openedCellsCount = 0;
            for (int i = 0; i < fieldSize; i++)
            {
                for (int j = 0; j < fieldSize; j++)
                {
                    if (cells[i][j].isOpened)
                    {
                        openedCellsCount++;
                    }
                }
            }
            if (openedCellsCount + MinesCount == fieldSize * fieldSize)
                return true;
            return false;
        }

        public void InitializeCells()
        {
            cells = new Cell[fieldSize][];
            for (int i = 0; i < cells.Length; i++)
            {
                cells[i] = new Cell[fieldSize];
            }
            for (int i = 0; i < fieldSize; i++)
            {
                for (int j = 0; j < fieldSize; j++)
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
            // Считаем, сколько мин поставили
            for (int i = 0; i < fieldSize; i++)
            {
                for (int j = 0; j < fieldSize; j++)
                {
                    if (cells[i][j].isMine)
                    {
                        MinesCount++;
                    }
                }
            }
        }

        /// <summary>
        /// Возвращает результат открывания ячейки с координатой (x, y):
        /// -1 - в ячейке мина - игра проиграна
        /// 0-9 количество мин, лежащих вокруг этой ячейки
        /// </summary>
        /// <param name="x">координата x ячейки</param>
        /// <param name="y">координата y ячейки</param>
        /// <returns>int</returns>
        public int OpenCell(int x, int y)
        {
            if (cells[x][y].isMine)
                return -1;
            if (cells[x][y].isOpened)
                return -2;
            cells[x][y].isOpened = true;

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

            return minesCounter;
        }

        public bool SetFlag(int x, int y)
        {
            cells[x][y].isFlag = !cells[x][y].isFlag;
            return cells[x][y].isFlag;
        }

        /// <summary>
        /// Проверяет одну конкретную ячейку с координатой (x, y),
        /// увеличивает counter на единицу, если в ячейке есть мина
        /// </summary>
        /// <param name="x">координата X ячейки</param>
        /// <param name="y">координата Y ячейки</param>
        /// <param name="counter">счётчик мин, увеличивается на 1 при наличии мины в проверяемой ячейке</param>
        private void checkCell(int x, int y, ref int counter)
        {
            if (x > -1 && x < fieldSize && y > -1 && y < fieldSize)
            {
                if (cells[x][y].isMine)
                    counter++;
            }
        }

    }
}
