using LoterestTcs.ServiceReference;
using System.Windows;
using LoterestTcs;
using System.ServiceModel;

namespace LoterestTcs.Ventanas
{
    /// <summary>
    /// Lógica de interacción para Verificacion.xaml
    /// </summary>
    public partial class Verificacion : Window
    {
        ServiceReference.Service1Client cliente = new ServiceReference.Service1Client();
        private string codigoVerificacion;
        private Jugador nuevoJugador;
        public Verificacion(string codigoGenerado, Jugador jugador)
        {
            InitializeComponent();
            codigoVerificacion = codigoGenerado;
            nuevoJugador = jugador;
        }

        private void VerificarButton_Click(object sender, RoutedEventArgs e)
        {
            string codigoIngresado = VerificacionTextBox.Text.Trim();

            if (ValidarCodigoIngresado(codigoIngresado))
            {
                if (string.Equals(codigoIngresado, codigoVerificacion))
                {
                    try
                    {
                        string ejecutar = cliente.Agregar(nuevoJugador);
                        string mensaje = ejecutar.ToString();
                        MessageBox.Show(mensaje, "Verificación", MessageBoxButton.OK, MessageBoxImage.Information);
                        Menu menu = new Menu(nuevoJugador.NombreJugador);
                        DesplegarVentana(menu);
                    }
                    catch (EndpointNotFoundException)
                    {
                        MessageBox.Show("Servidor no disponible, intente más tarde", "Servidor no disponible", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
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


    }
}
