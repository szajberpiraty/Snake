using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.models
{
    public class Meal:GamePoint
    {
        public Meal(int x, int y) : base(x, y)
        {
        }

        public int Points { get; } = 3;
    }
}
