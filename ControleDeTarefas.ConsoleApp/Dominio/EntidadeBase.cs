using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeTarefas.ConsoleApp.Dominio
{
    public abstract class EntidadeBase
    {
        public int id;

        public abstract bool Validar();
    }
}
