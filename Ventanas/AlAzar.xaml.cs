using LoterestTcs.Model;
using LoterestTcs.ServiceReferenceLoterest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace LoterestTcs.Ventanas
{
    /// <summary>
    /// Lógica de interacción para AlAzar.xaml
    /// </summary>
    public partial class AlAzar : Window
    {
        private int tiempoDisponible;
        private DispatcherTimer timer;
        private List<Image> imagenesDisponibles = new List<Image>();
        private List<int> numeroDeImagenSeleccionada = new List<int>();
        private List<Image> imagenesOcultas = new List<Image>();
        private List<Image> imagenesSeleccionadas = new List<Image>();
        private TableroJuego tableroJuego;
        private Jugador jugador;
        private string nombreUsuario;
        private string modoJuego = "AlAzar";
        private const int CANTIDADCARTAS = 16;
        private const int TIEMPOPARAELEGIRCARTAS = 0;

        public AlAzar(Jugador jugador, int tiempo, string nombreInvitado)
        {
            InitializeComponent();
            tiempoDisponible = tiempo;
            this.nombreUsuario = nombreInvitado;
            this.jugador = jugador;
        }
    }
}
