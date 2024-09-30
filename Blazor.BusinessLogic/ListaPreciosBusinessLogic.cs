using Blazor.Infrastructure;
using Blazor.Infrastructure.Entities;
using Dominus.Backend.Application;
using Dominus.Backend.DataBase;
using Dominus.Backend.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Blazor.BusinessLogic
{
    public class ListaPreciosLogic : GenericBusinessLogic<ListaPrecios>
    {
        public ListaPreciosLogic(IUnitOfWork unitWork) : base(unitWork)
        {
        }

        public ListaPreciosLogic(DataBaseSetting configuracionBD) : base(configuracionBD)
        {
        }

     public ListaPrecios Duplicate(ListaPrecios model, Decimal PorcentajeIncrementoServicio)
        {
            BlazorUnitWork unitOfWork = new BlazorUnitWork(UnitOfWork.Settings);
            ListaPrecios listaPrecios = new ListaPrecios();
            listaPrecios= Add(model);
            model.IsNew = false;

            List<PreciosServicios> preciosServicios = new List<PreciosServicios>();
            preciosServicios = unitOfWork.Repository<PreciosServicios>().FindAll(x => x.ListaPreciosId == model.RelacionaListaPreciosId, false);

            foreach(PreciosServicios precioServicio in preciosServicios)
            {
                precioServicio.Id = 0;
                precioServicio.ListaPreciosId = listaPrecios.Id;
                precioServicio.Precio = precioServicio.Precio + (precioServicio.Precio * (PorcentajeIncrementoServicio / 100));
                unitOfWork.Repository<PreciosServicios>().Add(precioServicio);
            }

            return model;
        }

    }



}

