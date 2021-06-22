using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeTarefas.ConsoleApp.Dominio
{
    public class Contato : EntidadeBase
    {
       
        public string nome, email, telefone, empresa, cargo;

        //para banco
        public Contato(int id,string nome, string email, string telefone, string empresa, string cargo)
        {
            this.id = id;
            this.nome = nome;
            this.email = email;
            this.telefone = telefone;
            this.empresa = empresa;
            this.cargo = cargo;
        }
        public Contato(string nome, string email, string telefone, string empresa, string cargo)
        {       
            this.nome = nome;
            this.email = email;
            this.telefone = telefone;
            this.empresa = empresa;
            this.cargo = cargo;
        }

        public override bool Validar()
        {
            if (!email.Contains("@"))
            {
                return false;
            }
            if (!email.Contains("mail"))
            {
                return false;

            }
            if (telefone.Length < 8)
            {
                return false;
            }
           
            return true;
        }
    }
}
