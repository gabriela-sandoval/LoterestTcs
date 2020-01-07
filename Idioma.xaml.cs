using System;
using System.Windows;

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
