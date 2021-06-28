using ControleDeTarefas.ConsoleApp.Controle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeTarefas.ConsoleApp.Telas
{
    public class TelaPrincipal : TelaBase
    {
        public void SelecionaTela()
        {
            string opcao;
            while (true)
            {
                ControladorTarefa controladorTarefa = new ControladorTarefa();
                TelaTarefa telaTarefa = new TelaTarefa(controladorTarefa);

                ControladorContato controladorContato = new ControladorContato();
                TelaContato telaContato = new TelaContato(controladorContato);

                ControladorCompromisso controladorCompromisso = new ControladorCompromisso();
                TelaCompromisso telaCompromisso = new TelaCompromisso(controladorCompromisso,telaContato);

                Console.Clear();             
                Console.WriteLine("Digite 1 para Tela Tarefa");
                Console.WriteLine("Digite 2 para Tela Contato");
                Console.WriteLine("Digite 3 para Tela Compromisso");
                Console.WriteLine("--------------------------");
                Console.WriteLine("Digite S para Sair");
                opcao = Console.ReadLine();

                if (opcao == "1")
                {                   
                    telaTarefa.RealizaAcao();
                }
                else if (opcao == "2")
                {
                
                    telaContato.RealizaAcao();
                }
                else if (opcao == "3")
                {
                   
                    telaCompromisso.RealizaAcao();
                    
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
