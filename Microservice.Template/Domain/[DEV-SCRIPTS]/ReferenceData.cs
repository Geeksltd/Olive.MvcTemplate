using System;
using System.Threading.Tasks;
using Olive;
using Olive.Entities;
using Olive.Entities.Data;
using Olive.Security;

namespace Domain
{
    public class ReferenceData
    {
        static Task Create(IEntity item) => Database.Instance.Save(item, SaveBehaviour.BypassAll);

        public static async Task Create()
        {
            // ...
        }
    }
}