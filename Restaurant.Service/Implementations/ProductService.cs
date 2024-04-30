using Restaurant.DAL.Interfaces;
using Restaurant.Domain.Entity;
using Restaurant.Domain.Request.Product;
using Restaurant.Service.Interfaces;

namespace Restaurant.Service.Implementations;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly SavingImageService _savingImage;

    public ProductService(IProductRepository repository, SavingImageService savingImage)
    {
        _repository = repository;
        _savingImage = savingImage;
    }

    public async Task<Product?> Get(int id)
    {
        return await _repository.Get(id);
    }

    public async Task<Product?> GetByTitle(string title)
    {
        return await _repository.GetByTitle(title);
    }

    public async Task<IEnumerable<Product?>> GetByLater(string word)
    {
        return await _repository.GetByLater(word);
    }

    public async Task<IEnumerable<Product?>> Select()
    {
        return await _repository.Select();
    }

    public async Task<bool> Create(ProductCreateRequest request)
    {
        var product = await _repository.GetByTitle(request.Title);

        if (product == null)
        {
            product = new Product
            {
                Title = request.Title,
                Description = request.Description,
                Price = request.Price,
                CatalogId = request.CatalogId,
            };

            product.Img = await _savingImage.Save(request.Img, "product");

            await _repository.Create(product);

            return true;
        }

        return false;
    }

    public async Task<bool> Update(ProductUpdateRequest request)
    {
        var product = await _repository.Get(request.Id);

        if (product != null)
        {
            product.Title = request.Title ?? product.Title;
            product.Description = request.Description ?? product.Description;
            product.Price = request.Price ?? product.Price;
            product.CatalogId = request.CatalogId ?? product.CatalogId;

            if (request.Img != null)
            {
                product.Img = await _savingImage.Save(request.Img, "product");
            }

            await _repository.Update(product);

            return true;
        }

        return false;
    }

    public async Task<bool> Delete(ProductDeleteRequest request)
    {
        var product = await _repository.Get(request.Id);

        if (product != null)
        {
            await _repository.Delete(product);

            return true;
        }

        return false;
    }
}