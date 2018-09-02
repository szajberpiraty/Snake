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
        private TimeSpan PlayTime;

        //A játék indítása
        public void Start()
        {
            //Pontszámok nullázása
            PlayTime = TimeSpan.FromSeconds(0);
            Timer = new DispatcherTimer(TimeSpan.FromMilliseconds(100),DispatcherPriority.Normal,ItIsTimeForShow,Application.Current.Dispatcher);
        }
        //A játék megállítása
        public void Stop()
        {
           
        }
    }
}
