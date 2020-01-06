using LoterestTcs.Model;
using LoterestTcs.ServiceReferenceLoterest;
using System.Windows;

namespace LoterestTcs
{
    /// <summary>
    /// Lógica de interacción para AlAzar.xaml
    /// Ventana que muestra el modo de juego al azar.
    /// </summary>
    public partial class AlAzar : Window
    {
        private TableroJuego tableroJuego;
        private Jugador jugador;
        private string nombreUsuario;
        private string modoJuego = "AlAzar";

        /// <summary>
        /// Inicialización de componentes de ventana AlAzar
        /// </summary>
        /// <param name="jugador">Recibe un parametro jugador de tipo jugador</param>
        /// <param name="tiempo">Recibe un parametro tiempo de tipo string</param>
        /// <param name="nombreInvitado">Recibe un parametro nombre de un invitado de tipo string</param>
        public AlAzar(Jugador jugador, int tiempo, string nombreInvitado)
        {
            InitializeComponent();
            this.nombreUsuario = nombreInvitado;
            this.jugador = jugador;
        }

        private void RegresarButton_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu(jugador);
            DesplegarVentana(menu);
        }

        private void DesplegarVentana(Window ventana)
        {
            ventana.Show();
            this.Close();
        }
    }
}
