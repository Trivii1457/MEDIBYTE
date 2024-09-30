using System;
using Blazor.WebApp.Models;
using DevExpress.XtraReports.UI;
using WebApp.Reportes.HistoriasClinicas;

namespace WebApp.Reportes.HistoriaClinicasNotasAclaratorias
{
    public partial class HistoriaClinicasNotasAclaratoriasReporte
    {
        private InformacionReporte InformacionReporte { get; set; }
        public void SetInformacionReporte(InformacionReporte informacionReporte)
        {
            this.InformacionReporte = informacionReporte;
        }
        public HistoriaClinicasNotasAclaratoriasReporte()
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

            var subReporteDiagnosticos = new HistoriasClinicasDiagnosticosSubReporte();
            subReporteDiagnosticos.SetConnectionParameters(this.FuenteDatos.ConnectionParameters);
            this.HistoriasClinicasDiagnosticosSubReporte.ReportSource = subReporteDiagnosticos;

        }
    }
}
