using IMS.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.UseCases.Interfaces
{
    public interface IInventoryTransactionRepository
    {
        void PurchaseAsync(string poNumber, Inventory inventory, int quantity, string doneBy, decimal price);
    }
}
