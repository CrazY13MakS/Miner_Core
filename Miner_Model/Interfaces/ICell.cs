using System;
using System.Drawing;

namespace Miner_Model.Interfaces
{
    /// <summary>
    /// Description of the Cell in miner
    /// </summary>
    public interface ICell
    {
        /// <summary>
        /// Location of current cell on the field
        /// </summary>
        Point Location { get; set; }
        /// <summary>
        /// Has current cell bomb
        /// </summary>
        bool Bomb { get; set; }
        /// <summary>
        /// Is the cell already open
        /// </summary>
        bool IsOpen { get; set; }
        /// <summary>
        /// Has current cell flag
        /// </summary>
        CellMark Mark { get; set; }
        /// <summary>
        /// Bombs around current cell
        /// </summary>
        int BombsAround { get; set; }
        /// <summary>
        /// Reset all all properties except Location
        /// </summary>
        void Reset();
       // /// <summary>
       // /// Set bombs around current cell
       // /// </summary>
       // /// <param name="value">Bombs count</param>
       // void SetBombsAround(int value);
    }
}
