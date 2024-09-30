using Blazor.WebApp.Models;
using DevExpress.XtraPrinting.Drawing;
using System;

namespace WebApp.Reportes.General.RadicacionCuentasReporte
{
    public partial class RadicacionCuentasReporte
    {
        private InformacionReporte InformacionReporte { get; set; }
        public RadicacionCuentasReporte(InformacionReporte informacionReporte)
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
