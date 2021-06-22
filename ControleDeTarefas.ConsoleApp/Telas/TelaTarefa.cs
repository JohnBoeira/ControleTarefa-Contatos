using ControleDeTarefas.ConsoleApp.Controle;
using ControleDeTarefas.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeTarefas.ConsoleApp.Telas
{
    public class TelaTarefa : TelaBase<Tarefa>
    {
        ControladorTarefa controladorTarefa;

        public TelaTarefa()
        {
            controladorTarefa = new ControladorTarefa();
        }

        public string ObterOpcao()
        {
            string opcao;
            do
            {
                Console.Clear();
                MostrarOpcoesBasicaCRUD();
                Console.WriteLine("Entre 4 para Concluir");
                Console.WriteLine("Entre 5 para listar tarefas abertas");
                Console.WriteLine("Entre 6 para listar tarefas fechadas");
                Console.WriteLine("Entre S para Voltar");
                opcao = Console.ReadLine();
            } while (opcao != "1" && opcao !="2" && opcao != "3" && opcao != "4" && opcao != "5" && opcao != "6" && opcao.ToLower() != "s");
            return opcao;
        }       

        public void ExecutarUmaAcao()
        {
            
            while (true)
            {
                string opcao = ObterOpcao();
                switch (opcao)
                {
                    case "1": controladorTarefa.Inserir(PegarRegistro("Inserir")); break;
                    case "2": controladorTarefa.Editar(SelecionarIdParaAlteracao(), PegarRegistro("Editar")); break;
                    case "3": controladorTarefa.Excluir(SelecionarIdParaAlteracao()); break;
                    case "4": controladorTarefa.ConcluirTarefa(SelecionarIdParaAlteracao()); break;
                    case "5": ListagemOrdenadaAberto(controladorTarefa.ListarTarefasAbertas()); break;
                    case "6": ListagemOrdenadaFechado(controladorTarefa.ListarTarefasFechadas()); break;
                    case "S": break;
                }
            }
           
        }

        public Tarefa PegarRegistro(string opcao)
        {
            if (opcao == "Inserir")
            {
                Console.WriteLine("Digite titulo: ");
                string titulo = Console.ReadLine();
                Console.WriteLine("Digite prioridade");
                int prioridade = Convert.ToInt32(Console.ReadLine());
                return new Tarefa(titulo, prioridade);
            }
            else
            {
                Tarefa tarefa = new Tarefa();
                Console.WriteLine("Digite titulo: ");
                tarefa.titulo = Console.ReadLine();
                Console.WriteLine("Digite prioridade");
                tarefa.prioridade = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Digite percentual");
                tarefa.percentualDeConclusao = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Deseja editar também data de conclusão (SIM/NÃO)");
                if (Console.ReadLine() == "SIM")
                {
                    Console.WriteLine("Digite data conclusão: ");
                    tarefa.dataConclusao = Convert.ToDateTime(Console.ReadLine());
                }
                return tarefa;
            }
        }
        private void ListagemOrdenadaAberto(List<Tarefa> tarefas)
        {
            tarefas.OrderBy(x => x.prioridade == 3).ThenBy(x => x.prioridade == 2).ThenBy(x => x.prioridade == 1);
            foreach (var item in tarefas)
            {
                Console.WriteLine("ID: " +item.id + " Titulo: " +item.titulo + " Prioridade: " +item.prioridade + " Data Criação: " + item.dataDeCriacao.ToShortDateString() + " Percentagem" +item.percentualDeConclusao);
            }
            Console.ReadLine();
        }
        private void ListagemOrdenadaFechado(List<Tarefa> tarefas)
        {
            tarefas.OrderBy(x => x.prioridade == 3).ThenBy(x => x.prioridade == 2).ThenBy(x => x.prioridade == 1);
            foreach (var item in tarefas)
            {
                Console.WriteLine("ID: " + item.id + " Titulo: " + item.titulo + " Prioridade: " + item.prioridade + " Data Criação: " + item.dataDeCriacao.ToShortDateString() + " Data conclusão: " +item.dataConclusao.ToShortDateString()+ " Percentagem" + item.percentualDeConclusao);
            }
            Console.ReadLine();
        }
    }
}
