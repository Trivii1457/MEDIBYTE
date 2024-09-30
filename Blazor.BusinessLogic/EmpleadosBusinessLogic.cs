using Blazor.Infrastructure;
using Blazor.Infrastructure.Entities;
using DevExpress.Spreadsheet;
using Dominus.Backend.Application;
using Dominus.Backend.DataBase;
using Dominus.Backend.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace Blazor.BusinessLogic
{
    public class EmpleadosBusinessLogic : GenericBusinessLogic<Empleados>
    {
        public EmpleadosBusinessLogic(IUnitOfWork unitWork) : base(unitWork)
        {
        }

        public EmpleadosBusinessLogic(DataBaseSetting configuracionBD) : base(configuracionBD)
        {
        }

        public override Empleados Add(Empleados data)
        {
            var logicaData = new GenericBusinessLogic<Empleados>(this.UnitOfWork.Settings);
            var logicaArchivo = new GenericBusinessLogic<Archivos>(logicaData.UnitOfWork);
            logicaData.BeginTransaction();
            try
            {
                if (data.FirmaDigitalArchivo != null)
                {
                    data.FirmaDigitalArchivoId = ManageArchivo(data.FirmaDigitalArchivo, data.FirmaDigitalArchivoId, logicaArchivo);
                    data.FirmaDigitalArchivo.Id = data.FirmaDigitalArchivoId.GetValueOrDefault(0);
                }
                if (data.SelloDigitalArchivo != null)
                {
                    data.SelloDigitalArchivoId = ManageArchivo(data.SelloDigitalArchivo, data.SelloDigitalArchivoId, logicaArchivo);
                    data.SelloDigitalArchivo.Id = data.SelloDigitalArchivoId.GetValueOrDefault(0);
                }

                data = logicaData.Add(data);
                logicaData.CommitTransaction();
                return data;
            }
            catch (Exception e)
            {
                logicaData.RollbackTransaction();
                throw e;
            }
        }

        public override Empleados Modify(Empleados data)
        {
            var logicaEmpleado = new GenericBusinessLogic<Empleados>(this.UnitOfWork.Settings);
            var logicaArchivo = new GenericBusinessLogic<Archivos>(logicaEmpleado.UnitOfWork);
            logicaEmpleado.BeginTransaction();
            try
            {
                if (data.FirmaDigitalArchivo != null)
                {
                    data.FirmaDigitalArchivoId = ManageArchivo(data.FirmaDigitalArchivo, data.FirmaDigitalArchivoId, logicaArchivo);
                    data.FirmaDigitalArchivo.Id = data.FirmaDigitalArchivoId.GetValueOrDefault(0);
                }
                if (data.SelloDigitalArchivo != null)
                {
                    data.SelloDigitalArchivoId = ManageArchivo(data.SelloDigitalArchivo, data.SelloDigitalArchivoId, logicaArchivo);
                    data.SelloDigitalArchivo.Id = data.SelloDigitalArchivoId.GetValueOrDefault(0);
                }

                data = logicaEmpleado.Modify(data);
                //data = unitOfWork.Repository<Empleados>().Modify(data);
                logicaEmpleado.CommitTransaction();
                return data;
            }
            catch (Exception e)
            {
                logicaEmpleado.RollbackTransaction();
                throw e;
            }
        }

        public override Empleados Remove(Empleados data)
        {
            BlazorUnitWork unitOfWork = new BlazorUnitWork(UnitOfWork.Settings);
            unitOfWork.BeginTransaction();
            try
            {
                data = unitOfWork.Repository<Empleados>().Remove(data);
                EliminarArchivoDeMaestro(data.FirmaDigitalArchivoId, unitOfWork);
                EliminarArchivoDeMaestro(data.SelloDigitalArchivoId, unitOfWork);

                unitOfWork.CommitTransaction();
                return data;
            }
            catch (Exception e)
            {
                unitOfWork.RollbackTransaction();
                throw e;
            }
        }

        public string CargarDatosPlantilla(MemoryStream memoryStream, bool modificaRegistros, string usuario, long empresaId)
        {
            Dictionary<string, List<string>> erroresExcel = new Dictionary<string, List<string>>();

            try
            {
                Workbook workbook = new Workbook();
                workbook.LoadDocument(memoryStream);

                Worksheet sheet = workbook.Worksheets.FirstOrDefault(x => x.Name == "EMPLEADOS");
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
                        var logicaData = new GenericBusinessLogic<Empleados>(this.UnitOfWork.Settings);

                        CellValue cellTipoEmpleado = sheet.GetCellValue(sheet.Columns["A"].Index, i);
                        CellValue cellNumeroIdentificacion = sheet.GetCellValue(sheet.Columns["C"].Index, i);
                        CellValue cellPrimerNombre = sheet.GetCellValue(sheet.Columns["D"].Index, i);
                        CellValue cellSegundoNombre = sheet.GetCellValue(sheet.Columns["E"].Index, i);
                        CellValue cellPrimerApellido = sheet.GetCellValue(sheet.Columns["F"].Index, i);
                        CellValue cellSegundoApellido = sheet.GetCellValue(sheet.Columns["G"].Index, i);
                        CellValue cellFechaNacimiento = sheet.GetCellValue(sheet.Columns["H"].Index, i);
                        CellValue cellTelefono = sheet.GetCellValue(sheet.Columns["I"].Index, i);
                        CellValue cellDireccion = sheet.GetCellValue(sheet.Columns["J"].Index, i);
                        
                        CellValue cellRegistroMedico = sheet.GetCellValue(sheet.Columns["L"].Index, i);
                        CellValue cellEstadoCivil = sheet.GetCellValue(sheet.Columns["M"].Index, i);
                        CellValue cellTipoSangre = sheet.GetCellValue(sheet.Columns["N"].Index, i);
                        CellValue cellCrearUsuario = sheet.GetCellValue(sheet.Columns["O"].Index, i);
                        CellValue cellPassTemp = sheet.GetCellValue(sheet.Columns["P"].Index, i);
                        CellValue cellCorreo = sheet.GetCellValue(sheet.Columns["Q"].Index, i);

                        // Campos ocultos para las foraneas
                        CellValue cellIdTipoEmpleado = sheet.GetCellValue(sheet.Columns["Q"].Index, i);
                        CellValue cellIdTipoIdentificacion = sheet.GetCellValue(sheet.Columns["R"].Index, i);
                        CellValue cellIdGenero = sheet.GetCellValue(sheet.Columns["S"].Index, i);

                        if (string.IsNullOrWhiteSpace(cellTipoEmpleado.TextValue))
                        {
                            tieneDato = false;
                            break;
                        }
                        else
                        {
                            Empleados data = logicaData.FindById(x => x.TiposIdentificacionId == cellIdTipoIdentificacion.NumericValue && x.NumeroIdentificacion == cellNumeroIdentificacion.TextValue, false);
                            if (data == null)
                            {
                                data = new Empleados();
                                data.Id = 0;
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
                                    data.IsNew = false;
                                    data.UpdatedBy = usuario;
                                    data.LastUpdate = DateTime.Now;
                                }
                                else
                                {
                                    erroresFila.Add($"Ya existe el registro en la base de datos.");
                                }
                            }

                            if (!cellIdTipoEmpleado.IsEmpty)
                                data.TipoEmpleados = Convert.ToInt64(cellIdTipoEmpleado.TextValue);
                            if (!cellIdTipoIdentificacion.IsEmpty)
                                data.TiposIdentificacionId = Convert.ToInt64(cellIdTipoIdentificacion.NumericValue);
                            if (!cellNumeroIdentificacion.IsEmpty)
                                data.NumeroIdentificacion = cellNumeroIdentificacion.IsNumeric ? cellNumeroIdentificacion.NumericValue.ToString() : cellNumeroIdentificacion.TextValue.ToString();
                            data.PrimerNombre = cellPrimerNombre.TextValue;
                            data.SegundoNombre = cellSegundoNombre.TextValue;
                            data.PrimerApellido = cellPrimerApellido.TextValue;
                            data.SegundoApellido = cellSegundoApellido.TextValue;
                            if (!cellFechaNacimiento.IsEmpty && cellFechaNacimiento.IsDateTime)
                                data.FechaNacimiento = cellFechaNacimiento.DateTimeValue;
                            data.Telefono = cellTelefono.IsNumeric ? cellTelefono.NumericValue.ToString() : cellTelefono.TextValue;
                            data.Direccion = cellDireccion.TextValue;
                            if(!cellIdGenero.IsEmpty)
                                data.GenerosId = Convert.ToInt64(cellIdGenero.NumericValue);
                            data.RegistroMedico = cellRegistroMedico.TextValue;
                            if (!cellEstadoCivil.IsEmpty)
                                data.EstadosCivilesId = Convert.ToInt64(cellEstadoCivil.TextValue);
                            if (!cellTipoSangre.IsEmpty)
                                data.TiposSangreId = Convert.ToInt64(cellTipoSangre.TextValue);

                            data.DV = DApp.Util.CalcularDigitoVerificacion(data.NumeroIdentificacion);
                            data.EmpresasId = empresaId;

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
