using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENAE202409018.Dtos.ProductDTOs
{
    public class GetIdResultProductDTO
    {
        public int Id { get; set; }
        [Display(Name = "Nombre")]
        public string NombreENAE { get; set; }
        [Display(Name = "Descripcion")]
        public string DescripcionENAE { get; set; }
        [Display(Name = "Precio")]
        public Decimal PrecioENAE { get; set; }

    }
}
