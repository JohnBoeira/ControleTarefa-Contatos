using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeTarefas.ConsoleApp.Telas
{
    public class TelaPrincipal
    {
        public void SelecionaTela()
        {
            string opcao;
            while (true)
            {
                Console.Clear();             
                Console.WriteLine("Digite 1 para Tela Tarefa");
                Console.WriteLine("Digite 2 para Tela Contato");
                Console.WriteLine("--------------------------");
                Console.WriteLine("Digite S para Sair");
                opcao = Console.ReadLine();

                if (opcao == "1")
                {
                    TelaTarefa telaTarefa = new TelaTarefa();
                    telaTarefa.ExecutarUmaAcao();
                }
                else if (opcao == "2")
                {
                    TelaContato telaContato = new TelaContato();
                    telaContato.ExecutarUmaAcao();
                }
                else if (opcao.Equals("s", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Opção incorreta!");
                }
            }
        }
    }
}
