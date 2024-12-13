using WebData.Model;

namespace WebAPI.IRepository
{
    public interface IRepProduct
    {
        Task<List<Product>> GetAll();
        Task Create(Product product, IFormFile file, List<int> Category);
        Task Update(Product product, IFormFile file, List<int> Category, int id);
        Task Delete(int id);
        Task<Product> GetById(int id);
    }
}
