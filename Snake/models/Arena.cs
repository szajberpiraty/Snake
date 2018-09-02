using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Snake.models
{
    public class Arena
    {
        private DispatcherTimer GameTimer;
        private TimeSpan PlayTime;
        private MainWindow MainWindow;

        public Arena(MainWindow mainWindow)
        {
            this.MainWindow = mainWindow; //A konstruktor megkapja a MainWindow-t
        }

        //A játék indítása
        public void Start()
        {
            //Pontszámok nullázása
            PlayTime = TimeSpan.FromSeconds(0);
            GameTimer = new DispatcherTimer(TimeSpan.FromMilliseconds(100),DispatcherPriority.Normal,ItIsTimeForShow,Application.Current.Dispatcher);
        }

        private void ItIsTimeForShow(object sender, EventArgs e)
        {
            //Frissíteni a játékidőt
            PlayTime = PlayTime.Add(TimeSpan.FromMilliseconds(100));

            //Itt meg kéne valahogy keresni a MainWindow képernyőt és arra írni. Ez idejétmúlt megoldás.
            //A korszerű megoldás, hogy at Arena megkapja a MainWindow-t, elmenti magának, dolgozik rajta.

            MainWindow.LabelPlayTime.Content = $"Játékidő{PlayTime.ToString("mm\\:ss")}";
        }

        //A játék megállítása
        public void Stop()
        {
           
        }
    }
}
