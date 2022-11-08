using OSL.Inventory.B2.Service.DTOs.BaseDto;
using OSL.Inventory.B2.Service.DTOs.BaseDto.Interfaces;

namespace OSL.Inventory.B2.Service.DTOs
{
    public class SupplierDto : BaseDto<long>, IDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
    }
}
