using ControleDeTarefas.ConsoleApp.Controle;
using ControleDeTarefas.ConsoleApp.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeTarefas.ConsoleApp.Telas
{
    public abstract class TelaCadastroBasico<T> : TelaBase where T : EntidadeBase
    {
        ControladorBase<T> controladorBase;
        
        public TelaCadastroBasico(ControladorBase<T> controladorBase)
        {
            this.controladorBase = controladorBase;
        }

        #region CRUD
        public void Inserir()
        { 

            T registro = ObterRegistro("Inserir");

            bool resultadoValidacao = controladorBase.Inserir(registro);

            if (resultadoValidacao)
                ApresentarMensagem("Item adicionado com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Não possível adicionar items", TipoMensagem.Erro);
                Inserir();
            }
        }
        public void Editar()
        {
            Listar();
            int id = ObterIdParaAlteracao();
            bool achoRegistro = controladorBase.SelecionarUmRegistroPorId(id) != null;
            

            
            if (achoRegistro == false)
            {
                ApresentarMensagem("Id não encontrado", TipoMensagem.Erro);
                return;  
            }

            T registro = ObterRegistro("Editar");

            bool resultadoValidacao = controladorBase.Editar(id, registro);

            if (resultadoValidacao)
                ApresentarMensagem("Editado com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Erro ao editar", TipoMensagem.Erro);
                Editar();
            }
        }
        public void Excluir()
        {
            Listar();

            int id = ObterIdParaAlteracao();
            bool achoRegistro = controladorBase.SelecionarUmRegistroPorId(id) != null;

            if (achoRegistro == false)
            {
                ApresentarMensagem("Id não encontrado", TipoMensagem.Erro);
                return;
            }

            bool conseguiuExcluir = controladorBase.Excluir(id);

            if (conseguiuExcluir)
                ApresentarMensagem("Registro excluido", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Erro ao excluir", TipoMensagem.Erro);
                Excluir();
            }
        }
        public void Listar()
        {         
            List<T> registros = controladorBase.ListarRegistrosDoBanco();

            if (registros.Count == 0)
            {
                ApresentarMensagem("Não há registros", TipoMensagem.Erro);
                return;
            }
            ListarRegistros();
        }
        #endregion

        #region metodos abstract
        protected abstract T ObterRegistro(string tipo);

        protected abstract void ListarRegistros();

        protected abstract void CasosEspeciais();
        #endregion

        protected int ObterIdParaAlteracao()
        {
            Console.WriteLine("Digite id para Alteração:");
            return Convert.ToInt32(Console.ReadLine());
        }
        
        public void RealizaAcao()
        {
            Console.Clear();
            string opcao = ObterOpcaoCrud();
            switch (opcao)
            {
                case "1": Inserir(); break;
                case "2": Listar(); break;
                case "3": Editar(); break;
                case "4": Excluir(); break;
                case "5": CasosEspeciais(); break;
                default:
                    break;
            }
            
        }
    }
}
