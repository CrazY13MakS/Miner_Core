using Miner_Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Miner_Model
{
    /// <summary>
    /// Miner game
    /// </summary>
    public class Miner<TResult, TCell> where TResult : IResult, new()
                                       where TCell : ICell
    {
        TResult _resultl;
        TCell _cell;
        private bool _isFirstStep;
        private bool _isGameOver;

        public List<List<ICell>> Field { get; set; }
        public List<ICell> Cells { get; set; }

        private IDifficult _difficult;
        public IDifficult Difficult
        {
            get { return _difficult; }
            private set
            {
                ValidateDifficult(value);
                _difficult = value;
            }
        }

        public event EventHandler GameOver;

        public IResult LastResult { get; private set; }
        public Miner(IDifficult difficult)
        {
            // ValidateDifficult(difficult);
            Difficult = difficult;
            CreateField();
        }

        /// <summary>
        /// Validate difficult
        /// </summary>
        /// <param name="difficult">Difficult for chack</param>
        private void ValidateDifficult(IDifficult difficult)
        {
            if (difficult == null)
            {
                throw new ArgumentNullException($"{nameof(difficult)} can't be null");
            }
            if (difficult.Height < 5 || difficult.Width < 5)
            {
                throw new ArgumentException($"Width and Height must be greater or equal 5. Current values: width = {difficult.Width}; height = {difficult.Height}");
            }
            if (difficult.BombsCount >= difficult.Width * difficult.Height)
            {
                throw new ArgumentException($"Bombs count on field must be lower than width*height. Current values:width = {difficult.Width}; height = {difficult.Height}; bombs count = {difficult.BombsCount}");
            }
        }

        /// <summary>
        /// Ititialize cells:  set bombs and bombs around
        /// </summary>
        /// <param name="firstPoint"> Point where bomb can't be set</param>
        protected virtual void InitGame(Point firstPoint)
        {
            // CreateField();
            SetBombs(firstPoint);
            SetBombsAround();
        }

        /// <summary>
        /// Set number of bombs around each cell
        /// </summary>
        protected void SetBombsAround()
        {
            if (Cells == null)
            {
                throw new NullReferenceException($"{nameof(Cells)} must be init before set bombs around");
            }
            foreach (var item in Cells)
            {
                SetBombsAroundCell(item);
            }
        }

        /// <summary>
        /// Generate location for bombs and set them
        /// </summary>
        ///   <param name="firstPoint"> Point where bomb can't be set</param>
        protected void SetBombs(Point firstPoint)
        {
            if (Cells == null)
            {
                throw new ArgumentNullException($"Before set bombs init property: {nameof(Cells)}");
            }
            foreach (var item in GenerateBombsLoacation(firstPoint))
            {
                Cells[item.Y * Difficult.Height + item.X].Bomb = true;
            }
        }

        /// <summary>
        /// Generate list of points where set bombs
        /// </summary>
        /// <param name="firstPoint">point where cant't be bomb</param>
        /// <returns></returns>
        protected List<Point> GenerateBombsLoacation(Point firstPoint)
        {

            int bombs = 0;
            List<Point> bombsList = new List<Point>() { firstPoint };
            int X, Y;
            Random rand = new Random();
            while (bombs < Difficult.BombsCount)
            {
                X = rand.Next(Difficult.Width);
                Y = rand.Next(Difficult.Height);
                var point = new Point(X, Y);
                if (!bombsList.Contains(point))
                {
                    bombsList.Add(point);
                    bombs++;
                }
            }
            return bombsList;
        }

        /// <summary>
        /// Create list of cells
        /// </summary>
        protected void CreateField()
        {
            // Field = new List<List<ICell>>(Difficult.Height);
            // for (int i = 0; i < Difficult.Height; i++)
            // {
            //     Field.Add(new List<ICell>(Difficult.Width));
            //     for (int j = 0; j < Difficult.Width; j++)
            //     {                   
            //         Field[i].Add(new Cell(new Point(i,j)));
            //     }
            // }      
            Cells = new List<ICell>(Difficult.Height * Difficult.Width);
            for (int i = 0; i < Difficult.Height; i++)
            {
                for (int j = 0; j < Difficult.Width; j++)
                {
                    Cells.Add(new Cell(new Point(j, i)));
                }
            }

        }

        /// <summary>
        /// Set number of bombs around current cell
        /// </summary>
        /// <param name="cell">Cell around which we count the number</param>
        protected void SetBombsAroundCell(ICell cell)
        {
            if (cell == null)
            {
                throw new NullReferenceException($"{nameof(cell)} can't be null");
            }
            var aroundPoints = GetAroundPoints(cell.Location);
            int count = (int)Cells.Where(x => x.Bomb && aroundPoints.Contains(x.Location)).Count();
            cell.BombsAround = count;
        }
        /// <summary>
        /// Get list of points around current location
        /// </summary>
        /// <param name="point">Point around which we find neighbors</param>
        /// <returns>List of point which are around the cell</returns>
        private List<Point> GetAroundPoints(Point point)
        {
            List<Point> list = new List<Point>();
            list.Add(new Point(point.X - 1, point.Y - 1));
            list.Add(new Point(point.X, point.Y - 1));
            list.Add(new Point(point.X + 1, point.Y - 1));
            list.Add(new Point(point.X - 1, point.Y));
            list.Add(new Point(point.X + 1, point.Y));
            list.Add(new Point(point.X - 1, point.Y + 1));
            list.Add(new Point(point.X, point.Y + 1));
            list.Add(new Point(point.X + 1, point.Y + 1));
            list = list.Where(x => !IsPointOutsideTheField(x)).ToList();
            return list;
        }

        /// <summary>
        /// Checking whether the transmitted point leaves the field boundary
        /// </summary>
        /// <param name="point">Point for check</param>
        /// <returns></returns>
        private bool IsPointOutsideTheField(Point point)
        {
            return point.X < 0
                || point.Y < 0
                || point.X >= Difficult.Width
                || point.Y >= Difficult.Height;
        }

        /// <summary>
        /// Restart current game
        /// </summary>
        public virtual void Restart()
        {
            Cells.ForEach(ResetCellForRestart);
            _isGameOver = false;

        }
        /// <summary>
        /// Close and remove flag
        /// </summary>
        /// <param name="cell">Cell to reset</param>
        private void ResetCellForRestart(ICell cell)
        {
            cell.Flag = false;
            cell.IsOpen = false;
        }

        /// <summary>
        /// Create new game with current settings
        /// </summary>
        public virtual void NewGame()
        {
            Cells.ForEach(x => x.Reset());
            _isFirstStep = true;
            _isGameOver = false;
        }

        /// <summary>
        /// Create new game with new difficult
        /// </summary>
        /// <param name="difficult">New difficult</param>
        public virtual void NewGame(IDifficult difficult)
        {
            Difficult = difficult;
            CreateField();
            NewGame();
        }

        /// <summary>
        /// Open cell
        /// </summary>
        /// <param name="point">Cell location</param>
        public virtual void OpenCell(Point point)
        {
            if (IsPointOutsideTheField(point))
            {
                throw new ArgumentOutOfRangeException($"Point with coorditnates: {point} is outside the field");
            }
            if (_isGameOver)
            {
                GameOver?.Invoke(this, EventArgs.Empty);
                return;
            }
            if (_isFirstStep)
            {
                InitGame(point);
            }
            var cell = Cells.FirstOrDefault(x => x.Location == point);
            cell.IsOpen = true;
            if (cell.Bomb)
            {
                _isGameOver = true;
                //TODO: Result, change cell constructor LastResult
            }
        }
    }
}
