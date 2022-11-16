using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSL.Inventory.B2.Service.DTOs
{
    public class ProductDatatableViewDto
    {
        public string Name { get; set; } = string.Empty;
        public string InStockString { get; set; }
        public string PricePerUnitString { get; set; }
        public string StatusHtml { get; set; } = string.Empty;
        public string ActionLinkHtml { get; set; } = string.Empty;
    }
}
