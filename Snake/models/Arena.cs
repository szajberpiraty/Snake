﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
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
            GameTimer = new DispatcherTimer(TimeSpan.FromMilliseconds(100), DispatcherPriority.Normal, ItIsTimeForShow, Application.Current.Dispatcher);
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
                    break;
                case Key.Right:
                    break;
                case Key.Up:
                    break;
                case Key.Down:
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

        }

       
    }
}