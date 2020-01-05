using LoterestTcs.Model;
using LoterestTcs.ServiceReferenceLoterest;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace LoterestTcs.Ventanas
{
    /// <summary>
    /// Lógica de interacción para Loteria.xaml
    /// </summary>
    public partial class Loteria : Window, IUserManagerCallback
    {
        private Jugador jugador;
        private TableroJuego tableroJuego;
        private int tiempoDisponible = 5;
        private int numeroCartaActual = 0;
        private int puntajeJugador = 3600;
        private string nombreUsuario;
        private string modoJuego;
        private List<Image> cartasMarcadas = new List<Image>();
        private List<Image> imagenesVisibles = new List<Image>();
        private List<Image> mazoCartas = new List<Image>();
        private DispatcherTimer dispatcherTimer;

        public Loteria(Jugador jugador, string nombreUsuario, TableroJuego tableroPartida, string modoJuegoElegido)
        {
            InitializeComponent();
            this.jugador = jugador;
            this.nombreUsuario = nombreUsuario;
            tableroJuego = tableroPartida;
            modoJuego = modoJuegoElegido;
            GuardarCartasVisibles();
            CrearMazoCartas();
            MostrarImagenesVisibles();
            CambiarCarta(numeroCartaActual);
            CuentaRegresiva();
        }

        private void GuardarCartasVisibles()
        {
            imagenesVisibles.Add(ImageCarta1_1);
            imagenesVisibles.Add(ImageCarta1_2);
            imagenesVisibles.Add(ImageCarta1_3);
            imagenesVisibles.Add(ImageCarta1_4);
            imagenesVisibles.Add(ImageCarta2_1);
            imagenesVisibles.Add(ImageCarta2_2);
            imagenesVisibles.Add(ImageCarta2_3);
            imagenesVisibles.Add(ImageCarta2_4);
            imagenesVisibles.Add(ImageCarta3_1);
            imagenesVisibles.Add(ImageCarta3_2);
            imagenesVisibles.Add(ImageCarta3_3);
            imagenesVisibles.Add(ImageCarta3_4);
            imagenesVisibles.Add(ImageCarta4_1);
            imagenesVisibles.Add(ImageCarta4_2);
            imagenesVisibles.Add(ImageCarta4_3);
            imagenesVisibles.Add(ImageCarta4_4);
        }

        void CrearMazoCartas()
        {
            Image image;
            for(int i = 1; i <=54; i++)
            {
                image = new Image();
                Uri uri = new Uri("LoterestTcs/Cartas/" + i.ToString() + ".png", UriKind.Relative);
                image.Source = new BitmapImage(uri);
                mazoCartas.Add(image);
            }
        }

        public void MostrarImagenesVisibles()
        {
            for(int i = 0; i <= 15; i++)
            {
                imagenesVisibles[i].Source = tableroJuego.CartasDeTabla[i].Source;
            }
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
            if(tiempoDisponible >= 0)
            {
                TiempoTextBlock.Text = tiempoDisponible.ToString();
                tiempoDisponible--;
            }
            else
            {
                dispatcherTimer.Stop();
                if (numeroCartaActual <= 53)
                {
                    if (cartasMarcadas.Count < 16)
                    {
                        tiempoDisponible = 5;
                        CambiarCarta(numeroCartaActual);
                        dispatcherTimer.Start();
                    }
                    else
                    {
                        try
                        {
                            InstanceContext instanceContext = new InstanceContext(this);
                            UserManagerClient userManagerClient = new UserManagerClient(instanceContext);
                            userManagerClient.FinalizarJuego(jugador.NombreJugador, nombreUsuario);
                            userManagerClient.PuntajeMaximo(jugador.NombreJugador, puntajeJugador);
                            this.Close();
                        }
                        catch (EndpointNotFoundException)
                        {
                            MessageBox.Show("Operación inválida, intente nuevamente", "Operación inválida", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    if(numeroCartaActual > 16)
                    {
                        puntajeJugador = puntajeJugador - 100;
                    }
                }
                else
                {
                    this.Close();
                }
            }
        }

        void CambiarCarta(int nuevoNumeroDeCarta)
        {
            CartaImage.Source = mazoCartas[nuevoNumeroDeCarta].Source;
            numeroCartaActual++;
        }

        private void AgregarCartaMarcada(Image image)
        {
            if(CompararCartas(image))
            {
                cartasMarcadas.Add(image);
            }
        }

        private bool CompararCartas(Image image)
        {
            if(CartaImage.Source.ToString().Equals(image.Source.ToString()))
            {
                image.Opacity = .5;
                return true;
            }
            else
            {
                MessageBox.Show("Selección de carta inválida", "Selección inválida", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
        }

        private void ImageCarta1_1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AgregarCartaMarcada(ImageCarta1_1);
        }

        private void ImageCarta1_2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AgregarCartaMarcada(ImageCarta1_2);
        }

        private void ImageCarta1_3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AgregarCartaMarcada(ImageCarta1_3);
        }

        private void ImageCarta1_4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AgregarCartaMarcada(ImageCarta1_4);
        }

        private void ImageCarta2_1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AgregarCartaMarcada(ImageCarta2_1);
        }

        private void ImageCarta2_2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AgregarCartaMarcada(ImageCarta2_2);
        }

        private void ImageCarta2_3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AgregarCartaMarcada(ImageCarta2_3);
        }

        private void ImageCarta2_4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AgregarCartaMarcada(ImageCarta2_4);
        }

        private void ImageCarta3_1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AgregarCartaMarcada(ImageCarta3_1);
        }

        private void ImageCarta3_2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AgregarCartaMarcada(ImageCarta3_2);
        }

        private void ImageCarta3_3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AgregarCartaMarcada(ImageCarta3_3);
        }

        private void ImageCarta3_4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AgregarCartaMarcada(ImageCarta3_4);
        }

        private void ImageCarta4_1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AgregarCartaMarcada(ImageCarta4_1);
        }

        private void ImageCarta4_2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AgregarCartaMarcada(ImageCarta4_2);
        }

        private void ImageCarta4_3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AgregarCartaMarcada(ImageCarta4_3);
        }

        private void ImageCarta4_4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AgregarCartaMarcada(ImageCarta4_4);
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
