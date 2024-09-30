using Blazor.Infrastructure.Entities;
using Dominus.Backend.Application;
using Dominus.Backend.Data;
using Dominus.Backend.DataBase;
using System.Collections.Generic;

namespace Blazor.BusinessLogic
{
    public static class BusinessLogicExtentions
    {
        public static GenericBusinessLogic<T> GetBusinessLogic<T>(this Dominus.Backend.DataBase.BusinessLogic logic) where T : BaseEntity
        {
            if (typeof(T) == typeof(User))
                return new UserBusinessLogic(logic.settings) as GenericBusinessLogic<T>;
            if (typeof(T) == typeof(Empleados))
                return new EmpleadosBusinessLogic(logic.settings) as GenericBusinessLogic<T>;
            if (typeof(T) == typeof(ListaPrecios))
                return new ListaPreciosLogic(logic.settings) as GenericBusinessLogic<T>;
            if (typeof(T) == typeof(LiquidacionHonorarios))
                return new LiquidacionHonorariosLogic(logic.settings) as GenericBusinessLogic<T>;
            if (typeof(T) == typeof(Empresas))
                return new EmpresasBusinessLogic(logic.settings) as GenericBusinessLogic<T>;
            if (typeof(T) == typeof(RadicacionCuentas))
                return new RadicacionCuentasBusinessLogic(logic.settings) as GenericBusinessLogic<T>;
            if (typeof(T) == typeof(Admisiones))
                return new AdmisionesBusinessLogic(logic.settings) as GenericBusinessLogic<T>;
            if (typeof(T) == typeof(Atenciones))
                return new AtencionesBusinessLogic(logic.settings) as GenericBusinessLogic<T>;
            if (typeof(T) == typeof(ProgramacionAgenda))
                return new ProgramacionAgendaBusinessLogic(logic.settings) as GenericBusinessLogic<T>;
            if (typeof(T) == typeof(ProgramacionCitas))
                return new ProgramacionCitasBusinessLogic(logic.settings) as GenericBusinessLogic<T>;
            if (typeof(T) == typeof(Archivos))
                return new ArchivosBusinessLogic(logic.settings) as GenericBusinessLogic<T>;
            if (typeof(T) == typeof(EntregaResultados))
                return new EntregaResultadosBusinessLogic(logic.settings) as GenericBusinessLogic<T>;
            if (typeof(T) == typeof(AdmisionesServiciosPrestados))
                return new AdmisionesServiciosPrestadosBusinessLogic(logic.settings) as GenericBusinessLogic<T>;
            if (typeof(T) == typeof(Notas))
                return new NotasBusinessLogic(logic.settings) as GenericBusinessLogic<T>;
            if (typeof(T) == typeof(NotasDetalles))
                return new NotasDetallesBusinessLogic(logic.settings) as GenericBusinessLogic<T>;
            if (typeof(T) == typeof(Rips))
                return new RipsBusinessLogic(logic.settings) as GenericBusinessLogic<T>;
            return new GenericBusinessLogic<T>(logic.settings);

        }

        public static UserBusinessLogic UserBusinessLogic(this Dominus.Backend.DataBase.BusinessLogic logic)
        {
            return new UserBusinessLogic(logic.settings);
        }
        public static EmpleadosBusinessLogic EmpleadosBusinessLogic(this Dominus.Backend.DataBase.BusinessLogic logic)
        {
            return new EmpleadosBusinessLogic(logic.settings);
        }
        public static PacientesBusinessLogic PacientesBusinessLogic(this Dominus.Backend.DataBase.BusinessLogic logic)
        {
            return new PacientesBusinessLogic(logic.settings);
        }
        public static ListaPreciosLogic ListaPreciosLogic(this Dominus.Backend.DataBase.BusinessLogic logic)
        {
            return new ListaPreciosLogic(logic.settings);
        }
        public static LiquidacionHonorariosLogic LiquidacionHonorariosLogic(this Dominus.Backend.DataBase.BusinessLogic logic)
        {
            return new LiquidacionHonorariosLogic(logic.settings);
        }
        public static FacturasBusinessLogic FacturasBusinessLogic(this Dominus.Backend.DataBase.BusinessLogic logic)
        {
            return new FacturasBusinessLogic(logic.settings);
        }
        public static EmpresasBusinessLogic EmpresasBusinessLogic(this Dominus.Backend.DataBase.BusinessLogic logic)
        {
            return new EmpresasBusinessLogic(logic.settings);
        }
        public static AdmisionesBusinessLogic AdmisionesBusinessLogic(this Dominus.Backend.DataBase.BusinessLogic logic)
        {
            return new AdmisionesBusinessLogic(logic.settings);
        }
        public static AtencionesBusinessLogic AtencionesBusinessLogic(this Dominus.Backend.DataBase.BusinessLogic logic)
        {
            return new AtencionesBusinessLogic(logic.settings);
        }
        public static PreciosServiciosLogic PreciosServiciosLogic(this Dominus.Backend.DataBase.BusinessLogic logic)
        {
            return new PreciosServiciosLogic(logic.settings);
        }

