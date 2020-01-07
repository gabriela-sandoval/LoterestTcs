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

namespace LoterestTcs
{
    /// <summary>
    /// Lógica de interacción para Idioma.xaml
    /// </summary>
    public partial class Idioma : Window
    {
        public Idioma()
        {
            InitializeComponent();
        }

        private void EspañolRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var resources = new ResourceDictionary();
            resources.Source = new Uri("pack://application:,,,/Idioma/Strings.xaml");
            Application.Current.Resources.MergedDictionaries.Add(resources);
        }

        private void InglesRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var resources = new ResourceDictionary();
            resources.Source = new Uri("pack://application:,,,/Idioma/Strings_en_US.xaml");
            Application.Current.Resources.MergedDictionaries.Add(resources);
        }

        private void AceptarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
