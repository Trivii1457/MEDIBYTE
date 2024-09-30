using Blazor.Infrastructure;
using Blazor.Infrastructure.Entities;
using DevExpress.Spreadsheet;
using Dominus.Backend.DataBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace Blazor.BusinessLogic
{
    public class ServiciosConsultoriosBusinessLogic : GenericBusinessLogic<ServiciosConsultorios>
    {
        public ServiciosConsultoriosBusinessLogic(IUnitOfWork unitWork) : base(unitWork)
        {
        }

        public ServiciosConsultoriosBusinessLogic(DataBaseSetting configuracionBD) : base(configuracionBD)
        {
        }
        public string CargarDatosPlantilla(MemoryStream memoryStream, bool modificaRegistros, string usuario, long idConsultorio)
        {
            Dictionary<string, List<string>> erroresExcel = new Dictionary<string, List<string>>();

            try
            {
                Workbook workbook = new Workbook();
                workbook.LoadDocument(memoryStream);

                Worksheet sheet = workbook.Worksheets.FirstOrDefault(x => x.Name == "CONSULTORIOS_SERVICIOS");
                if (sheet == null)
                {
                    erroresExcel.Add("Error Hoja", new List<string> { $"Plantilla defectuosa. No contiene la hoja CONVENIOS_SERVICIOS." });
                }
                else
                {
                    bool tieneDato = true;
                    for (int i = 1; tieneDato; i++)
                    {
                        List<string> erroresFila = new List<string>();
                        var logicaData = new GenericBusinessLogic<ServiciosConsultorios>(this.UnitOfWork.Settings);
                        ServiciosConsultorios data = null;

                        CellValue cellCodigoCups = sheet.GetCellValue(sheet.Columns["A"].Index, i);

                        if (cellCodigoCups.IsEmpty)
                        {
                            tieneDato = false;
                            break;
                        }
                        else
                        {
                            try
                            {
                                Servicios servicio = new GenericBusinessLogic<Servicios>(this.UnitOfWork.Settings).FindById(x => x.Cups.Codigo == cellCodigoCups.TextValue, true);
                                if (servicio == null)
                                {
                                    throw new Exception($"No existe un servicio creado en el sistema con el codigo cups {cellCodigoCups.TextValue}.");
                                }

                                data = logicaData.FindById(x => x.ServiciosId == servicio.Id && x.ConsultoriosId == idConsultorio, false);
                                if (data == null)
                                {
                                    data = new ServiciosConsultorios();
                                    data.Id = 0;
                                    data.IsNew = true;
                                    data.CreatedBy = usuario;
                                    data.UpdatedBy = usuario;
                                    data.CreationDate = DateTime.Now;
                                    data.LastUpdate = DateTime.Now;

                                    data.ConsultoriosId = idConsultorio;
                                    data.ServiciosId = servicio.Id;
                                }
                                else
                                {
                                    if (modificaRegistros)
                                    {
                                        data.IsNew = false;
                                        data.UpdatedBy = usuario;
                                        data.LastUpdate = DateTime.Now;
                                    }
                                    else
                                    {
                                        throw new Exception($"Ya existe el registro. Cups: {cellCodigoCups.TextValue} - Servicio {servicio.Nombre}.");
                                    }
                                }

                                List<ValidationResult> erroresentity = new List<ValidationResult>();
                                ValidationContext vc = new ValidationContext(data, null, null);
                                Validator.TryValidateObject(data, vc, erroresentity, validateAllProperties: true);
                                if (erroresentity != null && erroresentity.Count > 0)
                                {
                                    foreach (var item in erroresentity)
                                    {
                                        erroresFila.Add(item.ErrorMessage);
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                erroresFila.Add(e.Message);
                            }

                            if (erroresFila != null && erroresFila.Count > 0)
                            {
                                erroresExcel.Add($"Fila {i + 1}", erroresFila);
                            }
                            else
                            {
                                try
                                {
                                    if (data.IsNew)
                                        logicaData.Add(data);
                                    else
                                    {
                                        if (modificaRegistros)
                                        {
                                            logicaData.Modify(data);
                                        }
                                    }
                                }
                                catch (Exception e)
                                {
                                    erroresExcel.Add($"Fila {i + 1}", new List<string> { e.Message });
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                erroresExcel.Add("Error Plantilla", new List<string> { $"Error en leer la plantilla. | {e.Message}" });
            }


            string PathTempFileErrors = null;
            if (erroresExcel != null && erroresExcel.Count > 0)
            {
                List<string> errores = new List<string>();
                foreach (var item in erroresExcel)
                {
                    string errorFila = null;
                    foreach (var error in item.Value)
                    {
                        errorFila += error;
                    }
                    errores.Add($"{item.Key} : {errorFila}");
                }
                PathTempFileErrors = Path.GetTempFileName();
                File.WriteAllLines(PathTempFileErrors, errores);
            }

            return PathTempFileErrors;
        }

    }

}

