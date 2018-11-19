using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceGenerator.Models
{
    public class Client
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
            var client = invoice.Client;

            if (client.VATCode != null && client.VATCode != "")
                client.IsVatPayer = true;
            else
                client.IsVatPayer = false;
        }
    }
}
