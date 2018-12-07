using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfiguradorRackPadrao
{
    public class Componente
    {
        public string item { get; set; } = "";
        public int? qt { get; set; } // Nullable type
        public string scm { get; set; } = "";
    }
}
