using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeItAirlines.Entities
{
    public class SmartForTwo
    {
        public List<Tripulacao> Tripulacoes { get; set; }
        public bool NoTerminal { get; set; }

        public SmartForTwo()
        {
            this.Tripulacoes = new List<Tripulacao>();
            this.NoTerminal = true;
        }

        public string Viajar(Tripulacao tripulacao)
        {
            return this.ValidarPossibilidadeViagem(tripulacao);
        }

        private string ValidarPossibilidadeViagem(Tripulacao tripulacao)
        {
            return (!tripulacao.PermiteDirigirForTwo && this.Tripulacoes.Count == 0) || (this.Tripulacoes.Count > 0 && !this.Tripulacoes.Any(x => x.PermiteDirigirForTwo)) ? "Apenas o piloto, chefe de serviço e o policial pode dirigir o Smart" : "";
        }

        private string ValidarCapacidadeSmart()
        {
            return this.Tripulacoes.Count > 2 ? "O Smart tem capacidade para duas pessoas" : "";
        }

        public string CarregarPersonagem(Tripulacao tripulacao)
        {
            var result = ValidarCapacidadeSmart();

            if (ReferenceEquals("", result))
            {
                var validacao = Viajar(tripulacao);
                if (ReferenceEquals("", validacao))
                {
                    this.Tripulacoes.Add(tripulacao);
                }
                else
                {
                    result = validacao;
                }
            }

            return result;
        }
    }
}
