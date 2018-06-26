using System;
using System.Collections.Generic;
using System.Text;

namespace Miner_Model.Interfaces
{
  public  interface IResult
    {
        DateTime Start { get; set; }
        DateTime End { get; set; }
        IDifficult Difficult { get; set; }
        bool Win { get; }
        int BombsLeft { get; }
    }
}
