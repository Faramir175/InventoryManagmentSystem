using IMS.Core;
using IMS.UseCases.Interfaces;
using IMS.UseCases.Inventories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.UseCases.Inventories
{
    public class EditInventoryUseCase : IEditInventoryUseCase
    {
        private readonly IInventoryRepository _inventoryRep;
        public EditInventoryUseCase(IInventoryRepository inventoryRep)
        {
            _inventoryRep = inventoryRep;
        }

        public async Task ExecuteAsync(Inventory inventory)
        {
            await _inventoryRep.EditInventoryAsync(inventory);
        }
    }
}
