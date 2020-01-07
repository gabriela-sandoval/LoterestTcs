using LoterestTcs.ServiceReferenceLoterest;
using System;
using System.ServiceModel;
using System.Windows;

namespace LoterestTcs
{
    /// <summary>
    /// Lógica de interacción para PerfilJugador.xaml
    /// Ventana que muestra el perfil del jugador y permite modificar la contraseña.
    /// </summary>
    public partial class PerfilJugador : Window, IUserManagerCallback
    {
        private Jugador jugador;

        /// <summary>
        /// Inicialización de componentes de ventana PerfilJugador.
        /// </summary>
        /// <param name="jugadorRecibido">Recibe un parametro jugador de tipo jugador</param>
        public PerfilJugador(Jugador jugadorRecibido)
        {
            InitializeComponent();
            jugador = jugadorRecibido;
            NombreTextBoxModificarCuenta.Text = jugadorRecibido.NombreJugador;
            ContraseñaBoxModificarCuenta.Password = jugadorRecibido.ContraseñaJugador;
            RepetirContraseñaBoxModificarCuenta.Password = jugadorRecibido.ContraseñaJugador;
            NombreJugadorLabel.Content = jugadorRecibido.NombreJugador;
        }

        private void LimpiarButtonModificarCuenta_Click(object sender, RoutedEventArgs e)
        {
            ContraseñaBoxModificarCuenta.Password = "";
            RepetirContraseñaBoxModificarCuenta.Password = "";
            ContinuarButtonModificarCuenta.IsEnabled = true;
        }

        private void ContinuarButtonModificarCuenta_Click(object sender, RoutedEventArgs e)
        {
            string nuevoNombreUsuario = NombreTextBoxModificarCuenta.Text.Trim();
            string nuevaContraseña = ContraseñaBoxModificarCuenta.Password.Trim();
            string repetirNuevaContraseña = RepetirContraseñaBoxModificarCuenta.Password.Trim();

            if (nuevaContraseña.Equals(repetirNuevaContraseña))
            {
                if (ValidarDatosIngresados(nuevoNombreUsuario, nuevaContraseña, repetirNuevaContraseña))
                {
                    ModificarCuenta(nuevoNombreUsuario, nuevaContraseña);
                    MainWindow mainWindow = new MainWindow();
                    DesplegarVentana(mainWindow);
                }
                else
                {

                    MessageBox.Show(Application.Current.Resources["DatosInvalidos"].ToString());
                }
            }
            else
            {
                MessageBox.Show(Application.Current.Resources["ContraseñasNoCoinciden"].ToString());
            }

        }

        private void DesplegarVentana(Window ventana)
        {
            ventana.Show();
            this.Close();
        }

        private bool ValidarDatosIngresados(string nombreUsuario, string contraseñaUsuario, string repetirContraseñaUsuario)
        {
            bool datosValidos = false;

            if (nombreUsuario != "" && contraseñaUsuario != "" && repetirContraseñaUsuario != "")
            {
                datosValidos = true;
                return datosValidos;
            }
            else
            {
                return datosValidos;
            }
        }

        private void ModificarCuenta(string nombreModificado, string contraseñaModificada)
        {
            try
            {
                InstanceContext instanceContext = new InstanceContext(this);
                UserManagerClient userManagerClient = new UserManagerClient(instanceContext);
                jugador.NombreJugador = nombreModificado;
                jugador.ContraseñaJugador = contraseñaModificada;
                userManagerClient.CambiarDatosJugador(jugador);
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

        private void VerContraseña_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            VerContraseñaTextBox.Visibility = Visibility.Visible;
            ContraseñaBoxModificarCuenta.Visibility = Visibility.Hidden;
            VerContraseñaTextBox.Text = ContraseñaBoxModificarCuenta.Password;
        }

        private void VerContraseña_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            VerContraseñaTextBox.Visibility = Visibility.Hidden;
            ContraseñaBoxModificarCuenta.Visibility = Visibility.Visible;
            VerContraseñaTextBox.Text = String.Empty;
        }

        private void VerContraseñaRepetida_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            VerContraseñaRepetidaTextBox.Visibility = Visibility.Visible;
            RepetirContraseñaBoxModificarCuenta.Visibility = Visibility.Hidden;
            VerContraseñaRepetidaTextBox.Text = RepetirContraseñaBoxModificarCuenta.Password;
        }

        private void VerContraseñaRepetida_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            VerContraseñaRepetidaTextBox.Visibility = Visibility.Hidden;
            RepetirContraseñaBoxModificarCuenta.Visibility = Visibility.Visible;
            VerContraseñaRepetidaTextBox.Text = String.Empty;
        }

        /// <summary>
        /// Implementación de interfaz  de IUserManagerCallback.
        /// </summary>
        /// <param name="mensaje">Recibe un parametro mensaje de tipo string</param>
        public void Respuesta(string mensaje)
        {
            MessageBox.Show(mensaje, "Perfil", MessageBoxButton.OK, MessageBoxImage.Information);
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
            throw new NotImplementedException();
        }

        public void RecibirInvitacion(string nombreUsuario, string mensajeUsuario, string modoJuego)
        {
            throw new NotImplementedException();
        }

        public void FinPartida(string mensaje)
        {
            throw new NotImplementedException();
        }
    }
}
