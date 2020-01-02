using LoterestTcs.ServiceReferenceLoterest;
using System.Windows;

namespace LoterestTcs.Ventanas
{
    /// <summary>
    /// Lógica de interacción para Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        private Jugador jugador;
        public Menu(Jugador jugadorRecuperado)
        {
            InitializeComponent();
            NombreJugadorLabel.Content = jugadorRecuperado.NombreJugador;
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
