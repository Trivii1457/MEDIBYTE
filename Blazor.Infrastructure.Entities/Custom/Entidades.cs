using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Blazor.Infrastructure.Entities
{

    public partial class Entidades 
    {
        [NotMapped]
        public List<EntidadesResponsabilidadesFiscales> EntidadesResponsabilidadesFiscales { get; set; }

        [NotMapped]
        public List<EntidadesEsquemasImpuestos> EntidadesEsquemasImpuestos { get; set; }

        public string GetResponsabilidadesFiscales()
        {
            string responsabilidades = "";

            if (EntidadesResponsabilidadesFiscales != null && EntidadesResponsabilidadesFiscales.Count > 0)
                for (int i = 0; i < EntidadesResponsabilidadesFiscales.Count; i++)
                {
                    if (i == 0)
                        responsabilidades += EntidadesResponsabilidadesFiscales[i].ResponsabilidadesFiscales.Codigo;
                    else
                        responsabilidades += ";" + EntidadesResponsabilidadesFiscales[i].ResponsabilidadesFiscales.Codigo;
                }
            return responsabilidades;
        }

        public string GetCodigoImpuestoRecaudados()
        {
            string responsabilidades = "";

            if (EntidadesEsquemasImpuestos != null && EntidadesEsquemasImpuestos.Count > 0)
                responsabilidades = EntidadesEsquemasImpuestos.FirstOrDefault().EsquemasImpuestos.Codigo;

            return responsabilidades;
        }

        public string GetRegimenFiscal()
        {
            return EsResponsabledeIva;
        }

        public string GetNombreImpuestoRecaudados()
        {
            string responsabilidades = "";
            if (EntidadesEsquemasImpuestos != null && EntidadesEsquemasImpuestos.Count > 0)
                responsabilidades = EntidadesEsquemasImpuestos.FirstOrDefault().EsquemasImpuestos.Descripcion;
            return responsabilidades;
        }

    }
 }
