using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeItAirlines.Entities
{
    public class Regra
    {
        public string ValidarTripulacao(List<Tripulacao> tripulacaoLocal)
        {
            List<Tripulacao> result = new List<Tripulacao>();
            tripulacaoLocal.ForEach(trip => {
                if (trip.Id == 2 || trip.Id == 3)
                {
                    if (!tripulacaoLocal.Any(x => x.Id == 1) && tripulacaoLocal.Where(x => x.Id != 2 && x.Id != 3).ToList().Count > 0 && tripulacaoLocal.Any(x => x.Id == 4))
                    {
                        result.Add(trip);
                    }
                }

                if (trip.Id == 5 || trip.Id == 6)
                {
                    if (!tripulacaoLocal.Any(x => x.Id == 4) && tripulacaoLocal.Where(x => x.Id != 5 && x.Id != 6).ToList().Count > 0 && tripulacaoLocal.Any(x => x.Id == 1))
                    {
                        result.Add(trip);
                    }
                }

                if (trip.Id == 8 && !tripulacaoLocal.Any(x => x.Id == 7) && tripulacaoLocal.Where(x => x.Id != 8).ToList().Count > 0)
                {
                    result.Add(trip);
                }

            });

            return this.FormatarTextoRetornoValidacao(result);
        }

        private string FormatarTextoRetornoValidacao(List<Tripulacao> tripulacoes)
        {
            string formatarTextoRetorno = string.Empty;

            tripulacoes.ForEach(trip =>
            {
                if (ReferenceEquals(formatarTextoRetorno, ""))
                {
                    formatarTextoRetorno = $"O(a) {trip.Descricao} não pode ficar sozinho(a) com {String.Join(',', trip.NaoFicaSozinho.Select(x => x.Descricao))}";
                }
                else
                {
                    formatarTextoRetorno += $"\nO(a) {trip.Descricao} não pode ficar sozinho(a) com {String.Join(',', trip.NaoFicaSozinho.Select(x => x.Descricao))}";
                }
            });

            return formatarTextoRetorno;
        }
    }
}
