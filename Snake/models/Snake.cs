using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.models
{
    public class Snake
    {
        public SnakeDirections Direction { get; set; }
        /// <summary>
        /// A kígyó pontjait tartalmazó lista
        /// </summary>
        public List<GamePoint> Gamepoints { get; set; }
        public int Length { get; set; }
    }
}
