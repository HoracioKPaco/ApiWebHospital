using ApiWebHospital.Data.RepositoryI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWebHospital.Data.UnitOfWork
{
    public interface IUnitOfWork: IDisposable //objetos que tiene recursos no administrados(arch,DB,handles) deben ser liberados
    {
        public IMedicoRepository MedicoRepository { get; }
        public IPacienteRepository PacienteRepository { get; }
        public IIngresoRepository IingresoRepository { get; }
        public IEgresoRepository EgresoRepository { get; }

        Task<int> save();
    }
}
