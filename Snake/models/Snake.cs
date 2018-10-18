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
        /// MEgevett ételek
        /// </summary>
        public int EatenMealsCount { get; set; } = 0;
        /// <summary>
        /// Elért pontszám
        /// </summary>
        public int Points { get; set; } = 0;

        /// <summary>
        /// A kígyó pontjait tartalmazó lista
        /// </summary>
        /// Mindig inicializáljunk!
        public List<GamePoint> Gamepoints { get; set; } = new List<GamePoint>();
        public int Length {

            get { return Gamepoints.Count; }

            
        }
        /// <summary>
        /// A kígyó eszik egy ételt
        /// </summary>
        /// <param name="meal"></param>
        public void Eat(GamePoint meal)
        {
            EatenMealsCount += 1;
            Points += 1;
        }
    }
}
