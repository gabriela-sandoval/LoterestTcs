﻿using LoterestTcs.ServiceReferenceLoterest;
using System;
using System.ServiceModel;
using System.Windows;

namespace LoterestTcs.Ventanas
{
    /// <summary>
    /// Lógica de interacción para PerfilJugador.xaml
    /// </summary>
    public partial class PerfilJugador : Window, IUserManagerCallback
    {
        private Jugador jugador;

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

            if(nuevaContraseña.Equals(repetirNuevaContraseña))
            {
                if(ValidarDatosIngresados(nuevoNombreUsuario, nuevaContraseña, repetirNuevaContraseña))
                {
                    ModificarCuenta(nuevoNombreUsuario, nuevaContraseña);
                    Menu menu = new Menu(jugador);
                    DesplegarVentana(menu);
                }
                else
                {

                    MessageBox.Show("Algún campo se encuentra vacío, intente nuevamente", "Campos inválidos", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Las contraseñas no coinciden, intente nuevamente", "Contraseñas incorrectas", MessageBoxButton.OK, MessageBoxImage.Error);
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
            }catch(EndpointNotFoundException)
            {
                MessageBox.Show("Operación inválida, intente nuevamente", "Operación inválida", MessageBoxButton.OK, MessageBoxImage.Error);
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
