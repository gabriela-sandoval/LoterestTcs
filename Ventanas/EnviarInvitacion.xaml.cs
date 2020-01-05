using LoterestTcs.ServiceReferenceLoterest;
using System;
using System.ServiceModel;
using System.Windows;

namespace LoterestTcs.Ventanas
{
    /// <summary>
    /// Lógica de interacción para EnviarInvitacion.xaml
    /// </summary>
    public partial class EnviarInvitacion : Window, IUserManagerCallback
    {
        private Jugador jugador;
        private string modoJuego;

        public EnviarInvitacion(Jugador jugadorRecibido, string modoJuegoElegido)
        {
            InitializeComponent();
            modoJuego = modoJuegoElegido;
            jugador = jugadorRecibido;
        }

        private void ContinuarButton_Click(object sender, RoutedEventArgs e)
        {
            string invitado = InvitacionTextBox.Text.Trim();
            string mensaje = "Mensaje de invitación";
            
            try
            {
                if(ValidarDatosIngresados(invitado))
                {
                    InstanceContext instanceContext = new InstanceContext(this);
                    UserManagerClient userManagerClient = new UserManagerClient(instanceContext);
                    userManagerClient.EnviarInvitacion(mensaje, modoJuego, jugador.NombreJugador, invitado);
                    Menu menu = new Menu(jugador);
                    DesplegarVentana(menu);
                }
                else
                {
                    MessageBox.Show("Campo vacío, intente nuevamente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }catch(EndpointNotFoundException)
            {
                MessageBox.Show("Operación inválida, intente nuevamente", "Operación inválida", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidarDatosIngresados(string nombreUsuario)
        {
            bool datosValidos = false;

            if (nombreUsuario != "")
            {
                datosValidos = true;
                return datosValidos;
            }
            else
            {
                return datosValidos;
            }
        }

        private void DesplegarVentana(Window window)
        {
            window.Show();
            this.Close();
        }

        private void CancelarButton_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu(jugador);
            DesplegarVentana(menu);
        }

        public void Respuesta(string mensaje)
        {
            MessageBox.Show(mensaje, "Enviar invitación", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void DevuelveCuenta(Jugador jugador)
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
            string invitado = InvitacionTextBox.Text.Trim();
            const int TIEMPOSELECCIONCARTAS = 60;

            if (opcion)
            {
                if(modoJuego.Equals("Loteria"))
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        Tablero tablero = new Tablero(jugador, TIEMPOSELECCIONCARTAS, invitado);
                        this.Close();
                        tablero.Show();
                    });
                }
                if(modoJuego.Equals("AlAzar"))
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        AlAzar alAzar = new AlAzar(jugador, TIEMPOSELECCIONCARTAS, invitado);
                        this.Close();
                        alAzar.Show();
                    });
                }
            }
            else
            {
                MessageBox.Show("Mensaje de invitación rechazado", "Enviar invitación", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public void FinPartida(string mensaje)
        {
            throw new NotImplementedException();
        }

        public void RecibirInvitacion(string nombreUsuario, string mensajeUsuario, string modoJuego)
        {
            throw new NotImplementedException();
        }
    }
}
