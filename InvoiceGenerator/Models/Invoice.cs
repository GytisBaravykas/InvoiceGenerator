using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceGenerator.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public ServiceProvider ServiceProvider { get; set; }
        public Client Client { get; set; }

        public DateTime ?Date { get; set; }

        public Service Service { get; set; }
    }
}
