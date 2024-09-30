using System;
using Blazor.WebApp.Models;

namespace WebApp.Reportes.Facturas
{
    public partial class FacturasParticularReporte
    {
        private InformacionReporte InformacionReporte { get; set; }
        public void SetInformacionReporte(InformacionReporte informacionReporte)
        {
            this.InformacionReporte = informacionReporte;
        }
        public FacturasParticularReporte()
        {
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