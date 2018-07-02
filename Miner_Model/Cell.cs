using Miner_Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Miner_Model
{
    class Cell : ICell
    {
        Point _location;
        bool _isOpen;
        bool _bomb;
        CellMark _mark;
        int _bombsAround;

        /// <summary>
        /// Describe cell location
        /// </summary>
        public Point Location { get => _location;  set => _location = value; }
        /// <summary>
        /// Has current cell bomb or not
        /// </summary>
        public bool Bomb { get => _bomb;  set => _bomb = value; }
        /// <summary>
        /// Is current cell already opened
        /// </summary>
        public bool IsOpen { get => _isOpen;  set => _isOpen = value; }
        /// <summary>
        /// Describe mark on cell
        /// </summary>
        public CellMark Mark { get => _mark; set => _mark = value; }
        /// <summary>
        /// Number of bombs around
        /// </summary>
        public int BombsAround { get => _bombsAround;  set => SetBombsAround(value); }

        /// <summary>
        /// Create cell instance
        /// </summary>
        /// <param name="location">Cell location on the field</param>
        /// <param name="bomb">Is there a bomb in cell. Default - false</param>
        /// <param name="bombsAround">Bombs around current cell</param>
        public Cell(Point location, bool bomb=false, int bombsAround = 0)
        {
            if(bombsAround<0)
            {
                throw new ArgumentException($"Number of bombs arount the cell must be greater or equal 0. Current value: {bombsAround}");
            }
            Location = location;
            Bomb = bomb;
            BombsAround = bombsAround;
        }


        private void SetBombsAround(int value)
        {
            if (value < 0)
            {
                throw new Exception($"BombsAround must be >=0. Current value: {value}");
            }
            _bombsAround = value;
        }


        /// <summary>
        /// Set all fields to default value
        /// </summary>
        public void Reset()
        {
            Bomb = false;
            IsOpen = false;
            Mark =  CellMark.None;
            BombsAround = 0;
            
        }
    }
}
