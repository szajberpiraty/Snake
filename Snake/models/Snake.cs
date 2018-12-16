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
        public int EatenMealsCount { get; private set; } = 0;
        /// <summary>
        /// Elért pontszám
        /// </summary>
        public int Points { get; private set; } = 0;

        /// <summary>
        /// A kígyó pontjait tartalmazó lista
        /// </summary>
        /// Mindig inicializáljunk!
        public List<GamePoint> Gamepoints { get; set; } = new List<GamePoint>();
        public int Length {

            get { return Gamepoints.Count; }

            
        }

        public GamePoint Head
        {


            get
            {
                        return this.Gamepoints[0];

            }
            set
            {
                Gamepoints.Insert(0, value);
            }
                
                
                
         }

        /// <summary>
        /// A kígyó nyaka
        /// </summary>
        public GamePoint Neck
        {
            get { return Gamepoints[1]; }
        }

        public GamePoint TailEnd
        {


            get
            {
                return Gamepoints[Gamepoints.Count - 1];
            }
                
                
        }


        /// <summary>
        /// A kígyó eszik egy ételt
        /// </summary>
        /// <param name="meal"></param>
        public void Eat(Meal meal)
        {
            EatenMealsCount += 1;
            Points += meal.Points;
        }
    }
}
