using System;
using Blazor.WebApp.Models;
using DevExpress.XtraReports.UI;

namespace WebApp.Reportes.AtencionesResultado
{
    public partial class AtencionesResultadoReporte
    {
        private InformacionReporte InformacionReporte { get; set; }
        public AtencionesResultadoReporte(InformacionReporte informacionReporte)
        {
            this.InformacionReporte = informacionReporte;
            InitializeComponent();
        }
        protected override void BeforeReportPrint()
        {
            this.P_Ids.Value = InformacionReporte.Ids;
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
        }
    }
}
