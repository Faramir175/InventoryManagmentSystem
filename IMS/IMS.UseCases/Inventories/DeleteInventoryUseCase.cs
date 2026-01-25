using IMS.Core;
using IMS.UseCases.Interfaces;
using IMS.UseCases.Inventories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.UseCases.Inventories
{
    public class DeleteInventoryUseCase : IDeleteInventoryUseCase
    {
        private readonly IInventoryRepository _inventoryRep;
        public DeleteInventoryUseCase(IInventoryRepository inventoryRep)
        {
            _inventoryRep = inventoryRep;
        }
        public async Task ExecuteAsync(Guid invId)
        {
            await _inventoryRep.DeleteInventoryByIdAsync(invId);
        }
    }
}
