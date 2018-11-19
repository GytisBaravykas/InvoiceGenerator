using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceGenerator.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float VAT { get; set; }

        public bool InEU { get; set; }

    }
}
