using LoterestTcs.ServiceReferenceLoterest;
using System;
using System.ServiceModel;
using System.Windows;

namespace LoterestTcs
{
    /// <summary>
    /// Lógica de interacción para EnviarInvitacion.xaml
    /// Ventana que permite enviar una invitación a un jugador en específico
    /// </summary>
    public partial class EnviarInvitacion : Window, IUserManagerCallback
    {
        private Jugador jugador;
        private string modoJuego;

        /// <summary>
        /// Inicialización de componentes de ventana EnviarInvitacion
        /// </summary>
        /// <param name="jugadorRecibido">Recibe un parametro jugador de tipo jugador</param>
        /// <param name="modoJuegoElegido">Recibe un parametro modo de juego de tipo string</param>
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
                if (ValidarDatosIngresados(invitado))
                {
                    InstanceContext instanceContext = new InstanceContext(this);
                    UserManagerClient userManagerClient = new UserManagerClient(instanceContext);
                    userManagerClient.EnviarInvitacion(mensaje, modoJuego, jugador.NombreJugador, invitado);
                    Menu menu = new Menu(jugador);
                    DesplegarVentana(menu);
                }
                else
                {
                    MessageBox.Show(Application.Current.Resources["DatosInvalidosInvitacion"].ToString());
                }
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show(Application.Current.Resources["OperacionInvalida"].ToString());
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

        /// <summary>
        /// Implementación de interfaz  de IUserManagerCallback.
        /// </summary>
        /// <param name="mensaje">Recibe un parametro mensaje de tipo string</param>
        public void Respuesta(string mensaje)
        {
            mensaje = Application.Current.Resources["MensajeUsuarioNoConectado"].ToString();
            MessageBox.Show(mensaje);
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

        /// <summary>
        /// Implementacióin de interfaz de IUserManagerCallback
        /// </summary>
        /// <param name="opcion">Recibe un parametro opción de tipo book</param>
        /// <param name="nombreUsuario">Recibe un parametro nombre de usuario de tipo string</param>
        /// <param name="modoJuego">Recibe un parametro modo de juego de tipo string</param>
        public void RecibirConfirmacion(bool opcion, string nombreUsuario, string modoJuego)
        {
            string invitado = InvitacionTextBox.Text.Trim();
            const int TIEMPOSELECCIONCARTAS = 60;

            if (opcion)
            {
                if (modoJuego.Equals("Loteria"))
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        Tablero tablero = new Tablero(jugador, TIEMPOSELECCIONCARTAS, invitado);
                        this.Close();
                        tablero.Show();
                    });
                }
                if (modoJuego.Equals("AlAzar"))
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
                MessageBox.Show(Application.Current.Resources["MensajeInvitacionRechazada"].ToString());
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
