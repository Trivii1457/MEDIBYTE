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
    public class BancosBusinessLogic : GenericBusinessLogic<Bancos>
    {
        public BancosBusinessLogic(IUnitOfWork unitWork) : base(unitWork)
        {
        }

        public BancosBusinessLogic(DataBaseSetting configuracionBD) : base(configuracionBD)
        {
        }

        public string CargarDatosPlantilla(MemoryStream memoryStream, bool modificaRegistros, string usuario)
        {
            Dictionary<string, List<string>> erroresExcel = new Dictionary<string, List<string>>();

            try
            {
                Workbook workbook = new Workbook();
                workbook.LoadDocument(memoryStream);

                Worksheet sheet = workbook.Worksheets.FirstOrDefault(x => x.Name == "BANCOS");
                if (sheet == null)
                {
                    erroresExcel.Add("Error Hoja", new List<string> { $"Plantilla defectuosa. No contiene la hoja BANCOS." });
                }
                else
                {
                    bool tieneDato = true;
                    for (int i = 1; tieneDato; i++)
                    {
                        List<string> erroresFila = new List<string>();
                        var logicaData = new GenericBusinessLogic<Bancos>(this.UnitOfWork.Settings);

                        CellValue cellCodigo = sheet.GetCellValue(sheet.Columns["A"].Index, i);
                        CellValue cellDescripcion = sheet.GetCellValue(sheet.Columns["B"].Index, i);

                        if (cellCodigo.IsEmpty)
                        {
                            tieneDato = false;
                            break;
                        }
                        else
                        {
                            Bancos data = logicaData.FindById(x => x.Codigo == cellCodigo.TextValue, false);
                            if (data == null)
                            {
                                data = new Bancos();
                                data.Id = 0;
                                data.Codigo = cellCodigo.TextValue;
                                data.Descripcion = cellDescripcion.TextValue;
                                data.IsNew = true;
                                data.CreatedBy = usuario;
                                data.UpdatedBy = usuario;
                                data.CreationDate = DateTime.Now;
                                data.LastUpdate = DateTime.Now;
                            }
                            else
                            {
                                if (modificaRegistros)
                                {
                                    data.Descripcion = cellDescripcion.TextValue;
                                    data.IsNew = false;
                                    data.UpdatedBy = usuario;
                                    data.LastUpdate = DateTime.Now;
                                }
                                else
                                {
                                    erroresFila.Add($"Ya existe el registro en la base de datos.");
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
                                    var error = "";
                                    while (e.InnerException != null)
                                    {
                                        error += $"{e.Message}. ";
                                        e = e.InnerException;
                                    }
                                    erroresExcel.Add($"Fila {i + 1}", new List<string> { error });
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

