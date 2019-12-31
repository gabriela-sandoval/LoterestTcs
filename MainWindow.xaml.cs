using System;
using System.ServiceModel;
using System.Windows;
using CorreoDeVerificacion;
using LoterestTcs.ServiceReference;
using LoterestTcs.Ventanas;

namespace LoterestTcs
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        ServiceReference.Service1Client cliente = new ServiceReference.Service1Client();

        private void SiguienteButtonCrearCuenta_Click(object sender, RoutedEventArgs e)
        {
            EnvioDeCorreo envioDeCorreo = new EnvioDeCorreo();
            string codigoVerificacion = GenerarCodigoVerificacion().ToString();

            Jugador nuevoJugador = new Jugador();
            nuevoJugador.NombreJugador = NombreTextBoxCrearCuenta.Text.Trim();
            nuevoJugador.CorreoJugador = CorreoTextBoxCrearCuenta.Text.Trim();
            nuevoJugador.ContraseñaJugador = ContraseñaBoxCrearCuenta.Password.Trim();

            string repetirContraseña = RepetirContraseñaBoxCrearCuenta.Password.Trim();

            if (Equals(nuevoJugador.ContraseñaJugador, repetirContraseña))
            {
                if (ValidarDatosIngresadosRegistro(nuevoJugador.NombreJugador, nuevoJugador.CorreoJugador, nuevoJugador.ContraseñaJugador, repetirContraseña))
                {
                    if (ValidarCorreo(nuevoJugador.CorreoJugador))
                    {
                        if (envioDeCorreo.EnviarCorreo(nuevoJugador.CorreoJugador, codigoVerificacion))
                        {
                            Verificacion verificación = new Verificacion(codigoVerificacion, nuevoJugador);
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

        private bool ValidarCorreo(string correoJugador)
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
            JugadorRegistrado jugadorRegistrado = new JugadorRegistrado();
            jugadorRegistrado.NombreJugador = NombreTextBoxIniciarSesion.Text.Trim();
            jugadorRegistrado.ContraseñaJugador = ContaseñaBoxIniciarSesion.Password.Trim();

            if (ValidarDatosInicioSesion(jugadorRegistrado.NombreJugador, jugadorRegistrado.ContraseñaJugador))
            {
                try
                {
                    string ejecutar = cliente.IniciarSesion(jugadorRegistrado);
                    string mensaje = ejecutar.ToString();
                    MessageBox.Show(mensaje, "Inicio de sesión", MessageBoxButton.OK, MessageBoxImage.Information);
                    //Menu menu = new Menu(jugadorRegistrado.NombreJugador);
                    //DesplegarVentana(menu);
                }
                catch (EndpointNotFoundException)
                {
                    MessageBox.Show("Servidor no disponible, intente más tarde", "Servidor no disponible", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Algún campo se encuentra vacío, intente nuevamente", "Campos inválidos", MessageBoxButton.OK, MessageBoxImage.Error);
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
            ContaseñaBoxIniciarSesion.Visibility = Visibility.Hidden;
            VerContraseñaTextBoxIniciarSesión.Text = ContaseñaBoxIniciarSesion.Password;
        }

        private void VerContraseñaIniciarSesion_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            VerContraseñaTextBoxIniciarSesión.Visibility = Visibility.Hidden;
            ContaseñaBoxIniciarSesion.Visibility = Visibility.Visible;
            VerContraseñaTextBoxIniciarSesión.Text = String.Empty;
        }
    }
}