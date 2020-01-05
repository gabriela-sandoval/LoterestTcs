using LoterestTcs.ServiceReferenceLoterest;
using System.Windows;

namespace LoterestTcs.Ventanas
{
    /// <summary>
    /// Lógica de interacción para Menu.xaml
    /// </summary>
    public partial class Menu : Window, IUserManagerCallback
    {
        private Jugador jugador;

        public Menu(Jugador jugadorRecuperado)
        {
            InitializeComponent();
            NombreJugadorLabel.Content = jugadorRecuperado.NombreJugador;
            PuntajeLoteriaLabel.Content = jugadorRecuperado.PuntajeJugador;
            PuntajeAlAzarLabel.Content = jugadorRecuperado.PuntajeJugadorAlAzar;
            this.jugador = jugadorRecuperado;
        }

        private void LoteriaButtonMenu_Click(object sender, RoutedEventArgs e)
        {
            EnviarInvitacion enviarInvitacion = new EnviarInvitacion(jugador, "Loteria");
            DesplegarVentana(enviarInvitacion);
        }

        private void DesplegarVentana(Window window)
        {
            window.Show();
            this.Close();
        }

        private void SalirButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            DesplegarVentana(mainWindow);
        }

        private void ConfiguracionButton_Click(object sender, RoutedEventArgs e)
        {
            PerfilJugador perfilJugador = new PerfilJugador(jugador);
            DesplegarVentana(perfilJugador);
        }

        private void PuntajesButton_Click(object sender, RoutedEventArgs e)
        {
            PuntajesJugadores puntajes = new PuntajesJugadores(jugador);
            DesplegarVentana(puntajes);
        }

        private void AlAzarButton_Click(object sender, RoutedEventArgs e)
        {
            EnviarInvitacion enviarInvitacion = new EnviarInvitacion(jugador, "AlAzar");
            DesplegarVentana(enviarInvitacion);
        }

        public void Respuesta(string mensaje)
        {
            throw new System.NotImplementedException();
        }

        public void DevuelveCuenta(Jugador jugador)
        {
            throw new System.NotImplementedException();
        }

        public void MensajeChat(string mensaje)
        {
            throw new System.NotImplementedException();
        }

        public void MostrarPuntajes(PuntajeUsuario[] puntajesUsuarios)
        {
            throw new System.NotImplementedException();
        }

        public void RecibirConfirmacion(bool opcion, string nombreUsuario, string modoJuego)
        {
            throw new System.NotImplementedException();
        }

        public void RecibirInvitacion(string nombreUsuario, string mensajeUsuario, string modoJuego)
        {
            throw new System.NotImplementedException();
        }

        public void FinPartida(string mensaje)
        {
            throw new System.NotImplementedException();
        }
    }
}
