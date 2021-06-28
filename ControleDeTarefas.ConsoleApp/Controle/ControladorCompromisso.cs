using ControleDeTarefas.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeTarefas.ConsoleApp.Controle
{
    
    public class ControladorCompromisso : ControladorBase<Compromisso>
    {
        public override List<Compromisso> LerRegistros(SqlDataReader leitorRegistro)
        {
            List<Compromisso> compromisso = new List<Compromisso>();
            while (leitorRegistro.Read())
            {
                int id = Convert.ToInt32(leitorRegistro["id"]);
                string assunto = Convert.ToString(leitorRegistro["assunto"]);
                string local = Convert.ToString(leitorRegistro["local"]);
                string link = Convert.ToString(leitorRegistro["link"]);
                DateTime dataInicio = Convert.ToDateTime(leitorRegistro["dataInicio"]);
                DateTime dataTermino = Convert.ToDateTime(leitorRegistro["dataTermino"]);
                int contato_id = Convert.ToInt32(leitorRegistro["contato_id"]);
                string nome = Convert.ToString(leitorRegistro["nome"]);
                // int id,string assunto, string local, string link, DateTime dataInicio, DateTime dataTermino, int contato_id)
                compromisso.Add(new Compromisso(id,nome,assunto,local,link,dataInicio,dataTermino,contato_id));
            }
            return compromisso;
        }
        protected override string PegarStringSelecao()
        {
            return @"SELECT  compr.id,
                         [assunto],
                         [local],
                         [dataInicio],
                         [dataTermino],
                         [contato_id],
                         [link],
                         [nome]
                   FROM TB_Compromisso compr
                   LEFT JOIN TB_Contatos con
                   ON contato_id = con.Id";
        }
        #region CRUD
        protected override void EditarNoBanco(Compromisso compromisso, int id)
        {
            if (ValidarHoraMarcada(compromisso))
            {
                SqlConnection con;
                SqlCommand comando;
                AbrirConexao(out con, out comando);
                //config comando
                string sqlAtualizacao =
                        @"UPDATE TB_Compromisso
	                SET	
		                [assunto] = @assunto, 
		                [local]=@local, 
		                [link] = @link,                       
                        [dataInicio] = @dataInicio,
                        [dataTermino] = @dataTermino,                       
                        [contato_id] = @contato_id
	                WHERE 
		                [ID] = @ID";

                comando.CommandText = sqlAtualizacao;

                comando.Parameters.AddWithValue("assunto", compromisso.assunto);
                comando.Parameters.AddWithValue("local", compromisso.local);
                comando.Parameters.AddWithValue("link", compromisso.link);
                comando.Parameters.AddWithValue("dataInicio", compromisso.dataInicio);
                comando.Parameters.AddWithValue("dataTermino", compromisso.dataTermino);
                comando.Parameters.AddWithValue("contato_id", compromisso.contato_id);
                comando.Parameters.AddWithValue("ID", id);

                //executa

                comando.ExecuteNonQuery();

                con.Close();
            }            
        }
        protected override void ExcluirNoBanco(int id)
        {
            SqlConnection con;
            SqlCommand comando;
            AbrirConexao(out con, out comando);
            //config comando

            string sqlAtualizacao =
                    @"DELETE FROM TB_Compromisso	                
	                WHERE 
		                [ID] = @ID";

            comando.CommandText = sqlAtualizacao;

            comando.Parameters.AddWithValue("ID", id);

            //executa

            comando.ExecuteNonQuery();

            con.Close();
        }
        protected override void InserirNoBanco(Compromisso compromisso)
        {
            if (ValidarHoraMarcada(compromisso))
            {
                SqlConnection con;
                SqlCommand comando;
                AbrirConexao(out con, out comando);
                //config comando       

                string sqlInsercao = @"INSERT INTO TB_Compromisso
                                    ([assunto], 
                                    [local], 
                                    [dataInicio], 
                                    [dataTermino],
                                    [contato_id],
                                    [link])
                                VALUES (@assunto, @local, @dataInicio,
                                @dataTermino, @contato_id, @link);";

                sqlInsercao += @"SELECT SCOPE_IDENTITY();";

                comando.CommandText = sqlInsercao;

                comando.Parameters.AddWithValue("assunto", compromisso.assunto);
                comando.Parameters.AddWithValue("local", compromisso.local);
                comando.Parameters.AddWithValue("dataInicio", compromisso.dataInicio);
                comando.Parameters.AddWithValue("dataTermino", compromisso.dataTermino);
                comando.Parameters.AddWithValue("contato_id", compromisso.contato_id);
                comando.Parameters.AddWithValue("link", compromisso.link);
                //executa
                object id = comando.ExecuteScalar();

                compromisso.id = Convert.ToInt32(id);

                con.Close();
            }
           
        }
        public List<Compromisso> ListarRegistrosPassados()
        {
            List<Compromisso> compromissos = ListarRegistrosDoBanco();
            List<Compromisso> compromissosPassados = new List<Compromisso>();
            foreach (var compromisso in compromissos)
            {
                if (compromisso.dataTermino < DateTime.Now)
                {
                    compromissosPassados.Add(compromisso);
                }
            }
            return compromissosPassados;
        }
        private List<Compromisso> ListarRegistrosFuturos()
        {
            List<Compromisso> compromissos = ListarRegistrosDoBanco();
            List<Compromisso> compromissosFuturos = new List<Compromisso>();
            foreach (var compromisso in compromissos)
            {         
                if (compromisso.dataTermino > DateTime.Today && compromisso.dataInicio > DateTime.Today)
                {
                    compromissosFuturos.Add(compromisso);
                }
            }
            return compromissosFuturos;
        }
        #endregion      
        public bool ValidarHoraMarcada(Compromisso compromisso)
        {         
            foreach (var item in ListarRegistrosDoBanco())
            {
                bool dataJaMarcada = compromisso.dataInicio <= item.dataTermino && compromisso.dataInicio >= item.dataInicio;
                if (dataJaMarcada)
                {
                    return false;
                }
            }
            return true;
        }
        public List<Compromisso> ListarRegistrosDiarios()
        {
            List<Compromisso> compromissos = ListarRegistrosDoBanco();
            List<Compromisso> compromissosDiarios = new List<Compromisso>();
            foreach (var compromisso in compromissos)
            {
                bool compromissoEhHoje = compromisso.dataInicio.Date == DateTime.Today && compromisso.dataTermino.Date == DateTime.Today;
                if (compromissoEhHoje)
                {
                    compromissosDiarios.Add(compromisso);
                }
            }
            return compromissosDiarios;
        }
        public List<Compromisso> ListarRegistrosSemanal()
        {
            List<Compromisso> compromissos = ListarRegistrosFuturos();
            List<Compromisso> compromissosSemanal = new List<Compromisso>();
            foreach (var compromisso in compromissos)
            {
                bool compromissoEhNaSemana = compromisso.dataInicio.Date <= DateTime.Today.AddDays(7) && compromisso.dataTermino.Date <= DateTime.Today.AddDays(7) && compromisso.dataInicio.Date >= DateTime.Today;
                if (compromissoEhNaSemana)
                {
                    compromissosSemanal.Add(compromisso);
                }
            }
            return compromissosSemanal;
        }
    }
}
