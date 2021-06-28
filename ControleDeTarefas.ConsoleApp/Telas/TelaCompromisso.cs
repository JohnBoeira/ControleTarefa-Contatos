using ControleDeTarefas.ConsoleApp.Controle;
using ControleDeTarefas.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeTarefas.ConsoleApp.Telas
{
    public class TelaCompromisso : TelaCadastroBasico<Compromisso>
    {
        TelaContato telaContato;
        ControladorCompromisso controladorCompromisso;
        public TelaCompromisso(ControladorBase<Compromisso> controladorBase, TelaContato telaContato) : base(controladorBase)
        {
            this.telaContato = telaContato;
            controladorCompromisso = new ControladorCompromisso();
        }
        protected override void CasosEspeciais()
        {
            Console.Clear();
            Console.WriteLine("Digite 1 para listar compromissos da semana");
            Console.WriteLine("Digite 2 para listar compromissos diário ");
            Console.WriteLine("Digite 3 para listar compromissos passados ");
            string opcao = Console.ReadLine();
            switch (opcao)
            {
                case "1": ListarSemanal(); break;
                case "2": ListarDiario(); break;
                case "3": ListarPassado(); break;
                default:
                    break;
            }
        }
        protected override void ListarRegistros()
        {
            List<Compromisso> compromissos = controladorCompromisso.ListarRegistrosDoBanco();

            foreach (var item in compromissos)
            {
                Console.WriteLine("ID:" + item.id + "Assunto: " + item.assunto + " Local: " + item.local + " Data início: " + item.dataInicio + " Data termino:" + item.dataTermino + " Link: " + item.link + " Contato: " + item.nome);
            }
            Console.ReadLine();
        }
        private void ListarDiario()
        {
            List<Compromisso> compromissos = controladorCompromisso.ListarRegistrosDiarios();

            foreach (var item in compromissos)
            {
                Console.WriteLine("ID:" + item.id+"Assunto: " + item.assunto + " Local: " + item.local + " Data início: " + item.dataInicio + " Data termino:" + item.dataTermino + " Link: " + item.link + " Contato: " + item.nome);
            }
            Console.ReadLine();

        }
        private void ListarPassado()
        {
            List<Compromisso> compromissos = controladorCompromisso.ListarRegistrosPassados();

            foreach (var item in compromissos)
            {
                Console.WriteLine("ID:" + item.id + "Assunto: " + item.assunto + " Local: " + item.local + " Data início: " + item.dataInicio + " Data termino:" + item.dataTermino + " Link: " + item.link + " Contato: " + item.nome);
            }
            Console.ReadLine();

        }
        private void ListarSemanal()
        {
            List<Compromisso> compromissos = controladorCompromisso.ListarRegistrosSemanal();

            foreach (var item in compromissos)
            {
                Console.WriteLine("ID:" + item.id + "Assunto: " + item.assunto + " Local: " + item.local + " Data início: " + item.dataInicio + " Data termino:" + item.dataTermino + " Link: " + item.link + " Contato: " + item.nome);
            }
            Console.ReadLine();

        }
        protected override Compromisso ObterRegistro(string tipo)
        {
            Console.WriteLine("Digite assunto: ");
            string assunto = Console.ReadLine();
            Console.WriteLine("Digite local: ");
            string local = Console.ReadLine();
            Console.WriteLine("Data do compromisso: ");
            DateTime dataInicio = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Data do termino: ");
            DateTime dataTermino = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Link: ");
            string link = Console.ReadLine();
            Console.WriteLine("Deseja adicionar contato: (SIM/NÃO)");
            if (Console.ReadLine() == "SIM")
            {
                telaContato.Listar();
                Console.WriteLine("Digite id contato: ");
                int idContato = Convert.ToInt32(Console.ReadLine());
                return new Compromisso(assunto, local, link, dataInicio, dataTermino, idContato);
            }
            return new Compromisso(assunto, local, link, dataInicio, dataTermino, 0);

        }
    }
}
