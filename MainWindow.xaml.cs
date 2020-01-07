using CorreoDeVerificacion;
using LoterestTcs.Model;
using LoterestTcs.ServiceReferenceLoterest;
using System;
using System.Linq;
using System.ServiceModel;
using System.Windows;

namespace LoterestTcs
{
    [CallbackBehavior(UseSynchronizationContext = false)]
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// Ventana principal en la que se muestra el inicio de sesión y el registro del jugador.
    /// </summary>
    public partial class MainWindow : Window, IUserManagerCallback
    {
        private Jugador jugador;

        /// <summary>
        /// Inicialización de componentes de ventana MainWindow.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SiguienteButtonCrearCuenta_Click(object sender, RoutedEventArgs e)
        {
            EnvioDeCorreo envioDeCorreo = new EnvioDeCorreo();
            string codigoVerificacion = GenerarCodigoVerificacion().ToString();

            string NombreJugador = NombreTextBoxCrearCuenta.Text.Trim();
            string CorreoJugador = CorreoTextBoxCrearCuenta.Text.Trim();
            string ContraseñaJugador = ContraseñaBoxCrearCuenta.Password.Trim();

            string repetirContraseña = RepetirContraseñaBoxCrearCuenta.Password.Trim();

            if (Equals(ContraseñaJugador, repetirContraseña))
            {
                if (ValidarDatosIngresadosRegistro(NombreJugador, CorreoJugador, ContraseñaJugador, repetirContraseña))
                {
                    if (ValidarCorreo(CorreoJugador))
                    {
                        if (envioDeCorreo.EnviarCorreo(CorreoJugador, codigoVerificacion))
                        {
                            Usuario usuario = new Usuario()
                            {
                                NombreUsuario = NombreJugador,
                                CorreoUsuario = CorreoJugador,
                                ContraseñaUsuario = ContraseñaJugador
                            };

                            Verificacion verificación = new Verificacion(codigoVerificacion, usuario);
                            DesplegarVentana(verificación);
                        }
                        else
                        {
                            MessageBox.Show(Application.Current.Resources["ErrorEnvioDeCorreo"].ToString());
                        }
                    }
                    else
                    {
                        MessageBox.Show(Application.Current.Resources["CorreoInvalido"].ToString());
                        CorreoTextBoxCrearCuenta.Clear();
                    }
                }
                else
                {
                    MessageBox.Show(Application.Current.Resources["DatosInvalidos"].ToString());
                }
            }
            else
            {
                MessageBox.Show(Application.Current.Resources["ContraseñasNoCoinciden"].ToString()); ContraseñaBoxCrearCuenta.Clear();
                RepetirContraseñaBoxCrearCuenta.Clear();
            }
        }

        private bool ValidarDatosIngresadosRegistro(string nombreJugador, string correoJugador, string contraseñaJugador, string repetirContraseñaJugador)
        {
            bool datosValidos = false;

            if ((nombreJugador != "") && (correoJugador != "") && (contraseñaJugador != "") && (repetirContraseñaJugador != ""))
            {
                datosValidos = true;
                return datosValidos;
            }
            else
            {
                return datosValidos;
            }
        }

        private bool ValidarCorreo(String correoJugador)
        {
            bool correoValido = false;

            if ((correoJugador.Contains("@gmail.com")) ||
                (correoJugador.Contains("@hotmail.com")) ||
                (correoJugador.Contains("@yahoo")) ||
                (correoJugador.Contains("@uv.mx")) ||
                (correoJugador.Contains("@estudiantes.uv.mx")) ||
                (correoJugador.Contains("@outlook.com")))
            {
                correoValido = true;
                return correoValido;
            }
            else
            {
                return correoValido;
            }
        }

        private int GenerarCodigoVerificacion()
        {
            int codigoDeVerificacion;
            Random numeroAleatorio = new Random();
            codigoDeVerificacion = numeroAleatorio.Next(100000, 999999);
            return codigoDeVerificacion;
        }

        private void DesplegarVentana(Window ventana)
        {
            ventana.Show();
            this.Close();
        }

        private void IngresarButtonIniciarSesion_Click(object sender, RoutedEventArgs e)
        {
            string nombreUsuario = NombreTextBoxIniciarSesion.Text.Trim();
            string contraseñaUsuario = ContraseñaBoxIniciarSesion.Password.Trim();
            try
            {
                InstanceContext instanceContext = new InstanceContext(this);
                UserManagerClient userManagerClient = new UserManagerClient(instanceContext);
                if (ValidarDatosInicioSesion(nombreUsuario, contraseñaUsuario))
                {
                    userManagerClient.IniciarSesion(nombreUsuario, contraseñaUsuario);
                }
                else
                {
                    MessageBox.Show(Application.Current.Resources["DatosInvalidos"].ToString());
                }
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show(Application.Current.Resources["OperacionInvalida"].ToString());
            }
        }

        private bool ValidarDatosInicioSesion(string nombreJugador, string contraseñaJugador)
        {
            bool datosValidos = false;

            if ((nombreJugador != "") && (contraseñaJugador != ""))
            {
                datosValidos = true;
                return datosValidos;
            }
            else
            {
                return datosValidos;
            }
        }

