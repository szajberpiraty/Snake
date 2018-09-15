using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Snake.models
{
    public class Arena
    {
        /// <summary>
        /// A játék ütemét adó időzítő
        /// </summary>
        private DispatcherTimer GameTimer;

        /// <summary>
        /// A játékidő mérésére szolgál
        /// </summary>
        private TimeSpan PlayTime;

        //Az elért pontok
        private int Points;

        //A megevett ételek száma
        private int EatenMealsCount;

        //Lesz egy saját kígyó is
        private Snake Snake;



        /// <summary>
        /// A képernyő amin a játék fut
        /// </summary>
        private MainWindow MainWindow;
        /// <summary>
        /// Az ételek listája, amit a kígyó megehet
        /// </summary>
        private List<GamePoint> Meals;
        /// <summary>
        /// Véletlenszám generátor
        /// </summary>
        private Random randomNumberGenerator;

        public Arena(MainWindow mainWindow)
        {
            this.MainWindow = mainWindow; //A konstruktor megkapja a MainWindow-t
            SetNewGameCounters();
            ShowGameCounters();
        }

        //A játék indítása
        public void Start()
        {
            SetNewGameCounters();
            SetMealsForStart();
            GameTimer = new DispatcherTimer(TimeSpan.FromMilliseconds(100), DispatcherPriority.Normal, ItIsTimeForShow, Application.Current.Dispatcher);
        }

        private void SetMealsForStart()
        {
            Meals = new List<GamePoint>();

            randomNumberGenerator = new Random();

            //ez így már jó, de a kibányászást ki kellene szervezni egy függvénybe
            //kezelni kell az az esetet, amikor olyan helyre teszünk ételt ahol már van

            for (int i = 0; i < ArenaSettings.MealsCountForStart; i++)
            {
                var x = randomNumberGenerator.Next(1,ArenaSettings.MaxX);
                var y = randomNumberGenerator.Next(1, ArenaSettings.MaxY);
                var meal = new GamePoint(x: x,y: y);

                //megjelenítés

                //minden sor MaxX elemből áll, úgy lehet megtalálni az adott koordinátát, hogy 
                // leszámolok annyit, amennyi sor van, plusz az x
                //tehát a harmadik sor 5. eleméhez 2*20 + 5-1 
                var child=MainWindow.GridArena.Children[(meal.Y - 1) * ArenaSettings.MaxX + (meal.X - 1)];

                //A children gyűjtemény UIElement elemekből áll, az imageawesome eléréséhez ki kell bányászni belőle
                //így már van icon property
                ((FontAwesome.WPF.ImageAwesome)child).Icon = FontAwesome.WPF.FontAwesomeIcon.Star;
                ((FontAwesome.WPF.ImageAwesome)child).Foreground = Brushes.Red;
                ((FontAwesome.WPF.ImageAwesome)child).Spin = true;


                //hozzáadni a listához
                Meals.Add(meal);
            }
        }

        //A játék megállítása
        public void Stop()
        {
            GameTimer.Stop();
        }


        private void ItIsTimeForShow(object sender, EventArgs e)
        {
            //Frissíteni a játékidőt
            PlayTime = PlayTime.Add(TimeSpan.FromMilliseconds(100));

            //Itt meg kéne valahogy keresni a MainWindow képernyőt és arra írni. Ez idejétmúlt megoldás.
            //A korszerű megoldás, hogy at Arena megkapja a MainWindow-t, elmenti magának, dolgozik rajta.

            ShowGameCounters();
        }

        /// <summary>
        /// Ha leütik valamelyik gombot, itt megérkezik
        /// </summary>
        /// <param name="key">jelzi hogy melyik nyílgombot ütötték le</param>
        public void KeyDown(Key key)
        {
            switch (key)
            {
                case Key.Left:
                    Snake.Direction = SnakeDirections.Left;
                    break;
                case Key.Right:
                    Snake.Direction = SnakeDirections.Right;
                    break;
                case Key.Up:
                    Snake.Direction = SnakeDirections.Up;
                    break;
                case Key.Down:
                    Snake.Direction = SnakeDirections.Down;
                    break;
                default:
                    throw new Exception($"Erre a gombra nem vagyunk felkészülve!{key}");
                    
            }
        }

        private void SetNewGameCounters()
        {
            //Pontszámok nullázása
            Points = 0;
            EatenMealsCount = 0;
            PlayTime = TimeSpan.FromSeconds(0);
            Snake = new Snake();
        }

        private void ShowGameCounters()
        {
            MainWindow.LabelPlayTime.Content = $"Játékidő:{PlayTime.ToString("mm\\:ss")}";
            MainWindow.LabelPoints.Content=$"Pontszám:{Points}";
            MainWindow.LabelEatenMealsCount.Content=$"Megevett ételek:{EatenMealsCount}";
            MainWindow.LabelSnakeLength.Content=$"A kígyó hossza:{Snake.Length}";
            MainWindow.LabelKeyDown.Content = $"A kígyó iránya:{Snake.Direction}";

        }

       
    }
}
