using IMS.Core;
using IMS.UseCases.Interfaces;
using IMS.UseCases.Inventories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.UseCases.Inventories
{
    public class ViewInventoryByIdUseCase : IViewInventoryByIdUseCase
    {
        private readonly IInventoryRepository _inventoryRep;
        public ViewInventoryByIdUseCase(IInventoryRepository inventoryRep)
        {
            _inventoryRep = inventoryRep;
        }

        public async Task<Inventory> ExecuteAsync(Guid invId)
        {
            return await _inventoryRep.GetInventoryByIdAsync(invId);
        }
    }
}
