using Miner_Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Miner_Model
{
    public class Miner
    {
        public IDifficult Difficult { get; private set; }
        public Miner(IDifficult difficult)
        {
            Difficult = difficult;
        }
        

        public void OpenCell(Point point)
        {
            int a;
        }
    }
}
