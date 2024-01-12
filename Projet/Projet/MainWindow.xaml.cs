using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

        string salle = "0";
        int vitesse = 10;
        int vitessecube = 11;

        bool Gauche, Droite, Haut, Bas, E = false;
        bool ouvert = false;

        public MainWindow()
        {
            InitializeComponent();

            monCanvas.Focus();

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += BoucleJeux;
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(20);
            dispatcherTimer.Start();

            joueurSprite.ImageSource = new BitmapImage(new Uri("images/joueur1.png", UriKind.RelativeOrAbsolute));
            joueur.Fill = joueurSprite;

            cubeSprite.ImageSource = new BitmapImage(new Uri("images/carrer1.png", UriKind.RelativeOrAbsolute));
            cube.Fill = cubeSprite;

            porteSprite.ImageSource = new BitmapImage(new Uri("images/porte1.png", UriKind.RelativeOrAbsolute));
            porte.Fill = porteSprite;

            boutonSprite.ImageSource = new BitmapImage(new Uri("images/bouton.png", UriKind.RelativeOrAbsolute));
            bouton.Fill = boutonSprite;

            solSprite.ImageSource = new BitmapImage(new Uri("images/sol.png", UriKind.RelativeOrAbsolute));
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
            if (e.Key == Key.E)
            {
                E = true;
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
            if (e.Key == Key.E)
            {
                E = false;
            }
        }

        private void BoucleJeux(object sender, EventArgs e)
        {
            

            //création Hitbox
            joueurHitBox = new Rect(Canvas.GetLeft(joueur), Canvas.GetTop(joueur), joueur.Width-17, joueur.Height-5);
            cubeHitBox = new Rect(Canvas.GetLeft(cube), Canvas.GetTop(cube), cube.Width, cube.Height);
            boutonHitBox = new Rect(Canvas.GetLeft(bouton), Canvas.GetTop(bouton), bouton.Width-20, bouton.Height-20);
            porteHitBox = new Rect(Canvas.GetLeft(porte), Canvas.GetTop(porte), porte.Width, porte.Height);

            //collision joueur cube
            if (joueurHitBox.IntersectsWith(cubeHitBox) && E)
            {
                //faire bouger le cube
                if (Gauche && Canvas.GetLeft(joueur) > 50)
                {
                    Canvas.SetLeft(cube, Canvas.GetLeft(cube) - (vitessecube));
                }
                else if (Droite && Canvas.GetLeft(joueur) + cube.Width * 2.10 < Application.Current.MainWindow.Width)
                {
                    Canvas.SetLeft(cube, Canvas.GetLeft(cube) + (vitessecube));
                }
                else if (Haut && Canvas.GetTop(joueur) > 50)
                {
                    Canvas.SetTop(cube, Canvas.GetTop(cube) - (vitessecube));
                }
                else if (Bas && Canvas.GetTop(joueur) + cube.Height * 2.50 < Application.Current.MainWindow.Height)
                {
                    Canvas.SetTop(cube, Canvas.GetTop(cube) + (vitessecube));
                }
            }
            else
            {
                if (joueurHitBox.IntersectsWith(cubeHitBox))
                {//bloquer le personnage
                    if (Gauche && Canvas.GetLeft(joueur) > 0)
                    {
                        joueurSprite.ImageSource = new BitmapImage(new Uri("images/joueur4.png", UriKind.RelativeOrAbsolute));
                        joueur.Fill = joueurSprite;
                        Canvas.SetLeft(joueur, Canvas.GetLeft(joueur) - (vitesse));

                        if (joueurHitBox.IntersectsWith(cubeHitBox))
                        {
                            Canvas.SetLeft(joueur, Canvas.GetLeft(joueur) + (vitesse * 1.60));
                        }
                    }
                    else if (Droite && Canvas.GetLeft(joueur) + joueur.Width * 1.5 < Application.Current.MainWindow.Width)
                    {
                        joueurSprite.ImageSource = new BitmapImage(new Uri("images/joueur2.png", UriKind.RelativeOrAbsolute));
                        joueur.Fill = joueurSprite;
                        Canvas.SetLeft(joueur, Canvas.GetLeft(joueur) + (vitesse));

                        if (joueurHitBox.IntersectsWith(cubeHitBox))
                        {
                            Canvas.SetLeft(joueur, Canvas.GetLeft(joueur) - (vitesse*1.60));
                        }
                    }
                    else if (Haut && Canvas.GetTop(joueur) > 0)
                    {
                        joueurSprite.ImageSource = new BitmapImage(new Uri("images/joueur1.png", UriKind.RelativeOrAbsolute));
                        joueur.Fill = joueurSprite;
                        Canvas.SetTop(joueur, Canvas.GetTop(joueur) - (vitesse));

                        if (joueurHitBox.IntersectsWith(cubeHitBox))
                        {
                            Canvas.SetTop(joueur, Canvas.GetTop(joueur) + (vitesse*1.60));
                        }
                    }
                    else if (Bas && Canvas.GetTop(joueur) + joueur.Height * 1.5 < Application.Current.MainWindow.Height)
                    {
                        joueurSprite.ImageSource = new BitmapImage(new Uri("images/joueur3.png", UriKind.RelativeOrAbsolute));
                        joueur.Fill = joueurSprite;
                        Canvas.SetTop(joueur, Canvas.GetTop(joueur) + (vitesse));

                        if (joueurHitBox.IntersectsWith(cubeHitBox))
                        {
                            Canvas.SetTop(joueur, Canvas.GetTop(joueur) - (vitesse*1.60));
                        }
                    }
                }
                else
                {//bouger le personnage
                    if (Gauche && Canvas.GetLeft(joueur) > 0)
                    {
                        joueurSprite.ImageSource = new BitmapImage(new Uri("images/joueur4.png", UriKind.RelativeOrAbsolute));
                        joueur.Fill = joueurSprite;
                        Canvas.SetLeft(joueur, Canvas.GetLeft(joueur) - (vitesse));
                    }
                    else if (Droite && Canvas.GetLeft(joueur) + joueur.Width * 1.5 < Application.Current.MainWindow.Width)
                    {
                        joueurSprite.ImageSource = new BitmapImage(new Uri("images/joueur2.png", UriKind.RelativeOrAbsolute));
                        joueur.Fill = joueurSprite;
                        Canvas.SetLeft(joueur, Canvas.GetLeft(joueur) + (vitesse));
                    }
                    else if (Haut && Canvas.GetTop(joueur) > 0)
                    {
                        joueurSprite.ImageSource = new BitmapImage(new Uri("images/joueur1.png", UriKind.RelativeOrAbsolute));
                        joueur.Fill = joueurSprite;
                        Canvas.SetTop(joueur, Canvas.GetTop(joueur) - (vitesse));
                    }
                    else if (Bas && Canvas.GetTop(joueur) + joueur.Height * 1.5 < Application.Current.MainWindow.Height)
                    {
                        joueurSprite.ImageSource = new BitmapImage(new Uri("images/joueur3.png", UriKind.RelativeOrAbsolute));
                        joueur.Fill = joueurSprite;
                        Canvas.SetTop(joueur, Canvas.GetTop(joueur) + (vitesse));
                    }
                }
                
            }
            //collision joueur bouton / cube bouton
            if (joueurHitBox.IntersectsWith(boutonHitBox))
            {
                porteSprite.ImageSource = new BitmapImage(new Uri("images/porte2.png", UriKind.RelativeOrAbsolute));
                porte.Fill = porteSprite;
                ouvert = true;
            }
            else if (cubeHitBox.IntersectsWith(boutonHitBox))
            {
                cubeSprite.ImageSource = new BitmapImage(new Uri("images/carrer2.png", UriKind.RelativeOrAbsolute));
                cube.Fill = cubeSprite;
                porteSprite.ImageSource = new BitmapImage(new Uri("images/porte2.png", UriKind.RelativeOrAbsolute));
                porte.Fill = porteSprite;
                ouvert = true;
            }
            else
            {
                cubeSprite.ImageSource = new BitmapImage(new Uri("images/carrer1.png", UriKind.RelativeOrAbsolute));
                cube.Fill = cubeSprite;
                porteSprite.ImageSource = new BitmapImage(new Uri("images/porte1.png", UriKind.RelativeOrAbsolute));
                porte.Fill = porteSprite;
                ouvert = false;
            }
            //collision joueur porte
                if (ouvert && joueurHitBox.IntersectsWith(porteHitBox))
                {
                    switch (salle)
                    {
                        case "0":
                        {
                            salle = "1";
                            NumSalle.Content = "Salle : " + salle + "/6";
                            ouvert = false;
                            break;
                        }
                        case "1":
                        {
                            salle = "2";
                            NumSalle.Content = "Salle : " + salle + "/6";
                            ouvert = false;
                            break;
                        }
                        case "2":
                        {
                            salle = "3";
                            NumSalle.Content = "Salle : " + salle + "/6";
                            ouvert = false;
                            break;
                        }
                        case "3":
                        {
                            salle = "4";
                            NumSalle.Content = "Salle : " + salle + "/6";
                            ouvert = false;
                            break;
                        }
                        case "4":
                        {
                            salle = "5";
                            NumSalle.Content = "Salle : " + salle + "/6";
                            ouvert = false;
                            break;
                        }
                        case "5":
                        {
                            salle = "6";
                            NumSalle.Content = "Salle : " + salle + "/6";
                            ouvert = false;
                            break;
                        }
                        case "6":
                        {
                            salle = "7";
                            NumSalle.Content = "End";
                            ouvert = false;
                            break;
                        }
                    }
                }
            }
        }
    }
