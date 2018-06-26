using Miner_Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Miner_Model
{
    /// <summary>
    /// Describe difficult for miner game
    /// </summary>
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

        /// <summary>
        /// Constructor  
        /// </summary>
        /// <param name="width">Field width. Value must be >=5</param>
        /// <param name="height">Field height. Value must be >=5</param>
        /// <param name="bombsCount">Number of bombs on the field. Value must be >=5</param>
        /// <param name="name">Name for current difficult</param>
        public Difficult(int width, int height, int bombsCount, String name = "Custom")
        {
            if (width < 5 || height < 5)
            {
                throw new ArgumentException($"Width and height must be greater or equal than 5. Input values: width = {width}; height = {height}");
            }
            if (bombsCount < 5)
            {
                throw new ArgumentException($"Bombs count must be greater or equal than 5. Input value = {bombsCount} ");
            }
            Width = width;
            Height = height;
            BombsCount = bombsCount;
            Name = name ?? "Custom";
        }

        #region Static fields
        /// <summary>
        /// Low difficult: width = 9; height = 9; bombs = 16
        /// </summary>
        public static Difficult Low { get; private set; }
        /// <summary>
        /// Normal difficult: width = 16; height = 16; bombs = 49
        /// </summary>
        public static Difficult Normal { get; private set; }
        /// <summary>
        /// Hard difficult: width = 30; height = 16; bombs = 99
        /// </summary>
        public static Difficult Hard { get; private set; }

        Difficult()
        {
            Low = new Difficult()
            {
                BombsCount = 16,
                Height = 9,
                Width = 9,
                Name = "Low"
            };

            Normal = new Difficult()
            {
                BombsCount = 49,
                Height = 16,
                Width = 16,
                Name = "Normal"
            };
            Hard = new Difficult()
            {
                BombsCount = 99,
                Height = 16,
                Width = 30,
                Name = "Hard"
            };
        }
        #endregion
    }
}
