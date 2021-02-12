using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeItAirlines.Entities
{
    public class Aviao
    {
        public int Id { get; set; }
        public List<Tripulacao> Tripulacoes { get; set; }

        public Aviao()
        {
            this.Id = 2;
            this.Tripulacoes = new List<Tripulacao>();
        }

        public string ValidarTripulacao()
        {
            return new Regra().ValidarTripulacao(this.Tripulacoes);
        }

        public Tripulacao RetirarTripulacao(int id)
        {
            Tripulacao trip = this.Tripulacoes.Where(x => x.Id == id).FirstOrDefault();

            this.Tripulacoes.Remove(trip);

            return trip;

        }

    }
}
