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
        Rect cubeHitBox2;
        Rect bouleHitBox;
        Rect boutonPHitBox;
        Rect boutonP1HitBox;
        Rect boutonP2HitBox;
        Rect boutonBHitBox;
        Rect boutonCHitBox;
        Rect porteHitBox;
        Rect murHitBox;
        Rect murHitBox2;
        Rect portail1HitBox;
        Rect portail2HitBox;
        Rect portail1S1HitBox;
        Rect portail2S1HitBox;
        Rect portail1S2HitBox;
        Rect portail2S2HitBox;

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

        string salle = "0";
        int vitesse = 10;
        int vitessecube = 11;

        int largeurmax = 1100;
        int hauteurmax = 800;

        bool Gauche, Droite, Haut, Bas, E = false;
        bool ouvert = false;

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
                case "1":
                    {
                        //bouton
                        Canvas.SetTop(boutonP, hauteurmax);
                        Canvas.SetLeft(boutonP, largeurmax);

                        Canvas.SetTop(boutonP1, 369);
                        Canvas.SetLeft(boutonP1, 68);

                        Canvas.SetTop(boutonP2, 350);
                        Canvas.SetLeft(boutonP2, 948);

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

                        Canvas.SetTop(cube2, 495);
                        Canvas.SetLeft(cube2, 781);

                        Canvas.SetTop(boule, hauteurmax);
                        Canvas.SetLeft(boule, largeurmax);
                        //mur
                        Canvas.SetLeft(mur, 480);

                        Canvas.SetTop(mur2, hauteurmax);
                        Canvas.SetLeft(mur2, largeurmax);
                        //portail
                        Canvas.SetTop(portail1, 268);
                        Canvas.SetLeft(portail1, 406);

                        Canvas.SetTop(portail2, 268);
                        Canvas.SetLeft(portail2, 589);

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
                case "2":
                    {
                        //bouton
                        Canvas.SetTop(boutonP, hauteurmax);
                        Canvas.SetLeft(boutonP, largeurmax);

                        Canvas.SetTop(boutonP1, 64);
                        Canvas.SetLeft(boutonP1, 861);

                        Canvas.SetTop(boutonP2, 173);
                        Canvas.SetLeft(boutonP2, 861);

                        Canvas.SetTop(boutonB, 24);
                        Canvas.SetLeft(boutonB, 410);

                        Canvas.SetTop(boutonC, 657);
                        Canvas.SetLeft(boutonC, 408);
                        //joueur
                        Canvas.SetTop(joueur, 633);
                        Canvas.SetLeft(joueur, 20);
                        //cube/boule
                        Canvas.SetTop(cube1, 74);
                        Canvas.SetLeft(cube1, 983);

                        Canvas.SetTop(cube2, 588);
                        Canvas.SetLeft(cube2, 923);

                        Canvas.SetTop(boule, 511);
                        Canvas.SetLeft(boule, 191);
                        //mur
                        Canvas.SetLeft(mur, 480);

                        Canvas.SetTop(mur2, 334);
                        Canvas.SetLeft(mur2, 600);
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

        private void BoucleJeux(object sender, EventArgs e)
        {


            //création Hitbox
            joueurHitBox = new Rect(Canvas.GetLeft(joueur), Canvas.GetTop(joueur), joueur.Width - 17, joueur.Height - 5);
            cubeHitBox = new Rect(Canvas.GetLeft(cube1), Canvas.GetTop(cube1), cube1.Width, cube1.Height);
            cubeHitBox2 = new Rect(Canvas.GetLeft(cube2), Canvas.GetTop(cube2), cube2.Width, cube2.Height);
            bouleHitBox = new Rect(Canvas.GetLeft(boule), Canvas.GetTop(boule), boule.Width, boule.Height);
            boutonPHitBox = new Rect(Canvas.GetLeft(boutonP), Canvas.GetTop(boutonP), boutonP.Width - 20, boutonP.Height - 20);
            boutonP1HitBox = new Rect(Canvas.GetLeft(boutonP1), Canvas.GetTop(boutonP1), boutonP1.Width - 20, boutonP1.Height - 20);
            boutonP2HitBox = new Rect(Canvas.GetLeft(boutonP2), Canvas.GetTop(boutonP2), boutonP2.Width - 20, boutonP2.Height - 20);
            boutonBHitBox = new Rect(Canvas.GetLeft(boutonB), Canvas.GetTop(boutonB), boutonB.Width - 20, boutonB.Height - 20);
            boutonCHitBox = new Rect(Canvas.GetLeft(boutonC), Canvas.GetTop(boutonC), boutonC.Width - 20, boutonC.Height - 20);
            porteHitBox = new Rect(Canvas.GetLeft(porte), Canvas.GetTop(porte), porte.Width, porte.Height);
            murHitBox = new Rect(Canvas.GetLeft(mur), Canvas.GetTop(mur), mur.Width, mur.Height);
            murHitBox2 = new Rect(Canvas.GetLeft(mur2), Canvas.GetTop(mur2), mur2.Width, mur2.Height);
            portail1HitBox = new Rect(Canvas.GetLeft(portail1), Canvas.GetTop(portail1), portail1.Width-40, portail1.Height-40);
            portail2HitBox = new Rect(Canvas.GetLeft(portail2), Canvas.GetTop(portail2), portail2.Width-40, portail2.Height-40);
            portail1S1HitBox = new Rect(Canvas.GetLeft(portail1S1), Canvas.GetTop(portail1S1), portail1S1.Width - 40, portail1S1.Height - 40);
            portail2S1HitBox = new Rect(Canvas.GetLeft(portail2S1), Canvas.GetTop(portail2S1), portail2S1.Width - 40, portail2S1.Height - 40);
            portail1S2HitBox = new Rect(Canvas.GetLeft(portail1S2), Canvas.GetTop(portail1S2), portail1S2.Width - 40, portail1S2.Height - 40);
            portail2S2HitBox = new Rect(Canvas.GetLeft(portail2S2), Canvas.GetTop(portail2S2), portail2S2.Width - 40, portail2S2.Height - 40);

            //collision joueur cube
            if (joueurHitBox.IntersectsWith(cubeHitBox) && E)
            {
                //faire bouger le cube
                if (Gauche && Canvas.GetLeft(joueur) > 50)
                {
                    Canvas.SetLeft(cube1, Canvas.GetLeft(cube1) - (vitessecube));

                    if (cubeHitBox.IntersectsWith(murHitBox))
                    {
                        Canvas.SetLeft(cube1, Canvas.GetLeft(cube1) + (vitessecube*2));
                    }
                }
                else if (Droite && Canvas.GetLeft(joueur) + cube1.Width * 2.10 < Application.Current.MainWindow.Width)
                {
                    Canvas.SetLeft(cube1, Canvas.GetLeft(cube1) + (vitessecube));

                    if (cubeHitBox.IntersectsWith(murHitBox))
                    {
                        Canvas.SetLeft(cube1, Canvas.GetLeft(cube1) - (vitessecube*2));
                    }
                }
                else if (Haut && Canvas.GetTop(joueur) > 50)
                {
                    Canvas.SetTop(cube1, Canvas.GetTop(cube1) - (vitessecube));

                    if (cubeHitBox.IntersectsWith(murHitBox2))
                    {
                        Canvas.SetTop(cube1, Canvas.GetTop(cube1) + (vitessecube*2));
                    }
                }
                else if (Bas && Canvas.GetTop(joueur) + cube1.Height * 2.50 < Application.Current.MainWindow.Height)
                {
                    Canvas.SetTop(cube1, Canvas.GetTop(cube1) + (vitessecube));

                    if (cubeHitBox.IntersectsWith(murHitBox2))
                    {
                        Canvas.SetTop(cube1, Canvas.GetTop(cube1) - (vitessecube*2));
                    }
                }
            }
            else if (joueurHitBox.IntersectsWith(cubeHitBox2) && E)
            {
                //faire bouger le cube
                if (Gauche && Canvas.GetLeft(joueur) > 50)
                {
                    Canvas.SetLeft(cube2, Canvas.GetLeft(cube2) - (vitessecube));

                    if (cubeHitBox2.IntersectsWith(murHitBox))
                    {
                        Canvas.SetLeft(cube2, Canvas.GetLeft(cube2) + (vitessecube * 2));
                    }
                }
                else if (Droite && Canvas.GetLeft(joueur) + cube2.Width * 2.10 < Application.Current.MainWindow.Width)
                {
                    Canvas.SetLeft(cube2, Canvas.GetLeft(cube2) + (vitessecube));

                    if (cubeHitBox2.IntersectsWith(murHitBox))
                    {
                        Canvas.SetLeft(cube2, Canvas.GetLeft(cube2) - (vitessecube*2));
                    }
                }
                else if (Haut && Canvas.GetTop(joueur) > 50)
                {
                    Canvas.SetTop(cube2, Canvas.GetTop(cube2) - (vitessecube));

                    if (cubeHitBox2.IntersectsWith(murHitBox2))
                    {
                        Canvas.SetTop(cube2, Canvas.GetTop(cube2) + (vitessecube * 2));
                    }
                }
                else if (Bas && Canvas.GetTop(joueur) + cube2.Height * 2.50 < Application.Current.MainWindow.Height)
                {
                    Canvas.SetTop(cube2, Canvas.GetTop(cube2) + (vitessecube));

                    if (cubeHitBox2.IntersectsWith(murHitBox2))
                    {
                        Canvas.SetTop(cube2, Canvas.GetTop(cube2) - (vitessecube * 2));
                    }
                }
            }
            else if (joueurHitBox.IntersectsWith(bouleHitBox) && E)
            {
                //faire bouger la boule
                if (Gauche && Canvas.GetLeft(joueur) > 50)
                {
                    Canvas.SetLeft(cube2, Canvas.GetLeft(boule) - (vitessecube));

                    if (bouleHitBox.IntersectsWith(murHitBox))
                    {
                        Canvas.SetLeft(boule, Canvas.GetLeft(boule) + (vitessecube * 2));
                    }
                }
                else if (Droite && Canvas.GetLeft(joueur) + boule.Width * 2.10 < Application.Current.MainWindow.Width)
                {
                    Canvas.SetLeft(boule, Canvas.GetLeft(boule) + (vitessecube));

                    if (bouleHitBox.IntersectsWith(murHitBox))
                    {
                        Canvas.SetLeft(boule, Canvas.GetLeft(boule) - (vitessecube * 2));
                    }
                }
                else if (Haut && Canvas.GetTop(joueur) > 50)
                {
                    Canvas.SetTop(boule, Canvas.GetTop(boule) - (vitessecube));

                    if (bouleHitBox.IntersectsWith(murHitBox2))
                    {
                        Canvas.SetTop(boule, Canvas.GetTop(boule) + (vitessecube * 2));
                    }
                }
                else if (Bas && Canvas.GetTop(joueur) + boule.Height * 2.50 < Application.Current.MainWindow.Height)
                {
                    Canvas.SetTop(boule, Canvas.GetTop(boule) + (vitessecube));

                    if (bouleHitBox.IntersectsWith(murHitBox2))
                    {
                        Canvas.SetTop(boule, Canvas.GetTop(boule) - (vitessecube * 2));
                    }
                }
            }
            /*else if (joueurHitBox.IntersectsWith(cubeHitBox))
            {//contre cube sans avoir appuier sur e, bloquer le personnage
                if (Gauche && Canvas.GetLeft(joueur) > 0)
                {
                    joueurSprite.ImageSource = new BitmapImage(new Uri("images/joueur4.png", UriKind.RelativeOrAbsolute));
                    joueur.Fill = joueurSprite;
                    Canvas.SetLeft(joueur, Canvas.GetLeft(joueur) - (vitesse));

                    if (joueurHitBox.IntersectsWith(cubeHitBox))
                    {
                        Canvas.SetLeft(joueur, Canvas.GetLeft(joueur) + (vitesse * 2));
                    }
                }
                else if (Droite && Canvas.GetLeft(joueur) + joueur.Width * 1.5 < Application.Current.MainWindow.Width)
                {
                    joueurSprite.ImageSource = new BitmapImage(new Uri("images/joueur2.png", UriKind.RelativeOrAbsolute));
                    joueur.Fill = joueurSprite;
                    Canvas.SetLeft(joueur, Canvas.GetLeft(joueur) + (vitesse));

                    if (joueurHitBox.IntersectsWith(cubeHitBox))
                    {
                        Canvas.SetLeft(joueur, Canvas.GetLeft(joueur) - (vitesse * 2));
                    }
                }
                else if (Haut && Canvas.GetTop(joueur) > 0)
                {
                    joueurSprite.ImageSource = new BitmapImage(new Uri("images/joueur1.png", UriKind.RelativeOrAbsolute));
                    joueur.Fill = joueurSprite;
                    Canvas.SetTop(joueur, Canvas.GetTop(joueur) - (vitesse));

                    if (joueurHitBox.IntersectsWith(cubeHitBox))
                    {
                        Canvas.SetTop(joueur, Canvas.GetTop(joueur) + (vitesse * 2));
                    }
                }
                else if (Bas && Canvas.GetTop(joueur) + joueur.Height * 1.5 < Application.Current.MainWindow.Height)
                {
                    joueurSprite.ImageSource = new BitmapImage(new Uri("images/joueur3.png", UriKind.RelativeOrAbsolute));
                    joueur.Fill = joueurSprite;
                    Canvas.SetTop(joueur, Canvas.GetTop(joueur) + (vitesse));

                    if (joueurHitBox.IntersectsWith(cubeHitBox))
                    {
                        Canvas.SetTop(joueur, Canvas.GetTop(joueur) - (vitesse * 2));
                    }
                }
            }
            else if (joueurHitBox.IntersectsWith(cubeHitBox2))
            {//contre cube sans avoir appuier sur e, bloquer le personnage
                if (Gauche && Canvas.GetLeft(joueur) > 0)
                {
                    joueurSprite.ImageSource = new BitmapImage(new Uri("images/joueur4.png", UriKind.RelativeOrAbsolute));
                    joueur.Fill = joueurSprite;
                    Canvas.SetLeft(joueur, Canvas.GetLeft(joueur) - (vitesse));

                    if (joueurHitBox.IntersectsWith(cubeHitBox2))
                    {
                        Canvas.SetLeft(joueur, Canvas.GetLeft(joueur) + (vitesse * 2));
                    }
                }
                else if (Droite && Canvas.GetLeft(joueur) + joueur.Width * 1.5 < Application.Current.MainWindow.Width)
                {
                    joueurSprite.ImageSource = new BitmapImage(new Uri("images/joueur2.png", UriKind.RelativeOrAbsolute));
                    joueur.Fill = joueurSprite;
                    Canvas.SetLeft(joueur, Canvas.GetLeft(joueur) + (vitesse));

                    if (joueurHitBox.IntersectsWith(cubeHitBox2))
                    {
                        Canvas.SetLeft(joueur, Canvas.GetLeft(joueur) - (vitesse * 2));
                    }
                }
                else if (Haut && Canvas.GetTop(joueur) > 0)
                {
                    joueurSprite.ImageSource = new BitmapImage(new Uri("images/joueur1.png", UriKind.RelativeOrAbsolute));
                    joueur.Fill = joueurSprite;
                    Canvas.SetTop(joueur, Canvas.GetTop(joueur) - (vitesse));

                    if (joueurHitBox.IntersectsWith(cubeHitBox2))
                    {
                        Canvas.SetTop(joueur, Canvas.GetTop(joueur) + (vitesse * 2));
                    }
                }
                else if (Bas && Canvas.GetTop(joueur) + joueur.Height * 1.5 < Application.Current.MainWindow.Height)
                {
                    joueurSprite.ImageSource = new BitmapImage(new Uri("images/joueur3.png", UriKind.RelativeOrAbsolute));
                    joueur.Fill = joueurSprite;
                    Canvas.SetTop(joueur, Canvas.GetTop(joueur) + (vitesse));

                    if (joueurHitBox.IntersectsWith(cubeHitBox2))
                    {
                        Canvas.SetTop(joueur, Canvas.GetTop(joueur) - (vitesse * 2));
                    }
                }
            }*/
            else if (joueurHitBox.IntersectsWith(murHitBox) || joueurHitBox.IntersectsWith(murHitBox2))
            {//contre mur bloquer personnage
                if (Gauche && Canvas.GetLeft(joueur) > 0)
                {
                    joueurSprite.ImageSource = new BitmapImage(new Uri("images/joueur4.png", UriKind.RelativeOrAbsolute));
                    joueur.Fill = joueurSprite;
                    Canvas.SetLeft(joueur, Canvas.GetLeft(joueur) - (vitesse));
                    if (joueurHitBox.IntersectsWith(murHitBox))
                    {
                        Canvas.SetLeft(joueur, Canvas.GetLeft(joueur) + (vitesse) * 2);
                    }
                }
                else if (Droite && Canvas.GetLeft(joueur) + joueur.Width * 1.5 < Application.Current.MainWindow.Width)
                {
                    joueurSprite.ImageSource = new BitmapImage(new Uri("images/joueur2.png", UriKind.RelativeOrAbsolute));
                    joueur.Fill = joueurSprite;
                    Canvas.SetLeft(joueur, Canvas.GetLeft(joueur) + (vitesse));
                    if (joueurHitBox.IntersectsWith(murHitBox))
                    {
                        Canvas.SetLeft(joueur, Canvas.GetLeft(joueur) - (vitesse) * 2);
                    }
                }
                else if (Haut && Canvas.GetTop(joueur) > 0)
                {
                    joueurSprite.ImageSource = new BitmapImage(new Uri("images/joueur1.png", UriKind.RelativeOrAbsolute));
                    joueur.Fill = joueurSprite;
                    Canvas.SetTop(joueur, Canvas.GetTop(joueur) - (vitesse));
                    if (joueurHitBox.IntersectsWith(murHitBox2))
                    {
                        Canvas.SetTop(joueur, Canvas.GetTop(joueur) + (vitesse*2));
                    }
                }
                else if (Bas && Canvas.GetTop(joueur) + joueur.Height * 1.5 < Application.Current.MainWindow.Height)
                {
                    joueurSprite.ImageSource = new BitmapImage(new Uri("images/joueur3.png", UriKind.RelativeOrAbsolute));
                    joueur.Fill = joueurSprite;
                    Canvas.SetTop(joueur, Canvas.GetTop(joueur) + (vitesse));
                    if (joueurHitBox.IntersectsWith(murHitBox2))
                    {
                        Canvas.SetTop(joueur, Canvas.GetTop(joueur) - (vitesse*2));
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

            //collision joueur boutonP / cube boutonP
            if (cubeHitBox.IntersectsWith(boutonP1HitBox) || cubeHitBox.IntersectsWith(boutonP2HitBox))
            {
                cubeSprite.ImageSource = new BitmapImage(new Uri("images/carrer2.png", UriKind.RelativeOrAbsolute));
                cube1.Fill = cubeSprite;
            }
            if (cubeHitBox2.IntersectsWith(boutonP1HitBox) || cubeHitBox2.IntersectsWith(boutonP2HitBox))
            {
                cubeSprite2.ImageSource = new BitmapImage(new Uri("images/carrer2.png", UriKind.RelativeOrAbsolute));
                cube2.Fill = cubeSprite2;
            }
            if (joueurHitBox.IntersectsWith(boutonPHitBox))
            {
                porteSprite.ImageSource = new BitmapImage(new Uri("images/porte2.png", UriKind.RelativeOrAbsolute));
                porte.Fill = porteSprite;
                ouvert = true;
            }
            else if (joueurHitBox.IntersectsWith(boutonP1HitBox) && cubeHitBox.IntersectsWith(boutonP2HitBox) || joueurHitBox.IntersectsWith(boutonP2HitBox) && cubeHitBox.IntersectsWith(boutonP1HitBox) || joueurHitBox.IntersectsWith(boutonP1HitBox) && cubeHitBox2.IntersectsWith(boutonP2HitBox) || joueurHitBox.IntersectsWith(boutonP2HitBox) && cubeHitBox2.IntersectsWith(boutonP1HitBox))
            {
                porteSprite.ImageSource = new BitmapImage(new Uri("images/porte2.png", UriKind.RelativeOrAbsolute));
                porte.Fill = porteSprite;
                ouvert = true;
            }
            else if (cubeHitBox.IntersectsWith(boutonPHitBox))
            {
                cubeSprite.ImageSource = new BitmapImage(new Uri("images/carrer2.png", UriKind.RelativeOrAbsolute));
                cube1.Fill = cubeSprite;
                porteSprite.ImageSource = new BitmapImage(new Uri("images/porte2.png", UriKind.RelativeOrAbsolute));
                porte.Fill = porteSprite;
                ouvert = true;
            }
            else if (cubeHitBox2.IntersectsWith(boutonPHitBox))
            {
                cubeSprite2.ImageSource = new BitmapImage(new Uri("images/carrer2.png", UriKind.RelativeOrAbsolute));
                cube2.Fill = cubeSprite2;
                porteSprite.ImageSource = new BitmapImage(new Uri("images/porte2.png", UriKind.RelativeOrAbsolute));
                porte.Fill = porteSprite;
                ouvert = true;
            }
            else if (cubeHitBox.IntersectsWith(boutonP1HitBox) && cubeHitBox2.IntersectsWith(boutonP2HitBox) || cubeHitBox2.IntersectsWith(boutonP1HitBox) && cubeHitBox.IntersectsWith(boutonP2HitBox))
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
            //collision cube boutonC
            if (cubeHitBox.IntersectsWith(boutonCHitBox))
            {
                cubeSprite.ImageSource = new BitmapImage(new Uri("images/carrer2.png", UriKind.RelativeOrAbsolute));
                cube1.Fill = cubeSprite;
                Canvas.SetLeft(portail1S2, Canvas.GetLeft(boutonC)-(4));
                Canvas.SetTop(portail1S2, Canvas.GetTop(boutonC) -(boutonC.Height * 2));
                Canvas.SetLeft(portail2S2, Canvas.GetLeft(boutonC) +(boutonC.Width + mur.Width)-(10));
                Canvas.SetTop(portail2S2, Canvas.GetTop(boutonC) - (boutonC.Height * 2));
            }
            else if (cubeHitBox2.IntersectsWith(boutonCHitBox))
            {
                cubeSprite2.ImageSource = new BitmapImage(new Uri("images/carrer2.png", UriKind.RelativeOrAbsolute));
                cube2.Fill = cubeSprite2;
                Canvas.SetLeft(portail1S1, Canvas.GetLeft(boutonC)-(4));
                Canvas.SetTop(portail1S1, Canvas.GetTop(boutonC) - (boutonC.Height*2));
                Canvas.SetLeft(portail2S1, Canvas.GetLeft(boutonC) + (boutonC.Width + mur.Width)-(10));
                Canvas.SetTop(portail2S1, Canvas.GetTop(boutonC) - (boutonC.Height*2));
            }
            else
            {
                Canvas.SetLeft(portail1S2, largeurmax);
                Canvas.SetTop(portail1S2, hauteurmax);
                Canvas.SetLeft(portail2S2, largeurmax);
                Canvas.SetTop(portail2S2, hauteurmax);
                
            }
            //collision boule boutonB
            if (bouleHitBox.IntersectsWith(boutonBHitBox))
            {
                bouleSprite.ImageSource = new BitmapImage(new Uri("images/boule2.png", UriKind.RelativeOrAbsolute));
                boule.Fill = bouleSprite;
                Canvas.SetLeft(portail1S1, Canvas.GetLeft(boutonB)-(4));
                Canvas.SetTop(portail1S1, Canvas.GetTop(boutonB) + (boutonB.Height));
                Canvas.SetLeft(portail2S1, Canvas.GetLeft(boutonB) + (boutonB.Width + mur.Width-10));
                Canvas.SetTop(portail2S1, Canvas.GetTop(boutonB) + (boutonB.Height));
            }
            else
            {
                bouleSprite.ImageSource = new BitmapImage(new Uri("images/boule1.png", UriKind.RelativeOrAbsolute));
                boule.Fill = bouleSprite;
                Canvas.SetLeft(portail1S1, largeurmax);
                Canvas.SetTop(portail1S1, hauteurmax);
                Canvas.SetLeft(portail2S1, largeurmax);
                Canvas.SetTop(portail2S1, hauteurmax);
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
                Creation_Niveaux();
            }
            //collision joueur portail
            if (joueurHitBox.IntersectsWith(portail1HitBox))
            {
                Canvas.SetLeft(joueur, Canvas.GetLeft(portail2)+(portail2.Height));
            }
            else if (joueurHitBox.IntersectsWith(portail2HitBox))
            {
                Canvas.SetLeft(joueur, Canvas.GetLeft(portail1)-(portail1.Height));
            }
            else if (joueurHitBox.IntersectsWith(portail1S1HitBox))
            {
                Canvas.SetLeft(joueur, Canvas.GetLeft(portail2S1) + (portail2.Height));
            }
            else if (joueurHitBox.IntersectsWith(portail2S1HitBox))
            {
                Canvas.SetLeft(joueur, Canvas.GetLeft(portail1S1) - (portail1.Height));
            }
            else if (joueurHitBox.IntersectsWith(portail1S2HitBox))
            {
                Canvas.SetLeft(joueur, Canvas.GetLeft(portail2S2) + (portail2.Height));
            }
            else if (joueurHitBox.IntersectsWith(portail2S2HitBox))
            {
                Canvas.SetLeft(joueur, Canvas.GetLeft(portail1S2) - (portail1.Height));
            }
            //collision cube 1 portail
            if (cubeHitBox.IntersectsWith(portail1HitBox))
            {
                Canvas.SetLeft(cube1, Canvas.GetLeft(portail2) + (portail2.Height));
            }
            else if (cubeHitBox.IntersectsWith(portail2HitBox))
            {
                Canvas.SetLeft(cube1, Canvas.GetLeft(portail1) - (portail1.Height));
            }
            else if(cubeHitBox.IntersectsWith(portail1S1HitBox))
            {
                Canvas.SetLeft(cube1, Canvas.GetLeft(portail2S1) + (portail2S1.Height));
            }
            else if (cubeHitBox.IntersectsWith(portail2S1HitBox))
            {
                Canvas.SetLeft(cube1, Canvas.GetLeft(portail1S1) - (portail1S1.Height));
            }
            else if(cubeHitBox.IntersectsWith(portail1S2HitBox))
            {
                Canvas.SetLeft(cube1, Canvas.GetLeft(portail2S2) + (portail2S2.Height));
            }
            else if (cubeHitBox.IntersectsWith(portail2S2HitBox))
            {
                Canvas.SetLeft(cube1, Canvas.GetLeft(portail1S2) - (portail1S2.Height));
            }
            //collision cube 2 portail
            if (cubeHitBox2.IntersectsWith(portail1HitBox))
            {
                Canvas.SetLeft(cube2, Canvas.GetLeft(portail2) + (portail2.Height));
            }
            else if (cubeHitBox2.IntersectsWith(portail2HitBox))
            {
                Canvas.SetLeft(cube2, Canvas.GetLeft(portail1) - (portail1.Height));
            }
            else if(cubeHitBox2.IntersectsWith(portail1S1HitBox))
            {
                Canvas.SetLeft(cube2, Canvas.GetLeft(portail2S1) + (portail2S1.Height));
            }
            else if (cubeHitBox2.IntersectsWith(portail2S1HitBox))
            {
                Canvas.SetLeft(cube2, Canvas.GetLeft(portail1S1) - (portail1S1.Height));
            }
            else if(cubeHitBox2.IntersectsWith(portail1S2HitBox))
            {
                Canvas.SetLeft(cube2, Canvas.GetLeft(portail2S2) + (portail2S2.Height));
            }
            else if (cubeHitBox2.IntersectsWith(portail2S2HitBox))
            {
                Canvas.SetLeft(cube2, Canvas.GetLeft(portail1S2) - (portail1S2.Height));
            }
        }

    }
}
