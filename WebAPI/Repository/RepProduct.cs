using Microsoft.EntityFrameworkCore;
using WebAPI.IRepository;
using WebData.Data;
using WebData.Model;

namespace WebAPI.Repository
{
    public class RepProduct : IRepProduct
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string[] _AllowExtension = { ".png", ".jpg", ".jpeg", ".gif" };
        private readonly string FileUploads = "uploads";

        public RepProduct(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<List<Product>> GetAll()
        {
            if (_db.products == null)
            {
                return new List<Product>(); // trả list empty
            }
            var Products = await _db.products
                .Include(x => x.Category)
                .ToListAsync();
            return Products;
        }

        public async Task Create(Product product, IFormFile file, List<int> Category) // Get danh mục id mà product liên kết
        {
            var Extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_AllowExtension.Contains(Extension))
            {
                throw new ArgumentException("File must format .png, .jpg, .gif and .jpeg");
            }
            var FileName = Guid.NewGuid().ToString() + Extension;
            var UploadPath = Path.Combine(_webHostEnvironment.WebRootPath, FileUploads);
            Directory.CreateDirectory(UploadPath);
            var FilePath = Path.Combine(UploadPath, FileName);
            using (var Stream = new FileStream(FilePath, FileMode.Create))
            {
                await file.CopyToAsync(Stream);
            }
            // add đối tượng cho sp mới
            var Newproducts = new Product()
            {
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                IFormImage = FileName,
                Description = product.Description,
                Category = new List<Category>() // tạo list empty để add vào after
            };
            _db.products.Add(Newproducts);
            await _db.SaveChangesAsync();

            // Liên kết product vs category
            foreach (var categoryId in Category)
            {
                var category = await _db.categories.FindAsync(categoryId);
                if (category != null)
                {
                    Newproducts.Category.Add(category); // add danh mục liên kết vs product
                }
            }
            await _db.SaveChangesAsync();
        }

        public async Task Update(Product product, IFormFile file, List<int> Category, int id)
        {
            var ProductExisting = await _db.products.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
            if (ProductExisting == null)
            {
                throw new KeyNotFoundException("Product not found");
            }
            // Handle image upload
            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!_AllowExtension.Contains(extension))
                {
                    throw new ArgumentException("File must be in .png, .jpg, .gif, or .jpeg format");
                }
                var fileName = Guid.NewGuid().ToString() + extension;
                var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, FileUploads);
                Directory.CreateDirectory(uploadPath);
                var filePath = Path.Combine(uploadPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                ProductExisting.IFormImage = fileName; // Update with new image
            }
            else
            {
                ProductExisting.IFormImage = ProductExisting.IFormImage;
            }

            // Retain existing values if not update
            ProductExisting.Name = product.Name ?? ProductExisting.Name;
            ProductExisting.Price = product.Price != 0 ? product.Price : ProductExisting.Price;
            ProductExisting.Quantity = product.Quantity != 0 ? product.Quantity : ProductExisting.Quantity;
            ProductExisting.Description = product.Description ?? ProductExisting.Description;


            ProductExisting.Category.Clear(); // xóa danh mục cũ
            foreach (var CategoryId in Category)
            {
                var category = await _db.categories.FindAsync(CategoryId);
                if (category != null)
                {
                    ProductExisting.Category.Add(category);
                }
            }

            _db.products.Update(ProductExisting);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var ProductId = await _db.products.FirstOrDefaultAsync(x => x.Id == id);
            _db.products.Remove(ProductId);
            await _db.SaveChangesAsync();
        }

        public async Task<Product> GetById(int id)
        {
            var ProductId = await _db.products.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
            return ProductId;
        }


    }
}
