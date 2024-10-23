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

        // initialisation des variables

        int vie = 7;
        int score = 0;
        string GuessWord;
        char[] wordDisplay;

        // initialisation du jeu

        public void StartUpGame()
        {
            // initialisation des variables
            vie = 7;
            timeLeft = 60;

            // lecture du fichier mot-Hangman.txt
            List<string> listWord = File.ReadAllLines("../../Ressource/mot-Hangman.txt").ToList();
            Random random = new Random();

            // selection d'un mot aléatoire
            int mot = random.Next(0, listWord.Count);
            GuessWord = listWord[mot];
            wordDisplay = new string('_', GuessWord.Length).ToCharArray();
            UpdateMotAffiche();

            // affichage des variables
            TB_display_Vie.Text = "vie : " + vie.ToString();
            TB_display_Score.Text = "score : " + score.ToString();
            Uri ressource = new Uri("Ressource/Img/7.png", UriKind.Relative);
            Img_Pendu.Source = new BitmapImage(ressource);
            AllElementButton();

            // timer
            if (timer != null)
            {
                timer.Stop();
            }

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        // fonction timer
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


        // demarrer le jeu au chargement de la fenetre
        public void Windows_Loaded(object sender, RoutedEventArgs e)
        {
            StartUpGame();
        }


        // fonction click sur les boutons
        private void BTN_Click(object sender, RoutedEventArgs e)
        {
            // son
            MediaPlayer playMedia = new MediaPlayer();
            var uri = new Uri("../../Ressource/sound-btn.mp3", UriKind.Relative);
            playMedia.Open(uri);
            playMedia.Play();

            // recuperation du bouton cliqué    
            Button btn = sender as Button;
            String btnContent = btn.Content.ToString().ToLower();
            btn.IsEnabled = false;

            // verification si le mot contient la lettre
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
                // si le mot ne contient pas la lettre
                vie--;
                TB_display_Vie.Text = "vie : " + vie.ToString();
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
            // verification si le mot est trouvé
            if (new string(wordDisplay) == GuessWord)
            {
                timer.Stop();
                score = score+1;
                MessageBox.Show("Gagné");
                StartUpGame();
            }
        }

        // fonction pour mettre à jour le mot affiché
        private void UpdateMotAffiche()
        {
            TB_display.Text = new string(wordDisplay);
        }

        // fonction pour activer tous les boutons
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

    