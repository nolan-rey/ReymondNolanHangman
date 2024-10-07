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

namespace ReymondNolanHangman
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        int vie = 5;
        List<string> list = new List<string>() { "vache", "aigle", "bille", "canne", "coton" };
        Random random = new Random();
        string mot = random.Next(list.Length);


        private void BTN_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            String btnContent = btn.Content.ToString();
            btn.IsEnabled = false;
        }
    }
}
