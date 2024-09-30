using System;
using Blazor.WebApp.Models;
using DevExpress.XtraReports.UI;
using WebApp.Reportes.Incapacidades;
using WebApp.Reportes.IndicacionesMedicas;
using WebApp.Reportes.OrdenesMedicamentos;
using WebApp.Reportes.OrdenesServicios;

namespace WebApp.Reportes.HistoriasClinicas
{
    public partial class DocumentosAPacientesReporte
    {
        private InformacionReporte InformacionReporte { get; set; }
        public void SetInformacionReporte(InformacionReporte informacionReporte)
        {
            this.InformacionReporte = informacionReporte;
        }
        public DocumentosAPacientesReporte()
        {
            InitializeComponent();
        }
        protected override void BeforeReportPrint()
        {
            this.P_Ids.Value = InformacionReporte.Ids[0];
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

            var subReporteIncapacidades = new IncapacidadesReporte();
            subReporteIncapacidades.SetInformacionReporte(InformacionReporte,true);
            this.IncapacidadesSubReporte.ReportSource = subReporteIncapacidades;

            var subReporteIndicacionesMedicas = new IndicacionesMedicasReporte();
            subReporteIndicacionesMedicas.SetInformacionReporte(InformacionReporte,true);
            this.IndicacionesMedicasSubReporte.ReportSource = subReporteIndicacionesMedicas;

            var subReporteOrdenesMedicamentos = new OrdenesMedicamentosReporte();
            subReporteOrdenesMedicamentos.SetInformacionReporte(InformacionReporte,true);
            this.OrdenesMedicamentosSubReporte.ReportSource = subReporteOrdenesMedicamentos;

            var subReporteOrdenesServicios = new OrdenesServiciosReporte();
            subReporteOrdenesServicios.SetInformacionReporte(InformacionReporte,true);
            this.OrdenesServiciosSubReporte.ReportSource = subReporteOrdenesServicios;
        }
    }
}
