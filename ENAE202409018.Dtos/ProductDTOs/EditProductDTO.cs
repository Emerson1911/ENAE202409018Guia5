using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENAE202409018.Dtos.ProductDTOs
{
    public class EditProductDTO
    {
        public EditProductDTO(GetIdResultProductDTO getIdResultProductDTO) {
            Id = getIdResultProductDTO.Id;
            NombreENAE = getIdResultProductDTO.NombreENAE;
            DescripcionENAE = getIdResultProductDTO.DescripcionENAE;
            PrecioENAE = getIdResultProductDTO.PrecioENAE;
        }
        public EditProductDTO()
        {
            NombreENAE = string.Empty;
        }
        public int Id { get; set; }
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo no puede contener mas de 50 caracteres...")]
        public string NombreENAE { get; set; }

        [Display(Name = "Descripcion")]
        [MaxLength(50, ErrorMessage = "El campo no puede contener mas de 50 caracteres...")]
        public string DescripcionENAE { get; set; }

        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El campo Precio es obligatorio..")]
        //[MaxLength(50, ErrorMessage = "El campo no puede contener mas de 50 caracteres...")]
        public Decimal PrecioENAE { get; set; }
    }
}
