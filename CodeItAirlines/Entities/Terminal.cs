using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeItAirlines.Entities
{
    public class Terminal
    {
        public int Id { get; set; }
        public List<Tripulacao> Tripulacoes { get; set; }

        public Terminal()
        {
            this.Id = 1;
            this.CarregarTripulacao();
            this.CarregarNaoPodeFicarSozinho();
        }

        public string ValidarTripulacao()
        {
            return new Regra().ValidarTripulacao(this.Tripulacoes);
        }

        private void CarregarTripulacao()
        {
            this.Tripulacoes = new List<Tripulacao>
            {
                new Tripulacao {Id = 1, Descricao = "Piloto", PermiteDirigirForTwo = true},
                new Tripulacao {Id = 2, Descricao = "Oficial 1", PermiteDirigirForTwo = false},
                new Tripulacao {Id = 3, Descricao = "Oficial 2", PermiteDirigirForTwo = false},
                new Tripulacao {Id = 4, Descricao = "Chefe De Serviço", PermiteDirigirForTwo = true},
                new Tripulacao {Id = 5, Descricao = "Comissária 1", PermiteDirigirForTwo = false},
                new Tripulacao {Id = 6, Descricao = "Comissária 2", PermiteDirigirForTwo = false},
                new Tripulacao {Id = 7, Descricao = "Policial", PermiteDirigirForTwo = true},
                new Tripulacao {Id = 8, Descricao = "Presidiário", PermiteDirigirForTwo = false}
            };
        }

        private void CarregarNaoPodeFicarSozinho()
        {
            this.Tripulacoes.ForEach(trip =>
            {
                switch (trip.Id)
                {
                    case 1:
                        trip.NaoFicaSozinho = this.Tripulacoes
                                                    .Where(x =>
                                                            x.Id.Equals(5) ||
                                                            x.Id.Equals(6) ||
                                                            x.Id.Equals(8)
                                                    )
                                                    .ToList();
                        break;
                    case 2:
                        trip.NaoFicaSozinho = this.Tripulacoes
                                    .Where(x =>
                                            x.Id.Equals(4) ||
                                            x.Id.Equals(8)
                                    )
                                    .ToList();
                        break;
                    case 3:
                        trip.NaoFicaSozinho = this.Tripulacoes
                                        .Where(x =>
                                                x.Id.Equals(4) ||
                                                x.Id.Equals(8)
                                        )
                                        .ToList();
                        break;
                    case 4:
                        trip.NaoFicaSozinho = this.Tripulacoes
                                        .Where(x =>
                                                x.Id.Equals(2) ||
                                                x.Id.Equals(3) ||
                                                x.Id.Equals(8)
                                        )
                                        .ToList();
                        break;
                    case 5:
                        trip.NaoFicaSozinho = this.Tripulacoes
                                        .Where(x =>
                                                x.Id.Equals(1) ||
                                                x.Id.Equals(8)
                                        )
                                        .ToList();
                        break;
                    case 6:
                        trip.NaoFicaSozinho = this.Tripulacoes
                                    .Where(x =>
                                            x.Id.Equals(1) ||
                                            x.Id.Equals(8)
                                    )
                                    .ToList();
                        break;
                    case 7:
                        trip.NaoFicaSozinho = new List<Tripulacao>();
                        break;
                    case 8:
                        trip.NaoFicaSozinho = this.Tripulacoes
                                    .Where(x =>
                                            x.Id.Equals(1) ||
                                            x.Id.Equals(2) ||
                                            x.Id.Equals(3) ||
                                            x.Id.Equals(4) ||
                                            x.Id.Equals(5) ||
                                            x.Id.Equals(6)
                                            )
                                            .ToList();
                        break;
                }
            });
        }

        public void RetirarTripulacao(int id)
        {
            Tripulacao trip = this.Tripulacoes.Where(x => x.Id == id).FirstOrDefault();
            this.Tripulacoes.Remove(trip);

        }

        public string ColocarTripulacao(Tripulacao trip)
        {
            this.Tripulacoes.Add(trip);
            var validacao = this.ValidarTripulacao();

            if (!ReferenceEquals("", validacao))
            {
                this.Tripulacoes.Remove(trip);
            }

            return validacao;
        }
    }
}
