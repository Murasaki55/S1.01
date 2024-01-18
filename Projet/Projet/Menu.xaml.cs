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
using System.Windows.Shapes;

namespace Projet
{
    /// <summary>
    /// Logique d'interaction pour Menu.xaml
    /// </summary>
    public partial class Menu : Window
    { 
        public string choixSalle = "0";
        public Menu()
        {
            InitializeComponent();
        }

        private void jouer_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void quitter_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void niv1_Click(object sender, RoutedEventArgs e)
        {
            choixSalle = "1";
        }

        private void niv2_Click(object sender, RoutedEventArgs e)
        {
            choixSalle = "2";
        }

        private void niv3_Click(object sender, RoutedEventArgs e)
        {
            choixSalle = "3";
        }

        private void niv4_Click(object sender, RoutedEventArgs e)
        {
            choixSalle = "4";
        }

        private void niv5_Click(object sender, RoutedEventArgs e)
        {
            choixSalle = "5";
        }
    }
}
