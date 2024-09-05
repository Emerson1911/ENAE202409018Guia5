using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENAE202409018.Dtos.ProductDTOs
{
    public class SearchQueryProductDTO
    {
        [Display(Name = "Nombre")]
        public string NombreENAE_Like { get; set; }
        [Display(Name = "Descripcion")]
        public string DescripcionENAE { get; set; }
        [Display(Name = "Precio")]
        public Decimal PrecioENAE { get; set; }
        [Display(Name = "Pagina")]
        public int Skip { get; set; }
        [Display(Name = "CantReg x Pagina")]
        public int Take { get; set; }
        public byte SendRowCount { get; set; }
    }
}
