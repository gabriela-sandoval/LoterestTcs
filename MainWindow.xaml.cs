using System;
using System.ServiceModel;
using System.Windows;
using CorreoDeVerificacion;
using LoterestTcs.Ventanas;
using LoterestTcs.ServiceReferenceLoterest;
using System.Threading.Tasks;
using LoterestTcs.Model;

namespace LoterestTcs
{
    [CallbackBehavior(UseSynchronizationContext = false)]
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IUserManagerCallback
    {
        private Jugador jugador;
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
                if(ValidarDatosInicioSesion(nombreUsuario, contraseñaUsuario))
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

        public void Respuesta(string mensaje)
        {
            MessageBox.Show(mensaje, "Iniciar sesión", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void DevuelveCuenta(Jugador jugador)
        {
            this.Dispatcher.Invoke(() =>
            {
                Menu ventana = new Menu(jugador);
                DesplegarVentana(ventana);

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

        public void RecibirConfirmacion(bool opcion, string nombreUsuario)
        {
            throw new NotImplementedException();
        }

        public void RecibirInvitacion(string nombreUsuario, string mensajeUsuario)
        {
            throw new NotImplementedException();
        }

        public void FinPartida(string mensaje)
        {
            throw new NotImplementedException();
        }
    }
}