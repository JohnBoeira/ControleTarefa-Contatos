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
    #region requisitos
    //[OK]Assunto, local, data do compromisso, hora de início e término.
    //[ok]deverá permitir o registro do link da reunião.
    //[OK] Para os compromissos futuros, deverá ser disponibilizado a possibilidade de filtra-los por período.uma visibilidade semanal e diária.
    //[ok]compromissos estão relacionados com algum contato de sua agenda
    //[OK]possibilidade de registrar novos compromissos, editar e excluir os já existentes
    //[OK]visualizar os compromissos que já passaram e que vão acontecer de forma separada
    //[] não deve deixar adicionar um registro numa hora já marcada
    //[ok]aparecer o nome do contato na visualização do compromissos
    #endregion

    [TestClass]
    public class ControleCompromisso
    {
        ControladorCompromisso controladorCompromisso;
        public ControleCompromisso()
        {
            controladorCompromisso = new ControladorCompromisso();
        }

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
            string sqlReseta = @"DELETE FROM TB_Compromisso; DBCC CHECKIDENT('TB_Compromisso', RESEED, 0)";

            comando.CommandText = sqlReseta;
            comando.ExecuteNonQuery();
            con.Close();
        }
        public void InserirRegistroNoBanco()
        {
            DeletaTabela();
            Compromisso compromisso = new Compromisso("Fazer integrador", "Casa" , "e231-2343e" , new DateTime(2021,06,27, 12,00,00), new DateTime(2021, 06, 27, 16, 00, 00), 1);
            controladorCompromisso.Inserir(compromisso);
        }
        public void InserirRegistroNoBancoDataPassada()
        {
            Compromisso compromisso = new Compromisso("Café", "Angeloni", "", new DateTime(2021, 06, 21, 13, 00, 00), new DateTime(2021, 06, 21, 17, 00, 00), 1);
            controladorCompromisso.Inserir(compromisso);
        }
        [TestMethod]
        public void Inserir()
        {
            InserirRegistroNoBanco();             
            Assert.AreEqual("JUJU", controladorCompromisso.SelecionarUmRegistroPorId(1).nome);
        }
        [TestMethod]
        public void Excluir()
        {
            InserirRegistroNoBanco();
            controladorCompromisso.Excluir(1);
            Assert.AreEqual(0, controladorCompromisso.ListarRegistrosDoBanco().Count);
        }
        [TestMethod]
        public void Editar()
        {
            InserirRegistroNoBanco();
            Compromisso compromisso = new Compromisso("Café", "Angeloni", "", new DateTime(2021, 06, 27, 13, 00, 00), new DateTime(2021, 06, 21, 17, 00, 00), 1);
            controladorCompromisso.Editar(1,compromisso);
            Assert.AreEqual(1, controladorCompromisso.ListarRegistrosDoBanco().Count);
            Assert.AreEqual("Angeloni", controladorCompromisso.SelecionarUmRegistroPorId(1).local);
            Assert.AreEqual("JUJU", controladorCompromisso.SelecionarUmRegistroPorId(1).nome);           
        }
        [TestMethod]
        public void ListarCompromissosPassados()
        {
            InserirRegistroNoBanco();
            InserirRegistroNoBancoDataPassada();           
            Assert.AreEqual(1, controladorCompromisso.ListarRegistrosPassados().Count);
        }
        [TestMethod]
        public void ListarCompromissosSemanais()
        {
            DeletaTabela();
            InserirRegistroNoBancoDataPassada();
            DateTime dataHoje = DateTime.Now.AddDays(2);
            Compromisso compromisso = new Compromisso("Fazer integrador", "Casa", "e231-2343e", dataHoje, dataHoje, 1);
            controladorCompromisso.Inserir(compromisso);
            Assert.AreEqual(1, controladorCompromisso.ListarRegistrosSemanal().Count);
        }
        [TestMethod]
        public void ListarCompromissosDiarios()
        {
            DeletaTabela();
            DateTime dataHoje = DateTime.Now;
            Compromisso compromisso = new Compromisso("Fazer integrador", "Casa", "e231-2343e", dataHoje, dataHoje, 1);
            controladorCompromisso.Inserir(compromisso);
            
            Assert.AreEqual(1, controladorCompromisso.ListarRegistrosDiarios().Count);
        }
        [TestMethod]
        public void InserindoRegistroNumaHoraMarcada()
        {
            //Arrange
            DeletaTabela();
            DateTime dataHoje = DateTime.Now;
            DateTime dataHojeMaisTarde = DateTime.Now.AddHours(2);
            Compromisso compromisso1 = new Compromisso("Fazer integrador", "Casa", "e231-2343e", dataHoje, dataHojeMaisTarde, 1);
            DateTime dataHojeMais1h = DateTime.Now.AddHours(1);
            Compromisso compromisso2 = new Compromisso("Fazer integrador", "Casa", "e231-2343e", dataHojeMais1h, dataHoje, 1);
            //act
            controladorCompromisso.Inserir(compromisso1);
            //Assert
            Assert.IsFalse(controladorCompromisso.ValidarHoraMarcada(compromisso2));
        }

    }
}
