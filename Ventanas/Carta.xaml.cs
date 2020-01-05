using LoterestTcs.Model;
using LoterestTcs.ServiceReferenceLoterest;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace LoterestTcs.Ventanas
{
    /// <summary>
    /// Lógica de interacción para Carta.xaml
    /// </summary>
    public partial class Carta : Window
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
        private string modoJuego = "Loteria";
        private const int CANTIDADCARTAS = 16;
        private const int TIEMPOPARAELEGIRCARTAS = 0;

        public Carta(Jugador jugador, int tiempo, string nombreInvitado)
        {
            InitializeComponent();
        }
    }
}
