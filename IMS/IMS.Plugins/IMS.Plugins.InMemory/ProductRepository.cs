using IMS.Core;
using IMS.UseCases.Interfaces;
using System.Xml.Linq;

namespace IMS.Plugins.InMemory
{
    public class ProductRepository : IProductRepository
    {
        private List<Product> _products;
        public ProductRepository()
        {
            _products = new List<Product>()
            {
                new Product{Id = Guid.Parse("74ab7b1f-16ec-426f-9bed-6d35da181a06"), Name = "Bike",Quantity= 10,Price= 150},
                new Product{Id = Guid.Parse("74ab7b1f-16ec-426f-9bed-6d35da181a05"), Name = "Car",Quantity= 10,Price= 1750},
            };
        }

        public Task AddProductAsync(Product product)
        {
            if(_products.Any(i => i.Name.Equals(product.Name,StringComparison.OrdinalIgnoreCase)))
            {
                return Task.CompletedTask;
            }
            product.Id = Guid.NewGuid();
            _products.Add(product);
            return Task.CompletedTask;
        }

        public Task DeleteProductByIdAsync(Guid prodId)
        {
            var product = _products.FirstOrDefault(i => i.Id == prodId);
            if (product != null)
            {
                _products.Remove(product);
            }
            return Task.CompletedTask;
        }

        public Task EditProductAsync(Product product)
        {
            if(_products.Any(i => i.Id != product.Id && i.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase)))
            {
                return Task.CompletedTask;
            }

            var existingProduct = _products.FirstOrDefault(i => i.Id.Equals(product.Id));

            if (existingProduct is not null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Quantity = product.Quantity;
                existingProduct.Price = product.Price;
                existingProduct.ProductInventories = product.ProductInventories;
            }

            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Product>> GetProductsByNameAsync(string name)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                return await Task.FromResult(_products);
            }
            return _products.Where(i => i.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<Product?> GetProductByIdAsync(Guid prodId)
        {
            var prod = await Task.FromResult(_products.FirstOrDefault(i => i.Id.Equals(prodId)));
            var newProd = new Product();
            if(prod != null)
            {
                newProd.Id = prod.Id;
                newProd.Name = prod.Name;
                newProd.Price = prod.Price;
                newProd.Quantity = prod.Quantity;
                newProd.ProductInventories = new List<ProductInventory>();
                if(prod.ProductInventories != null && prod.ProductInventories.Count > 0)
                {
                    foreach(var pi in prod.ProductInventories)
                    {
                        var newPi = new ProductInventory
                        {
                            InventoryId = pi.InventoryId,
                            ProductId = pi.ProductId,
                            Quantity = pi.Quantity,
                            Inventory = new Inventory(),
                            Product = prod
                        };
                        if(pi.Inventory != null)
                        {
                            newPi.Inventory.Id = pi.Inventory.Id;
                            newPi.Inventory.Name = pi.Inventory.Name;
                            newPi.Inventory.Price = pi.Inventory.Price;
                            newPi.Inventory.Quantity = pi.Inventory.Quantity;
                        }
                        newProd.ProductInventories.Add(newPi);
                    }
                }
            }
            return await Task.FromResult(newProd);
        }
    }
}