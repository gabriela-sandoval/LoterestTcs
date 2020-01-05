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
            MessageBoxResult result = MessageBox.Show("¿Deseas salir? ¡Se cerrará tu sesión!", "Salir", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    MessageBox.Show("¡Que mal! Es una lastima que te vallas, ¡vuelve pronto!", "Salir");
                    MainWindow mainWindow = new MainWindow();
                    DesplegarVentana(mainWindow);
                    break;
                case MessageBoxResult.No:
                    MessageBox.Show("¡Nos alegra que te quedes!, ahora a seguir en el juego", "Salir");
                    break;
            }
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
        private void AcercaDeButton_Click(object sender, RoutedEventArgs e)
        {
            AcercaDe acercaDe = new AcercaDe();
            acercaDe.ShowDialog();
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