        private void VerContraseña_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            VerContraseñaTextBox.Visibility = Visibility.Visible;
            ContraseñaBoxCrearCuenta.Visibility = Visibility.Hidden;
            VerContraseñaTextBox.Text = ContraseñaBoxCrearCuenta.Password;
        }

        private void VerContraseña_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            VerContraseñaTextBox.Visibility = Visibility.Hidden;
            ContraseñaBoxCrearCuenta.Visibility = Visibility.Visible;
            VerContraseñaTextBox.Text = String.Empty;
        }

        private void VerContraseñaRepetida_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            VerContraseñaRepetidaTextBox.Visibility = Visibility.Visible;
            RepetirContraseñaBoxCrearCuenta.Visibility = Visibility.Hidden;
            VerContraseñaRepetidaTextBox.Text = RepetirContraseñaBoxCrearCuenta.Password;
        }

        private void VerContraseñaRepetida_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            VerContraseñaRepetidaTextBox.Visibility = Visibility.Hidden;
            RepetirContraseñaBoxCrearCuenta.Visibility = Visibility.Visible;
            VerContraseñaRepetidaTextBox.Text = String.Empty;
        }

        private void VerContraseñaIniciarSesion_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            VerContraseñaTextBoxIniciarSesión.Visibility = Visibility.Visible;
            ContraseñaBoxIniciarSesion.Visibility = Visibility.Hidden;
            VerContraseñaTextBoxIniciarSesión.Text = ContraseñaBoxIniciarSesion.Password;
        }

        private void VerContraseñaIniciarSesion_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            VerContraseñaTextBoxIniciarSesión.Visibility = Visibility.Hidden;
            ContraseñaBoxIniciarSesion.Visibility = Visibility.Visible;
            VerContraseñaTextBoxIniciarSesión.Text = String.Empty;
        }

        /// <summary>
        /// Implementación de interfaz  de IUserManagerCallback.
        /// </summary>
        /// <param name="mensaje">Recibe un parametro mensaje de tipo string</param>
        public void Respuesta(string mensaje)
        {
            mensaje = Application.Current.Resources["CredencialesInvalidas"].ToString();
            MessageBox.Show(mensaje);
        }

        /// <summary>
        /// Implementación de interfaz  de IUserManagerCallback.
        /// </summary>
        /// <param name="jugador">Recibe un parametro jugador de tipo jugador</param>
        public void DevuelveCuenta(Jugador jugador)
        {
            this.Dispatcher.Invoke(() =>
            {
                this.jugador = jugador;
                Menu menu = new Menu(jugador);
                DesplegarVentana(menu);
            });
        }

        public void MensajeChat(string mensaje)
        {
            SalaChat.ObtenerMensaje(mensaje);
        }

        public void MostrarPuntajes(PuntajeUsuario[] puntajesUsuarios)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Implementación de interfaz  de IUserManagerCallback.
        /// </summary>
        /// <param name="opcion">Recibe un parametro opcion de tipo bool</param>
        /// <param name="nombreUsuario">Recibe un parametro nombre de usuario de tipo string</param>
        /// <param name="modoJuego">Recibe un parametro modo de juego de tipo string</param>
        public void RecibirConfirmacion(bool opcion, string nombreUsuario, string modoJuego)
        {
            if (opcion == true)
            {
                if (modoJuego.Equals("Loteria"))
                {
                    MessageBox.Show(Application.Current.Resources["ConfirmacionInvitacion"].ToString());
                    Dispatcher.Invoke(() =>
                    {
                        Tablero tablero = new Tablero(jugador, 60, nombreUsuario);
                        this.Close();
                        tablero.Show();
                    });
                }

                if (modoJuego.Equals("AlAzar"))
                {
                    MessageBox.Show(Application.Current.Resources["ConfirmacionInvitacion"].ToString());
                    Dispatcher.Invoke(() =>
                    {
                        AlAzar alAzar = new AlAzar(jugador, 60, nombreUsuario);
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

        /// <summary>
        /// Implementación de interfaz  de IUserManagerCallback.
        /// </summary>
        /// <param name="nombreUsuario">Recibe un parametro nombre de usuario de tipo string</param>
        /// <param name="mensajeUsuario">Recibe un parametro mensaje usuario de tipo string</param>
        /// <param name="modoJuego">Recibe un parametro modo de juego de tipo string</param>
        public void RecibirInvitacion(string nombreUsuario, string mensajeUsuario, string modoJuego)
        {
            Dispatcher.Invoke(() =>
            {
                ConfirmacionDeInvitacion confirmacionDeInvitacion = new ConfirmacionDeInvitacion(jugador, nombreUsuario, modoJuego, mensajeUsuario);
                DesplegarVentana(confirmacionDeInvitacion);
            });
        }

        public void FinPartida(string mensaje)
        {
            MessageBox.Show(mensaje, "Fin partida", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void EspañolRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var resources = new ResourceDictionary();
            resources.Source = new Uri("pack://application:,,,/Idioma/Strings.xaml");
            Application.Current.Resources.MergedDictionaries.Add(resources);
        }

        private void InglesRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var resources = new ResourceDictionary();
            resources.Source = new Uri("pack://application:,,,/Idioma/Strings_en_US.xaml");
            Application.Current.Resources.MergedDictionaries.Add(resources);

        }
    }
}