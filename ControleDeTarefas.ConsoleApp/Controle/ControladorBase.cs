using ControleDeTarefas.ConsoleApp;
using ControleDeTarefas.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeTarefas.ConsoleApp.Controle
{
    public abstract class ControladorBase<T> where T : EntidadeBase
    {
        public List<T> registros;

        public ControladorBase()
        {
            registros = new List<T>();
        }
        #region CRUD
        public bool Inserir(T registro)
        {
            if (registro.Validar())
            {
                InserirNoBanco(registro);
                return true;
            }
            return false;
        }

        public bool Editar(int id, T registro)
        {
            bool idExiste = SelecionarUmRegistroPorId(id) != null;
            if (idExiste && registro.Validar())
            {
                EditarNoBanco(registro, id);
                return true;
            }

            return false;
        }

        public bool Excluir(int id)
        {
            bool idExiste = SelecionarUmRegistroPorId(id) != null;
            if (idExiste)
            {
                ExcluirNoBanco(id);
                return true;
            }
            return false;
        }

        public List<T> ListarRegistrosDoBanco()
        {
            SqlConnection con;
            SqlCommand comando;
            AbrirConexao(out con, out comando);

            string sqlSelecao = PegarStringSelecao();

            comando.CommandText = sqlSelecao;

            SqlDataReader leitorRegistro = comando.ExecuteReader();

            registros = LerRegistros(leitorRegistro);
            con.Close();
            return registros;
        }
        #endregion
        public T SelecionarUmRegistroPorId(int id)
        {
            ListarRegistrosDoBanco();
            return registros.Find(x => x.id == id);
        }

        protected void AbrirConexao(out SqlConnection con, out SqlCommand comando)
        {
            string enderecoDeConexao = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DB_ControleDeTarefa;Integrated Security=True;Pooling=False";
            //abre conexao
            con = new SqlConnection(enderecoDeConexao);
            con.Open();
            //config comando
            comando = new SqlCommand();
            comando.Connection = con;
        }

        protected abstract void InserirNoBanco(T registro);
        protected abstract void EditarNoBanco(T registro, int id);
        protected abstract void ExcluirNoBanco(int id);
        protected abstract string PegarStringSelecao();
        public abstract List<T> LerRegistros(SqlDataReader leitorRegistro);
    }
}
