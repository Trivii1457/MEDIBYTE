using Dominus.Backend.DataBase;

namespace Blazor.Infrastructure
{
    public class BlazorUnitWork : IUnitOfWork
    {
        public BlazorUnitWork(DataBaseSetting confg) : base(confg)
        {
            DbContext = new BlazorContext(confg);
        }
    }
}
