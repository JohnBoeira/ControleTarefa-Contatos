using ControleDeTarefas.ConsoleApp.Controle;
using ControleDeTarefas.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeTarefas.ConsoleApp.Telas
{
    public abstract class TelaBase<T> where T : EntidadeBase
    {
   
        protected int SelecionarIdParaAlteracao()
        {
            Console.WriteLine("Digite id para alteração:");
            return Convert.ToInt32(Console.ReadLine());
        }
   
        protected void MostrarOpcoesBasica()
        {
            Console.WriteLine("Entre 1 para Inserir");
            Console.WriteLine("Entre 2 para Editar");
            Console.WriteLine("Entra 3 para Excluir:");
        }

    }
}
