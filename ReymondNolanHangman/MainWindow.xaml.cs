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
           StartUpGame();
        }

        int vie = 5;
        string GuessWord;
        char[] wordDisplay;


        public void StartUpGame()
        {
            vie = 5;
            List<string> listWord = new List<string>() { "vache", "aigle", "bille", "canne", "coton" };

            Random random = new Random();
            int mot = random.Next(0,listWord.Count);
            GuessWord = listWord[mot];
            wordDisplay = new string('*', GuessWord.Length).ToCharArray();
            UpdateMotAffiche();
            TB_display_Vie.Text = "vie : " + vie.ToString();

            foreach (Button btn in FindVisualChildren<Button>(this))
            {
                btn.IsEnabled = true;
            }
        }

        public void Windows_Loaded(object sender, RoutedEventArgs e)
        {
            StartUpGame();
        }

        private void BTN_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            String btnContent = btn.Content.ToString().ToLower();
            btn.IsEnabled = false;

            if (GuessWord.Contains(btnContent))
            {
                for (int i = 0; i < GuessWord.Length; i++)
                {
                    if (GuessWord[i].ToString() == btnContent)
                    {
                        wordDisplay[i] = GuessWord[i];
                    }
                }
            }
            else {
                vie--;
                TB_display_Vie.Text = "vie : " + vie.ToString();
                if (vie == 0) {
                    MessageBox.Show("Perdu! Le mot était : " + GuessWord);
                    StartUpGame();
                    return;
                }
            }
           UpdateMotAffiche() ;
            if (new string(wordDisplay) == GuessWord)
            {
                MessageBox.Show("Gagné");
                StartUpGame() ;
            }
        }

        private void UpdateMotAffiche()
        {
            TB_display.Text = new string(wordDisplay);
        }

        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
    }
}

    