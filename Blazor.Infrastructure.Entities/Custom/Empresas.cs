using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Blazor.Infrastructure.Entities
{

    public partial class Empresas 
    {

        [NotMapped]
        public List<EmpresasResponsabilidadesFiscales> EmpresasResponsabilidadesFiscales { get; set; }

        [NotMapped]
        public List<EmpresasEsquemasImpuestos> EmpresasEsquemasImpuestos { get; set; }

        public string GetResponsabilidadesFiscales()
        {
            string responsabilidades = "";

            if(EmpresasResponsabilidadesFiscales!=null && EmpresasResponsabilidadesFiscales.Count>0)
                for (int i = 0; i < EmpresasResponsabilidadesFiscales.Count; i++)
                {
                    if (i == 0)
                        responsabilidades += EmpresasResponsabilidadesFiscales[i].ResponsabilidadesFiscales.Codigo;
                    else
                        responsabilidades += ";"+EmpresasResponsabilidadesFiscales[i].ResponsabilidadesFiscales.Codigo;
                }
            return responsabilidades;
        }

        public string GetCodigoImpuestoRecaudados()
        {
            string responsabilidades = "";

            if (EmpresasEsquemasImpuestos != null && EmpresasEsquemasImpuestos.Count > 0)
                responsabilidades = EmpresasEsquemasImpuestos.FirstOrDefault().EsquemasImpuestos.Codigo;

            return responsabilidades;
        }

        public string GetNombreImpuestoRecaudados()
        {
            string responsabilidades = "";
            if (EmpresasEsquemasImpuestos != null && EmpresasEsquemasImpuestos.Count > 0)
                responsabilidades = EmpresasEsquemasImpuestos.FirstOrDefault().EsquemasImpuestos.Descripcion;
            return responsabilidades;
        }

    }
 }
