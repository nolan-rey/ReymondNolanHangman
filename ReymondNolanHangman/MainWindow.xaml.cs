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
using System.Windows.Threading;

namespace ReymondNolanHangman
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int timeLeft = 60;
        private DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();
            StartUpGame();
        }

        int vie = 7;
        int score = 0;
        string GuessWord;
        char[] wordDisplay;


        public void StartUpGame()
        {
            vie = 7;
            timeLeft = 60;
            List<string> listWord = File.ReadAllLines("../../Ressource/mot-Hangman.txt").ToList();

            Random random = new Random();
            int mot = random.Next(0, listWord.Count);
            GuessWord = listWord[mot];
            wordDisplay = new string('_', GuessWord.Length).ToCharArray();
            UpdateMotAffiche();
            TB_display_Vie.Text = "vie : " + vie.ToString();
            TB_display_Score.Text = "score : " + score.ToString();
            Uri ressource = new Uri("Ressource/Img/7.png", UriKind.Relative);
            Img_Pendu.Source = new BitmapImage(ressource);
            AllElementButton();

            if (timer != null)
            {
                timer.Stop();
            }

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                timeLeft--;
                TB_Timer.Text = "Temps restant : " + timeLeft.ToString() + "s";
            }
            if (timeLeft == 0)
            {
                MessageBox.Show("Perdu! Le mot était : " + GuessWord);
                StartUpGame();
                return;
            }
        }

        public void Windows_Loaded(object sender, RoutedEventArgs e)
        {
            StartUpGame();
        }

        private void BTN_Click(object sender, RoutedEventArgs e)
        {
            MediaPlayer playMedia = new MediaPlayer();
            var uri = new Uri("../../Ressource/sound-btn.mp3", UriKind.Relative);
            playMedia.Open(uri);
            playMedia.Play();
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
                    timer.Stop();
                    MessageBox.Show("Perdu! Le mot était : " + GuessWord);
                    StartUpGame();
                    return;
                }
            }
            UpdateMotAffiche();
            if (new string(wordDisplay) == GuessWord)
            {
                timer.Stop();
                score = score+1;
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

    