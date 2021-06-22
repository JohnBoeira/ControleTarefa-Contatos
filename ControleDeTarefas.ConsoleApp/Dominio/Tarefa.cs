
using ControleDeTarefas.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeTarefas.ConsoleApp.Dominio
{
    public class Tarefa : EntidadeBase
    {
        public string titulo;
        public int prioridade;
        public DateTime dataDeCriacao;
        public DateTime dataConclusao;
        public int percentualDeConclusao;
        //para inserir
        public Tarefa(string titulo, int prioridade)
        {
            this.titulo = titulo;
            this.prioridade = prioridade;
            this.dataDeCriacao = DateTime.Now;       
            this.percentualDeConclusao = 0;
        }

        //para banco
        public Tarefa(int id, string titulo, int prioridade, DateTime dataDeCriacao, DateTime dataConclusao, int percentualDeConclusao)
        {
            this.id = id;
            this.titulo = titulo;
            this.prioridade = prioridade;
            this.dataDeCriacao = dataDeCriacao;
            this.dataConclusao = dataConclusao;
            this.percentualDeConclusao = percentualDeConclusao;
        }
        //para editar
        public Tarefa()
        {

        }
        
        public override bool Validar()
        {
            if (titulo.Length == 0)
            {
                return false;
            }
            else if (prioridade < 0 || prioridade > 3)
            {
                return false;
            }
            
            return true;
        }
    }
}
