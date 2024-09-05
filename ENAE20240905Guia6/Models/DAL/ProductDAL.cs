using Microsoft.EntityFrameworkCore;
using ENAE20240905Guia6.Models.EN;

namespace ENAE20240905Guia6.Models.DAL
{
    public class ProductDAL
    {
        readonly CRMContext _context;

        public ProductDAL(CRMContext context)
        {
            _context = context;
        }

        public async Task<int>create(ProductENAE productENAE)
        {
            _context.Add(productENAE);
            return await _context.SaveChangesAsync();
        }

        public async Task<ProductENAE> GetById(int id)
        {
            var product = await _context.ProductENAE.FirstOrDefaultAsync(p => p.id == id);
            return product != null ? product : new ProductENAE();
        }

        public async Task<int> Edit(ProductENAE productENAE)
        {
            int result = 0;
            var productUodate= await GetById(productENAE.id);
            if (productUodate.id != 0) { 
                productUodate.NombreENAE = productENAE.NombreENAE;
                productUodate.DescripcionENAE = productENAE.DescripcionENAE;
                productUodate.PrecioENAE = productENAE.PrecioENAE;
                result= await _context.SaveChangesAsync();
            }
            return result;
        }

        public async Task<int> Delete(int id)
        {
            int result = 0;
            var productDelete = await GetById(id);
            if(productDelete.id > 0)
            {
                _context.ProductENAE.Remove(productDelete);
                result = await _context.SaveChangesAsync();
            }
            return result;
        }

        private IQueryable<ProductENAE> Query(ProductENAE productENAE)
        {
            var query = _context.ProductENAE.AsQueryable();
            if(!string.IsNullOrWhiteSpace(productENAE.NombreENAE))
                query = query.Where(s => s.NombreENAE.Contains(productENAE.NombreENAE));
            return query;
        }

        public async Task<int> CountSearch(ProductENAE productENAE)
        {
            return await Query(productENAE).CountAsync();
        }

        public async Task<List<ProductENAE>> Seacrh(ProductENAE productENAE, int take =10,int skip = 0)
        {
            take = take==0 ? 10 : take;
            var query = Query(productENAE);
            query = query.OrderByDescending(s => s.id).Skip(skip).Take(take);
            return await query.ToListAsync();
        }
    }
}
