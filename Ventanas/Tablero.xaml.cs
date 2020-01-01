using LoterestTcs.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace LoterestTcs.Ventanas
{
    /// <summary>
    /// Lógica de interacción para Tablero.xaml
    /// </summary>
    public partial class Tablero : Window
    {
        private int tiempoDisponible;
        private DispatcherTimer timer;
        private List<Image> imagenesDisponibles = new List<Image>();
        private List<int> numeroDeImagenSeleccionada = new List<int>();
        private List<Image> imagenesOcultas = new List<Image>();
        private List<Image> imagenesSeleccionadas = new List<Image>();
        private TableroJuego tableroJuego;
        private const int CANTIDADCARTAS = 16;
        private const int TIEMPOPARAELEGIRCARTAS = 0;
        public Tablero(int tiempo)
        {
            InitializeComponent();
            CartasDisponibles();
            tiempoDisponible = tiempo;
            LugaresTablero();
            
        }

        private void CartasDisponibles()
        {
            Image image;
            for (int i = 1; i <= 52; i++)
            {
                image = new Image();
                Uri uri = new Uri("Cartas/" + i.ToString() + ".png", UriKind.Relative);
                image.Source = new BitmapImage(uri);
                imagenesDisponibles.Add(image);
            }
        }

        private void GenerarTableroAleatorio()
        {
            imagenesSeleccionadas.Clear();
            SeleccionarImagenesAleatorias(numeroDeImagenSeleccionada);

            for(int i = 0; i<=15; i++)
            {
                imagenesSeleccionadas.Add(AgregarImagenALista(i));
            }
        }

        private Image AgregarImagenALista(int indice)
        {
            Image imagen = imagenesDisponibles[numeroDeImagenSeleccionada[indice]];
            return imagen;
        }

        private void SeleccionarImagenesAleatorias(List<int> imagenes)
        {
            int numeroImagen;
            while (imagenes.Count <CANTIDADCARTAS)
            {
                numeroImagen = NumeroAleatorio();
                if(!imagenes.Contains(numeroImagen))
                {
                    imagenes.Add(numeroImagen);
                }
            }
        }

        private int NumeroAleatorio()
        {
            int numero;
            Random numeroAleatorio = new Random();
            numero = numeroAleatorio.Next(1, 54);
            return numero;
        }

        private void LugaresTablero()
        {
            imagenesSeleccionadas.Add(ImageCarta1_1);
            imagenesSeleccionadas.Add(ImageCarta1_2);
            imagenesSeleccionadas.Add(ImageCarta1_3);
            imagenesSeleccionadas.Add(ImageCarta1_4);
            imagenesSeleccionadas.Add(ImageCarta2_1);
            imagenesSeleccionadas.Add(ImageCarta2_2);
            imagenesSeleccionadas.Add(ImageCarta2_3);
            imagenesSeleccionadas.Add(ImageCarta2_4);
            imagenesSeleccionadas.Add(ImageCarta3_1);
            imagenesSeleccionadas.Add(ImageCarta3_2);
            imagenesSeleccionadas.Add(ImageCarta3_3);
            imagenesSeleccionadas.Add(ImageCarta3_4);
            imagenesSeleccionadas.Add(ImageCarta4_1);
            imagenesSeleccionadas.Add(ImageCarta4_2);
            imagenesSeleccionadas.Add(ImageCarta4_3);
            imagenesSeleccionadas.Add(ImageCarta4_4);
        }

        private void ValidarImagen(Image imagen)
        {
            if (imagenesOcultas.Count < CANTIDADCARTAS)
            {
                foreach (var imagenSeleccionada in imagenesSeleccionadas)
                {
                    if (imagenSeleccionada.Source == null)
                    {
                        OcultarCarta(imagen);
                        imagenSeleccionada.Source = imagen.Source;
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("¡Sólo puedes elegir 16 cartas!", "Eleccion máxima", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void OcultarCarta(Image imagen)
        {
            imagen.Visibility = Visibility.Hidden;
            imagenesOcultas.Add(imagen);
        }

        private void RestablecerImagen(Image imagenARestablecer)
        {
            foreach (Image imagen in imagenesOcultas)
            {
                if (imagen.Source.Equals(imagenARestablecer.Source))
                {
                    imagen.Visibility = Visibility.Visible;
                    imagenARestablecer.Source = null;
                    imagenesOcultas.Remove(imagen);
                    break;
                }
            }
        }

        private void CuentaRegresiva()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {

            if (tiempoDisponible >= TIEMPOPARAELEGIRCARTAS)
            {
                SegundosTextBlock.Text = tiempoDisponible.ToString();
                tiempoDisponible--;
            }
            else
            {
                timer.Stop();
                tableroJuego = new TableroJuego();

                if (imagenesOcultas.Count < CANTIDADCARTAS)
                {
                    GenerarTableroAleatorio();
                    tableroJuego.CartasDeTabla= imagenesSeleccionadas;
                    //Chat ventana = new Chat(tabla, cuenta, nombreUsuario);
                    //DesplegarVentana(ventana);
                }
                else
                {
                    tableroJuego.CartasDeTabla = imagenesSeleccionadas;
                    //Chat ventana = new Chat(tabla, cuenta, nombreUsuario);
                    //DesplegarVentana(ventana);
                }
            }
        }

        private void Image1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image1);
        }

        private void Image2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image2);
        }

        private void Image3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image3);
        }

        private void Image4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image4);
        }

        private void Image5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image5);
        }

        private void Image6_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image6);
        }

        private void Image7_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image7);
        }

        private void Image8_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image8);
        }

        private void Image9_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image9);
        }

        private void Image10_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image10);
        }

        private void Image11_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image11);
        }

        private void Image12_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image12);
        }

        private void Image13_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image13);
        }

        private void Image14_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image14);
        }

        private void Image15_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image15);
        }

        private void Image16_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image16);
        }

        private void Image17_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image17);
        }

        private void Image18_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image18);
        }

        private void Image19_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image19);
        }

        private void Image20_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image20);
        }

        private void Image21_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image21);
        }

        private void Image22_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image22);
        }

        private void Image23_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image23);
        }

        private void Image24_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image24);
        }

        private void Image25_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image25);
        }

        private void Image26_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image26);
        }

        private void Image27_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image27);
        }

        private void Image28_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image28);
        }

        private void Image29_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image29);
        }

        private void Image30_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image30);
        }

        private void Image31_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image31);
        }

        private void Image32_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image32);
        }

        private void Image33_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image33);
        }

        private void Image34_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image34);
        }

        private void Image35_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image35);
        }

        private void Image36_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image36);
        }

        private void Image37_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image37);
        }

        private void Image38_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image38);
        }

        private void Image39_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image39);
        }

        private void Image40_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image40);
        }

        private void Image41_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image41);
        }

        private void Image42_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image42);
        }

        private void Image43_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image43);
        }

        private void Image44_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image44);
        }

        private void Image45_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image45);
        }

        private void Image46_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image46);
        }

        private void Image47_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image47);
        }

        private void Image48_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image48);
        }

        private void Image49_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image49);
        }

        private void Image50_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image50);
        }

        private void Image51_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image51);
        }

        private void Image52_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image52);
        }

        private void Image53_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image53);
        }

        private void Image54_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ValidarImagen(Image54);
        }

        private void ImageCarta1_1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RestablecerImagen(ImageCarta1_1);
        }

        private void ImageCarta1_2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RestablecerImagen(ImageCarta1_2);
        }

        private void ImageCarta1_3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RestablecerImagen(ImageCarta1_3);
        }

        private void ImageCarta1_4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RestablecerImagen(ImageCarta1_4);
        }

        private void ImageCarta2_1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RestablecerImagen(ImageCarta2_1);
        }

        private void ImageCarta2_2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RestablecerImagen(ImageCarta2_2);
        }

        private void ImageCarta2_3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RestablecerImagen(ImageCarta2_3);
        }

        private void ImageCarta2_4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RestablecerImagen(ImageCarta2_4);
        }

        private void ImageCarta3_1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RestablecerImagen(ImageCarta3_1);
        }

        private void ImageCarta3_2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RestablecerImagen(ImageCarta3_2);
        }

        private void ImageCarta3_3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RestablecerImagen(ImageCarta3_3);
        }

        private void ImageCarta3_4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RestablecerImagen(ImageCarta3_4);
        }

        private void ImageCarta4_1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RestablecerImagen(ImageCarta4_1);
        }

        private void ImageCarta4_2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RestablecerImagen(ImageCarta4_2);
        }

        private void ImageCarta4_3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RestablecerImagen(ImageCarta4_3);
        }

        private void ImageCarta4_4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RestablecerImagen(ImageCarta4_4);
        }

        private void RegresarButton_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu("Prueba");
            menu.Show();
            this.Close();
        }
    }
}
