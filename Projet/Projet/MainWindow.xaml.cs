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

using System.Windows.Threading;

namespace Projet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Rect joueurHitBox;
        Rect cubeHitBox;
        Rect boutonHitBox;
        Rect porteHitBox;

        ImageBrush joueurSprite = new ImageBrush();
        ImageBrush solSprite = new ImageBrush();
        ImageBrush cubeSprite = new ImageBrush();
        ImageBrush boutonSprite = new ImageBrush();
        ImageBrush porteSprite = new ImageBrush();

        int salle = 0;
        double vitesse = 0.025;

        bool Gauche, Droite, Haut, Bas = false;

        public MainWindow()
        {
            InitializeComponent();

            monCanvas.Focus();

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += BoucleJeux;
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(20);
            dispatcherTimer.Start();

            joueurSprite.ImageSource = new BitmapImage(new Uri("P:\\S1.01\\Projet\\Projet\\Image\\joueur.png"));
            joueur.Fill = joueurSprite;

            cubeSprite.ImageSource = new BitmapImage(new Uri("P:\\S1.01\\Projet\\Projet\\Image\\carrer1.png"));
            cube.Fill = cubeSprite;

            porteSprite.ImageSource = new BitmapImage(new Uri("P:\\S1.01\\Projet\\Projet\\Image\\porte1.png"));
            porte.Fill = porteSprite;

            boutonSprite.ImageSource = new BitmapImage(new Uri("P:\\S1.01\\Projet\\Projet\\Image\\bouton.png"));
            bouton.Fill = boutonSprite;

            solSprite.ImageSource = new BitmapImage(new Uri("P:\\S1.01\\Projet\\Projet\\Image\\sol.jpg"));
            sol.Fill = solSprite;
        }

        private void Canvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                Gauche = true;
            }
            if (e.Key == Key.Right)
            {
                Droite = true;
            }
            if (e.Key == Key.Up)
            {
                Haut = true;
            }
            if (e.Key == Key.Down)
            {
                Bas = true;
            }

        }

        private void Canvas_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                Gauche = false;
            }
            if (e.Key == Key.Right)
            {
                Droite = false;
            }
            if (e.Key == Key.Up)
            {
                Haut = false;
            }
            if (e.Key == Key.Down)
            {
                Bas = false;
            }
        }

        private void BoucleJeux(object sender, EventArgs e)
        {
            if (Gauche && Canvas.GetLeft(joueur) > 0)
            {
                Canvas.SetLeft(joueur, Canvas.GetLeft(joueur) - (vitesse));
            }
            else if (Droite && Canvas.GetLeft(joueur) + joueur.Width * 1.5< Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(joueur, Canvas.GetLeft(joueur) + (vitesse));
            }
            else if (Haut && Canvas.GetTop(joueur) > 0)
            {
                Canvas.SetTop(joueur, Canvas.GetTop(joueur) - (vitesse));
            }
            else if (Bas && Canvas.GetTop(joueur) + joueur.Height * 1.5< Application.Current.MainWindow.Height)
            {
                Canvas.SetTop(joueur, Canvas.GetTop(joueur) + (vitesse));
            }
        }
    }
}
