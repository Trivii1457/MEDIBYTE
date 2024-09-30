using Blazor.Infrastructure;
using Blazor.Infrastructure.Entities;
using Dominus.Backend.Application;
using Dominus.Backend.DataBase;
using System;
using System.Linq;

namespace Blazor.BusinessLogic
{
    public class NotasDetallesBusinessLogic : GenericBusinessLogic<NotasDetalles>
    {
        public NotasDetallesBusinessLogic(IUnitOfWork unitWork) : base(unitWork)
        {
        }

        public NotasDetallesBusinessLogic(DataBaseSetting configuracionBD) : base(configuracionBD)
        {
        }

        public override NotasDetalles Add(NotasDetalles data)
        {
            BlazorUnitWork unitOfWork = new BlazorUnitWork(UnitOfWork.Settings);
            unitOfWork.BeginTransaction();
            try
            {
                data = unitOfWork.Repository<NotasDetalles>().Add(data);
                var nota = unitOfWork.Repository<Notas>().Table.FirstOrDefault(x => x.Id == data.NotasId);

                if (nota == null)
                {
                    throw new Exception("Error encontrando encabezado de la nota.");
                }

                var detalles = unitOfWork.Repository<NotasDetalles>().Table.Where(x => x.NotasId == data.NotasId);

                nota.ValorTotal = detalles.Sum(x => x.ValorTotal);
                nota.ValorSubtotal = detalles.Sum(x => x.SubTotal);
                nota.ValorDescuentos = detalles.Sum(x => x.SubTotal * (x.PorcDescuento / 100));
                nota.ValorImpuestos = detalles.Sum(x => (x.SubTotal - (x.SubTotal * (x.PorcDescuento / 100))) * (x.PorcImpuesto / 100));

                nota.MontoEscrito = DApp.Util.NumeroEnLetras(nota.ValorTotal);
                nota = unitOfWork.Repository<Notas>().Modify(nota);

                unitOfWork.CommitTransaction();
                return data;
            }
            catch (Exception e)
            {
                unitOfWork.RollbackTransaction();
                throw e;
            }

        }

        public override NotasDetalles Remove(NotasDetalles data)
        {
            BlazorUnitWork unitOfWork = new BlazorUnitWork(UnitOfWork.Settings);
            unitOfWork.BeginTransaction();
            try
            {
                data = unitOfWork.Repository<NotasDetalles>().Remove(data);
                var nota = unitOfWork.Repository<Notas>().Table.FirstOrDefault(x => x.Id == data.NotasId);

                if (nota == null)
                {
                    throw new Exception("Error encontrando encabezado de la nota.");
                }

                var detalles = unitOfWork.Repository<NotasDetalles>().Table.Where(x => x.NotasId == data.NotasId);

                nota.ValorTotal = detalles.Sum(x => x.ValorTotal);
                nota.ValorSubtotal = detalles.Sum(x => x.SubTotal);
                nota.ValorDescuentos = detalles.Sum(x => x.SubTotal * (x.PorcDescuento / 100));
                nota.ValorImpuestos = detalles.Sum(x => (x.SubTotal - (x.SubTotal * (x.PorcDescuento / 100))) * (x.PorcImpuesto / 100));

                nota.MontoEscrito = DApp.Util.NumeroEnLetras(nota.ValorTotal);
                nota = unitOfWork.Repository<Notas>().Modify(nota);

                unitOfWork.CommitTransaction();
                return data;
            }
            catch (Exception e)
            {
                unitOfWork.RollbackTransaction();
                throw e;
            }
        }

    }
}
