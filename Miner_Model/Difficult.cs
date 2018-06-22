using Miner_Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Miner_Model
{
    public class Difficult : IDifficult
    {
        int _fieldWidth;
        int _fieldHeight;
        int _bombsCount;
        String _name;
        public int Width { get => _fieldWidth; private set => _fieldWidth = value; }

        public int Height { get => _fieldHeight; private set => _fieldHeight = value; }

        public int BombsCount { get => _bombsCount; private set => _bombsCount = value; }

        public string Name { get => _name; set => _name = value; }

        public Difficult(int width, int height, int bombsCount,String name="Custom")
        {
            Width = width;
        }

        #region Static fields
        public static Difficult Low { get; private set; }
        public static Difficult Normal { get; private set; }
        public static Difficult Hard { get; private set; }

        Difficult()
        {
            Low = new Difficult()
            {
                BombsCount = 16,
                Height = 9,
                Width = 9,
                 Name="Low"
            };

            Normal = new Difficult()
            {
                BombsCount = 55,
                Height = 20,
                Width = 20,
                Name = "Middle"
            };
            Hard = new Difficult()
            {
                BombsCount = 10,
                Height = 50,
                Width = 50,
                Name = "Hard"
            };
        }
        #endregion
    }
}
