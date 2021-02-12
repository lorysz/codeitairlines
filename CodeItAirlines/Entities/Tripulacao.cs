using System;
using System.Collections.Generic;
using System.Text;

namespace CodeItAirlines.Entities
{
    public class Tripulacao
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool PermiteDirigirForTwo { get; set; }
        public List<Tripulacao> NaoFicaSozinho { get; set; }
    }
}
