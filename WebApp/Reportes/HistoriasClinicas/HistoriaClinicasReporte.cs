using System;
using Blazor.WebApp.Models;
using DevExpress.XtraReports.UI;

namespace WebApp.Reportes.HistoriasClinicas
{
    public partial class HistoriasClinicasReporte
    {
        private InformacionReporte InformacionReporte { get; set; }
        public void SetInformacionReporte(InformacionReporte informacionReporte)
        {
            this.InformacionReporte = informacionReporte;
        }
        public HistoriasClinicasReporte()
        {
            InitializeComponent();
        }
        protected override void BeforeReportPrint()
        {
            this.P_Ids.Value = InformacionReporte.Ids;
            this.P_UsuarioGenero.Value = InformacionReporte.ParametrosAdicionales["P_UsuarioGenero"];
            this.logoEmpresa.ImageSource = InformacionReporte.LogoEmpresa;
            base.BeforeReportPrint();
        }

        protected override void OnReportInitialize()
        {
            this.P_Ids.Visible = false;
            base.OnReportInitialize();
        }

        protected override void OnDataSourceDemanded(EventArgs e)
        {
            this.FuenteDatos.ConnectionParameters = InformacionReporte.DataConnectionParametersBase;

            var subReporteRespuestas = new HistoriasClinicasRespuestasSubReporte();
            subReporteRespuestas.SetConnectionParameters(this.FuenteDatos.ConnectionParameters);
            this.HistoriasClinicasRespuestasSubReporte.ReportSource = subReporteRespuestas;

            var subReporteDiagnosticos = new HistoriasClinicasDiagnosticosSubReporte();
            subReporteDiagnosticos.SetConnectionParameters(this.FuenteDatos.ConnectionParameters);
            this.HistoriasClinicasDiagnosticosSubReporte.ReportSource = subReporteDiagnosticos;

            var subReporteOrdenesMedicamentos = new HistoriasClinicasOrdenesMedicamentosSubReporte();
            subReporteOrdenesMedicamentos.SetConnectionParameters(this.FuenteDatos.ConnectionParameters);
            this.HistoriasClinicasOrdenesMedicamentosSubReporte.ReportSource = subReporteOrdenesMedicamentos;

            var subReporteOrdenesServicios = new HistoriasClinicasOrdenesServiciosSubReporte();
            subReporteOrdenesServicios.SetConnectionParameters(this.FuenteDatos.ConnectionParameters);
            this.HistoriasClinicasOrdenesServiciosSubReporte.ReportSource = subReporteOrdenesServicios;

            var subReporteIndicacionesMedicas = new HistoriasClinicasIndicacionesMedicasSubReporte();
            subReporteIndicacionesMedicas.SetConnectionParameters(this.FuenteDatos.ConnectionParameters);
            this.HistoriasClinicasIndicacionesMedicasSubReporte.ReportSource = subReporteIndicacionesMedicas;

            var subReporteIncapacidades = new HistoriasClinicasIncapacidadesSubReporte();
            subReporteIncapacidades.SetConnectionParameters(this.FuenteDatos.ConnectionParameters);
            this.HistoriasClinicasIncapacidadesSubReporte.ReportSource = subReporteIncapacidades;
        }
    }
}