        public static ProgramacionAgendaBusinessLogic ProgramacionAgendaBusinessLogic(this Dominus.Backend.DataBase.BusinessLogic logic)
        {
            return new ProgramacionAgendaBusinessLogic(logic.settings);
        }

        public static ProgramacionCitasBusinessLogic ProgramacionCitasBusinessLogic(this Dominus.Backend.DataBase.BusinessLogic logic)
        {
            return new ProgramacionCitasBusinessLogic(logic.settings);
        }

        public static ImagenesDiagnosticasDetalleBusinessLogic ImagenesDiagnosticasDetalleBusinessLogic(this Dominus.Backend.DataBase.BusinessLogic logic)
        {
            return new ImagenesDiagnosticasDetalleBusinessLogic(logic.settings);
        }

        public static AtencionesResultadoBusinessLogic AtencionesResultadoBusinessLogic(this Dominus.Backend.DataBase.BusinessLogic logic)
        {
            return new AtencionesResultadoBusinessLogic(logic.settings);
        }

        public static BancosBusinessLogic BancosBusinessLogic(this Dominus.Backend.DataBase.BusinessLogic logic)
        {
            return new BancosBusinessLogic(logic.settings);
        }

        public static RipsBusinessLogic RipsBusinessLogic(this Dominus.Backend.DataBase.BusinessLogic logic)
        {
            return new RipsBusinessLogic(logic.settings);
        }

        public static ConfiguracionEnvioEmailBusinessLogic ConfiguracionEnvioEmailBusinessLogic(this Dominus.Backend.DataBase.BusinessLogic logic)
        {
            return new ConfiguracionEnvioEmailBusinessLogic(logic.settings);
        }

        public static FacturasGeneracionBusinessLogic FacturasGeneracionBusinessLogic(this Dominus.Backend.DataBase.BusinessLogic logic)
        {
            return new FacturasGeneracionBusinessLogic(logic.settings);
        }

        public static NotasBusinessLogic NotasBusinessLogic(this Dominus.Backend.DataBase.BusinessLogic logic)
        {
            return new NotasBusinessLogic(logic.settings);
        }

        public static ConveniosServiciosBusinessLogic ConveniosServiciosBusinessLogic(this Dominus.Backend.DataBase.BusinessLogic logic)
        {
            return new ConveniosServiciosBusinessLogic(logic.settings);
        }

        public static ServiciosConsultoriosBusinessLogic ServiciosConsultoriosBusinessLogic(this Dominus.Backend.DataBase.BusinessLogic logic)
        {
            return new ServiciosConsultoriosBusinessLogic(logic.settings);
        }

        #region Identificadores estaticos

        //public static Dictionary<string, Dictionary<string, string>> GetResources(this Dominus.Backend.DataBase.BusinessLogic logic)
        //{
        //    return DApp.DefaultLanguage.GetResources();
        //}

        public static List<KeyValue> GetTransporterType(this Dominus.Backend.DataBase.BusinessLogic logic)
        {
            return new List<KeyValue>
            {
                new KeyValue { Key = 1, Id = "1", Value = "Mensajeria interna" },
                new KeyValue { Key = 2, Id = "2", Value = "Mensajeria externa" },
                new KeyValue { Key = 3, Id = "3", Value = "Transportadoras externas" }
            };
        }

        public static List<KeyValue> GetWareHouseTypes(this Dominus.Backend.DataBase.BusinessLogic logic)
        {
            return new List<KeyValue>
            {
                new KeyValue { Key = 1, Id = "1", Value = "Ventas" },
                new KeyValue { Key = 2, Id = "2", Value = "Devoluciones" },
                new KeyValue { Key = 3, Id = "3", Value = "Averias" },
                new KeyValue { Key = 4, Id = "4", Value = "Cuarentena" }
            };
        }

