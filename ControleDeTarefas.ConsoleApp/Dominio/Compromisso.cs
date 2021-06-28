using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeTarefas.ConsoleApp.Dominio
{
    public class Compromisso : EntidadeBase
    {
  
        public string assunto, local, link, nome;
        public DateTime dataInicio;
        public DateTime dataTermino;
        public int contato_id;
        //para adicionar
        public Compromisso(string assunto, string local, string link,DateTime dataInicio, DateTime dataTermino, int contato_id)
        {
            this.assunto = assunto;
            this.local = local;
            this.link = link;
            this.contato_id = contato_id;
            this.dataInicio = dataInicio;
            this.dataTermino = dataTermino;
        }
        //para listar
        public Compromisso(int id,string nome,string assunto, string local, string link, DateTime dataInicio, DateTime dataTermino, int contato_id)
        {
            this.nome = nome;
            this.id = id;
            this.assunto = assunto;
            this.local = local;
            this.link = link;
            this.contato_id = contato_id;
            this.dataInicio = dataInicio;
            this.dataTermino = dataTermino;
        }

        public override bool Validar()
        {
            if (string.IsNullOrEmpty(assunto))
            {
                return false;
            }
            else if (string.IsNullOrEmpty(link) && string.IsNullOrEmpty(local))
            {

            }
            return true;
        }
    }
}
