using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.AccessControl;
using System.Security.Cryptography;
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
        Rect joueurBoiteCollision;
        Rect cubeBoiteCollision;
        Rect cubeBoiteCollision2;
        Rect bouleBoiteCollision;
        Rect boutonPBoiteCollision;
        Rect boutonP1BoiteCollision;
        Rect boutonP2BoiteCollision;
        Rect boutonBBoiteCollision;
        Rect boutonCBoiteCollision;
        Rect porteBoiteCollision;
        Rect murBoiteCollision;
        Rect murBoiteCollision2;
        Rect portail1BoiteCollision;
        Rect portail2BoiteCollision;
        Rect portail1S1BoiteCollision;
        Rect portail2S1BoiteCollision;
        Rect portail1S2BoiteCollision;
        Rect portail2S2BoiteCollision;
        Rect tourelleHBoiteCollision;
        Rect tourelleVBoiteCollision;

        ImageBrush joueurSprite = new ImageBrush();
        ImageBrush solSprite = new ImageBrush();
        ImageBrush cubeSprite = new ImageBrush();
        ImageBrush cubeSprite2 = new ImageBrush();
        ImageBrush bouleSprite = new ImageBrush();
        ImageBrush boutonSprite = new ImageBrush();
        ImageBrush boutonBSprite = new ImageBrush();
        ImageBrush boutonCSprite = new ImageBrush();
        ImageBrush porteSprite = new ImageBrush();
        ImageBrush murSprite = new ImageBrush();
        ImageBrush portail1Sprite = new ImageBrush();
        ImageBrush portail2Sprite = new ImageBrush();
        ImageBrush tourelleHSprite = new ImageBrush();
        ImageBrush tourelleVSprite = new ImageBrush();

        string salle = "3";
        int vitesse = 10;
        int vitessecube = 11;
        int vitesseballe = 20;

        int largeurmax = 1100;
        int hauteurmax = 800;

        bool Gauche, Droite, Haut, Bas, E = false;
        bool ouvert = false;

        List<Rectangle> objetSupprimer = new List<Rectangle>();

        public MainWindow()
        {
            InitializeComponent();
            Menu fenetreMenu = new Menu();
            fenetreMenu.ShowDialog();

            monCanvas.Focus();

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += BoucleJeux;
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(20);
            dispatcherTimer.Start();

            CacherObjet();

            joueurSprite.ImageSource = new BitmapImage(new Uri("images/joueur1.png", UriKind.RelativeOrAbsolute));
            joueur.Fill = joueurSprite;

            cubeSprite.ImageSource = new BitmapImage(new Uri("images/carrer1.png", UriKind.RelativeOrAbsolute));
            cube1.Fill = cubeSprite;
            cubeSprite2.ImageSource = new BitmapImage(new Uri("images/carrer1.png", UriKind.RelativeOrAbsolute));
            cube2.Fill = cubeSprite2;
            bouleSprite.ImageSource = new BitmapImage(new Uri("images/boule1.png", UriKind.RelativeOrAbsolute));
            boule.Fill = bouleSprite;

            porteSprite.ImageSource = new BitmapImage(new Uri("images/porte1.png", UriKind.RelativeOrAbsolute));
            porte.Fill = porteSprite;

            boutonSprite.ImageSource = new BitmapImage(new Uri("images/Bouton.png", UriKind.RelativeOrAbsolute));
            boutonP.Fill = boutonSprite;
            boutonP1.Fill = boutonSprite;
            boutonP2.Fill = boutonSprite;
            boutonBSprite.ImageSource = new BitmapImage(new Uri("images/boutonBoule.png", UriKind.RelativeOrAbsolute));
            boutonB.Fill = boutonBSprite;
            boutonCSprite.ImageSource = new BitmapImage(new Uri("images/boutonCube.png", UriKind.RelativeOrAbsolute));
            boutonC.Fill = boutonCSprite;

            solSprite.ImageSource = new BitmapImage(new Uri("images/sol.png", UriKind.RelativeOrAbsolute));
            sol.Fill = solSprite;

            murSprite.ImageSource = new BitmapImage(new Uri("images/mur.jpg", UriKind.RelativeOrAbsolute));
            mur.Fill = murSprite;
            mur2.Fill = murSprite;

            portail1Sprite.ImageSource = new BitmapImage(new Uri("images/Portail1.png", UriKind.RelativeOrAbsolute));
            portail1.Fill = portail1Sprite;
            portail1S1.Fill = portail1Sprite;
            portail1S2.Fill = portail1Sprite;

            portail2Sprite.ImageSource = new BitmapImage(new Uri("images/Portail2.png", UriKind.RelativeOrAbsolute));
            portail2.Fill = portail2Sprite;
            portail2S1.Fill = portail2Sprite;
            portail2S2.Fill = portail2Sprite;

            tourelleHSprite.ImageSource = new BitmapImage(new Uri("images/tourelle.png", UriKind.RelativeOrAbsolute));
            tourelleH.Fill = tourelleHSprite;
            tourelleVSprite.ImageSource = new BitmapImage(new Uri("images/tourelle3.png", UriKind.RelativeOrAbsolute));
            tourelleV.Fill = tourelleVSprite;

            NumSalle.Content = "Salle : " + salle + "/5";
            Creation_Niveaux();
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

        private void Creation_Niveaux()
        {
            switch (salle)
            {
                case "0":
                    {
                        //bouton
                        Canvas.SetTop(boutonP, 176);
                        Canvas.SetLeft(boutonP, 948);
                        boutonP.Visibility = Visibility.Visible;
                        //joueur
                        Canvas.SetTop(joueur, 633);
                        Canvas.SetLeft(joueur, 20);
                        //cube/boule
                        Canvas.SetTop(cube1, 392);
                        Canvas.SetLeft(cube1, 349);
                        cube1.Visibility = Visibility.Visible;
                        break;
                    }
                case "1":
                    {
                        //bouton
                        Canvas.SetTop(boutonP1, 369);
                        Canvas.SetLeft(boutonP1, 68);
                        boutonP1.Visibility = Visibility.Visible;

                        Canvas.SetTop(boutonP2, 350);
                        Canvas.SetLeft(boutonP2, 948);
                        boutonP2.Visibility = Visibility.Visible;
                        //joueur
                        Canvas.SetTop(joueur, 633);
                        Canvas.SetLeft(joueur, 20);
                        //cube/boule
                        Canvas.SetTop(cube1, 392);
                        Canvas.SetLeft(cube1, 349);
                        cube1.Visibility = Visibility.Visible;

                        Canvas.SetTop(cube2, 495);
                        Canvas.SetLeft(cube2, 781);
                        cube2.Visibility = Visibility.Visible;
                        //mur
                        Canvas.SetLeft(mur, 480);
                        mur.Visibility = Visibility.Visible;
                        //portail
                        Canvas.SetTop(portail1, 268);
                        Canvas.SetLeft(portail1, 406);
                        portail1.Visibility = Visibility.Visible;

                        Canvas.SetTop(portail2, 268);
                        Canvas.SetLeft(portail2, 589);
                        portail2.Visibility = Visibility.Visible;
                        break;

                    }
                case "2":
                    {
                        //bouton
                        Canvas.SetTop(boutonP1, 64);
                        Canvas.SetLeft(boutonP1, 861);
                        boutonP1.Visibility = Visibility.Visible;

                        Canvas.SetTop(boutonP2, 173);
                        Canvas.SetLeft(boutonP2, 861);
                        boutonP2.Visibility = Visibility.Visible;

                        Canvas.SetTop(boutonB, 24);
                        Canvas.SetLeft(boutonB, 410);
                        boutonB.Visibility = Visibility.Visible;

                        Canvas.SetTop(boutonC, 657);
                        Canvas.SetLeft(boutonC, 408);
                        boutonC.Visibility = Visibility.Visible;
                        //joueur
                        Canvas.SetTop(joueur, 633);
                        Canvas.SetLeft(joueur, 20);
                        //cube/boule
                        Canvas.SetTop(cube1, 74);
                        Canvas.SetLeft(cube1, 983);
                        cube1.Visibility = Visibility.Visible;

                        Canvas.SetTop(cube2, 588);
                        Canvas.SetLeft(cube2, 923);
                        cube2.Visibility = Visibility.Visible;

                        Canvas.SetTop(boule, 511);
                        Canvas.SetLeft(boule, 191);
                        boule.Visibility = Visibility.Visible;
                        //mur
                        Canvas.SetLeft(mur, 480);
                        mur.Visibility = Visibility.Visible;

                        Canvas.SetTop(mur2, 334);
                        Canvas.SetLeft(mur2, 600);
                        mur2.Visibility = Visibility.Visible;
                        //portail
                        Canvas.SetTop(portail1S1, 99);
                        Canvas.SetLeft(portail1S1, 408);

                        Canvas.SetTop(portail2S1, 99);
                        Canvas.SetLeft(portail2S1, 592);

                        Canvas.SetTop(portail1S2, 458);
                        Canvas.SetLeft(portail1S2, 410);

                        Canvas.SetTop(portail2S2, 458);
                        Canvas.SetLeft(portail2S2, 592);
                        break;
                    }
                case "3":
                    {
                        //bouton
                        Canvas.SetTop(boutonP, 176);
                        Canvas.SetLeft(boutonP, 948);

                        Canvas.SetTop(boutonP1, hauteurmax);
                        Canvas.SetLeft(boutonP1, largeurmax);

                        Canvas.SetTop(boutonP2, hauteurmax);
                        Canvas.SetLeft(boutonP2, largeurmax);

                        Canvas.SetTop(boutonB, hauteurmax);
                        Canvas.SetLeft(boutonB, largeurmax);

                        Canvas.SetTop(boutonC, hauteurmax);
                        Canvas.SetLeft(boutonC, largeurmax);
                        //joueur
                        Canvas.SetTop(joueur, 633);
                        Canvas.SetLeft(joueur, 20);
                        //cube/boule
                        Canvas.SetTop(cube1, 392);
                        Canvas.SetLeft(cube1, 349);

                        Canvas.SetTop(cube2, hauteurmax);
                        Canvas.SetLeft(cube2, largeurmax);

                        Canvas.SetTop(boule, hauteurmax);
                        Canvas.SetLeft(boule, largeurmax);
                        //mur
                        Canvas.SetTop(mur2, 342);
                        Canvas.SetLeft(mur2, 0);
                        mur2.Visibility = Visibility.Visible;
                        //portail
                        Canvas.SetTop(portail1, hauteurmax);
                        Canvas.SetLeft(portail1, largeurmax);

                        Canvas.SetTop(portail2, hauteurmax);
                        Canvas.SetLeft(portail2, largeurmax);

                        Canvas.SetTop(portail1S1, hauteurmax);
                        Canvas.SetLeft(portail1S1, largeurmax);

                        Canvas.SetTop(portail2S1, hauteurmax);
                        Canvas.SetLeft(portail2S1, largeurmax);

                        Canvas.SetTop(portail1S2, hauteurmax);
                        Canvas.SetLeft(portail1S2, largeurmax);

                        Canvas.SetTop(portail2S2, hauteurmax);
                        Canvas.SetLeft(portail2S2, largeurmax);
                        //tourelle
                        Canvas.SetTop(tourelleH, 10);
                        Canvas.SetLeft(tourelleH, 191);

                        Canvas.SetTop(tourelleV, 491);
                        Canvas.SetLeft(tourelleV, 983);
                        tourelleH.Visibility = Visibility.Visible;
                        tourelleV.Visibility = Visibility.Visible;
                        break;
                    }
                case "4":
                    {
                        //bouton
                        Canvas.SetTop(boutonP, 176);
                        Canvas.SetLeft(boutonP, 948);

                        Canvas.SetTop(boutonP1, hauteurmax);
                        Canvas.SetLeft(boutonP1, largeurmax);

                        Canvas.SetTop(boutonP2, hauteurmax);
                        Canvas.SetLeft(boutonP2, largeurmax);

                        Canvas.SetTop(boutonB, hauteurmax);
                        Canvas.SetLeft(boutonB, largeurmax);

                        Canvas.SetTop(boutonC, hauteurmax);
                        Canvas.SetLeft(boutonC, largeurmax);
                        //joueur
                        Canvas.SetTop(joueur, 633);
                        Canvas.SetLeft(joueur, 20);
                        //cube/boule
                        Canvas.SetTop(cube1, 392);
                        Canvas.SetLeft(cube1, 349);

                        Canvas.SetTop(cube2, hauteurmax);
                        Canvas.SetLeft(cube2, largeurmax);

                        Canvas.SetTop(boule, hauteurmax);
                        Canvas.SetLeft(boule, largeurmax);
                        //mur
                        Canvas.SetLeft(mur, largeurmax);

                        Canvas.SetTop(mur2, hauteurmax);
                        Canvas.SetLeft(mur2, largeurmax);
                        //portail
                        Canvas.SetTop(portail1, hauteurmax);
                        Canvas.SetLeft(portail1, largeurmax);

                        Canvas.SetTop(portail2, hauteurmax);
                        Canvas.SetLeft(portail2, largeurmax);

                        Canvas.SetTop(portail1S1, hauteurmax);
                        Canvas.SetLeft(portail1S1, largeurmax);

                        Canvas.SetTop(portail2S1, hauteurmax);
                        Canvas.SetLeft(portail2S1, largeurmax);

                        Canvas.SetTop(portail1S2, hauteurmax);
                        Canvas.SetLeft(portail1S2, largeurmax);

                        Canvas.SetTop(portail2S2, hauteurmax);
                        Canvas.SetLeft(portail2S2, largeurmax);
                        break;
                    }
                case "5":
                    {
                        //bouton
                        Canvas.SetTop(boutonP, 176);
                        Canvas.SetLeft(boutonP, 948);

                        Canvas.SetTop(boutonP1, hauteurmax);
                        Canvas.SetLeft(boutonP1, largeurmax);

                        Canvas.SetTop(boutonP2, hauteurmax);
                        Canvas.SetLeft(boutonP2, largeurmax);

                        Canvas.SetTop(boutonB, hauteurmax);
                        Canvas.SetLeft(boutonB, largeurmax);

                        Canvas.SetTop(boutonC, hauteurmax);
                        Canvas.SetLeft(boutonC, largeurmax);
                        //joueur
                        Canvas.SetTop(joueur, 633);
                        Canvas.SetLeft(joueur, 20);
                        //cube/boule
                        Canvas.SetTop(cube1, 392);
                        Canvas.SetLeft(cube1, 349);

                        Canvas.SetTop(cube2, hauteurmax);
                        Canvas.SetLeft(cube2, largeurmax);

                        Canvas.SetTop(boule, hauteurmax);
                        Canvas.SetLeft(boule, largeurmax);
                        //mur
                        Canvas.SetLeft(mur, largeurmax);

                        Canvas.SetTop(mur2, hauteurmax);
                        Canvas.SetLeft(mur2, largeurmax);
                        //portail
                        Canvas.SetTop(portail1, hauteurmax);
                        Canvas.SetLeft(portail1, largeurmax);

                        Canvas.SetTop(portail2, hauteurmax);
                        Canvas.SetLeft(portail2, largeurmax);

                        Canvas.SetTop(portail1S1, hauteurmax);
                        Canvas.SetLeft(portail1S1, largeurmax);

                        Canvas.SetTop(portail2S1, hauteurmax);
                        Canvas.SetLeft(portail2S1, largeurmax);

                        Canvas.SetTop(portail1S2, hauteurmax);
                        Canvas.SetLeft(portail1S2, largeurmax);

                        Canvas.SetTop(portail2S2, hauteurmax);
                        Canvas.SetLeft(portail2S2, largeurmax);
                        //tourelle
                        Canvas.SetTop(tourelleH, 69);
                        Canvas.SetLeft(tourelleV, 191);

                        Canvas.SetTop(tourelleH, 69);
                        Canvas.SetLeft(tourelleV, 338);
                        break;
                    }
                case "6":
                    {
                        //bouton
                        Canvas.SetTop(boutonP, 176);
                        Canvas.SetLeft(boutonP, 948);

                        Canvas.SetTop(boutonP1, hauteurmax);
                        Canvas.SetLeft(boutonP1, largeurmax);

                        Canvas.SetTop(boutonP2, hauteurmax);
                        Canvas.SetLeft(boutonP2, largeurmax);

                        Canvas.SetTop(boutonB, hauteurmax);
                        Canvas.SetLeft(boutonB, largeurmax);

                        Canvas.SetTop(boutonC, hauteurmax);
                        Canvas.SetLeft(boutonC, largeurmax);
                        //joueur
                        Canvas.SetTop(joueur, 633);
                        Canvas.SetLeft(joueur, 20);
                        //cube/boule
                        Canvas.SetTop(cube1, 392);
                        Canvas.SetLeft(cube1, 349);

                        Canvas.SetTop(cube2, hauteurmax);
                        Canvas.SetLeft(cube2, largeurmax);

                        Canvas.SetTop(boule, hauteurmax);
                        Canvas.SetLeft(boule, largeurmax);
                        //mur
                        Canvas.SetLeft(mur, largeurmax);

                        Canvas.SetTop(mur2, hauteurmax);
                        Canvas.SetLeft(mur2, largeurmax);
                        //portail
                        Canvas.SetTop(portail1, hauteurmax);
                        Canvas.SetLeft(portail1, largeurmax);

                        Canvas.SetTop(portail2, hauteurmax);
                        Canvas.SetLeft(portail2, largeurmax);

                        Canvas.SetTop(portail1S1, hauteurmax);
                        Canvas.SetLeft(portail1S1, largeurmax);

                        Canvas.SetTop(portail2S1, hauteurmax);
                        Canvas.SetLeft(portail2S1, largeurmax);

                        Canvas.SetTop(portail1S2, hauteurmax);
                        Canvas.SetLeft(portail1S2, largeurmax);

                        Canvas.SetTop(portail2S2, hauteurmax);
                        Canvas.SetLeft(portail2S2, largeurmax);
                        break;
                    }
            }
        }

        private void BoiteCollision()
        {
            joueurBoiteCollision = new Rect(Canvas.GetLeft(joueur), Canvas.GetTop(joueur), joueur.Width - 17, joueur.Height - 5);
            cubeBoiteCollision = new Rect(Canvas.GetLeft(cube1), Canvas.GetTop(cube1), cube1.Width, cube1.Height);
            cubeBoiteCollision2 = new Rect(Canvas.GetLeft(cube2), Canvas.GetTop(cube2), cube2.Width, cube2.Height);
            bouleBoiteCollision = new Rect(Canvas.GetLeft(boule), Canvas.GetTop(boule), boule.Width, boule.Height);
            boutonPBoiteCollision = new Rect(Canvas.GetLeft(boutonP), Canvas.GetTop(boutonP), boutonP.Width - 20, boutonP.Height - 20);
            boutonP1BoiteCollision = new Rect(Canvas.GetLeft(boutonP1), Canvas.GetTop(boutonP1), boutonP1.Width - 20, boutonP1.Height - 20);
            boutonP2BoiteCollision = new Rect(Canvas.GetLeft(boutonP2), Canvas.GetTop(boutonP2), boutonP2.Width - 20, boutonP2.Height - 20);
            boutonBBoiteCollision = new Rect(Canvas.GetLeft(boutonB), Canvas.GetTop(boutonB), boutonB.Width - 20, boutonB.Height - 20);
            boutonCBoiteCollision = new Rect(Canvas.GetLeft(boutonC), Canvas.GetTop(boutonC), boutonC.Width - 20, boutonC.Height - 20);
            porteBoiteCollision = new Rect(Canvas.GetLeft(porte), Canvas.GetTop(porte), porte.Width, porte.Height);
            murBoiteCollision = new Rect(Canvas.GetLeft(mur), Canvas.GetTop(mur), mur.Width, mur.Height);
            murBoiteCollision2 = new Rect(Canvas.GetLeft(mur2), Canvas.GetTop(mur2), mur2.Width, mur2.Height);
            portail1BoiteCollision = new Rect(Canvas.GetLeft(portail1), Canvas.GetTop(portail1), portail1.Width - 40, portail1.Height - 40);
            portail2BoiteCollision = new Rect(Canvas.GetLeft(portail2), Canvas.GetTop(portail2), portail2.Width - 40, portail2.Height - 40);
            portail1S1BoiteCollision = new Rect(Canvas.GetLeft(portail1S1), Canvas.GetTop(portail1S1), portail1S1.Width - 40, portail1S1.Height - 40);
            portail2S1BoiteCollision = new Rect(Canvas.GetLeft(portail2S1), Canvas.GetTop(portail2S1), portail2S1.Width - 40, portail2S1.Height - 40);
            portail1S2BoiteCollision = new Rect(Canvas.GetLeft(portail1S2), Canvas.GetTop(portail1S2), portail1S2.Width - 40, portail1S2.Height - 40);
            portail2S2BoiteCollision = new Rect(Canvas.GetLeft(portail2S2), Canvas.GetTop(portail2S2), portail2S2.Width - 40, portail2S2.Height - 40);
            tourelleHBoiteCollision = new Rect(Canvas.GetLeft(tourelleH)-(150), Canvas.GetTop(tourelleH)+150, tourelleH.Width + 300, tourelleH.Height + 300);
            tourelleVBoiteCollision = new Rect(Canvas.GetLeft(tourelleV) - (300), Canvas.GetTop(tourelleV) - (100), tourelleV.Width + 300, tourelleV.Height + 300);
        }

        private void CacherObjet()
        {
            boutonP.Visibility = Visibility.Hidden;
            boutonP1.Visibility = Visibility.Hidden;
            boutonP2.Visibility = Visibility.Hidden;
            boutonB.Visibility = Visibility.Hidden;
            boutonC.Visibility = Visibility.Hidden;
            cube1.Visibility = Visibility.Hidden;
            cube2.Visibility = Visibility.Hidden;
            boule.Visibility = Visibility.Hidden;
            mur.Visibility = Visibility.Hidden;
            mur2.Visibility = Visibility.Hidden;
            portail1.Visibility = Visibility.Hidden;
            portail2.Visibility = Visibility.Hidden;
            portail1S1.Visibility = Visibility.Hidden;
            portail2S1.Visibility = Visibility.Hidden;
            portail1S2.Visibility = Visibility.Hidden;
            portail2S2.Visibility = Visibility.Hidden;
            tourelleH.Visibility = Visibility.Hidden;
            tourelleV.Visibility = Visibility.Hidden;
        }

        private void CubeBouleCollision(Rectangle cube, Rect BoiteCollision)
        {
            if (Gauche && Canvas.GetLeft(joueur) > 50)
            {
                Canvas.SetLeft(cube, Canvas.GetLeft(cube) - (vitessecube));

                if (BoiteCollision.IntersectsWith(murBoiteCollision) && mur.Visibility == Visibility.Visible)
                {
                    Canvas.SetLeft(cube, Canvas.GetLeft(cube) + (vitessecube * 2));
                }
            }
            else if (Droite && Canvas.GetLeft(joueur) + cube.Width * 2.10 < Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(cube, Canvas.GetLeft(cube) + (vitessecube));

                if (BoiteCollision.IntersectsWith(murBoiteCollision) && mur.Visibility == Visibility.Visible)
                {
                    Canvas.SetLeft(cube, Canvas.GetLeft(cube) - (vitessecube * 2));
                }
            }
            else if (Haut && Canvas.GetTop(joueur) > 50)
            {
                Canvas.SetTop(cube, Canvas.GetTop(cube) - (vitessecube));

                if (BoiteCollision.IntersectsWith(murBoiteCollision2) && mur2.Visibility == Visibility.Visible)
                {
                    Canvas.SetTop(cube, Canvas.GetTop(cube) + (vitessecube * 2));
                }
            }
            else if (Bas && Canvas.GetTop(joueur) + cube.Height * 2.50 < Application.Current.MainWindow.Height)
            {
                Canvas.SetTop(cube, Canvas.GetTop(cube) + (vitessecube));

                if (BoiteCollision.IntersectsWith(murBoiteCollision2) && mur2.Visibility == Visibility.Visible)
                {
                    Canvas.SetTop(cube, Canvas.GetTop(cube) - (vitessecube * 2));
                }
            }
        }
        private void MurCollisionJoueur()
        {
            if (Gauche && Canvas.GetLeft(joueur) > 0)
            {
                joueurSprite.ImageSource = new BitmapImage(new Uri("images/joueur4.png", UriKind.RelativeOrAbsolute));
                joueur.Fill = joueurSprite;
                Canvas.SetLeft(joueur, Canvas.GetLeft(joueur) - (vitesse));
                if (joueurBoiteCollision.IntersectsWith(murBoiteCollision))
                {
                    Canvas.SetLeft(joueur, Canvas.GetLeft(joueur) + (vitesse) * 2);
                }
            }
            else if (Droite && Canvas.GetLeft(joueur) + joueur.Width * 1.5 < Application.Current.MainWindow.Width)
            {
                joueurSprite.ImageSource = new BitmapImage(new Uri("images/joueur2.png", UriKind.RelativeOrAbsolute));
                joueur.Fill = joueurSprite;
                Canvas.SetLeft(joueur, Canvas.GetLeft(joueur) + (vitesse));
                if (joueurBoiteCollision.IntersectsWith(murBoiteCollision))
                {
                    Canvas.SetLeft(joueur, Canvas.GetLeft(joueur) - (vitesse) * 2);
                }
            }
            else if (Haut && Canvas.GetTop(joueur) > 0)
            {
                joueurSprite.ImageSource = new BitmapImage(new Uri("images/joueur1.png", UriKind.RelativeOrAbsolute));
                joueur.Fill = joueurSprite;
                Canvas.SetTop(joueur, Canvas.GetTop(joueur) - (vitesse));
                if (joueurBoiteCollision.IntersectsWith(murBoiteCollision2))
                {
                    Canvas.SetTop(joueur, Canvas.GetTop(joueur) + (vitesse * 2));
                }
            }
            else if (Bas && Canvas.GetTop(joueur) + joueur.Height * 1.5 < Application.Current.MainWindow.Height)
            {
                joueurSprite.ImageSource = new BitmapImage(new Uri("images/joueur3.png", UriKind.RelativeOrAbsolute));
                joueur.Fill = joueurSprite;
                Canvas.SetTop(joueur, Canvas.GetTop(joueur) + (vitesse));
                if (joueurBoiteCollision.IntersectsWith(murBoiteCollision2))
                {
                    Canvas.SetTop(joueur, Canvas.GetTop(joueur) - (vitesse * 2));
                }
            }
        }
        private void JoueurMouvement()
        {
            if (joueurBoiteCollision.IntersectsWith(cubeBoiteCollision) && E && cube1.Visibility == Visibility.Visible)
            {
                //faire bouger le cube1
                CubeBouleCollision(cube1, cubeBoiteCollision);
            }
            else if (joueurBoiteCollision.IntersectsWith(cubeBoiteCollision2) && E && cube2.Visibility == Visibility.Visible)
            {
                //faire bouger le cube
                CubeBouleCollision(cube2, cubeBoiteCollision2);
            }
            else if (joueurBoiteCollision.IntersectsWith(bouleBoiteCollision) && E && boule.Visibility == Visibility.Visible)
            {
                //faire bouger la boule
                CubeBouleCollision(boule, bouleBoiteCollision);
            }
            else if (joueurBoiteCollision.IntersectsWith(murBoiteCollision) && mur.Visibility == Visibility.Visible || joueurBoiteCollision.IntersectsWith(murBoiteCollision2) && mur2.Visibility == Visibility.Visible)
            {//contre mur bloquer personnage
                MurCollisionJoueur();
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
        private void CollisionBoutonPorte()
        {
            if (cubeBoiteCollision.IntersectsWith(boutonP1BoiteCollision) && cube1.Visibility == Visibility.Visible && boutonP1.Visibility == Visibility.Visible || cubeBoiteCollision.IntersectsWith(boutonP2BoiteCollision) && cube1.Visibility == Visibility.Visible && boutonP2.Visibility == Visibility.Visible)
            {
                cubeSprite.ImageSource = new BitmapImage(new Uri("images/carrer2.png", UriKind.RelativeOrAbsolute));
                cube1.Fill = cubeSprite;
            }
            if (cubeBoiteCollision2.IntersectsWith(boutonP1BoiteCollision) && cube2.Visibility == Visibility.Visible && boutonP1.Visibility == Visibility.Visible || cubeBoiteCollision2.IntersectsWith(boutonP2BoiteCollision) && cube2.Visibility == Visibility.Visible && boutonP2.Visibility == Visibility.Visible)
            {
                cubeSprite2.ImageSource = new BitmapImage(new Uri("images/carrer2.png", UriKind.RelativeOrAbsolute));
                cube2.Fill = cubeSprite2;
            }
            if (joueurBoiteCollision.IntersectsWith(boutonPBoiteCollision) && boutonP.Visibility == Visibility.Visible)
            {
                porteSprite.ImageSource = new BitmapImage(new Uri("images/porte2.png", UriKind.RelativeOrAbsolute));
                porte.Fill = porteSprite;
                ouvert = true;
            }
            else if (joueurBoiteCollision.IntersectsWith(boutonP1BoiteCollision) && cubeBoiteCollision.IntersectsWith(boutonP2BoiteCollision) && boutonP1.Visibility == Visibility.Visible && boutonP2.Visibility == Visibility.Visible && cube1.Visibility == Visibility.Visible || joueurBoiteCollision.IntersectsWith(boutonP2BoiteCollision) && cubeBoiteCollision.IntersectsWith(boutonP1BoiteCollision) && boutonP1.Visibility == Visibility.Visible && boutonP2.Visibility == Visibility.Visible && cube1.Visibility == Visibility.Visible || joueurBoiteCollision.IntersectsWith(boutonP1BoiteCollision) && cubeBoiteCollision2.IntersectsWith(boutonP2BoiteCollision) && boutonP1.Visibility == Visibility.Visible && boutonP2.Visibility == Visibility.Visible && cube2.Visibility == Visibility.Visible || joueurBoiteCollision.IntersectsWith(boutonP2BoiteCollision) && cubeBoiteCollision2.IntersectsWith(boutonP1BoiteCollision) && boutonP1.Visibility == Visibility.Visible && boutonP2.Visibility == Visibility.Visible && cube2.Visibility == Visibility.Visible)
            {
                porteSprite.ImageSource = new BitmapImage(new Uri("images/porte2.png", UriKind.RelativeOrAbsolute));
                porte.Fill = porteSprite;
                ouvert = true;
            }
            else if (cubeBoiteCollision.IntersectsWith(boutonPBoiteCollision) && boutonP.Visibility == Visibility.Visible && cube1.Visibility == Visibility.Visible)
            {
                cubeSprite.ImageSource = new BitmapImage(new Uri("images/carrer2.png", UriKind.RelativeOrAbsolute));
                cube1.Fill = cubeSprite;
                porteSprite.ImageSource = new BitmapImage(new Uri("images/porte2.png", UriKind.RelativeOrAbsolute));
                porte.Fill = porteSprite;
                ouvert = true;
            }
            else if (cubeBoiteCollision2.IntersectsWith(boutonPBoiteCollision) && boutonP.Visibility == Visibility.Visible && cube2.Visibility == Visibility.Visible)
            {
                cubeSprite2.ImageSource = new BitmapImage(new Uri("images/carrer2.png", UriKind.RelativeOrAbsolute));
                cube2.Fill = cubeSprite2;
                porteSprite.ImageSource = new BitmapImage(new Uri("images/porte2.png", UriKind.RelativeOrAbsolute));
                porte.Fill = porteSprite;
                ouvert = true;
            }
            else if (cubeBoiteCollision.IntersectsWith(boutonP1BoiteCollision) && cubeBoiteCollision2.IntersectsWith(boutonP2BoiteCollision) && boutonP2.Visibility == Visibility.Visible && cube1.Visibility == Visibility.Visible && boutonP1.Visibility == Visibility.Visible && cube2.Visibility == Visibility.Visible || cubeBoiteCollision2.IntersectsWith(boutonP1BoiteCollision) && cubeBoiteCollision.IntersectsWith(boutonP2BoiteCollision) && boutonP2.Visibility == Visibility.Visible && cube1.Visibility == Visibility.Visible && boutonP1.Visibility == Visibility.Visible && cube2.Visibility == Visibility.Visible)
            {
                porteSprite.ImageSource = new BitmapImage(new Uri("images/porte2.png", UriKind.RelativeOrAbsolute));
                porte.Fill = porteSprite;
                ouvert = true;
            }
            else
            {
                cubeSprite.ImageSource = new BitmapImage(new Uri("images/carrer1.png", UriKind.RelativeOrAbsolute));
                cube1.Fill = cubeSprite;
                cubeSprite2.ImageSource = new BitmapImage(new Uri("images/carrer1.png", UriKind.RelativeOrAbsolute));
                cube2.Fill = cubeSprite2;
                porteSprite.ImageSource = new BitmapImage(new Uri("images/porte1.png", UriKind.RelativeOrAbsolute));
                porte.Fill = porteSprite;
                ouvert = false;
            }
        }

        private void CollisionCubeBoutonC()
        {
            if (cubeBoiteCollision.IntersectsWith(boutonCBoiteCollision) && boutonC.Visibility == Visibility.Visible && cube1.Visibility == Visibility.Visible)
            {
                cubeSprite.ImageSource = new BitmapImage(new Uri("images/carrer2.png", UriKind.RelativeOrAbsolute));
                cube1.Fill = cubeSprite;
                portail1S2.Visibility = Visibility.Visible;
                portail2S2.Visibility = Visibility.Visible;
            }
            else if (cubeBoiteCollision2.IntersectsWith(boutonCBoiteCollision) && boutonC.Visibility == Visibility.Visible && cube2.Visibility == Visibility.Visible)
            {
                cubeSprite2.ImageSource = new BitmapImage(new Uri("images/carrer2.png", UriKind.RelativeOrAbsolute));
                cube2.Fill = cubeSprite2;
                portail1S2.Visibility = Visibility.Visible;
                portail2S2.Visibility = Visibility.Visible;
            }
            else
            {
                portail1S2.Visibility = Visibility.Hidden;
                portail2S2.Visibility = Visibility.Hidden;

            }
        }

        private void CollisionBoutonB()
        {
            if (bouleBoiteCollision.IntersectsWith(boutonBBoiteCollision) && boutonB.Visibility == Visibility.Visible && boule.Visibility == Visibility.Visible)
            {
                bouleSprite.ImageSource = new BitmapImage(new Uri("images/boule2.png", UriKind.RelativeOrAbsolute));
                boule.Fill = bouleSprite;
                portail1S1.Visibility = Visibility.Visible;
                portail2S1.Visibility = Visibility.Visible;
            }
            else
            {
                bouleSprite.ImageSource = new BitmapImage(new Uri("images/boule1.png", UriKind.RelativeOrAbsolute));
                boule.Fill = bouleSprite;
                portail1S1.Visibility = Visibility.Hidden;
                portail2S1.Visibility = Visibility.Hidden;
            }

        }
        private void CollisionJoueurPorte()
        {
            if (ouvert && joueurBoiteCollision.IntersectsWith(porteBoiteCollision))
            {
                switch (salle)
                {
                    case "0":
                        {
                            salle = "1";
                            NumSalle.Content = "Salle : " + salle + "/5";
                            ouvert = false;
                            CacherObjet();
                            break;
                        }
                    case "1":
                        {
                            salle = "2";
                            NumSalle.Content = "Salle : " + salle + "/5";
                            ouvert = false;
                            CacherObjet();
                            break;
                        }
                    case "2":
                        {
                            salle = "3";
                            NumSalle.Content = "Salle : " + salle + "/5";
                            ouvert = false;
                            CacherObjet();
                            break;
                        }
                    case "3":
                        {
                            salle = "4";
                            NumSalle.Content = "Salle : " + salle + "/5";
                            ouvert = false;
                            CacherObjet();
                            break;
                        }
                    case "4":
                        {
                            salle = "5";
                            NumSalle.Content = "Salle : " + salle + "/6";
                            ouvert = false;
                            CacherObjet();
                            break;
                        }
                    case "5":
                        {
                            salle = "6";
                            NumSalle.Content = "Salle : " + salle + "/5";
                            ouvert = false;
                            CacherObjet();
                            break;
                        }
                    case "6":
                        {
                            salle = "7";
                            NumSalle.Content = "End";
                            ouvert = false;
                            CacherObjet();
                            break;
                        }
                }
                Creation_Niveaux();
            }
        }
        private void CollisionPortail(Rectangle objet, Rect objetBoiteCollision)
        {
            if (objetBoiteCollision.IntersectsWith(portail1BoiteCollision) && portail1.Visibility == Visibility.Visible)
            {
                Canvas.SetLeft(objet, Canvas.GetLeft(portail2) + (portail2.Height));
            }
            else if (objetBoiteCollision.IntersectsWith(portail2BoiteCollision) && portail2.Visibility == Visibility.Visible)
            {
                Canvas.SetLeft(objet, Canvas.GetLeft(portail1) - (portail1.Height));
            }
            else if (objetBoiteCollision.IntersectsWith(portail1S1BoiteCollision) && portail1S1.Visibility == Visibility.Visible)
            {
                Canvas.SetLeft(objet, Canvas.GetLeft(portail2S1) + (portail2.Height));
            }
            else if (objetBoiteCollision.IntersectsWith(portail2S1BoiteCollision) && portail2S1.Visibility == Visibility.Visible)
            {
                Canvas.SetLeft(objet, Canvas.GetLeft(portail1S1) - (portail1.Height));
            }
            else if (objetBoiteCollision.IntersectsWith(portail1S2BoiteCollision) && portail1S2.Visibility == Visibility.Visible)
            {
                Canvas.SetLeft(objet, Canvas.GetLeft(portail2S2) + (portail2.Height));
            }
            else if (objetBoiteCollision.IntersectsWith(portail2S2BoiteCollision) && portail2S2.Visibility == Visibility.Visible)
            {
                Canvas.SetLeft(objet, Canvas.GetLeft(portail1S2) - (portail1.Height));
            }
        }
        private void CreerBalleH(double x, double y)
        {
            Rectangle balleH = new Rectangle
            {
                Tag = "balleH",
                Height = 40,
                Width = 15,
                Fill = Brushes.Yellow,
                Stroke = Brushes.Black,
                StrokeThickness = 5
            };
            Canvas.SetLeft(balleH, x);
            Canvas.SetTop(balleH, y);
            monCanvas.Children.Add(balleH);
        }
        private void CreerBalleV(double x, double y)
        {
            Rectangle balleV = new Rectangle
            {
                Tag = "balleV",
                Height = 15,
                Width = 40,
                Fill = Brushes.Yellow,
                Stroke = Brushes.Black,
                StrokeThickness = 5
            };
            Canvas.SetLeft(balleV, x);
            Canvas.SetTop(balleV, y);
            monCanvas.Children.Add(balleV);
        }

        private void BoucleJeux(object sender, EventArgs e)
        {
            //création BoiteCollision
            BoiteCollision();

            //collision joueur cube
            JoueurMouvement();

            //collision joueur boutonP / cube boutonP
            CollisionBoutonPorte();
            //collision cube boutonC
            CollisionCubeBoutonC();
            //collision boule boutonB
            CollisionBoutonB();
            //collision joueur porte
            CollisionJoueurPorte();
            //collision joueur portail
            CollisionPortail(joueur, joueurBoiteCollision);
            CollisionPortail(cube1, cubeBoiteCollision);
            CollisionPortail(cube2, cubeBoiteCollision2);
            CollisionPortail(boule, bouleBoiteCollision);
            //collision joueur tourelle
            if (joueurBoiteCollision.IntersectsWith(tourelleHBoiteCollision))
            {
                tourelleHSprite.ImageSource = new BitmapImage(new Uri("images/tourelle2.png", UriKind.RelativeOrAbsolute));
                tourelleH.Fill = tourelleHSprite;
                CreerBalleH(Canvas.GetLeft(joueur), Canvas.GetTop(tourelleH));
                foreach (var x in monCanvas.Children.OfType<Rectangle>())
                {
                    if (x is Rectangle && (string)x.Tag == "balleH")
                    {
                        Canvas.SetTop(x, Canvas.GetTop(x) + (vitesseballe));
                        if (Canvas.GetTop(x) < 410)
                        {
                            objetSupprimer.Add(x);
                        }
                        Rect balletourelle = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    }
                }

            }
            else if (joueurBoiteCollision.IntersectsWith(tourelleVBoiteCollision))
            {
                tourelleVSprite.ImageSource = new BitmapImage(new Uri("images/tourelle4.png", UriKind.RelativeOrAbsolute));
                tourelleV.Fill = tourelleVSprite;
                CreerBalleV(Canvas.GetLeft(tourelleV), Canvas.GetTop(joueur));
                foreach (var x in monCanvas.Children.OfType<Rectangle>())
                {
                    if (x is  Rectangle && (string)x.Tag == "balleV")
                    {
                        Canvas.SetLeft(x, Canvas.GetLeft(x) - (vitesseballe));
                        if (Canvas.GetLeft(x) < 480)
                        {
                            objetSupprimer.Add(x);
                        }
                        Rect balletourelle = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    }
                }
            }
            else
            {
                tourelleHSprite.ImageSource = new BitmapImage(new Uri("images/tourelle.png", UriKind.RelativeOrAbsolute));
                tourelleH.Fill = tourelleHSprite;
                tourelleVSprite.ImageSource = new BitmapImage(new Uri("images/tourelle3.png", UriKind.RelativeOrAbsolute));
                tourelleV.Fill = tourelleVSprite;
            }
            foreach (Rectangle y in objetSupprimer)
            {
                monCanvas.Children.Remove(y);
            }
        }
    }
}