        public static List<KeyValue> GetIdentificationTypes(this Dominus.Backend.DataBase.BusinessLogic logic)
        {
            return new List<KeyValue>
            {
                new KeyValue { Key = 0, Id = "CC", Value = "Cedula de ciudadania" },
                new KeyValue { Key = 1, Id = "CE", Value = "Cedula de extranjeria" },
                new KeyValue { Key = 2, Id = "TI", Value = "Tarjeta de identidad" },
                new KeyValue { Key = 3, Id = "RC", Value = "Registro civil" },
                new KeyValue { Key = 4, Id = "NIT", Value = "Numero de identificaión tributaria" },
                new KeyValue { Key = 5, Id = "PA", Value = "Pasaporte" },
                new KeyValue { Key = 6, Id = "NU", Value = "No. Unico de Id. Personal" },
                new KeyValue { Key = 7, Id = "AS", Value = "Adulto sin identificación" },
                new KeyValue { Key = 8, Id = "MS", Value = "Menor sin Identificación" },
                new KeyValue { Key = 9, Id = "CD", Value = "Carnet diplomático" },
                new KeyValue { Key = 10, Id = "CN", Value = "Certificado Nacido Vivo" },
                new KeyValue { Key = 11, Id = "SC", Value = "Salvo Conducto" },
                new KeyValue { Key = 12, Id = "PE", Value = "Per Especial Permanencia" },
                new KeyValue { Key = 13, Id = "XX", Value = "Otro" }
            };
        }

        public static List<KeyValue> GetGenders(this Dominus.Backend.DataBase.BusinessLogic logic)
        {
            return new List<KeyValue>
            {
                new KeyValue { Id = "M", Value = "Masculino" },
                new KeyValue { Id = "F", Value = "Femenino" }
            };
        }

        public static List<KeyValue> GetTransactionTypes(this Dominus.Backend.DataBase.BusinessLogic logic)
        {


            return new List<KeyValue>
            {
                new KeyValue { Key = 1, Id = "1", Value = "Orden de compra" },
                new KeyValue { Key = 2, Id = "2", Value = "Entrada de almacen" },
                new KeyValue { Key = 3, Id = "3", Value = "Devolucion de compras" },
                new KeyValue { Key = 4, Id = "4", Value = "Ajustes positivos" },
                new KeyValue { Key = 5, Id = "5", Value = "Ajustes negativos" },
                new KeyValue { Key = 6, Id = "6", Value = "Transferencias de Salida" },
                new KeyValue { Key = 7, Id = "7", Value = "Remisiones" },
                new KeyValue { Key = 8, Id = "8", Value = "Descargue de pendientes formulas medicas" },
                new KeyValue { Key = 9, Id = "9", Value = "Requisiciones" },
                new KeyValue { Key = 10, Id = "10", Value = "Descargue de pendientes requisiciones" },
                new KeyValue { Key = 11, Id = "11", Value = "Suministros" },
                new KeyValue { Key = 12, Id = "12", Value = "Transferencias de Entrada" },
                new KeyValue { Key = 13, Id = "13", Value = "Cotizacion de compra" },
                new KeyValue { Key = 14, Id = "14", Value = "Entrada por compra" }
            };
        }

        public static List<KeyValue> GetDocumentStates(this Dominus.Backend.DataBase.BusinessLogic logic)
        {
            return new List<KeyValue>
            {
                new KeyValue { Key = 0, Id = "E", Value = "En elaboraciòn" },
                new KeyValue { Key = 1, Id = "D", Value = "Definitivo" },
                new KeyValue { Key = 2, Id = "A", Value = "Anulado" },
                new KeyValue { Key = 3, Id = "L", Value = "Legalizado" }
            };
        }

        public static List<KeyValue> GetDocumentStatesPending(this Dominus.Backend.DataBase.BusinessLogic logic)
        {
            return new List<KeyValue>
            {
                new KeyValue { Key = 0, Id = "S", Value = "Separado" },
                new KeyValue { Key = 1, Id = "D", Value = "Descargado" },
                new KeyValue { Key = 2, Id = "A", Value = "Anulado" }
            };
        }

