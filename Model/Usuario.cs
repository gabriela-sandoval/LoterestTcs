using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoterestTcs.Model
{
    public class Usuario
    {
        public string NombreUsuario { get; set; }
        public string CorreoUsuario { get; set; }
        public string ContraseñaUsuario { get; set; }
        public int? PuntajeUsuario { get; set; }
        public TableroJuego TableroJuego { get; set; }

    }
}
