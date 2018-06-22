using System;
using System.Collections.Generic;
using System.Text;

namespace Miner_Model.Interfaces
{
    public interface IDifficult
    {
        int Width { get; }
        int Height { get; }
        int BombsCount { get; }
        String Name { get; set; }
    }
}
