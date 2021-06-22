using ControleDeTarefas.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeTarefas.ConsoleApp.Controle
{
    public class ControladorContato : ControladorBase<Contato>
    {
        public List<Contato> ListarPorCargo(string cargo)
        {
            List<Contato> listaPorCargo = new List<Contato>();
            List<Contato> contatos = ListarRegistrosDoBanco();
            foreach (var contato in contatos)
            {
                if (contato.cargo == cargo)
                {
                    listaPorCargo.Add(contato);
                }
            }
            return listaPorCargo;
        }
        #region CRUD override
        protected override void InserirNoBanco(Contato registro)
        {
            SqlConnection con;
            SqlCommand comando;
            AbrirConexao(out con, out comando);
            //config comando       

            string sqlInsercao = @"INSERT INTO TB_Contatos ([nome], [telefone], [email], [empresa], [cargo]) VALUES (@nome, @telefone, @email, @empresa, @cargo);";

            sqlInsercao += @"SELECT SCOPE_IDENTITY();";

            comando.CommandText = sqlInsercao;

            comando.Parameters.AddWithValue("nome", registro.nome);
            comando.Parameters.AddWithValue("telefone", registro.telefone);
            comando.Parameters.AddWithValue("email", registro.email);
            comando.Parameters.AddWithValue("empresa", registro.email);
            comando.Parameters.AddWithValue("cargo", registro.cargo);

 
            //executa
            object id = comando.ExecuteScalar();

            registro.id = Convert.ToInt32(id);

            con.Close();
        }
        protected override void EditarNoBanco(Contato registro, int id)
        {
            SqlConnection con;
            SqlCommand comando;
            AbrirConexao(out con, out comando);
            //config comando

            string sqlAtualizacao =
                    @"UPDATE TB_Contatos
	                SET	
		                [nome] = @nome, 
		                [telefone]=@telefone, 
		                [email] = @email,                       
                        [empresa] = @empresa,
                        [cargo] = @cargo
	                WHERE 
		                [ID] = @ID";

            comando.CommandText = sqlAtualizacao;

            comando.Parameters.AddWithValue("nome", registro.nome);
            comando.Parameters.AddWithValue("telefone", registro.telefone);
            comando.Parameters.AddWithValue("email", registro.email);
            comando.Parameters.AddWithValue("empresa", registro.email);
            comando.Parameters.AddWithValue("cargo", registro.cargo);
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
            //config comando


            string sqlAtualizacao =
                    @"DELETE FROM TB_Contatos	                
	                WHERE 
		                [ID] = @ID";

            comando.CommandText = sqlAtualizacao;

            comando.Parameters.AddWithValue("ID", id);

            //executa

            comando.ExecuteNonQuery();

            con.Close();
        }
      
        public override List<Contato> LerRegistros(SqlDataReader leitorRegistro)
        {
            List<Contato> contatos = new List<Contato>();
            while (leitorRegistro.Read())
            {
                int id = Convert.ToInt32(leitorRegistro["Id"]);
                string nome = Convert.ToString(leitorRegistro["nome"]);
                string telefone = Convert.ToString(leitorRegistro["telefone"]);
                string email = Convert.ToString(leitorRegistro["email"]);
                string empresa = Convert.ToString(leitorRegistro["empresa"]);
                string cargo = Convert.ToString(leitorRegistro["cargo"]);
             
                contatos.Add(new Contato(id,nome,email,telefone,empresa,cargo));
            }
            return contatos;
        }

        protected override string PegarStringSelecao()
        {
            return @"SELECT * FROM TB_Contatos";
        }
        #endregion
    }
}
