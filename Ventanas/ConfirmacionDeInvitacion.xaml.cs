using LoterestTcs.ServiceReferenceLoterest;
using System;
using System.ServiceModel;
using System.Windows;
using LoterestTcs;

namespace LoterestTcs.Ventanas
{
    public partial class ConfirmacionDeInvitacion : Window, IUserManagerCallback
    {
        private string nombreUsuario;
        private string modoJuego;
        private Jugador jugador;
        private string mensajeUsuario;

        public ConfirmacionDeInvitacion(Jugador jugador, string nombreUsuario, string modoJuego, string mensaje)
        {
            InitializeComponent();
            this.jugador = jugador;
            this.nombreUsuario = nombreUsuario;
            this.modoJuego = modoJuego;
            this.mensajeUsuario = mensaje;
            MensajeInvitacionLabel.Content = mensaje;
        }

        private void AceptarButton_Click(object sender, RoutedEventArgs e)
        {
            const int TIEMPOSELECCIONCARTAS = 60;
            try
            {
                InstanceContext instanceContext = new InstanceContext(this);
                UserManagerClient userManagerClient = new UserManagerClient(instanceContext);
                userManagerClient.ConfirmarInvitacion(true, modoJuego, jugador.NombreJugador, nombreUsuario);
                if(modoJuego.Equals("Loteria"))
                {
                    Tablero tablero = new Tablero(jugador, TIEMPOSELECCIONCARTAS, nombreUsuario);
                    this.Close();
                    tablero.Show();
                }
                else if(modoJuego.Equals("AlAzar"))
                {
                    AlAzar alAzar = new AlAzar(jugador, TIEMPOSELECCIONCARTAS, nombreUsuario);
                    this.Close();
                    alAzar.Show();
                }
            }
            catch(InvalidOperationException)
            {
                MessageBox.Show("Operación inválida, intente nuevamente", "Operación inválida", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RechazarButton_Click(object sender, RoutedEventArgs e)
        {
            InstanceContext instanceContext = new InstanceContext(this);
            UserManagerClient userManagerClient = new UserManagerClient(instanceContext);
            userManagerClient.ConfirmarInvitacion(false, jugador.NombreJugador, modoJuego, nombreUsuario);
        }

        private void DesplegarVentana(Window ventana)
        {
            ventana.Show();
            this.Close();
        }

        public void DevuelveCuenta(Jugador jugador)
        {
            throw new NotImplementedException();
        }

        public void FinPartida(string mensaje)
        {
            throw new NotImplementedException();
        }

        public void MensajeChat(string mensaje)
        {
            throw new NotImplementedException();
        }

        public void MostrarPuntajes(PuntajeUsuario[] puntajesUsuarios)
        {
            throw new NotImplementedException();
        }

        public void RecibirConfirmacion(bool opcion, string nombreUsuario, string modoJuego)
        {
            throw new NotImplementedException();
        }

        public void RecibirInvitacion(string nombreUsuario, string mensajeUsuario, string modoJuego)
        {
            throw new NotImplementedException();
        }

        public void Respuesta(string mensaje)
        {
            throw new NotImplementedException();
        }
    }
}
