using ApiWebHospital.Data.RepositoryI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWebHospital.Data.UnitOfWork
{
    public class UnitToWork : IUnitOfWork
    {
        public IMedicoRepository MedicoRepository { get; }

        public IPacienteRepository PacienteRepository { get; }

        public IIngresoRepository IingresoRepository {  get; }

        public IEgresoRepository EgresoRepository { get; }

        private readonly EfContext efContext;

        public UnitToWork(EfContext efContext,IMedicoRepository medicoRepository, IPacienteRepository pacienteRepository, IIngresoRepository iingresoRepository, IEgresoRepository egresoRepository)
        {
            MedicoRepository = medicoRepository;
            PacienteRepository = pacienteRepository;
            IingresoRepository = iingresoRepository;
            EgresoRepository = egresoRepository;
            this.efContext=efContext;
        }

        public void Dispose()
        {
            efContext.Dispose();
        }

        public async Task<int> save()
        => await efContext.SaveChangesAsync();
    }
}
