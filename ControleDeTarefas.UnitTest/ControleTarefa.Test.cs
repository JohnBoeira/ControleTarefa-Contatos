using Microsoft.VisualStudio.TestTools.UnitTesting;
using ControleDeTarefas.ConsoleApp.Controle;
using System;
using ControleDeTarefas.ConsoleApp.Dominio;
using System.Data.SqlClient;

namespace ControleDeTarefas.UnitTest
{
    [TestClass]
    public class ControleTarefa
    {
        ControladorTarefa controladorTarefa = new ControladorTarefa();

        public void DeletaTabelaTarefa()
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
            string sqlReseta = @"DELETE FROM TB_Tarefa; DBCC CHECKIDENT('TB_Tarefa', RESEED, 0)";

            comando.CommandText = sqlReseta;
            comando.ExecuteNonQuery();
            con.Close();
        }
 
        public void InserirRegistroNoBanco()
        {
            DeletaTabelaTarefa();
            Tarefa tarefa = new Tarefa("Programar", 3);
            controladorTarefa.Inserir(tarefa);          
        }
        
        [TestMethod]
        public void Inserir()
        {
            DeletaTabelaTarefa();
            Tarefa tarefa = new Tarefa("Programar", 3);       
            controladorTarefa.Inserir(tarefa);          
            Assert.AreEqual("Programar", controladorTarefa.SelecionarUmRegistroPorId(1).titulo);
            Assert.AreEqual(1, controladorTarefa.ListarRegistrosDoBanco().Count) ;
        }

        [TestMethod]
        public void Editar()
        {     
            InserirRegistroNoBanco();
            Tarefa tarefa = new Tarefa("Desenhar", 2);
            //controladorTarefa.EditarNoBanco(tarefa, 1);
            controladorTarefa.Editar(1, tarefa);
            Assert.AreEqual("Desenhar", controladorTarefa.SelecionarUmRegistroPorId(1).titulo);
        }
        
        [TestMethod]
        public void Excluir()
        {
            
            InserirRegistroNoBanco();
            controladorTarefa.Excluir(1);
            Assert.AreEqual(0, controladorTarefa.ListarRegistrosDoBanco().Count);
        }

        [TestMethod]
        public void EditarPercentualConclusao()
        {
         
            InserirRegistroNoBanco();
            controladorTarefa.MudarPercentualConclusao(1, 90);
            Assert.AreEqual(90, controladorTarefa.SelecionarUmRegistroPorId(1).percentualDeConclusao);
        }

        [TestMethod]
        public void ConcluirTarefa()
        {
           
            InserirRegistroNoBanco();
            controladorTarefa.ConcluirTarefa(1);
            Assert.IsTrue(controladorTarefa.SelecionarUmRegistroPorId(1).dataConclusao != DateTime.MinValue);
        }

        [TestMethod]
        public void SelecionarTodosRegistros()
        {           
            InserirRegistroNoBanco();       
            Assert.AreEqual(1, controladorTarefa.ListarRegistrosDoBanco().Count);
            Assert.AreEqual(1, controladorTarefa.registros.Count);
        }

    }
}
