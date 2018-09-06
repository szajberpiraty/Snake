using Snake.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Snake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Arena Arena;
        public MainWindow()
        {
            InitializeComponent();
            //this: hivatkozik arra az osztálypéldányra, amiben éppen vagyok, vagyis a megjelenített ablakot küldöm be
            //az Arena példányba.(Dependency Injection)
            Arena = new Arena(this);
        }

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            //Good
            Arena.Start();
        }

        private void ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            //Good
            Arena.Stop();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                case Key.Right:
                case Key.Up:
                case Key.Down:
                    //Ha a key Left vagy Right vagy Up vagy Down, lefut
                    Arena.KeyDown(e.Key);
                    //Jelezzük, hogy nem kell további feldolgozás,
                    //ha nem tesszük, akkor a gombjaink kijelölését is elvégzi, és az nem kell
                    e.Handled = true;
                    break;
                default:
                    break;
            }
        }
    }
}
