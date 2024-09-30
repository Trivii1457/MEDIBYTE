using Blazor.Infrastructure.Entities;
using Dominus.Backend.DataBase;
using System.Collections.Generic;

namespace Blazor.Infrastructure.Models
{
    public class TokenModel
    {
        public string Token { get; set; }

        public DataBaseSetting Conecction { get; set; }

        public User User { get; set; }

        public Office Office { get; set; }

        public List<long> ProfilesId { get; set; }

        public long SessionId { get; set; }

    }
}
