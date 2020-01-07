using LoterestTcs.Model;
using LoterestTcs.ServiceReferenceLoterest;
using System.Collections.Generic;
using System.ServiceModel;
using System.Windows;

namespace LoterestTcs
{
    /// <summary>
    /// Lógica de interacción para Puntajes.xaml
    /// Ventana que muestra los puntajes de los jugadores en orden de mayor a menor.
    /// </summary>
    public partial class PuntajesJugadores : Window, IUserManagerCallback
    {
        private Jugador jugador;

        /// <summary>
        /// Inicialización de componentes de ventana PuntajesJugadores.
        /// </summary>
        /// <param name="jugador">Recibe un parametro jugador de tipo jugador</param>
        public PuntajesJugadores(Jugador jugador)
        {
            InitializeComponent();
            NombreJugadorLabel.Content = jugador.NombreJugador;
            try
            {
                InstanceContext instanceContext = new InstanceContext(this);
                UserManagerClient userManagerClient = new UserManagerClient(instanceContext);
                userManagerClient.SolicitarPuntaje();
                this.jugador = jugador;
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show(Application.Current.Resources["OperacionInvalida"].ToString());
            }
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

        /// <summary>
        /// Implementación de interfaz  de IUserManagerCallback.
        /// </summary>
        /// <param name="puntajesUsuarios">Recibe una lista puntaje usuario de tipo puntaje usuarios</param>
        public void MostrarPuntajes(PuntajeUsuario[] puntajesUsuarios)
        {
            List<Puntajes> puntajes = new List<Puntajes>();
            foreach (var valor in puntajesUsuarios)
            {
                puntajes.Add(new Puntajes() { NombreUsuario = valor.nombreUsuario, PuntajeUsuario = valor.puntajeUsuario });
            }
            this.Dispatcher.Invoke(() =>
            {
                PuntajeLoteriaListView.ItemsSource = puntajes;
            });
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
