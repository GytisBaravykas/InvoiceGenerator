using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceGenerator.Models
{
    public class ServiceProvider
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public Country Country { get; set; }
        public string Code { get; set; }
        public string VATCode { get; set; }
        public bool IsVatPayer { get; private set; }

        public void VatPayerState(Invoice invoice)
        {
            var serviceProvider = invoice.ServiceProvider;

            if (serviceProvider.VATCode != null && serviceProvider.VATCode != "")
                serviceProvider.IsVatPayer = true;
            else
                serviceProvider.IsVatPayer = false;
        }
    }
}
