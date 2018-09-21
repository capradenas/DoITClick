using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doitclick.Models.Application
{
    public class Marca
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public IEnumerable<MaterialDisponible> MaterialDiponible { get; set; }
        public IEnumerable<MaterialMensual> MaterialMensual { get; set; }
        public IEnumerable<Instrumento> Instrumento { get; set; }
    }
}