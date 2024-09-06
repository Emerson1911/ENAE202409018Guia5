using ENAE202409018.Dtos.ProductDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENAE202409018NET.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _httpClientCRMAPI;

        // Constructor que recibe una instancia de IHttpClientFactory para crear el cliente HTTP
        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClientCRMAPI = httpClientFactory.CreateClient("CRMAPI");
        }

        // GET: ProductController
        public async Task<IActionResult> Index(SearchQueryProductDTO searchQueryProductDTO, int CountRow = 0)
        {
            if(searchQueryProductDTO == null)
                searchQueryProductDTO = new SearchQueryProductDTO();

            if (searchQueryProductDTO.SendRowCount == 0)
                searchQueryProductDTO.SendRowCount = 2;
            if (searchQueryProductDTO.Take == 0)
                searchQueryProductDTO.Take = 10;


            var result = new SearchResultProductDTO();

            var response = await _httpClientCRMAPI.PostAsJsonAsync("/product/search", searchQueryProductDTO);

            if (response.IsSuccessStatusCode)
                result = await response.Content.ReadFromJsonAsync<SearchResultProductDTO>();

            result = result != null ? result : new SearchResultProductDTO();

            if (result.CountRow == 0 && searchQueryProductDTO.SendRowCount == 1)
                result.CountRow = CountRow;

            ViewBag.CountRow = result.CountRow;
            searchQueryProductDTO.SendRowCount = 0;
            ViewBag.SearchQuery = searchQueryProductDTO;

            return View(result);
        }

        // GET: ProductController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var result = new GetIdResultProductDTO();

            // Realizar una solicitud HTTP GET para obtener los detalles del cliente por ID
            var response = await _httpClientCRMAPI.GetAsync("/product/" + id);

            if (response.IsSuccessStatusCode)
                result = await response.Content.ReadFromJsonAsync<GetIdResultProductDTO>();

            return View(result ?? new GetIdResultProductDTO());
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductDTO createProductDTO)
        {
            try
            {
                // Realizar una solicitud HTTP POST para crear un nuevo cliente
                var response = await _httpClientCRMAPI.PostAsJsonAsync("/product", createProductDTO);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Error = "Error al intentar guardar el registro";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var result = new GetIdResultProductDTO();
            var response = await _httpClientCRMAPI.GetAsync("/product/" + id); // Corregido "/customer/" a "/product/"

            if (response.IsSuccessStatusCode && response.Content != null)
                result = await response.Content.ReadFromJsonAsync<GetIdResultProductDTO>();

            return View(new EditProductDTO(result ?? new GetIdResultProductDTO()));
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditProductDTO editProductDTO)
        {
            try
            {
                // Realizar una solicitud HTTP PUT para editar el producto
                var response = await _httpClientCRMAPI.PutAsJsonAsync("/product", editProductDTO); // Corregido "/customer/" a "/product/"

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Error = "Error al intentar editar el registro";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }

        }

        // GET: ProductController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var result = new GetIdResultProductDTO();
            var response = await _httpClientCRMAPI.GetAsync("/product/" + id);  // Corregir la ruta

            if (response.IsSuccessStatusCode)
                result = await response.Content.ReadFromJsonAsync<GetIdResultProductDTO>();

            return View(result ?? new GetIdResultProductDTO());
        }


        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, GetIdResultProductDTO getIdResultProductDTO)
        {
            try
            {
                var response = await _httpClientCRMAPI.DeleteAsync("/product/" + id); // Usar la ruta "/product/"

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Error = "Error al intentar eliminar el registro";
                return View(getIdResultProductDTO);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(getIdResultProductDTO);
            }
        }

    }
}
