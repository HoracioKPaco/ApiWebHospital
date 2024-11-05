// See https://aka.ms/new-console-template for more information
using ApiWebHospital.Data;
using ApiWebHospital.Data.Repository;
using ApiWebHospital.Data.RepositoryI;
using ApiWebHospital.Model.Layout;
using ApiWebHospital.Service;
using ApiWebHospital.SeviceImplement;
using AppPruevas;
using Microsoft.EntityFrameworkCore;


EfContext efContext = new EfContext();
IIngresoRepository ingresoRepository = new IngresoRepository(efContext);
IEgresoRepository egresoRepository = new EgresoRepository(efContext);
IMedicoRepository medicoRepository = new MedicoRepository(efContext);
IPacienteRepository pacienteRepository = new PacienteRepository(efContext);
IMedicoService medicoService = new MedicoService(medicoRepository);
IPacienteService pacienteService = new PacienteService(pacienteRepository);
IIngresoService ingresoService=new IngresoService(ingresoRepository,medicoService,pacienteService);

prueva d = new prueva(egresoRepository);
d.IniciarPruevas();