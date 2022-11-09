using OSL.Inventory.B2.Service.DTOs.BaseDto;
using OSL.Inventory.B2.Service.DTOs.BaseDto.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace OSL.Inventory.B2.Service.DTOs
{
    public class SupplierDto : BaseDto<long>, IDto
    {
        [Required(ErrorMessage = "First Name is required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Last Name is required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Email Address is required")]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; } = string.Empty;
        [Required(ErrorMessage = "Phone Number is required")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; } = string.Empty;
        [Required(ErrorMessage = "City Name is required")]
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
    }
}
