using ControleDeTarefas.ConsoleApp.Controle;
using ControleDeTarefas.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeTarefas.ConsoleApp.Telas
{
    public class TelaContato : TelaCadastroBasico<Contato>
    {
        ControladorContato controladorContato;

        public TelaContato(ControladorBase<Contato> controladorBase) : base(controladorBase)
        {
            this.controladorContato = new ControladorContato();
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
        protected override Contato ObterRegistro(string opcao)
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
        protected override void ListarRegistros()
        {
            List<Contato> contatos = controladorContato.ListarRegistrosDoBanco();
            foreach (var item in contatos)
            {
                Console.WriteLine("ID: " + item.id + "Nome: " + item.nome + "Telefone: " + item.telefone + "Email: " + item.email + "Empresa: " + item.empresa + "Cargo: " + item.cargo);
            }
            Console.ReadLine();
        }
        protected override void CasosEspeciais()
        {         
            Console.WriteLine("Entre 1 para listar por cargo");
           
            string opcao = Console.ReadLine();
            if (opcao == "1")
            {
                ListagemPorCargo();
            }          
            else
            {
                Console.WriteLine("Opção incorreta");
                ListarRegistros();
            }
        }
    }
}
