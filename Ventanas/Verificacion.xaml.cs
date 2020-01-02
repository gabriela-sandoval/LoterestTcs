using System.Windows;
using System.ServiceModel;
using LoterestTcs.ServiceReferenceLoterest;
using LoterestTcs.Model;
using System;

namespace LoterestTcs.Ventanas
{
    /// <summary>
    /// Lógica de interacción para Verificacion.xaml
    /// </summary>
    public partial class Verificacion : Window, IUserManagerCallback
    {
        private string codigoVerificacion;
        private Usuario usuarioCreado;

        public Verificacion(string codigoGenerado, Usuario usuario)
        {
            InitializeComponent();
            codigoVerificacion = codigoGenerado;
            usuarioCreado = usuario;
        }

        private void VerificarButton_Click(object sender, RoutedEventArgs e)
        {
            string codigoIngresado = VerificacionTextBox.Text.Trim();

            if (ValidarCodigoIngresado(codigoIngresado))
            {
                if (String.Equals(codigoIngresado, codigoVerificacion))
                {
                    RegistrarJugador();
                }
                else
                {
                    MessageBox.Show("Código incorrecto, intente nuevamente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Campos vacios, intente nuevamente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidarCodigoIngresado(string codigoVerificacion)
        {
            bool codigoValido = false;

            if (codigoVerificacion != "")
            {
                codigoValido = true;
                return codigoValido;
            }
            else
            {
                return codigoValido;
            }
        }

        private void CancelarButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Deseas salir? ¡Perderás todo tu registro!", "Cancelar", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    MessageBox.Show("¡Que mal! Es una lastima que te vallas, ¡vuelve pronto!", "Cancelar");
                    MainWindow mainWindow = new MainWindow();
                    DesplegarVentana(mainWindow);
                    break;
                case MessageBoxResult.No:
                    MessageBox.Show("¡Nos alegra que te quedes!, ahora puedes continuar con tu registro", "Cancelar");
                    break;
            }
        }

        private void DesplegarVentana(Window ventana)
        {
            ventana.Show();
            this.Close();
        }

        private void RegistrarJugador()
        {
            try
            {
                InstanceContext instanceContext = new InstanceContext(this);
                UserManagerClient userManagerClient = new UserManagerClient(instanceContext);
                Jugador jugador = new Jugador()
                {
                    NombreJugador = usuarioCreado.NombreUsuario,
                    CorreoJugador = usuarioCreado.CorreoUsuario,
                    ContraseñaJugador = usuarioCreado.ContraseñaUsuario,
                    PuntajeJugador = 0
                };

                userManagerClient.CrearCuentaJugador(jugador);
                userManagerClient.IniciarSesion(usuarioCreado.NombreUsuario, usuarioCreado.ContraseñaUsuario);
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Operación inválida", "Crear cuenta");
            }
        }

        public void Respuesta(string mensaje)
        {
            MessageBox.Show(mensaje, "Crear cuenta", MessageBoxButton.OK, MessageBoxImage.Information);
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
