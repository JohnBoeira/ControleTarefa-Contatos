using ControleDeTarefas.ConsoleApp.Controle;
using ControleDeTarefas.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeTarefas.ConsoleApp.Telas
{
    public class TelaContato : TelaBase<Contato>
    {
        ControladorContato controladorContato;

        public TelaContato()
        {
            controladorContato = new ControladorContato();
        }

        public string ObterOpcao()
        {
            string opcao;
            do
            {
                Console.Clear();
                MostrarOpcoesBasicaCRUD();
                Console.WriteLine("Entre 4 para Listar por Cargo");
                Console.WriteLine("Entre 5 para listar todos");
                Console.WriteLine("Entre S para Voltar");
                opcao = Console.ReadLine();
            } while (opcao != "1" && opcao != "2" && opcao != "3" && opcao != "4" && opcao != "5" &&  opcao.ToLower() != "s");
            return opcao;
        }

        public void ExecutarUmaAcao()
        {
            string opcao = null;
            while(opcao != "S")
            {
                 opcao = ObterOpcao();
                switch (opcao)
                {
                    case "1": controladorContato.Inserir(PegarRegistro()); break;
                    case "2": controladorContato.Editar(SelecionarIdParaAlteracao(), PegarRegistro()); break;
                    case "3": controladorContato.Excluir(SelecionarIdParaAlteracao()); break;
                    case "4": ListagemPorCargo(); break;
                    case "5": ListagemCompleta(); break;
                    case "S": break;
                }
            }

        }

        public Contato PegarRegistro()
        {

            Console.WriteLine("Digite nome: ");
            string nome = Console.ReadLine();
            Console.WriteLine("Digite telefone");
            string telefone = Console.ReadLine();
            Console.WriteLine("Digite email");
            string email = Console.ReadLine();
            Console.WriteLine("Digite empresa");
            string empresa = Console.ReadLine();
            Console.WriteLine("Digite cargo");
            string cargo = Console.ReadLine();
            return new Contato(nome, email, telefone, empresa, cargo);

        }

        private void ListagemPorCargo()
        {
            Console.WriteLine("Digite o cargo:");
            string cargo = Console.ReadLine();
            List<Contato> contatos = new List<Contato>();
            contatos = controladorContato.ListarPorCargo(cargo);
         
            foreach (var item in contatos)
            {
                Console.WriteLine("ID: " + item.id + "Nome: " +item.nome + "Telefone: " + item.telefone + "Email: " +item.email + "Empresa: " + item.empresa + "Cargo: " +item.cargo);
            }
            Console.ReadLine();
        }

        private void ListagemCompleta()
        {
            List<Contato> contatos = controladorContato.ListarRegistrosDoBanco();
            foreach (var item in contatos)
            {
                Console.WriteLine("ID: " + item.id + "Nome: " + item.nome + "Telefone: " + item.telefone + "Email: " + item.email + "Empresa: " + item.empresa + "Cargo: " + item.cargo);
            }
            Console.ReadLine();
        }
        
    }
}
