﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InvoiceGenerator.Models;

namespace InvoiceGenerator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private ApplicationDbContext _context;
        public InvoiceController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/Invoice
        [HttpGet]
        public IActionResult Get()
        {
            var invoices = _context.Invoices // it works
                .Include(c => c.Service)
                .Include(c => c.ServiceProvider)
                .Include(c => c.Client)
                .Include(c => c.ServiceProvider.Country)
                .Include(c => c.Client.Country)
                .ToList();
            return Ok(invoices);
        }

        // GET: api/Invoice/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var invoice = _context.Invoices // it works
                .Include(c => c.Service)
                .Include(c => c.ServiceProvider)
                .Include(c => c.Client)
                .Include(c => c.ServiceProvider.Country)
                .Include(c => c.Client.Country).SingleOrDefault(i=>i.Id == id);

            return Ok(invoice);
        }

        // POST: api/Invoice
        [HttpPost]
        public void CreateInvoice(Invoice invoice)
        {
            invoice.ServiceProvider.VatPayerState(invoice);
            invoice.Client.VatPayerState(invoice);
            invoice.Service.VatCalculator(invoice);
            invoice.Service.WholePriceCalculator(invoice);

            _context.Invoices.Add(invoice);
            _context.SaveChanges();
        }

        // PUT: api/Invoice/5
        [HttpPut("{id}")]
        public void Put(int id, Invoice invoice)  
        {
            var invoiceDb = _context.Invoices
                .Include(c => c.Service)
                .Include(c => c.ServiceProvider)
                .Include(c => c.Client)
                .Include(c => c.ServiceProvider.Country)
                .Include(c => c.Client.Country).SingleOrDefault(i => i.Id == id);

            invoiceDb.Date = invoice.Date;
            invoiceDb.Client = invoice.Client;
            invoiceDb.ServiceProvider = invoice.ServiceProvider;
            invoiceDb.Service = invoice.Service;

            _context.Update(invoiceDb);
            _context.SaveChanges();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)  // works
        {
            var invoiceDb = _context.Invoices
                .Include(c => c.Service)
                .Include(c => c.ServiceProvider)
                .Include(c => c.Client)
                .Include(c => c.ServiceProvider.Country)
                .Include(c => c.Client.Country).SingleOrDefault(i => i.Id == id);

            _context.Remove(invoiceDb);
            _context.SaveChanges();
        }
    }
}
