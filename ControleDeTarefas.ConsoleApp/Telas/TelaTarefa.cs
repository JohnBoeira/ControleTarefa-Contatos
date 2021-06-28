using ControleDeTarefas.ConsoleApp.Controle;
using ControleDeTarefas.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeTarefas.ConsoleApp.Telas
{
    public class TelaTarefa : TelaCadastroBasico<Tarefa>
    {
        ControladorTarefa controladorTarefa;

        public TelaTarefa(ControladorBase<Tarefa> controladorBase) : base(controladorBase)
        {
            controladorTarefa = new ControladorTarefa();
        }                 
        protected override void CasosEspeciais()
        {
            Console.Clear();
            Console.WriteLine("Entre 1 para listar tarefas abertas");
            Console.WriteLine("Entre 2 para listar tarefas fechadas");
            Console.WriteLine("Entre 3 para Concluir tarefa");
            string opcao = Console.ReadLine();
            if (opcao == "1")
            {
                ListagemOrdenadaAberto();
            }
            else if(opcao == "2"){
                ListagemOrdenadaFechado();
            }
            else if (opcao == "3")
            {
                ListarRegistros();
                int id =ObterIdParaAlteracao();
                controladorTarefa.ConcluirTarefa(id);
            }
            else
            {
                Console.WriteLine("Valor incorreto");
            }
        }
        protected override void ListarRegistros()
        {
            List<Tarefa> tarefas = controladorTarefa.ListarRegistrosDoBanco();
            tarefas.OrderBy(x => x.prioridade == 3).ThenBy(x => x.prioridade == 2).ThenBy(x => x.prioridade == 1);
            foreach (var item in tarefas)
            {
                Console.WriteLine("ID: " + item.id + " Titulo: " + item.titulo + " Prioridade: " + item.prioridade + " Data Criação: " + item.dataDeCriacao.ToShortDateString() + " Data conclusão: " + item.dataConclusao.ToShortDateString() + " Percentagem" + item.percentualDeConclusao);
            }
        }
        protected override Tarefa ObterRegistro(string opcao)
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
        private void ListagemOrdenadaAberto()
        {
            List<Tarefa> tarefas = controladorTarefa.ListarTarefasAbertas();
            tarefas.OrderBy(x => x.prioridade == 3).ThenBy(x => x.prioridade == 2).ThenBy(x => x.prioridade == 1);
            foreach (var item in tarefas)
            {
                Console.WriteLine("ID: " +item.id + " Titulo: " +item.titulo + " Prioridade: " +item.prioridade + " Data Criação: " + item.dataDeCriacao.ToShortDateString() + " Percentagem" +item.percentualDeConclusao);
            }
            Console.ReadLine();
        }
        private void ListagemOrdenadaFechado()
        {
            List<Tarefa> tarefas = controladorTarefa.ListarTarefasFechadas();
            tarefas.OrderBy(x => x.prioridade == 3).ThenBy(x => x.prioridade == 2).ThenBy(x => x.prioridade == 1);
            foreach (var item in tarefas)
            {
                Console.WriteLine("ID: " + item.id + " Titulo: " + item.titulo + " Prioridade: " + item.prioridade + " Data Criação: " + item.dataDeCriacao.ToShortDateString() + " Data conclusão: " +item.dataConclusao.ToShortDateString()+ " Percentagem" + item.percentualDeConclusao);
            }
            Console.ReadLine();
        }
    }
}
