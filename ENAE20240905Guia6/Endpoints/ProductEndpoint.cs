using ENAE202409018.Dtos.ProductDTOs;
using ENAE20240905Guia6.Models.EN;
using ENAE20240905Guia6.Models.DAL;
using static ENAE202409018.Dtos.ProductDTOs.SearchResultProductDTO;

namespace ENAE20240905Guia6.Endpoints
{
    public static class ProductEndpoint
    {
        public static void AddProductEndpoints(this WebApplication app)
        {
            app.MapPost("/product/search", async (SearchQueryProductDTO productDTO, ProductDAL productDal) =>
            {
                var product = new ProductENAE
                {
                    NombreENAE = productDTO.NombreENAE_Like != null ? productDTO.NombreENAE_Like : string.Empty
                };

                var producters = new List<ProductENAE>();
                int conutRow = 0;

                if (productDTO.SendRowCount == 2)
                {
                    producters = await productDal.Seacrh(product, skip: productDTO.Skip, take: productDTO.Take);
                    if (producters.Count > 0)
                        conutRow = await productDal.CountSearch(product);
                }
                else
                {
                    producters = await productDal.Seacrh(product, skip: productDTO.Skip, take: productDTO.Take);
                }
                var productResult = new SearchResultProductDTO
                {
                    Data = new List<SearchResultProductDTO.ProductDTOs>(),
                    CountRow = conutRow
                };
                producters.ForEach(a =>
                {
                    productResult.Data.Add(new SearchResultProductDTO.ProductDTOs
                    {
                        Id = a.id,
                        NombreENAE = a.NombreENAE,
                        DescripcionENAE = a.DescripcionENAE,
                        PrecioENAE = a.PrecioENAE
                    });
                });
                return productResult;
            });

            app.MapGet("/product/{id}", async (int id, ProductDAL productdal) =>
            {
                var product = await productdal.GetById(id);

                var productResult = new GetIdResultProductDTO
                {
                    Id = product.id,
                    NombreENAE = product.NombreENAE,
                    DescripcionENAE = product.DescripcionENAE,
                    PrecioENAE = product.PrecioENAE
                };
                if (productResult.Id > 0)
                    return Results.Ok(productResult);
                else
                    return Results.NotFound(productResult);
            });

            app.MapPost("product", async (CreateProductDTO productDTO, ProductDAL productDal) =>
            {
                var product = new ProductENAE
                {
                    NombreENAE = productDTO.NombreENAE,
                    DescripcionENAE = productDTO.DescripcionENAE,
                    PrecioENAE = productDTO.PrecioENAE
                };
                int result = await productDal.create(product);
                if (result != 0)
                    return Results.Ok(result);
                else
                    return Results.StatusCode(500);
            });

            app.MapPut("/product", async (EditProductDTO productDTO, ProductDAL productDAL) =>
            {
                // Crear un objeto 'Customer' a partir de los datos proporcionados
                var product = new ProductENAE
                {
                    id= productDTO.Id,
                    NombreENAE = productDTO.NombreENAE,
                    DescripcionENAE = productDTO.DescripcionENAE,
                    PrecioENAE = productDTO.PrecioENAE
                };

                // Intentar editar el cliente y devolver el resultado correspondiente
                int result = await productDAL.Edit(product);
                if (result != 0)
                    return Results.Ok(result);
                else
                    return Results.StatusCode(500);
            });

            app.MapDelete("/product/{id}", async (int id, ProductDAL productDAL) =>
            {
                // Intentar eliminar el cliente y devolver el resultado correspondiente
                int result = await productDAL.Delete(id);
                if (result != 0)
                    return Results.Ok(result);
                else
                    return Results.StatusCode(500);
            });
        }
    }
}
