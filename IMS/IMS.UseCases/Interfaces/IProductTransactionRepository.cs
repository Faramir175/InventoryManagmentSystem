using IMS.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.UseCases.Interfaces
{
    public interface IProductTransactionRepository
    {
        Task ProduceAsync(string productionNumber, Product product, int quantity, string doneBy);
    }
}
