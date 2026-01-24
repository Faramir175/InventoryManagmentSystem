using IMS.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.UseCases.Interfaces
{
    public interface IInventoryRepository
    {
        Task<IEnumerable<Inventory>> GetInventoriesByNameAsync(string name);
    }
}
