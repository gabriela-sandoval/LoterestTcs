using CorreoDeVerificacion;
using LoterestTcs.Model;
using LoterestTcs.ServiceReferenceLoterest;
using System;
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
                            MessageBox.Show("Error al enviar el correo", "Correo no enviado", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Correo inválido, intente nuevamente", "Correo inválido", MessageBoxButton.OK, MessageBoxImage.Error);
                        CorreoTextBoxCrearCuenta.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Algún campo se encuentra vacío, intente nuevamente", "Campos inválidos", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Las contraseñas no coinciden, intente nuevamente", "Contraseñas no coinciden", MessageBoxButton.OK, MessageBoxImage.Error);
                ContraseñaBoxCrearCuenta.Clear();
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
                    MessageBox.Show("Algún campo se encuentra vacío, intente nuevamente", "Campos inválidos", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Operación inválida, intente nuevamente", "Operación inválida", MessageBoxButton.OK, MessageBoxImage.Error);
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
            MessageBox.Show(mensaje, "Iniciar sesión", MessageBoxButton.OK, MessageBoxImage.Information);
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
            throw new NotImplementedException();
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
                    MessageBox.Show("Se aceptó tu invitación", "Confirmación", MessageBoxButton.OK, MessageBoxImage.Information);
                    Dispatcher.Invoke(() =>
                    {
                        Tablero tablero = new Tablero(jugador, 60, nombreUsuario);
                        this.Close();
                        tablero.Show();
                    });
                }

                if (modoJuego.Equals("AlAzar"))
                {
                    MessageBox.Show("Se aceptó tu invitación", "Confirmación", MessageBoxButton.OK, MessageBoxImage.Information);
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
                MessageBox.Show("No aceptó tu invitación", "Confirmación", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void IdiomaComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            string idioma = IdiomaComboBox.Text.Trim();

            if (idioma.Equals("ES Español"))
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("es-MX");
            }
            else
            {
                if (idioma.Equals("EN English"))
                {
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
                }
            }
        }
    }
}