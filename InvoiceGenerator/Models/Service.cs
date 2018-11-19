using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceGenerator.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public uint Quantity { get; set; }
        public double UnitPrice { get; set; }
        public float VAT { get; private set; }
        public double WholePrice { get; private set; }


        // VAT calculated by country data
        // cannot be set by user only the system
        public void VatCalculator(Invoice invoice)
        {
            var client = invoice.Client;
            var serviceProvider = invoice.ServiceProvider;
            var service = invoice.Service;

            if (serviceProvider.IsVatPayer == false)
            {
                service.VAT = 0;
            }
            else
            {
                if (client.Country.InEU == false)
                    service.VAT = 0;

                if (client.Country.InEU == true && client.IsVatPayer == false && client.Country.Name != serviceProvider.Country.Name)
                    service.VAT = client.Country.VAT;


                if (client.Country.InEU == true && client.IsVatPayer == true && client.Country.Name != serviceProvider.Country.Name)
                    service.VAT = 0;

                if (client.Country.Name == serviceProvider.Country.Name)
                    service.VAT = client.Country.VAT;
            }
        }
        public void WholePriceCalculator(Invoice invoice)
        {
            var service = invoice.Service;
            var wholePrice = service.UnitPrice * service.Quantity;
            wholePrice = wholePrice + wholePrice * service.VAT / 100;

            service.WholePrice = wholePrice;
        }
    }
}
