using Blazor.Infrastructure.Entities;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.XtraPrinting.Drawing;
using Dominus.Backend.DataBase;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Blazor.WebApp.Models
{
    public class InformacionReporte
    {
        public Empresas Empresa { get; set; }
        public DataBaseSetting BD { get; set; }
        public long[] Ids { get; set; }
        public Dictionary<string, object> ParametrosAdicionales { get; set; } = new Dictionary<string, object>();
        public DataConnectionParametersBase DataConnectionParametersBase
        {
            get
            {
                try
                {
                    MsSqlConnectionParameters SQLConexion = new MsSqlConnectionParameters(BD.DataSource,BD.InitialCatalog,BD.UserId,BD.Password,MsSqlAuthorizationType.SqlServer);
                    DataConnectionParametersBase dataConnectionParametersBase = SQLConexion;
                    return dataConnectionParametersBase;
                }
                catch (Exception e)
                {
                    throw new Exception("Error en ObtenerParametrosConexion(). | " + e.Message);
                }
            }
            private set { }
        }
        public ImageSource LogoEmpresa
        {
            get
            {
                try
                {
                    byte[] logoArchivo = Empresa?.LogoArchivos?.Archivo;
                    if (logoArchivo != null)
                    {
                        MemoryStream memstr = new MemoryStream(logoArchivo);
                        Image image = Image.FromStream(memstr);
                        return new ImageSource(image);
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Error en LogoEmpresa | " + e.Message);
                }
                
            }
            private set { }
        }
    }
}
