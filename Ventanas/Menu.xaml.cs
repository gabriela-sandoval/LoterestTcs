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

    }
}
