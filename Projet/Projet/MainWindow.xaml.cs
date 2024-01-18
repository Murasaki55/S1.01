using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
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
        private Rect joueurBoiteCollision;
        private Rect cubeBoiteCollision;
        private Rect cubeBoiteCollision2;
        private Rect bouleBoiteCollision;
        private Rect boutonPBoiteCollision;
        private Rect boutonP1BoiteCollision;
        private Rect boutonP2BoiteCollision;
        private Rect boutonBBoiteCollision;
        private Rect boutonCBoiteCollision;
        private Rect boutonC2BoiteCollision;
        private Rect boutonJBoiteCollision;
        private Rect porteBoiteCollision;
        private Rect murBoiteCollision;
        private Rect murBoiteCollision2;
        private Rect portail1BoiteCollision;
        private Rect portail2BoiteCollision;
        private Rect portail1S1BoiteCollision;
        private Rect portail2S1BoiteCollision;
        private Rect portail1S2BoiteCollision;
        private Rect portail2S2BoiteCollision;
        private Rect tourelleHBoiteCollision;
        private Rect tourelleVBoiteCollision;
        private Rect protectionHBoiteCollision;
        private Rect protectionVBoiteCollision;
        private Rect sortieBoiteCollision;
        private Rect portefinBoiteCollision;

        private ImageBrush joueurSprite = new ImageBrush();
        private ImageBrush solSprite = new ImageBrush();
        private ImageBrush cubeSprite = new ImageBrush();
        private ImageBrush cubeSprite2 = new ImageBrush();
        private ImageBrush bouleSprite = new ImageBrush();
        private ImageBrush boutonSprite = new ImageBrush();
        private ImageBrush boutonBSprite = new ImageBrush();
        private ImageBrush boutonCSprite = new ImageBrush();
        private ImageBrush boutonJSprite = new ImageBrush();
        private ImageBrush porteSprite = new ImageBrush();
        private ImageBrush megaPorteSprite = new ImageBrush();
        private ImageBrush murSprite = new ImageBrush();
        private ImageBrush portail1Sprite = new ImageBrush();
        private ImageBrush portail2Sprite = new ImageBrush();
        private ImageBrush tourelleHSprite = new ImageBrush();
        private ImageBrush tourelleVSprite = new ImageBrush();
        private ImageBrush portefinSprite = new ImageBrush();
        private ImageBrush f1Sprite = new ImageBrush();
        private ImageBrush f2Sprite = new ImageBrush();
        private ImageBrush f3Sprite = new ImageBrush();
        private ImageBrush f4Sprite = new ImageBrush();
        private ImageBrush f5Sprite = new ImageBrush();
        private ImageBrush f6Sprite = new ImageBrush();
        private ImageBrush f7Sprite = new ImageBrush();
        private ImageBrush f8Sprite = new ImageBrush();

        private readonly int VITESSE = 10;
        private readonly int VITESSECUBE = 11;
        private readonly int VITESSEBALLE = 20;

        private int tempsBoutonJ = 15;
        private string salle = "0";
        private bool Gauche, Droite, Haut, Bas, E , P, Echap = false;
        private bool ouvert = false;

        private List<Rectangle> objetSupprimer = new List<Rectangle>();

        private DispatcherTimer minutrieBoutonJ;
        private DispatcherTimer minutrietireH;
        private DispatcherTimer minutrietireV;
        public MainWindow()
        {
            InitializeComponent();
            Menu fenetreMenu = new Menu();
            fenetreMenu.ShowDialog();
            if (fenetreMenu.DialogResult == false)
                Application.Current.Shutdown();
            salle = fenetreMenu.choixSalle;

            monCanvas.Focus();

            DispatcherTimer minutrieRepetitive = new DispatcherTimer();
            minutrieRepetitive.Tick += BoucleJeux;
            minutrieRepetitive.Interval = TimeSpan.FromMilliseconds(20);
            minutrieRepetitive.Start();

            minutrieBoutonJ = new DispatcherTimer();
            minutrieBoutonJ.Tick += CollisionBoutonJ;
            minutrieBoutonJ.Interval = TimeSpan.FromMilliseconds(1000);
            minutrieBoutonJ.Start();

            minutrietireH = new DispatcherTimer();
            minutrietireH.Tick += CreerBalleH;
            minutrietireH.Interval = TimeSpan.FromMilliseconds(250);
            minutrietireH.Start();

            minutrietireV = new DispatcherTimer();
            minutrietireV.Tick += CreerBalleV;
            minutrietireV.Interval = TimeSpan.FromMilliseconds(250);
            minutrietireV.Start();

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
            portefinSprite.ImageSource = new BitmapImage(new Uri("images/porte2.png", UriKind.RelativeOrAbsolute));
            portefin.Fill = portefinSprite;
            megaPorteSprite.ImageSource = new BitmapImage(new Uri("images/megaPorte1.png", UriKind.RelativeOrAbsolute));
            megaPorte.Fill = megaPorteSprite;

            boutonSprite.ImageSource = new BitmapImage(new Uri("images/Bouton.png", UriKind.RelativeOrAbsolute));
            boutonP.Fill = boutonSprite;
            boutonP1.Fill = boutonSprite;
            boutonP2.Fill = boutonSprite;
            boutonBSprite.ImageSource = new BitmapImage(new Uri("images/boutonBoule.png", UriKind.RelativeOrAbsolute));
            boutonB.Fill = boutonBSprite;
            boutonCSprite.ImageSource = new BitmapImage(new Uri("images/boutonCube.png", UriKind.RelativeOrAbsolute));
            boutonC.Fill = boutonCSprite;
            boutonC2.Fill = boutonCSprite;
            boutonJSprite.ImageSource = new BitmapImage(new Uri("images/boutonJoueur.png", UriKind.RelativeOrAbsolute));
            boutonJ.Fill = boutonJSprite;

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

            f1Sprite.ImageSource = new BitmapImage(new Uri("images/flèche1.png", UriKind.RelativeOrAbsolute));
            f1.Fill = f1Sprite;
            f2Sprite.ImageSource = new BitmapImage(new Uri("images/flèche2.png", UriKind.RelativeOrAbsolute));
            f2.Fill = f2Sprite;
            f3Sprite.ImageSource = new BitmapImage(new Uri("images/flèche3.png", UriKind.RelativeOrAbsolute));
            f3.Fill = f3Sprite;
            f4Sprite.ImageSource = new BitmapImage(new Uri("images/flèche4.png", UriKind.RelativeOrAbsolute));
            f4.Fill = f4Sprite;
            f5Sprite.ImageSource = new BitmapImage(new Uri("images/flèche5.png", UriKind.RelativeOrAbsolute));
            f5.Fill = f5Sprite;
            f6Sprite.ImageSource = new BitmapImage(new Uri("images/flèche6.png", UriKind.RelativeOrAbsolute));
            f6.Fill = f6Sprite;
            f7Sprite.ImageSource = new BitmapImage(new Uri("images/flèche7.png", UriKind.RelativeOrAbsolute));
            f7.Fill = f7Sprite;
            f8Sprite.ImageSource = new BitmapImage(new Uri("images/flèche8.png", UriKind.RelativeOrAbsolute));
            f8.Fill = f8Sprite;

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
            if (e.Key == Key.P)
            {
                P = true;
            }
            if (e.Key == Key.Escape)
            {
                Pause fenetrePause = new Pause();
                fenetrePause.ShowDialog();
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
            if (e.Key == Key.P)
            {
                P = false;
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
                        Canvas.SetTop(boutonP1, 667);
                        Canvas.SetLeft(boutonP1, 742);
                        boutonP1.Visibility = Visibility.Visible;

                        Canvas.SetTop(boutonP2, 223);
                        Canvas.SetLeft(boutonP2, 0);
                        boutonP2.Visibility = Visibility.Visible;

                        Canvas.SetTop(boutonJ, 424);
                        Canvas.SetLeft(boutonJ, 313);
                        boutonJ.Visibility = Visibility.Visible;
                        //joueur
                        Canvas.SetTop(joueur, 633);
                        Canvas.SetLeft(joueur, 20);
                        //cube/boule
                        Canvas.SetTop(cube1, 608);
                        Canvas.SetLeft(cube1, 248);
                        cube1.Visibility = Visibility.Visible;

                        Canvas.SetTop(cube2, 84);
                        Canvas.SetLeft(cube2, 907);
                        cube2.Visibility = Visibility.Visible;
                        //mur
                        Canvas.SetTop(mur, 511);
                        Canvas.SetLeft(mur, 530);
                        mur.Visibility = Visibility.Visible;

                        Canvas.SetTop(mur2, 300);
                        Canvas.SetLeft(mur2, 0);
                        mur2.Visibility = Visibility.Visible;
                        //tourelle
                        Canvas.SetTop(tourelleH, 10);
                        Canvas.SetLeft(tourelleH, 191);
                        tourelleH.Visibility = Visibility.Visible;

                        Canvas.SetTop(tourelleV, 491);
                        Canvas.SetLeft(tourelleV, 983);
                        tourelleV.Visibility = Visibility.Visible;
                        //protection
                        Canvas.SetTop(protectionH, 144);
                        Canvas.SetLeft(protectionH, 10);

                        Canvas.SetTop(protectionV, 328);
                        Canvas.SetLeft(protectionV, 907);
                        break;
                    }
                case "4":
                    {
                        //bouton
                        Canvas.SetTop(boutonP, 250);
                        Canvas.SetLeft(boutonP, 10);
                        boutonP.Visibility = Visibility.Visible;
                        //joueur
                        Canvas.SetTop(joueur, 633);
                        Canvas.SetLeft(joueur, 20);
                        //cube/boule
                        Canvas.SetTop(cube1, 151);
                        Canvas.SetLeft(cube1, 782);
                        cube1.Visibility = Visibility.Visible;

                        Canvas.SetTop(cube2, 531);
                        Canvas.SetLeft(cube2, 211);
                        cube2.Visibility = Visibility.Visible;
                        //mur
                        Canvas.SetTop(mur2, 342);
                        Canvas.SetLeft(mur2, 0);
                        mur2.Visibility = Visibility.Visible;
                        //tourelle
                        Canvas.SetTop(tourelleH, 10);
                        Canvas.SetLeft(tourelleH, 191);
                        tourelleH.Visibility = Visibility.Visible;

                        Canvas.SetTop(tourelleV, 491);
                        Canvas.SetLeft(tourelleV, 983);
                        tourelleV.Visibility = Visibility.Visible;
                        break;
                    }
                case "5":
                    {
                        //bouton
                        Canvas.SetTop(boutonP1, 163);
                        Canvas.SetLeft(boutonP1, 937);
                        boutonP1.Visibility = Visibility.Visible;

                        Canvas.SetTop(boutonP2, 223);
                        Canvas.SetLeft(boutonP2, 10);
                        boutonP2.Visibility = Visibility.Visible;

                        Canvas.SetTop(boutonB, 74);
                        Canvas.SetLeft(boutonB, 610);
                        boutonB.Visibility = Visibility.Visible;

                        Canvas.SetTop(boutonC, 638);
                        Canvas.SetLeft(boutonC, 410);
                        boutonC.Visibility = Visibility.Visible;

                        Canvas.SetTop(boutonC2, 638);
                        Canvas.SetLeft(boutonC2, 610);
                        boutonC2.Visibility = Visibility.Visible;

                        Canvas.SetTop(boutonJ, 424);
                        Canvas.SetLeft(boutonJ, 313);
                        boutonJ.Visibility = Visibility.Visible;
                        //joueur
                        Canvas.SetTop(joueur, 633);
                        Canvas.SetLeft(joueur, 20);
                        //cube/boule
                        Canvas.SetTop(cube1, 608);
                        Canvas.SetLeft(cube1, 248);
                        cube1.Visibility = Visibility.Visible;

                        Canvas.SetTop(cube2, 190);
                        Canvas.SetLeft(cube2, 231);
                        cube2.Visibility = Visibility.Visible;

                        Canvas.SetTop(boule, 648);
                        Canvas.SetLeft(boule, 842);
                        boule.Visibility = Visibility.Visible;
                        //mur
                        Canvas.SetLeft(mur, 485);
                        Canvas.SetTop(mur, 0);
                        mur.Visibility = Visibility.Visible;

                        Canvas.SetTop(mur2, 300);
                        Canvas.SetLeft(mur2, 0);
                        mur2.Visibility = Visibility.Visible;
                        //portail
                        Canvas.SetTop(portail1S1, 205);
                        Canvas.SetLeft(portail1S1, 410);

                        Canvas.SetTop(portail2S1, 205);
                        Canvas.SetLeft(portail2S1, 592);

                        Canvas.SetTop(portail1S2, 458);
                        Canvas.SetLeft(portail1S2, 410);

                        Canvas.SetTop(portail2S2, 458);
                        Canvas.SetLeft(portail2S2, 592);
                        //tourelle
                        Canvas.SetTop(tourelleH, 10);
                        Canvas.SetLeft(tourelleH, 191);
                        tourelleH.Visibility = Visibility.Visible;

                        Canvas.SetTop(tourelleV, 491);
                        Canvas.SetLeft(tourelleV, 983);
                        tourelleV.Visibility = Visibility.Visible;
                        //protection
                        Canvas.SetTop(protectionH, 126);
                        Canvas.SetLeft(protectionH, 10);

                        Canvas.SetTop(protectionV, 328);
                        Canvas.SetLeft(protectionV, 937);
                        //???
                        fleche.Visibility = Visibility.Visible;
                        break;
                    }
                case "6":
                    {
                        porte.Visibility = Visibility.Hidden;
                        megaPorte.Visibility = Visibility.Visible;
                        //joueur
                        Canvas.SetTop(joueur, 633);
                        Canvas.SetLeft(joueur, 516);
                        break;
                    }
                case "7":
                    {
                        Canvas.SetTop(portefin, 130);
                        Canvas.SetLeft(portefin, 505);
                        portefin.Visibility = Visibility.Visible;
                        porte.Visibility = Visibility.Hidden;
                        Canvas.SetTop(joueur, 633);
                        Canvas.SetLeft(joueur, 516);
                        f1.Visibility = Visibility.Visible;
                        f2.Visibility = Visibility.Visible;
                        f3.Visibility = Visibility.Visible;
                        f4.Visibility = Visibility.Visible;
                        f5.Visibility = Visibility.Visible;
                        f6.Visibility = Visibility.Visible;
                        f7.Visibility = Visibility.Visible;
                        f8.Visibility = Visibility.Visible;
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
            boutonC2BoiteCollision = new Rect(Canvas.GetLeft(boutonC2), Canvas.GetTop(boutonC2), boutonC2.Width - 20, boutonC2.Height - 20);
            boutonJBoiteCollision = new Rect(Canvas.GetLeft(boutonJ), Canvas.GetTop(boutonJ), boutonJ.Width - 20, boutonJ.Height - 20);
            porteBoiteCollision = new Rect(Canvas.GetLeft(porte), Canvas.GetTop(porte), porte.Width, porte.Height);
            portefinBoiteCollision = new Rect(Canvas.GetLeft(portefin), Canvas.GetTop(portefin), portefin.Width, portefin.Height);
            murBoiteCollision = new Rect(Canvas.GetLeft(mur), Canvas.GetTop(mur), mur.Width, mur.Height);
            murBoiteCollision2 = new Rect(Canvas.GetLeft(mur2), Canvas.GetTop(mur2), mur2.Width, mur2.Height);
            portail1BoiteCollision = new Rect(Canvas.GetLeft(portail1), Canvas.GetTop(portail1), portail1.Width - 40, portail1.Height - 40);
            portail2BoiteCollision = new Rect(Canvas.GetLeft(portail2), Canvas.GetTop(portail2), portail2.Width - 40, portail2.Height - 40);
            portail1S1BoiteCollision = new Rect(Canvas.GetLeft(portail1S1), Canvas.GetTop(portail1S1), portail1S1.Width - 40, portail1S1.Height - 40);
            portail2S1BoiteCollision = new Rect(Canvas.GetLeft(portail2S1), Canvas.GetTop(portail2S1), portail2S1.Width - 40, portail2S1.Height - 40);
            portail1S2BoiteCollision = new Rect(Canvas.GetLeft(portail1S2), Canvas.GetTop(portail1S2), portail1S2.Width - 40, portail1S2.Height - 40);
            portail2S2BoiteCollision = new Rect(Canvas.GetLeft(portail2S2), Canvas.GetTop(portail2S2), portail2S2.Width - 40, portail2S2.Height - 40);
            tourelleHBoiteCollision = new Rect(Canvas.GetLeft(tourelleH)-(150), Canvas.GetTop(tourelleH)+150, tourelleH.Width + 300, tourelleH.Height + 300);
            tourelleVBoiteCollision = new Rect(Canvas.GetLeft(tourelleV) - (500), Canvas.GetTop(tourelleV) - (100), tourelleV.Width + 500, tourelleV.Height + 300);
            protectionHBoiteCollision = new Rect(Canvas.GetLeft(protectionH), Canvas.GetTop(protectionH), protectionH.Width, protectionH.Height);
            protectionVBoiteCollision = new Rect(Canvas.GetLeft(protectionV), Canvas.GetTop(protectionV), protectionV.Width, protectionV.Height);
            sortieBoiteCollision = new Rect(Canvas.GetLeft(sortie), Canvas.GetTop(sortie), sortie.Width, sortie.Height);
        }

        private void CacherObjet()
        {
            boutonP.Visibility = Visibility.Hidden;
            boutonP1.Visibility = Visibility.Hidden;
            boutonP2.Visibility = Visibility.Hidden;
            boutonB.Visibility = Visibility.Hidden;
            boutonC.Visibility = Visibility.Hidden;
            boutonC2.Visibility = Visibility.Hidden;
            boutonJ.Visibility = Visibility.Hidden;
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
            protectionH.Visibility = Visibility.Hidden;
            protectionV.Visibility = Visibility.Hidden;
            megaPorte.Visibility = Visibility.Hidden;
            fleche.Visibility = Visibility.Hidden;
            f1.Visibility = Visibility.Hidden;
            f2.Visibility = Visibility.Hidden;
            f3.Visibility = Visibility.Hidden;
            f4.Visibility = Visibility.Hidden;
            f5.Visibility = Visibility.Hidden;
            f6.Visibility = Visibility.Hidden;
            f7.Visibility = Visibility.Hidden;
            f8.Visibility = Visibility.Hidden;
        }
        private void Sortie()
        {
            if (joueurBoiteCollision.IntersectsWith(sortieBoiteCollision) && salle == "5")
            {
                salle = "7";
                NumSalle.Content = "???";
                CacherObjet();
                Creation_Niveaux();
            }
        }
        private void Triche()
        {
            if (P)
            {
                if (boutonP.Visibility == Visibility.Visible)
                {
                    Canvas.SetTop(cube1,Canvas.GetTop(boutonP));
                    Canvas.SetLeft(cube1, Canvas.GetLeft(boutonP));
                }
                else if (boutonP1.Visibility == Visibility.Visible && boutonP2.Visibility == Visibility.Visible)
                {
                    Canvas.SetTop(cube1, Canvas.GetTop(boutonP1));
                    Canvas.SetLeft(cube1, Canvas.GetLeft(boutonP1));
                    Canvas.SetTop(cube2, Canvas.GetTop(boutonP2));
                    Canvas.SetLeft(cube2, Canvas.GetLeft(boutonP2));
                }
            }
        }
        private void Ou()
        {
            if (joueurBoiteCollision.IntersectsWith(portefinBoiteCollision) && portefin.Visibility == Visibility.Visible)
            {
                joueur.Visibility = Visibility.Hidden;
                portefin.Visibility = Visibility.Hidden;
                sol.Visibility = Visibility.Hidden;
                CacherObjet();
            }
        }
        private void CubeBouleCollision(Rectangle cube, Rect BoiteCollision)
        {
            if (Gauche && Canvas.GetLeft(joueur) > 50)
            {
                Canvas.SetLeft(cube, Canvas.GetLeft(cube) - (VITESSECUBE));

                if (BoiteCollision.IntersectsWith(murBoiteCollision) && mur.Visibility == Visibility.Visible)
                {
                    Canvas.SetLeft(cube, Canvas.GetLeft(cube) + (VITESSECUBE * 2));
                }
            }
            else if (Droite && Canvas.GetLeft(joueur) + cube.Width * 2.10 < Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(cube, Canvas.GetLeft(cube) + (VITESSECUBE));

                if (BoiteCollision.IntersectsWith(murBoiteCollision) && mur.Visibility == Visibility.Visible)
                {
                    Canvas.SetLeft(cube, Canvas.GetLeft(cube) - (VITESSECUBE * 2));
                }
            }
            else if (Haut && Canvas.GetTop(joueur) > 50)
            {
                Canvas.SetTop(cube, Canvas.GetTop(cube) - ( VITESSECUBE));

                if (BoiteCollision.IntersectsWith(murBoiteCollision2) && mur2.Visibility == Visibility.Visible)
                {
                    Canvas.SetTop(cube, Canvas.GetTop(cube) + (VITESSECUBE * 2));
                }
            }
            else if (Bas && Canvas.GetTop(joueur) + cube.Height * 2.50 < Application.Current.MainWindow.Height)
            {
                Canvas.SetTop(cube, Canvas.GetTop(cube) + (VITESSECUBE));

                if (BoiteCollision.IntersectsWith(murBoiteCollision2) && mur2.Visibility == Visibility.Visible)
                {
                    Canvas.SetTop(cube, Canvas.GetTop(cube) - (VITESSECUBE * 2));
                }
            }
        }
        private void MurCollisionJoueur()
        {
            if (Gauche && Canvas.GetLeft(joueur) > 0)
            {
                joueurSprite.ImageSource = new BitmapImage(new Uri("images/joueur4.png", UriKind.RelativeOrAbsolute));
                joueur.Fill = joueurSprite;
                Canvas.SetLeft(joueur, Canvas.GetLeft(joueur) - (VITESSE));
                if (joueurBoiteCollision.IntersectsWith(murBoiteCollision) || joueurBoiteCollision.IntersectsWith(protectionVBoiteCollision))
                {
                    Canvas.SetLeft(joueur, Canvas.GetLeft(joueur) + (VITESSE) * 2);
                }
            }
            else if (Droite && Canvas.GetLeft(joueur) + joueur.Width * 1.5 < Application.Current.MainWindow.Width)
            {
                joueurSprite.ImageSource = new BitmapImage(new Uri("images/joueur2.png", UriKind.RelativeOrAbsolute));
                joueur.Fill = joueurSprite;
                Canvas.SetLeft(joueur, Canvas.GetLeft(joueur) + (VITESSE));
                if (joueurBoiteCollision.IntersectsWith(murBoiteCollision) || joueurBoiteCollision.IntersectsWith(protectionVBoiteCollision))
                {
                    Canvas.SetLeft(joueur, Canvas.GetLeft(joueur) - (VITESSE) * 2);
                }
            }
            else if (Haut && Canvas.GetTop(joueur) > 0)
            {
                joueurSprite.ImageSource = new BitmapImage(new Uri("images/joueur1.png", UriKind.RelativeOrAbsolute));
                joueur.Fill = joueurSprite;
                Canvas.SetTop(joueur, Canvas.GetTop(joueur) - (VITESSE));
                if (joueurBoiteCollision.IntersectsWith(murBoiteCollision2) || joueurBoiteCollision.IntersectsWith(protectionHBoiteCollision))
                {
                    Canvas.SetTop(joueur, Canvas.GetTop(joueur) + (VITESSE * 2));
                }
            }
            else if (Bas && Canvas.GetTop(joueur) + joueur.Height * 1.5 < Application.Current.MainWindow.Height)
            {
                joueurSprite.ImageSource = new BitmapImage(new Uri("images/joueur3.png", UriKind.RelativeOrAbsolute));
                joueur.Fill = joueurSprite;
                Canvas.SetTop(joueur, Canvas.GetTop(joueur) + (VITESSE));
                if (joueurBoiteCollision.IntersectsWith(murBoiteCollision2) || joueurBoiteCollision.IntersectsWith(protectionHBoiteCollision))
                {
                    Canvas.SetTop(joueur, Canvas.GetTop(joueur) - (VITESSE * 2));
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
            else if (joueurBoiteCollision.IntersectsWith(murBoiteCollision) && mur.Visibility == Visibility.Visible || joueurBoiteCollision.IntersectsWith(murBoiteCollision2) && mur2.Visibility == Visibility.Visible || joueurBoiteCollision.IntersectsWith(protectionHBoiteCollision) && protectionH.Visibility == Visibility.Visible || joueurBoiteCollision.IntersectsWith(protectionVBoiteCollision) && protectionV.Visibility == Visibility.Visible)
            {//contre mur ou protection bloquer personnage
                MurCollisionJoueur();
            }
            else
            {//bouger le personnage
                if (Gauche && Canvas.GetLeft(joueur) > 0)
                {
                    joueurSprite.ImageSource = new BitmapImage(new Uri("images/joueur4.png", UriKind.RelativeOrAbsolute));
                    joueur.Fill = joueurSprite;
                    Canvas.SetLeft(joueur, Canvas.GetLeft(joueur) - (VITESSE));
                }
                else if (Droite && Canvas.GetLeft(joueur) + joueur.Width * 1.5 < Application.Current.MainWindow.Width)
                {
                    joueurSprite.ImageSource = new BitmapImage(new Uri("images/joueur2.png", UriKind.RelativeOrAbsolute));
                    joueur.Fill = joueurSprite;
                    Canvas.SetLeft(joueur, Canvas.GetLeft(joueur) + (VITESSE));
                }
                else if (Haut && Canvas.GetTop(joueur) > 0)
                {
                    joueurSprite.ImageSource = new BitmapImage(new Uri("images/joueur1.png", UriKind.RelativeOrAbsolute));
                    joueur.Fill = joueurSprite;
                    Canvas.SetTop(joueur, Canvas.GetTop(joueur) - (VITESSE));
                }
                else if (Bas && Canvas.GetTop(joueur) + joueur.Height * 1.5 < Application.Current.MainWindow.Height)
                {
                    joueurSprite.ImageSource = new BitmapImage(new Uri("images/joueur3.png", UriKind.RelativeOrAbsolute));
                    joueur.Fill = joueurSprite;
                    Canvas.SetTop(joueur, Canvas.GetTop(joueur) + (VITESSE));
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
            if (cubeBoiteCollision.IntersectsWith(boutonCBoiteCollision) && boutonC.Visibility == Visibility.Visible && cube1.Visibility == Visibility.Visible || cubeBoiteCollision.IntersectsWith(boutonC2BoiteCollision) && boutonC2.Visibility == Visibility.Visible && cube1.Visibility == Visibility.Visible)
            {
                cubeSprite.ImageSource = new BitmapImage(new Uri("images/carrer2.png", UriKind.RelativeOrAbsolute));
                cube1.Fill = cubeSprite;
                portail1S2.Visibility = Visibility.Visible;
                portail2S2.Visibility = Visibility.Visible;
            }
            else if (cubeBoiteCollision2.IntersectsWith(boutonCBoiteCollision) && boutonC.Visibility == Visibility.Visible && cube2.Visibility == Visibility.Visible || cubeBoiteCollision2.IntersectsWith(boutonC2BoiteCollision) && boutonC2.Visibility == Visibility.Visible && cube2.Visibility == Visibility.Visible)
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
        private void CollisionBoutonJ(object sender, EventArgs e)
        {
            if (joueurBoiteCollision.IntersectsWith(boutonJBoiteCollision) && boutonJ.Visibility == Visibility.Visible)
            {
                tempsBoutonJ = 15;
                protectionH.Visibility = Visibility.Visible;
                protectionV.Visibility = Visibility.Visible;
            }
            else
            {
                tempsBoutonJ--;

                if (tempsBoutonJ <= 0)
                {
                    protectionH.Visibility = Visibility.Hidden;
                    protectionV.Visibility = Visibility.Hidden;
                }
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
                            NumSalle.Content = "Salle : " + salle + "/5";
                            ouvert = false;
                            CacherObjet();
                            break;
                        }
                    case "5":
                        {
                            salle = "6";
                            NumSalle.Content = "Fin";
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
        private void CreerBalleH(object sender, EventArgs e)
        {
            if (joueurBoiteCollision.IntersectsWith(tourelleHBoiteCollision) && tourelleH.Visibility == Visibility.Visible)
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
                Canvas.SetLeft(balleH, Canvas.GetLeft(joueur));
                Canvas.SetTop(balleH, Canvas.GetTop(tourelleH));
                monCanvas.Children.Add(balleH);
            }
        }
        private void CreerBalleV(object sender, EventArgs e)
        {
            if (joueurBoiteCollision.IntersectsWith(tourelleVBoiteCollision) && tourelleV.Visibility == Visibility.Visible)
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
                Canvas.SetLeft(balleV, Canvas.GetLeft(tourelleV));
                Canvas.SetTop(balleV, Canvas.GetTop(joueur));
                monCanvas.Children.Add(balleV);
            }
        }
        private void BoucleJeux(object sender, EventArgs e)
        {
            //création BoiteCollision
            BoiteCollision();

            //collision joueur cube
            JoueurMouvement();
            Sortie();

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
            if (joueurBoiteCollision.IntersectsWith(tourelleHBoiteCollision) && tourelleH.Visibility == Visibility.Visible)
            {//création balle pour la tourelleH
                tourelleHSprite.ImageSource = new BitmapImage(new Uri("images/tourelle2.png", UriKind.RelativeOrAbsolute));
                tourelleH.Fill = tourelleHSprite;
                foreach (var x in monCanvas.Children.OfType<Rectangle>())
                {
                    if (x is Rectangle && (string)x.Tag == "balleH")
                    {//balle avance
                        Canvas.SetTop(x, Canvas.GetTop(x) + (VITESSEBALLE));
                        if (Canvas.GetTop(x) > 410)
                        {
                            objetSupprimer.Add(x);
                        }
                        Rect balletourelleH = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                        if (balletourelleH.IntersectsWith(joueurBoiteCollision))
                        {//si balle touche joueur
                            Creation_Niveaux();
                        }
                        if (balletourelleH.IntersectsWith(cubeBoiteCollision) && cube1.Visibility == Visibility.Visible || balletourelleH.IntersectsWith(cubeBoiteCollision2) && cube2.Visibility == Visibility.Visible || balletourelleH.IntersectsWith(murBoiteCollision) && mur.Visibility == Visibility.Visible || balletourelleH.IntersectsWith(murBoiteCollision2) && mur2.Visibility == Visibility.Visible || balletourelleH.IntersectsWith(protectionHBoiteCollision) && protectionH.Visibility == Visibility.Visible)
                        {//ajout balle à liste de supression
                            objetSupprimer.Add(x);
                        }
                    }
                    if (x is Rectangle && (string)x.Tag == "balleV")
                    {//si il rest des balles de l'autres tourelle finir leur avancement
                        Canvas.SetLeft(x, Canvas.GetLeft(x) - (VITESSEBALLE));
                        if (Canvas.GetLeft(x) < 450 || Canvas.GetTop(x) > 342)
                        {
                            objetSupprimer.Add(x);
                        }
                        Rect balletourelleV = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                        if (balletourelleV.IntersectsWith(joueurBoiteCollision))
                        {
                            Creation_Niveaux();
                        }
                        if (balletourelleV.IntersectsWith(cubeBoiteCollision) && cube1.Visibility == Visibility.Visible || balletourelleV.IntersectsWith(cubeBoiteCollision2) && cube2.Visibility == Visibility.Visible || balletourelleV.IntersectsWith(murBoiteCollision) && mur.Visibility == Visibility.Visible || balletourelleV.IntersectsWith(murBoiteCollision2) && mur2.Visibility == Visibility.Visible || balletourelleV.IntersectsWith(protectionVBoiteCollision) && protectionV.Visibility == Visibility.Visible)
                        {
                            objetSupprimer.Add(x);
                        }
                    }
                }

            }
            else if (joueurBoiteCollision.IntersectsWith(tourelleVBoiteCollision) && tourelleV.Visibility == Visibility.Visible)
            {//création balle pour la tourelleV
                tourelleVSprite.ImageSource = new BitmapImage(new Uri("images/tourelle4.png", UriKind.RelativeOrAbsolute));
                tourelleV.Fill = tourelleVSprite;
                foreach (var x in monCanvas.Children.OfType<Rectangle>())
                {
                    if (x is  Rectangle && (string)x.Tag == "balleV")
                    {//balle avance
                        Canvas.SetLeft(x, Canvas.GetLeft(x) - (VITESSEBALLE));
                        if (Canvas.GetLeft(x) < 450)
                        {
                            objetSupprimer.Add(x);
                        }
                        Rect balletourelleV = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                        if (balletourelleV.IntersectsWith(joueurBoiteCollision))
                        {//si balle touche joueur
                            Creation_Niveaux();
                        }
                        if (balletourelleV.IntersectsWith(cubeBoiteCollision) && cube1.Visibility == Visibility.Visible || balletourelleV.IntersectsWith(cubeBoiteCollision2) && cube2.Visibility == Visibility.Visible || balletourelleV.IntersectsWith(murBoiteCollision) && mur.Visibility == Visibility.Visible || balletourelleV.IntersectsWith(murBoiteCollision2) && mur2.Visibility == Visibility.Visible || balletourelleV.IntersectsWith(protectionVBoiteCollision) && protectionV.Visibility == Visibility.Visible)
                        {//ajout de balle dans la liste de supression
                            objetSupprimer.Add(x);
                        }

                    }
                    if (x is Rectangle && (string)x.Tag == "balleH")
                    {//finir l'avancement des balle de l'autre tourelle
                        Canvas.SetTop(x, Canvas.GetTop(x) + (VITESSEBALLE));
                        if (Canvas.GetTop(x) > 410)
                        {
                            objetSupprimer.Add(x);
                        }
                        Rect balletourelleH = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                        if (balletourelleH.IntersectsWith(joueurBoiteCollision))
                        {
                            Creation_Niveaux();
                        }
                        if (balletourelleH.IntersectsWith(cubeBoiteCollision) && cube1.Visibility == Visibility.Visible || balletourelleH.IntersectsWith(cubeBoiteCollision2) && cube2.Visibility == Visibility.Visible || balletourelleH.IntersectsWith(murBoiteCollision) && mur.Visibility == Visibility.Visible || balletourelleH.IntersectsWith(murBoiteCollision2) && mur2.Visibility == Visibility.Visible || balletourelleH.IntersectsWith(protectionHBoiteCollision) && protectionH.Visibility == Visibility.Visible)
                        {
                            objetSupprimer.Add(x);
                        }
                    }
                }
            }
            else
            {
                tourelleHSprite.ImageSource = new BitmapImage(new Uri("images/tourelle.png", UriKind.RelativeOrAbsolute));
                tourelleH.Fill = tourelleHSprite;
                tourelleVSprite.ImageSource = new BitmapImage(new Uri("images/tourelle3.png", UriKind.RelativeOrAbsolute));
                tourelleV.Fill = tourelleVSprite;
                foreach (var x in monCanvas.Children.OfType<Rectangle>())
                {//si il rest des balles d'une des tourelles ou des deux faire finir leur avancement
                    if (x is Rectangle && (string)x.Tag == "balleV")
                    {
                        Canvas.SetLeft(x, Canvas.GetLeft(x) - (VITESSEBALLE));
                        if (Canvas.GetLeft(x) < 450)
                        {
                            objetSupprimer.Add(x);
                        }
                        Rect balletourelleV = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                        if (balletourelleV.IntersectsWith(joueurBoiteCollision))
                        {
                            Creation_Niveaux();
                        }
                        if (balletourelleV.IntersectsWith(cubeBoiteCollision) && cube1.Visibility == Visibility.Visible || balletourelleV.IntersectsWith(cubeBoiteCollision2) && cube2.Visibility == Visibility.Visible || balletourelleV.IntersectsWith(murBoiteCollision) && mur.Visibility == Visibility.Visible || balletourelleV.IntersectsWith(murBoiteCollision2) && mur2.Visibility == Visibility.Visible || balletourelleV.IntersectsWith(protectionVBoiteCollision) && protectionV.Visibility == Visibility.Visible)
                        {
                            objetSupprimer.Add(x);
                        }
                    }
                    if (x is Rectangle && (string)x.Tag == "balleH")
                    {
                        Canvas.SetTop(x, Canvas.GetTop(x) + (VITESSEBALLE));
                        if (Canvas.GetTop(x) > 410)
                        {
                            objetSupprimer.Add(x);
                        }
                        Rect balletourelleH = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                        if (balletourelleH.IntersectsWith(joueurBoiteCollision))
                        {
                            Creation_Niveaux();
                        }
                        if (balletourelleH.IntersectsWith(cubeBoiteCollision) && cube1.Visibility == Visibility.Visible || balletourelleH.IntersectsWith(cubeBoiteCollision2) && cube2.Visibility == Visibility.Visible || balletourelleH.IntersectsWith(murBoiteCollision) && mur.Visibility == Visibility.Visible || balletourelleH.IntersectsWith(murBoiteCollision2) && mur2.Visibility == Visibility.Visible || balletourelleH.IntersectsWith(protectionHBoiteCollision) && protectionH.Visibility == Visibility.Visible)
                        {
                            objetSupprimer.Add(x);
                        }
                    }
                }
            }
            foreach (Rectangle y in objetSupprimer)
            {//supression des balles dans la liste de supression
                monCanvas.Children.Remove(y);
            }

            Ou();
            Triche();
        }
    }
}