        public static List<KeyValue> GetAttentionTypes(this Dominus.Backend.DataBase.BusinessLogic logic)
        {
            return new List<KeyValue>
            {
                new KeyValue { Key = 0, Id = "A", Value = "Ambulatorio" },
                new KeyValue { Key = 1, Id = "H", Value = "Hospitalario" },
                new KeyValue { Key = 2, Id = "U", Value = "Urgencias" }
            };
        }

        public static List<KeyValue> GetDefectTypes(this Dominus.Backend.DataBase.BusinessLogic logic)
        {
            return new List<KeyValue>
            {
                new KeyValue { Key = 1, Value = "Critico" },
                new KeyValue { Key = 2, Value = "Mayor" },
                new KeyValue { Key = 3, Value = "Menor" }
            };
        }

        public static List<KeyValue> GetCustomerTypes(this Dominus.Backend.DataBase.BusinessLogic logic)
        {
            return new List<KeyValue>
            {
                new KeyValue { Key = 0, Id = "EPS", Value = "EPS" },
                new KeyValue { Key = 1, Id = "ARL", Value = "ARL" },
                new KeyValue { Key = 2, Id = "PRE", Value = "PREPAGADA" },
                new KeyValue { Key = 2, Id = "PCO", Value = "PLAN COMPLEMENTARIO" },
            };
        }

        public static List<KeyValue> GetAffiliateLevelRS(this Dominus.Backend.DataBase.BusinessLogic logic)
        {
            return new List<KeyValue>
            {
                new KeyValue { Key = 0, Id = "I", Value = "I" },
                new KeyValue { Key = 1, Id = "II", Value = "II" },
                new KeyValue { Key = 2, Id = "III", Value = "III" },
                new KeyValue { Key = 3, Id = "IV", Value = "IV" },
            };
        }

        public static List<KeyValue> GetAffiliateCategoryRC(this Dominus.Backend.DataBase.BusinessLogic logic)
        {
            return new List<KeyValue>
            {
                new KeyValue { Key = 0, Id = "A", Value = "A" },
                new KeyValue { Key = 1, Id = "B", Value = "B" },
                new KeyValue { Key = 2, Id = "C", Value = "C" },
            };
        }

        public static List<KeyValue> GetAffiliateLevelOrCategory(this Dominus.Backend.DataBase.BusinessLogic logic)
        {
            return new List<KeyValue>
            {
                new KeyValue { Key = 0, Id = "A", Value = "A" },
                new KeyValue { Key = 1, Id = "B", Value = "B" },
                new KeyValue { Key = 2, Id = "C", Value = "C" },
                new KeyValue { Key = 3, Id = "D", Value = "D" },
                new KeyValue { Key = 4, Id = "I", Value = "I" },
                new KeyValue { Key = 5, Id = "II", Value = "II" },
                new KeyValue { Key = 6, Id = "III", Value = "III" },
                new KeyValue { Key = 7, Id = "IV", Value = "IV" },
            };
        }

        public static List<KeyValue> GetAddressType(this Dominus.Backend.DataBase.BusinessLogic logic)
        {
            return new List<KeyValue>
            {
                new KeyValue { Key = 0, Id = "C", Value = "Casa" },
                new KeyValue { Key = 1, Id = "T", Value = "Trabajo" },
                new KeyValue { Key = 2, Id = "O", Value = "Otro" },
            };
        }

        public static List<KeyValue> GetRadicationType(this Dominus.Backend.DataBase.BusinessLogic logic)
        {
            return new List<KeyValue>
            {
                new KeyValue { Key = 0, Id = "V", Value = "Virtual" },
                new KeyValue { Key = 1, Id = "F", Value = "Física" },
            };
        }

        public static List<KeyValue> GetPriorityType(this Dominus.Backend.DataBase.BusinessLogic logic)
        {
            return new List<KeyValue>
            {
                new KeyValue { Key = 0, Id = "B", Value = "Baja" },
                new KeyValue { Key = 1, Id = "M", Value = "Media" },
                new KeyValue { Key = 2, Id = "A", Value = "Alta" }
            };
        }

        public static List<KeyValue> GetAssuranceType(this Dominus.Backend.DataBase.BusinessLogic logic)
        {
            return new List<KeyValue>
            {
                new KeyValue { Key = 0, Id = "OB", Value = "Obligatorio" },
                new KeyValue { Key = 1, Id = "VO", Value = "Voluntario" }
            };
        }

        #endregion
    }
}