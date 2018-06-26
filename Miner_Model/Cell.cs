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
        bool _flag;
        int _bombsAround;

        public Point Location { get => _location;  set => _location = value; }
        public bool Bomb { get => _bomb;  set => _bomb = value; }
        public bool IsOpen { get => _isOpen;  set => _isOpen = value; }
        public bool Flag { get => _flag; set => _flag = value; }
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



        public void Reset()
        {
            Bomb = false;
            IsOpen = false;
            Flag = false;
            BombsAround = 0;
            
        }
    }
}
