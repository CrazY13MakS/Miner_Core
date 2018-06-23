using Miner_Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Miner_Model
{
    public class Miner
    {
        public List<List<ICell>> Field { get; set; }
        public IDifficult Difficult { get; private set; }
        public Miner(IDifficult difficult)
        {
            if(difficult==null)
            {
                throw new ArgumentNullException($"{nameof(difficult)} can't be null");
            }
            if(difficult.Height<5||difficult.Width<5)
            {
                throw new ArgumentException($"Width and Height must be greater or equal 5. Current values: width = {difficult.Width}; height = {difficult.Height}");
            }
            if(difficult.BombsCount>= difficult.Width*difficult.Height)
            {
                throw new ArgumentException($"Bombs count on field must be lower than width*height. Current values:width = {difficult.Width}; height = {difficult.Height}; bombs count = {difficult.BombsCount}");
            }
            Difficult = difficult;
        }
        
        public virtual void InitField()
        {
            CreateField();
            SetBombs();
        }
        protected void SetBombs()
        {
            if(Field==null)
            {
                throw new ArgumentNullException($"Before set bombs init property: {nameof(Field)}");
            }
            int bombs = 0;
            while (bombs)
            {

            }

        }
        protected void CreateField()
        {
            Field = new List<List<ICell>>(Difficult.Height);
            for (int i = 0; i < Difficult.Height; i++)
            {
                Field.Add(new List<ICell>(Difficult.Width));
                for (int j = 0; j < Difficult.Width; j++)
                {
                    Field[i].Add(new Cell(new Point(i,j)));
                }
            }          
        }
        public virtual void Reset()
        {

        }
        public virtual void NewGame()
        {
            Reset();
        }
        public virtual void NewGame(IDifficult difficult)
        {

        }
        public virtual void OpenCell(Point point)
        {
            
        }
    }
}
