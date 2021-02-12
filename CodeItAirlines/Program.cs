using CodeItAirlines.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeItAirlines
{
    public class Program
    {
        public static bool fimPrograma = false;
        public static int step = 1;
        public static bool primeiraPergunta = true;
        public static bool segundaPergunta = false;
        public static Terminal terminal = new Terminal();
        public static Aviao aviao = new Aviao();
        public static SmartForTwo smart = new SmartForTwo();
        static void Main(string[] args)
        {
            string descricaoJogo1 = @"
A CodeIT Airlines é uma empresa de aviação que opera rotas internacionais a partir de Maringá.
Cada voo é tripulado por seis elementos, sendo que estes se dividem em dois grupos: a tripulação
técnica e a tripulação de cabine. A tripulação técnica é constituída pelo piloto e dois oficiais. A
tripulação de cabine é constituída pelo chefe de serviço de voo e duas comissárias.";

            string descricaoJogo2 = @"
O transporte dos tripulantes entre o terminal e o avião é efetuado através de um Smart Fortwo, que
é um veículo que leva apenas duas pessoas.Por política da empresa, apenas o piloto e o chefe de
serviço de voo podem dirigir este veículo.É também política da empresa que nenhum dos oficiais
pode ficar sozinho com o chefe de serviço de bordo, e nem nenhuma das comissárias pode ficar
sozinha com o piloto.";

            string descricaoJogo3 = @"
No terminal de embarque estão os seis tripulantes e ainda um policial junto com um presidiário.
Estes oito elementos terão que embarcar segundo as regras descritas acima. A empresa não coloca
nenhum limite para o número de viagens entre o terminal e o avião.";

            string descricaoJogo4 = @"
Por motivos de segurança o presidiário não pode ficar sozinho em momento algum com os
tripulantes sem a presença do policial, nem no terminal e nem no avião.De forma a facilitar o
processo, a empresa autorizou que o policial pudesse dirigir o veículo também.";



            Console.WriteLine(descricaoJogo1);
            Console.WriteLine(descricaoJogo2);
            Console.WriteLine(descricaoJogo3);
            Console.WriteLine(descricaoJogo4);
            Console.WriteLine("");
            Console.WriteLine("");

            while (!fimPrograma)
            {
                if (step == 1)
                {
                    if (terminal.Tripulacoes.Count == 0)
                    {
                        Console.WriteLine("Você Concluiu o jogo! Parabéns!");
                        fimPrograma = true;
                        step = 3;
                        primeiraPergunta = false;
                    }
                    while (primeiraPergunta)
                    {
                        string localSmart = smart.NoTerminal ? "Você está no terminal" : "Você está no avião";
                        Console.WriteLine(localSmart);


                        primeiraPergunta = false;
                        segundaPergunta = true;
                        step = 2;
                    }
                }

                if (step == 2)
                {
                    while (segundaPergunta)
                    {
                        var tripulacaoLocal = new List<Tripulacao>();
                        if (smart.NoTerminal)
                        {
                            tripulacaoLocal = terminal.Tripulacoes;
                        }
                        else
                        {
                            tripulacaoLocal = aviao.Tripulacoes;
                        }


                        CarregarSmart(tripulacaoLocal);


                        smart.Tripulacoes.ForEach(trip =>
                        {
                            Tripulacao personagem = new Tripulacao();

                            if (smart.NoTerminal)
                            {
                                terminal.RetirarTripulacao(trip.Id);
                            }
                            else
                            {
                                aviao.RetirarTripulacao(trip.Id);
                            }

                        });


                        bool valida = false;

                        string validacaoLocal = string.Empty;

                        if (smart.NoTerminal)
                        {
                            validacaoLocal = terminal.ValidarTripulacao();                            
                        }
                        else
                        {
                            validacaoLocal = aviao.ValidarTripulacao();
                        }

                        if (!ReferenceEquals("", validacaoLocal))
                        {
                            Console.WriteLine(validacaoLocal);
                            tripulacaoLocal.AddRange(smart.Tripulacoes);
                            valida = true;

                        }
                        else
                        {

                            string validacaoLocal2 = string.Empty;
                            if (smart.NoTerminal)
                            {
                                aviao.Tripulacoes.AddRange(smart.Tripulacoes);
                                validacaoLocal2 = aviao.ValidarTripulacao();
                            }
                            else
                            {
                                terminal.Tripulacoes.AddRange(smart.Tripulacoes);
                                validacaoLocal2 = aviao.ValidarTripulacao();
                            }

                            if (!ReferenceEquals("", validacaoLocal2))
                            {
                                Console.WriteLine(validacaoLocal2);
                                valida = true;

                                if (smart.NoTerminal)
                                {
                                    smart.Tripulacoes.ForEach(x => aviao.Tripulacoes.Remove(x));
                                    terminal.Tripulacoes.AddRange(smart.Tripulacoes);
                                }
                                else
                                {
                                    smart.Tripulacoes.ForEach(x => terminal.Tripulacoes.Remove(x));
                                    aviao.Tripulacoes.AddRange(smart.Tripulacoes);
                                }
                            }
                        }


                        if (!valida)
                        {
                            step = 1;
                            primeiraPergunta = true;
                            segundaPergunta = false;
                            smart.NoTerminal = !smart.NoTerminal;
                        }

                        smart.Tripulacoes = new List<Tripulacao>();


                    }
                }
            }
        }

        public static void CarregarSmart(List<Tripulacao> tripulacao)
        {
            bool viajar = false;
            while (!viajar)
            {
                string formatarTexto = string.Empty;
                var listaFiltered = tripulacao.Where(x => !smart.Tripulacoes.Any(z => z.Id == x.Id)).ToList();
                listaFiltered.OrderBy(x => x.Id).ToList().ForEach(trip =>
                {
                    if (ReferenceEquals("", formatarTexto))
                    {
                        formatarTexto = $"\n {trip.Id} - {trip.Descricao}";
                    }
                    else
                    {
                        formatarTexto += $"\n {trip.Id} - {trip.Descricao}";
                    }
                });

                Console.WriteLine($"Aperte 0 para sair do jogo ou ou Escolha o personagem voce irá levar e parte 9 para viajar: {formatarTexto}");
                int idPersonagemEscolhido = 0;

                try
                {
                    idPersonagemEscolhido = Convert.ToInt32(Console.ReadLine());

                    if (idPersonagemEscolhido == 0)
                    {
                        Console.WriteLine("Jogo finalizado!");
                        fimPrograma = true;
                        primeiraPergunta = false;
                        segundaPergunta = false;
                        viajar = true;
                    }
                    else if (idPersonagemEscolhido == 9)
                    {
                        if (smart.Tripulacoes.Count == 0)
                        {
                            Console.WriteLine("Precisa levar pelo menos o motorista");
                        } else
                        {
                            viajar = true;
                        }
                    }
                    else
                    {
                        if (smart.NoTerminal)
                        {
                            if (!terminal.Tripulacoes.Any(x => x.Id == idPersonagemEscolhido))
                            {
                                Console.WriteLine("Escolha um número válido");
                            }
                            else
                            {
                                var valida = smart.CarregarPersonagem(terminal.Tripulacoes.Where(x => x.Id == idPersonagemEscolhido).FirstOrDefault());

                                if (!ReferenceEquals("", valida))
                                {
                                    Console.WriteLine(valida);

                                }
                            }
                        }
                        else
                        {
                            if (!aviao.Tripulacoes.Any(x => x.Id == idPersonagemEscolhido))
                            {
                                Console.WriteLine("Escolha um número válido");
                            }
                            else
                            {
                                var valida = smart.CarregarPersonagem(aviao.Tripulacoes.Where(x => x.Id == idPersonagemEscolhido).FirstOrDefault());
                                if (!ReferenceEquals("", valida))
                                {
                                    Console.WriteLine(valida);

                                }
                            }
                        }

                        viajar = smart.Tripulacoes.Count == 2;
                    }
                }
                catch
                {
                    Console.WriteLine("Escolha um número válido");
                }
            }
        }


    }
}
