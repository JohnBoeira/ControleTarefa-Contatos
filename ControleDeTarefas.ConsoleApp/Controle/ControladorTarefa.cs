using ControleDeTarefas.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeTarefas.ConsoleApp.Controle
{
    public class ControladorTarefa : ControladorBase<Tarefa>
    {
        #region metodosCRUD
        protected override void InserirNoBanco(Tarefa tarefa)
        {
            SqlConnection con;
            SqlCommand comando;
            AbrirConexao(out con, out comando);
            //config comando       

            string sqlInsercao = @"INSERT INTO TB_Tarefa ([titulo], [prioridade], [dataDeCriacao], [percentualDeConclusao]) VALUES (@titulo, @prioridade, @dataDeCriacao, @percentualDeConclusao);";

            sqlInsercao += @"SELECT SCOPE_IDENTITY();";

            comando.CommandText = sqlInsercao;

            comando.Parameters.AddWithValue("titulo", tarefa.titulo);
            comando.Parameters.AddWithValue("prioridade", tarefa.prioridade);
            comando.Parameters.AddWithValue("dataDeCriacao", tarefa.dataDeCriacao);
            comando.Parameters.AddWithValue("percentualDeConclusao", tarefa.percentualDeConclusao);
            //executa
            object id = comando.ExecuteScalar();

            tarefa.id = Convert.ToInt32(id);

            con.Close();
        }
        protected override void EditarNoBanco(Tarefa tarefa, int id)
        {
            SqlConnection con;
            SqlCommand comando;
            AbrirConexao(out con, out comando);
            //config comando
            string sqlAtualizacao = null;
            if (tarefa.dataConclusao != DateTime.MinValue)
            {
                sqlAtualizacao =
                    @"UPDATE TB_Tarefa 
	                SET	
		                [titulo] = @titulo, 
		                [prioridade]=@prioridade, 
		                [dataConclusao] = @dataConclusao,                       
                        [percentualDeConclusao] = @percentualDeConclusao
	                WHERE 
		                [ID] = @ID";

                comando.Parameters.AddWithValue("dataConclusao", tarefa.dataConclusao);
            }
            else
            {
                sqlAtualizacao =
                    @"UPDATE TB_Tarefa 
	                SET	
		                [titulo] = @titulo, 
		                [prioridade]=@prioridade, 		                                  
                        [percentualDeConclusao] = @percentualDeConclusao
	                WHERE 
		                [ID] = @ID";
            }
            
            comando.CommandText = sqlAtualizacao;

            comando.Parameters.AddWithValue("titulo", tarefa.titulo);
            comando.Parameters.AddWithValue("prioridade", tarefa.prioridade);        
            comando.Parameters.AddWithValue("percentualDeConclusao", tarefa.percentualDeConclusao);
            comando.Parameters.AddWithValue("ID", id);
            //executa

            comando.ExecuteNonQuery();

            con.Close();
        }
        protected override void ExcluirNoBanco(int id)
        {
            SqlConnection con;
            SqlCommand comando;
            AbrirConexao(out con, out comando);

            string sqlExclusao =
                    @"DELETE FROM TB_Tarefa 	              
	                WHERE 
		                [ID] = @ID";

            comando.CommandText = sqlExclusao;

            comando.Parameters.AddWithValue("ID", id);
            //executa

            comando.ExecuteNonQuery();

            con.Close();
        }
        #endregion             
        public List<Tarefa> ListarTarefasAbertas()
        {
            List<Tarefa> todasTarefas = ListarRegistrosDoBanco();
            List<Tarefa> tarefasAbertas = new List<Tarefa>();
            foreach (var item in todasTarefas)
            {
                if (item.dataConclusao == DateTime.MinValue)
                {
                    tarefasAbertas.Add(item);
                }
            }
            return tarefasAbertas;
        }
        public List<Tarefa> ListarTarefasFechadas()
        {
            List<Tarefa> todasTarefas = ListarRegistrosDoBanco();
            List<Tarefa> tarefasFechadas = new List<Tarefa>();
            foreach (var item in todasTarefas)
            {
                if (item.dataConclusao != DateTime.MinValue)
                {
                    tarefasFechadas.Add(item);
                }
            }
            return tarefasFechadas;
        }
        
        public void ConcluirTarefa(int id)
        {
            string enderecoDeConexao = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DB_ControleDeTarefa;Integrated Security=True;Pooling=False";
            //abre conexao
            SqlConnection con = new SqlConnection(enderecoDeConexao);
            con.Open();
            //config comando
            SqlCommand comando = new SqlCommand();
            comando.Connection = con;

            string sqlAtualizacao =
                    @"UPDATE TB_Tarefa 
	                SET			               
                        [dataConclusao] = @dataConclusao,
                        [percentualDeConclusao] = @percentualDeConclusao
	                WHERE 
		                [ID] = @ID";

            comando.CommandText = sqlAtualizacao;

            comando.Parameters.AddWithValue("dataConclusao", DateTime.Now);
            comando.Parameters.AddWithValue("percentualDeConclusao", 100);
            comando.Parameters.AddWithValue("ID", id);
            //executa

            comando.ExecuteNonQuery();

            con.Close();
        }

        #region metodosOverride
        protected override string PegarStringSelecao()
        {
            return @"SELECT *                    
                    FROM 
                        TB_Tarefa";
        }
        public override List<Tarefa> LerRegistros(SqlDataReader leitorRegistro)
        {
            List<Tarefa> tarefas = new List<Tarefa>();
            while (leitorRegistro.Read())
            {
                int id = Convert.ToInt32(leitorRegistro["Id"]);
                string titulo = Convert.ToString(leitorRegistro["titulo"]);
                int prioridade = Convert.ToInt32(leitorRegistro["prioridade"]);
                DateTime dataDeCriacao = Convert.ToDateTime(leitorRegistro["dataDeCriacao"]);
                DateTime dataConclusao = DateTime.MinValue ;
                if (leitorRegistro["DataConclusao"] != DBNull.Value)
                    dataConclusao = Convert.ToDateTime(leitorRegistro["DataConclusao"]);
                int percentualDeConclusao = Convert.ToInt32(leitorRegistro["percentualDeConclusao"]);
                Tarefa tarefa = new Tarefa(id,titulo,prioridade, dataDeCriacao, dataConclusao,percentualDeConclusao);
              
                tarefas.Add(tarefa);
            }
            return tarefas;
        }
        #endregion
    }

}
