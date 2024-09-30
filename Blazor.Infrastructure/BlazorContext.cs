using Blazor.Infrastructure.Entities;
using Dominus.Backend.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace Blazor.Infrastructure
{
    public class BlazorContext : DContext
    {
        public DbSet<AdministracionHonorarios> AdministracionHonorarios { get; set; }
        public DbSet<AdministracionHonorariosLecturas> AdministracionHonorariosLecturas { get; set; }
        public DbSet<AdministracionHonorariosServicios> AdministracionHonorariosServicios { get; set; }
        public DbSet<Admisiones> Admisiones { get; set; }
        public DbSet<AdmisionesServiciosPrestados> AdmisionesServiciosPrestados { get; set; }
        public DbSet<AdmisionesServiciosPrestadosArchivos> AdmisionesServiciosPrestadosArchivos { get; set; }
        public DbSet<AdmisionesServiciosPrestadosResultado> AdmisionesServiciosPrestadosResultado { get; set; }
        public DbSet<Archivos> Archivos { get; set; }
        public DbSet<Atenciones> Atenciones { get; set; }
        public DbSet<AtencionesResultado> AtencionesResultado { get; set; }
        public DbSet<Audit> Audit { get; set; }
        public DbSet<Bancos> Bancos { get; set; }
        public DbSet<BasesRetencion> BasesRetencion { get; set; }
        public DbSet<Cajas> Cajas { get; set; }
        public DbSet<CategoriasIngresos> CategoriasIngresos { get; set; }
        public DbSet<CategoriasIngresosDetalles> CategoriasIngresosDetalles { get; set; }
        public DbSet<CategoriasServicios> CategoriasServicios { get; set; }
        public DbSet<CausalesGlosas> CausalesGlosas { get; set; }
        public DbSet<CausasExternas> CausasExternas { get; set; }
        public DbSet<CiclosCajas> CiclosCajas { get; set; }
        public DbSet<Ciudades> Ciudades { get; set; }
        public DbSet<Consultorios> Consultorios { get; set; }
        public DbSet<Convenios> Convenios { get; set; }
        public DbSet<ConveniosServicios> ConveniosServicios { get; set; }
        public DbSet<Cups> Cups { get; set; }
        public DbSet<Departamentos> Departamentos { get; set; }
        public DbSet<Diagnosticos> Diagnosticos { get; set; }
        public DbSet<EmpleadoProfesiones> EmpleadoProfesiones { get; set; }
        public DbSet<Empleados> Empleados { get; set; }
        public DbSet<EmpleadosEspecialidades> EmpleadosEspecialidades { get; set; }
        public DbSet<Empresas> Empresas { get; set; }
        public DbSet<EmpresasEsquemasImpuestos> EmpresasEsquemasImpuestos { get; set; }
        public DbSet<EmpresasResponsabilidadesFiscales> EmpresasResponsabilidadesFiscales { get; set; }
        public DbSet<Entidades> Entidades { get; set; }
        public DbSet<EntidadesEsquemasImpuestos> EntidadesEsquemasImpuestos { get; set; }
        public DbSet<EntidadesResponsabilidadesFiscales> EntidadesResponsabilidadesFiscales { get; set; }
        public DbSet<Especialidades> Especialidades { get; set; }
        public DbSet<EsquemasImpuestos> EsquemasImpuestos { get; set; }
        public DbSet<Estados> Estados { get; set; }
        public DbSet<EstadosCiviles> EstadosCiviles { get; set; }
        public DbSet<Etnia> Etnia { get; set; }
        public DbSet<Facturas> Facturas { get; set; }
        public DbSet<FacturasDetalles> FacturasDetalles { get; set; }
        public DbSet<FacturasGeneracion> FacturasGeneracion { get; set; }
        public DbSet<Festivos> Festivos { get; set; }
        public DbSet<Filiales> Filiales { get; set; }
        public DbSet<FinalidadConsulta> FinalidadConsulta { get; set; }
        public DbSet<FinalidadProcedimiento> FinalidadProcedimiento { get; set; }
        public DbSet<FormasPagos> FormasPagos { get; set; }
        public DbSet<Generos> Generos { get; set; }
        public DbSet<Glosas> Glosas { get; set; }
        public DbSet<HistoriasClinicas> HistoriasClinicas { get; set; }
        public DbSet<HorariosAtencion> HorariosAtencion { get; set; }
        public DbSet<Incapacidades> Incapacidades { get; set; }
        public DbSet<EnviaEmailLog> EnviaEmailLog { get; set; }
        public DbSet<IncapacidadesOrigenes> IncapacidadesOrigenes { get; set; }
        public DbSet<JobDetail> JobDetail { get; set; }
        public DbSet<JobLog> JobLog { get; set; }
        public DbSet<Job> Job { get; set; }
        public DbSet<LanguageResource> LanguageResource { get; set; }
        public DbSet<Languages> Languages { get; set; }
        public DbSet<LiquidacionHonorarios> LiquidacionHonorarios { get; set; }
        public DbSet<LiquidacionHonorariosDetalle> LiquidacionHonorariosDetalle { get; set; }
        public DbSet<ListaPrecios> ListaPrecios { get; set; }
        public DbSet<Medicamentos> Medicamentos { get; set; }
        public DbSet<MedicamentosConcentraciones> MedicamentosConcentraciones { get; set; }
        public DbSet<MedicamentosFormasFarmaceuticas> MedicamentosFormasFarmaceuticas { get; set; }
        public DbSet<MedicamentosViaAdministracion> MedicamentosViaAdministracion { get; set; }
        public DbSet<MediosPago> MediosPago { get; set; }
        public DbSet<MenuAction> MenuAction { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<Office> Office { get; set; }
        public DbSet<OrdenesMedicamentos> OrdenMedicamentos { get; set; }
        public DbSet<OrdenesMedicamentosDiagnosticos> OrdenesMedicamentosDiagnosticos { get; set; }
        public DbSet<OrdenesMedicamentosDetalles> OrdenesMedicamentosDetalles { get; set; }
        public DbSet<OrdenesServicios> OrdenServicios { get; set; }
        public DbSet<OrdenesServiciosDiagnosticos> OrdenesServiciosDiagnosticos { get; set; }
        public DbSet<OrdenesServiciosDetalles> OrdenesServiciosDetalles { get; set; }
        public DbSet<PacientesEntidades> PacientesEntidades { get; set; }
        public DbSet<Pacientes> Pacientes { get; set; }
        public DbSet<Paises> Paises { get; set; }
        public DbSet<PreciosServicios> PreciosServicios { get; set; }
        public DbSet<Profesiones> Profesiones { get; set; }
        public DbSet<ProfileNavigation> ProfileNavigation { get; set; }
        public DbSet<Profile> Profile { get; set; }
        public DbSet<ProfileUser> ProfileUser { get; set; }
        public DbSet<RangoEdad> RangoEdad { get; set; }
        public DbSet<Recaudos> Recaudos { get; set; }
        public DbSet<RecaudosDetalles> RecaudosDetalles { get; set; }
        public DbSet<RegistroIncapacidades> RegistroIncapacidades { get; set; }
        public DbSet<ResponsabilidadesFiscales> ResponsabilidadesFiscales { get; set; }
        public DbSet<Sedes> Sedes { get; set; }
        public DbSet<Servicios> Servicios { get; set; }
        public DbSet<ServiciosConsultorios> ServiciosConsultorios { get; set; }
        public DbSet<ServiciosSedes> ServiciosSedes { get; set; }
        public DbSet<Sessions> Sessions { get; set; }
        public DbSet<TipoEmpleados> TipoEmpleados { get; set; }
        public DbSet<TipoEntidades> TipoEntidades { get; set; }
        public DbSet<TiposAfiliados> TiposAfiliados { get; set; }
        public DbSet<TiposAtencion> TiposAtencion { get; set; }
        public DbSet<TiposCitas> TiposCitas { get; set; }
        public DbSet<TiposConsecutivos> TiposConsecutivos { get; set; }
        public DbSet<TiposContratos> TiposContratos { get; set; }
        public DbSet<TiposDiagnosticos> TiposDiagnosticos { get; set; }
        public DbSet<TiposDocumentos> TiposDocumentos { get; set; }
        public DbSet<TiposIdentificacion> TiposIdentificacion { get; set; }
        public DbSet<TiposPersonas> TiposPersonas { get; set; }
        public DbSet<TiposRegimenes> TiposRegimenes { get; set; }
        public DbSet<TiposSangre> TiposSangre { get; set; }
        public DbSet<TiposServicios> TiposServicios { get; set; }
        public DbSet<UserOffice> UserOffice { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<RadicacionCuentas> RadicacionCuentas { get; set; }
        public DbSet<RadicacionCuentasDetalles> RadicacionCuentasDetalles { get; set; }
        public DbSet<ProgramacionAgenda> ProgramacionAgenda { get; set; }
        public DbSet<ProgramacionDisponible> ProgramacionDisponible { get; set; }
        public DbSet<ServiciosEmpleados> ServiciosEmpleados { get; set; }
        public DbSet<Documentos> Documentos { get; set; }
        public DbSet<SedesDocumentos> SedesDocumentos { get; set; }
        public DbSet<ConfiguracionEnvioEmail> ConfiguracionEnvioEmail { get; set; }
        public DbSet<ProgramacionCitas> ProgramacionCitas { get; set; }
        public DbSet<ImagenesDiagnosticas> ImagenesDiagnosticas { get; set; }
        public DbSet<ImagenesDiagnosticasDetalle> ImagenesDiagnosticasDetalle { get; set; }
        public DbSet<HCPreguntas> HCPreguntas { get; set; }
        public DbSet<HCRespuestas> HCRespuestas { get; set; }
        public DbSet<HCTipos> HCTipos { get; set; }
        public DbSet<HCTiposPreguntas> HCTiposPreguntas { get; set; }
        public DbSet<HistoriasClinicasRespuestas> HistoriasClinicasRespuestas { get; set; }
        public DbSet<HistoriasClinicasDiagnosticos> HistoriasClinicasDiagnosticos { get; set; }
        public DbSet<HistoriasClinicasNotasAclaratorias> HistoriasClinicasNotasAclaratorias { get; set; }
        public DbSet<EntregaResultados> EntregaResultados { get; set; }
        public DbSet<EntregaResultadosDetalles> EntregaResultadosDetalles { get; set; }
        public DbSet<Parentescos> Parentescos { get; set; }
        public DbSet<MediosEntregas> MediosEntregas { get; set; }
        public DbSet<Rips> Rips { get; set; }
        public DbSet<IndicacionesMedicas> IndicacionesMedicas { get; set; }
        public DbSet<IndicacionesMedicasDetalles> IndicacionesMedicasDetalles { get; set; }
        public DbSet<Notas> Notas { get; set; }
        public DbSet<NotasDetalles> NotasDetalles { get; set; }
        public DbSet<NotasConceptos> NotasConceptos { get; set; }
        public DbSet<EventosDIAN> EventosDIAN { get; set; }
        public DbSet<ParametrosGenerales> ParametrosGenerales { get; set; }

        #region VISTAS
        public DbSet<ServiciosFacturar> ServiciosFacturar { get; set; }
        public DbSet<VEntregaAdmisiones> VEntregaAdmisiones { get; set; }
        public DbSet<VContabilizacionRegistro> VContabilizacionRegistro { get; set; }

        #endregion


        public BlazorContext(DataBaseSetting setting) : base(setting)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#if DEBUG
            optionsBuilder.LogTo(Console.WriteLine);
#endif
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
    }
}