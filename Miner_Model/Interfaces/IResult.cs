using System;
using System.Collections.Generic;
using System.Text;

namespace Miner_Model.Interfaces
{
  public  interface IResult
    {
        DateTimeOffset Start { get; set; }
        DateTimeOffset End { get; set; }
        IDifficult Difficult { get; set; }
        bool Win { get; set; }
        int BombsLeft { get; set; }
    }
}
