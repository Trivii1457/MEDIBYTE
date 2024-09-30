using System;
using Blazor.WebApp.Models;
using DevExpress.XtraReports.UI;

namespace WebApp.Reportes.EntregaAdmisiones
{
    public partial class EntregaAdmisionesReporte
    {
        private InformacionReporte InformacionReporte { get; set; }
        public void SetInformacionReporte(InformacionReporte informacionReporte)
        {
            this.InformacionReporte = informacionReporte;
        }
        public EntregaAdmisionesReporte()
        {
            InitializeComponent();
        }
        protected override void BeforeReportPrint()
        {
            this.p_FechaDesde.Value = InformacionReporte.ParametrosAdicionales["p_FechaDesde"];
            this.p_FechaHasta.Value = InformacionReporte.ParametrosAdicionales["p_FechaHasta"];
            this.p_SedeId.Value = InformacionReporte.ParametrosAdicionales["p_SedeId"];
            this.p_UsuarioGenero.Value = InformacionReporte.ParametrosAdicionales["p_UsuarioGenero"];
            this.logoEmpresa.ImageSource = InformacionReporte.LogoEmpresa;
            base.BeforeReportPrint();
        }

        protected override void OnReportInitialize()
        {
            this.p_FechaDesde.Visible = false;
            this.p_FechaHasta.Visible = false;
            this.p_SedeId.Visible = false;
            this.p_UsuarioGenero.Visible = false;
            base.OnReportInitialize();
        }

        protected override void OnDataSourceDemanded(EventArgs e)
        {
            this.FuenteDatos.ConnectionParameters = InformacionReporte.DataConnectionParametersBase;

            var entregaAdmisionesTotalesSubReporte = new EntregaAdmisionesTotalesSubReporte();
            entregaAdmisionesTotalesSubReporte.SetConnectionParameters(this.FuenteDatos.ConnectionParameters);
            this.EntregaAdmisionesTotalesSubReporte.ReportSource = entregaAdmisionesTotalesSubReporte;

        }
    }
}
