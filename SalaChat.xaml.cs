using LoterestTcs.Model;
using LoterestTcs.ServiceReferenceLoterest;
using System;
using System.ServiceModel;
using System.Windows;
using System.Windows.Threading;

namespace LoterestTcs
{
    /// <summary>
    /// Lógica de interacción para SalaChat.xaml
    /// Ventana que muestra las reglas del juego y la sala de chat.
    /// </summary>
    public partial class SalaChat : Window, IUserManagerCallback
    {
        private TableroJuego tableroJuego;
        private Jugador jugador;
        private string nombreUsuario;
        private int tiempoDisponible = 60;
        private DispatcherTimer dispatcherTimer;
        private string modoJuego;
        private const int TIEMPOLIMITE = 0;

        /// <summary>
        /// Inicialización de componentes de ventana SalaChat.
        /// </summary>
        /// <param name="tableroPartida">Recibe un parametro tablero de tipo tablero</param>
        /// <param name="jugador">Recibe un parametro jugador de tipo jugador</param>
        /// <param name="nombreUsuarioPartida">Recibe un parametro nombre de usuario de tipo string</param>
        /// <param name="modoJuegoElegido">Recibe un parametro modo de juego de tipo string</param>
        public SalaChat(TableroJuego tableroPartida, Jugador jugador, string nombreUsuarioPartida, string modoJuegoElegido)
        {
            InitializeComponent();
            tableroJuego = tableroPartida;
            this.jugador = jugador;
            modoJuego = modoJuegoElegido;
            nombreUsuario = nombreUsuarioPartida;
            CuentaRegresiva();
        }

        private void CuentaRegresiva()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Tick += Timer_Tick;
            dispatcherTimer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (tiempoDisponible >= TIEMPOLIMITE)
            {
                TiempoLabel.Content = tiempoDisponible.ToString();
                tiempoDisponible--;
            }
            else
            {
                dispatcherTimer.Stop();
                Loteria loteria = new Loteria(jugador, nombreUsuario, tableroJuego, modoJuego);
                DesplegarVentana(loteria);
            }
        }

        private void DesplegarVentana(Window ventana)
        {
            ventana.Show();
            this.Close();
        }

        private void ButtonEnviar_Click(object sender, RoutedEventArgs e)
        {
            string mensaje = MensajeTextBox.Text.Trim();
            InstanceContext instanceContext = new InstanceContext(this);
            UserManagerClient userManagerClient = new UserManagerClient(instanceContext);
            userManagerClient.EnviarMensajeChat(nombreUsuario, mensaje, jugador.NombreJugador);
        }

        public void DevuelveCuenta(Jugador jugador)
        {
            throw new System.NotImplementedException();
        }

        public void FinPartida(string mensaje)
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

        public void Respuesta(string mensaje)
        {
            throw new System.NotImplementedException();
        }
    }
}
