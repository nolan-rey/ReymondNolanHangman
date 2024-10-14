using ReymondNolanHangman.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
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
using System.IO;

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

        int vie = 7;
        string GuessWord;
        char[] wordDisplay;


        public void StartUpGame()
        {
            vie = 7;
            List<string> listWord = File.ReadAllLines("../../Ressource/mot-Hangman.txt").ToList();

            Random random = new Random();
            int mot = random.Next(0, listWord.Count);
            GuessWord = listWord[mot];
            wordDisplay = new string('_', GuessWord.Length).ToCharArray();
            UpdateMotAffiche();
            TB_display_Vie.Text = "vie : " + vie.ToString();
            Uri ressource = new Uri("Ressource/Img/7.png", UriKind.Relative);
            Img_Pendu.Source = new BitmapImage(ressource);
            AllElementButton();
            MediaPlayer playMedia = new MediaPlayer();
            var uri = new Uri("Ressource/Sound/StartUpGame.mp3", UriKind.Relative);
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
               // Ressource/Img/1.png
                Uri ressource = new Uri("Ressource/Img/" + vie + ".png", UriKind.Relative);
                Img_Pendu.Source = new BitmapImage(ressource);
                if (vie == 0) {
                    MessageBox.Show("Perdu! Le mot était : " + GuessWord);
                    StartUpGame();
                    return;
                }
            }
            UpdateMotAffiche();
            if (new string(wordDisplay) == GuessWord)
            {
                MessageBox.Show("Gagné");
                StartUpGame();
            }
        }

        private void UpdateMotAffiche()
        {
            TB_display.Text = new string(wordDisplay);
        }

        private void AllElementButton()
        {
            foreach (UIElement element in grdi_Keypad.Children)
            {
                if (element is Button btn)
                {
                    btn.IsEnabled = true;
                }
            }
        }
    }
}

    