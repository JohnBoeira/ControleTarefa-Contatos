using ControleDeTarefas.ConsoleApp.Controle;
using ControleDeTarefas.ConsoleApp.Dominio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeTarefas.UnitTest
{
    [TestClass]
    public class ControleContato
    {
        ControladorContato controladorContato = new ControladorContato();

        public void DeletaTabela()
        {
            SqlConnection con;
            SqlCommand comando;
            string enderecoDeConexao = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DB_ControleDeTarefa;Integrated Security=True;Pooling=False";
            //abre conexao
            con = new SqlConnection(enderecoDeConexao);
            con.Open();
            //config comando
            comando = new SqlCommand();
            comando.Connection = con;
            string sqlReseta = @"DELETE FROM TB_Contatos; DBCC CHECKIDENT('TB_Contatos', RESEED, 0)";

            comando.CommandText = sqlReseta;
            comando.ExecuteNonQuery();
            con.Close();
        }
        public void InserirRegistroNoBanco()
        {
            //DeletaTabela();
            Contato contato = new Contato("JUJU", "jucas@email.com", "80808080", "senai", "diretor");
            controladorContato.Inserir(contato);
         
        }
        [TestMethod]
        public void Inserir()
        {
            InserirRegistroNoBanco();
            Contato contato = new Contato("Juca","jucas@email.com","80808080","senai","diretor");
            controladorContato.Inserir(contato);
            Assert.AreEqual(2, controladorContato.ListarRegistrosDoBanco().Count);
            Assert.AreEqual("Juca", controladorContato.SelecionarUmRegistroPorId(2).nome);
        }
        [TestMethod]
        public void EditarContato()
        {
            InserirRegistroNoBanco();
            Contato contato = new Contato("Jose", "jus@email.com", "80808080", "BB", "gerente");
            controladorContato.Editar(1, contato);
            Assert.AreEqual(1, controladorContato.ListarRegistrosDoBanco().Count);
            Assert.AreEqual("Jose", controladorContato.SelecionarUmRegistroPorId(1).nome);
        }
        [TestMethod]
        public void ExcluirContato()
        {
            InserirRegistroNoBanco();      
            controladorContato.Excluir(1);
            Assert.AreEqual(0, controladorContato.ListarRegistrosDoBanco().Count);
 
        }
        [TestMethod]
        public void ListarRegistrosPorCargo()
        {
            InserirRegistroNoBanco();
            Assert.AreEqual(1, controladorContato.ListarPorCargo("diretor").Count);
            
        }
    }
}
