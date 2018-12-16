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

     

        //Lesz egy saját kígyó is
        private Snake Snake;



        /// <summary>
        /// A képernyő amin a játék fut
        /// </summary>
        private MainWindow MainWindow;
        /// <summary>
        /// Az ételek listája, amit a kígyó megehet
        /// </summary>
        private List<Meal> Meals;
        /// <summary>
        /// Véletlenszám generátor, az aréna létrejöttekor inicializálódik
        /// </summary>
        private Random randomNumberGenerator = new Random();

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
            //először a kígyót tesszük ki, így nem kell az ételekkel foglalkozni, az étel könnyebben igazodik a kígyóhoz

            SetSnakeForStart();

            SetMealsForStart();
            if (GameTimer != null)
            {
                GameTimer.Stop();
            }
            GameTimer = new DispatcherTimer(TimeSpan.FromMilliseconds(100), DispatcherPriority.Normal, ItIsTimeForShow, Application.Current.Dispatcher);
        }

        private void SetSnakeForStart()
        {
            var head = GetRandomMeal();

            Snake = new Snake();
            Snake.Gamepoints = new List<GamePoint>();
            Snake.Gamepoints.Add(head);



            //kígyóelhelyezés, vízszintesen rajzolunk, ha x 10-nél nagyobb balra, egyébként jobbra
            ShowSnakeHead(head);

            for (int i = 0; i < ArenaSettings.SnakeCountForStart; i++)
            {
                GamePoint gamePoint;
                if (head.X <= 10)
                {//jobbra nyúlik
                    gamePoint = new GamePoint(head.X + i + 1, head.Y);
                   
                }
                else
                {//balra nyúlik
                    gamePoint = new GamePoint(head.X - i - 1, head.Y);
                    
                }
                Snake.Gamepoints.Add(gamePoint);
                ShowSnakeTail(gamePoint);
            }

           

        }

        

        private void SetMealsForStart()
        {
            Meals = new List<Meal>();



            //ez így már jó, de a kibányászást ki kellene szervezni egy függvénybe
            //kezelni kell az az esetet, amikor olyan helyre teszünk ételt ahol már van

            while (Meals.Count < ArenaSettings.MealsCountForStart)
            {//Addig megyünk amíg sikerül minden ételt kirakni
                GetNewMeal();

                //megjelenítés vagy mi



            }
        }

        private void GetNewMeal()
        {
            var meal = GetRandomMeal();

            //A függvény igazat ad, ha a lambda igazat ad
            if (!Meals.Any(gamePoint => gamePoint.X == meal.X && gamePoint.Y == meal.Y) && !Snake.Gamepoints.Any(gamePoint => gamePoint.X == meal.X && gamePoint.Y == meal.Y))
            {

                //A children gyűjtemény UIElement elemekből áll, az imageawesome eléréséhez ki kell bányászni belőle
                //így már van icon property
                ShowMeal(meal);


                //hozzáadni a listához
                Meals.Add(meal);


            } //Csak akkkor továbbmenni, ha az étel nincs még a táblán
        }

        /// <summary>
        /// kijelöl egy véletlen pontot a képernyőn
        /// </summary>
        /// <returns></returns>
        private Meal GetRandomMeal()
        {
            var x = randomNumberGenerator.Next(1, ArenaSettings.MaxX+1);
            var y = randomNumberGenerator.Next(1, ArenaSettings.MaxY+1);
            var meal = new Meal(x: x, y: y);
            return meal;
        }

        /// <summary>
        /// Megjelenítjük az ételt
        /// </summary>
        /// <param name="child"></param>
        private void ShowMeal(GamePoint meal)
        {
            var child = GetGridArenaCell(meal);
            child.Icon = FontAwesome.WPF.FontAwesomeIcon.Star;
            child.Foreground = Brushes.Red;
            child.Spin = true;
            child.SpinDuration = 5;
        }
        /// <summary>
        /// Eltüntetjük az ételt
        /// </summary>
        /// <param name="meal"></param>
        private void HideMeal(GamePoint meal)
        {
            var child = GetGridArenaCell(meal);
            child.Icon = FontAwesome.WPF.FontAwesomeIcon.SquareOutline;
            child.Foreground = Brushes.Black;
            child.Spin = false;
            child.SpinDuration = 1;
        }

        private void ShowSnakeHead(GamePoint head)
        {
            var child = GetGridArenaCell(head);
            child.Icon = FontAwesome.WPF.FontAwesomeIcon.Circle;
            child.Foreground = Brushes.Green;
                        
        }

        /// <summary>
        /// A kígyó farkának megjelenítése
        /// </summary>
        private void ShowSnakeTail(GamePoint tail)
        {
            var child = GetGridArenaCell(tail);
            child.Icon = FontAwesome.WPF.FontAwesomeIcon.Circle;
            child.Foreground = Brushes.Blue;
        }

        private void HideSnakeTail(GamePoint tailEnd)
        {
            var child = GetGridArenaCell(tailEnd);
            child.Icon = FontAwesome.WPF.FontAwesomeIcon.SquareOutline;
            child.Foreground = Brushes.Black;
        }


        private FontAwesome.WPF.ImageAwesome GetGridArenaCell(GamePoint gamePoint)
        {

            //megjelenítés

            //minden sor MaxX elemből áll, úgy lehet megtalálni az adott koordinátát, hogy 
            // leszámolok annyit, amennyi sor van, plusz az x
            //tehát a harmadik sor 5. eleméhez 2*20 + 5-1 
            var child = MainWindow.GridArena.Children[(gamePoint.Y - 1) * ArenaSettings.MaxX + (gamePoint.X - 1)];
            var cell = (FontAwesome.WPF.ImageAwesome)child;

            return cell;
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

            //játékmenet frissítés
            // a kígyó feje mozog a kijelölt irányba
            var oldHead = Snake.Head;
            GamePoint newHead=null;

            switch (Snake.Direction)
            {
                case SnakeDirections.None:
                    break;
                case SnakeDirections.Left:
                    newHead = new GamePoint(oldHead.X-1,oldHead.Y);
                    break;
                case SnakeDirections.Right:
                    newHead = new GamePoint(oldHead.X + 1, oldHead.Y);
                    break;
                case SnakeDirections.Up:
                    newHead = new GamePoint(oldHead.X, oldHead.Y-1);
                    break;
                case SnakeDirections.Down:
                    newHead = new GamePoint(oldHead.X, oldHead.Y + 1);
                    break;
                default:
                    throw new Exception($"Erre nem vagyunk felkészülve!{Snake.Direction}");
                  
            }
            if (newHead == null)
            {//nincs új fej nincs mit tenni
                return;
            }
            //le kell ellenőrizni, hogy 
            //saját magába harapott-e
            if (Snake.Gamepoints.Any(gp=>gp.X==newHead.X && gp.Y==newHead.Y))
            {
                GameOver();
            }
            //megevett-e ételt
            var IsEated = Meals.Any(gp => gp.X == newHead.X && gp.Y == newHead.Y);
            if (IsEated)
            {//ételt ettünk
                //mégpedig ezt
                var meal = Meals.Single(gp => gp.X == newHead.X && gp.Y == newHead.Y);
                //todo:nem vesszük le a megevett ételt a listáról

                Snake.Eat(meal);
                Meals.Remove(meal);

                HideMeal(meal);
                while (Meals.Count < ArenaSettings.MealsCountForStart)
                {//Addig megyünk amíg sikerül minden ételt kirakni
                    GetNewMeal();

                    //megjelenítés vagy mi



                }
                
            }

            //nekiment-e a falnak
            if (newHead.X==0 || newHead.Y==0 || newHead.X==ArenaSettings.MaxX+1 || newHead.Y==ArenaSettings.MaxY+1)
            {
                GameOver();
            }

            //megjeleníteni a kígyó új helyzetét

            ShowSnakeTail(oldHead);


            //Ha nem evett
            if (!IsEated) { 
            var tailEnd = Snake.Gamepoints[Snake.Gamepoints.Count - 1];
            HideSnakeTail(tailEnd);
                Snake.Gamepoints.Remove(tailEnd);
            }

            Snake.Gamepoints.Insert(0,newHead);//todo settert implementálni
            ShowSnakeHead(newHead);


            //Itt meg kéne valahogy keresni a MainWindow képernyőt és arra írni. Ez idejétmúlt megoldás.
            //A korszerű megoldás, hogy at Arena megkapja a MainWindow-t, elmenti magának, dolgozik rajta.

            ShowGameCounters();
        }

        

        private void GameOver()
        {
            Stop();
            MessageBox.Show("Játék vége!");

        }

        /// <summary>
        /// Ha leütik valamelyik gombot, itt megérkezik
        /// </summary>
        /// <param name="key">jelzi hogy melyik nyílgombot ütötték le</param>
        /// 

        

      
        public void KeyDown(Key key)
        {
            if (ArenaSettings.IsSittingInTheHeadOfSnake)
            {//ha a kígyó fejében ülünk
                switch (key)
                {
                    case Key.Left:
                        switch (Snake.Direction)
                        {
                            case SnakeDirections.None:
                                //Kidolgozni
                                var head = Snake.Head;
                                var neck = Snake.Neck;

                                if (head.X<neck.X)
                                {//A kígyó balra áll
                                    Snake.Direction = SnakeDirections.Down;
                                }
                                else
                                {
                                    Snake.Direction = SnakeDirections.Up;
                                }
                                break;
                            case SnakeDirections.Left:
                                Snake.Direction = SnakeDirections.Down;
                                break;
                            case SnakeDirections.Right:
                                Snake.Direction = SnakeDirections.Up;
                                break;
                            case SnakeDirections.Up:
                                Snake.Direction = SnakeDirections.Left;
                                break;
                            case SnakeDirections.Down:
                                Snake.Direction = SnakeDirections.Right;
                                break;
                            default:
                                throw new Exception($"Erre az irányra nem vagyunk felkészülve!{Snake.Direction}");
                                
                        }
                        break;
                    case Key.Right:
                        switch (Snake.Direction)
                        {
                            case SnakeDirections.None:
                                //Kidolgozni
                                var head = Snake.Head;
                                var neck = Snake.Neck;

                                if (head.X < neck.X)
                                {//A kígyó balra áll
                                    Snake.Direction = SnakeDirections.Up;
                                }
                                else
                                {
                                    Snake.Direction = SnakeDirections.Down;
                                }


                                break;
                            case SnakeDirections.Left:
                                Snake.Direction = SnakeDirections.Up;
                                break;
                            case SnakeDirections.Right:
                                Snake.Direction = SnakeDirections.Down;
                                break;
                            case SnakeDirections.Up:
                                Snake.Direction = SnakeDirections.Right;
                                break;
                            case SnakeDirections.Down:
                                Snake.Direction = SnakeDirections.Left;
                                break;
                            default:
                                throw new Exception($"Erre az irányra nem vagyunk felkészülve!{Snake.Direction}");
                                break;
                        }
                        break;
                    case Key.Up:
                    case Key.Down:
                        //Ezek az esetek nem működnek
                        //
                        break;
                    default:
                        throw new Exception($"Erre a gombra nem vagyunk felkészülve!{key}");



                }
            }
            else
            {//kívülről nézzük
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

            
           
        }

        private void SetNewGameCounters()
        {
            //Pontszámok nullázása
          
            PlayTime = TimeSpan.FromSeconds(0);
            Snake = new Snake();
        }

        private void ShowGameCounters()
        {
            MainWindow.LabelPlayTime.Content = $"Játékidő:{PlayTime.ToString("mm\\:ss")}";
            MainWindow.LabelPoints.Content = $"Pontszám:{Snake.Points}";
            MainWindow.LabelEatenMealsCount.Content = $"Megevett ételek:{Snake.EatenMealsCount}";
            MainWindow.LabelSnakeLength.Content = $"A kígyó hossza:{Snake.Length}";
            MainWindow.LabelKeyDown.Content = $"A kígyó iránya:{Snake.Direction}";

        }


    }
}