using LoterestTcs.ServiceReference;
using System.Windows;

namespace LoterestTcs.Ventanas
{
    /// <summary>
    /// Lógica de interacción para Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {

        public Menu(string nombreJugador)
        {
            InitializeComponent();
            NombreJugadorLabel.Content = nombreJugador;
        }

        private void LoteriaButtonMenu_Click(object sender, RoutedEventArgs e)
        {
            Tablero tablero = new Tablero(10);
            DesplegarVentana(tablero);

        }

        private void DesplegarVentana(Window window)
        {
            window.Show();
            this.Close();
        }
    }
}
