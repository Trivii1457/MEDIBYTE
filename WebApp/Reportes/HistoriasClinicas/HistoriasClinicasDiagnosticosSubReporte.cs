using System;
using Blazor.WebApp.Models;
using DevExpress.XtraReports.UI;

namespace WebApp.Reportes.HistoriasClinicas
{
    public partial class HistoriasClinicasDiagnosticosSubReporte
    {
        public HistoriasClinicasDiagnosticosSubReporte()
        {
            InitializeComponent();
        }
        public void SetConnectionParameters(DevExpress.DataAccess.ConnectionParameters.DataConnectionParametersBase connectionParameters)
        {
            this.FuenteDatos.ConnectionParameters = connectionParameters;
        }
    }
}
