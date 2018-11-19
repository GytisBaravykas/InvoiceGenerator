using InvoiceGenerator.Models;
using System;
using System.Linq;
using Xunit;

namespace InvoiceGeneratorTests
{

    public class PropertyCalculatorTests
    {
        Invoice SPNotVatPayer = new Invoice
        {
            Id = 1,
            ServiceProvider = new ServiceProvider
            {
                Id = 1,
                Name = "UAB Jack&Robin",
                Address = "122 Levington street, London",
                Code = "1455787955",
                Country = new Country
                {
                    Id = 1,
                    Name = "Lithuania",
                    InEU = true,
                    VAT = 21
                }
            },
            Client = new Client
            {
                Id = 2,
                Name = "UAB Smiltens&Co",
                Address = "122 Levington street, London",
                Code = "1455787955",
                VATCode = "LT123654789",
                Country = new Country
                {
                    Id = 2,
                    Name = "Lithuania",
                    InEU = true,
                    VAT = 21
                }
            },
            Service = new Service
            {
                Id = 1,
                Name = "Bananas",
                Quantity = 20,
                UnitPrice = 1.33
            }
        };
        Invoice SPIsVatPayerClientIsNotInEu = new Invoice
        {
            Id = 1,
            ServiceProvider = new ServiceProvider
            {
                Id = 1,
                Name = "UAB Jack&Robin",
                Address = "122 Levington street, London",
                Code = "1455787955",
                VATCode = "LT123654789",
                Country = new Country
                {
                    Id = 1,
                    Name = "Lithuania",
                    InEU = true,
                    VAT = 21
                }
            },
            Client = new Client
            {
                Id = 2,
                Name = "UAB Smiltens&Co",
                Address = "122 Levington street, London",
                Code = "1455787955",
                Country = new Country
                {
                    Id = 2,
                    Name = "Russia",
                    InEU = false,
                    VAT = 18
                }
            },
            Service = new Service
            {
                Id = 1,
                Name = "Bananas",
                Quantity = 20,
                UnitPrice = 1.33
            }
        };
        Invoice SPIsVatPayerClientIsNotVatPayerDifferentCountry = new Invoice
        {
            Id = 1,
            ServiceProvider = new ServiceProvider
            {
                Id = 1,
                Name = "UAB Jack&Robin",
                Address = "122 Levington street, London",
                Code = "1455787955",
                VATCode = "LT123654789",
                Country = new Country
                {
                    Id = 1,
                    Name = "Lithuania",
                    InEU = true,
                    VAT = 21
                }
            },
            Client = new Client
            {
                Id = 2,
                Name = "UAB Smiltens&Co",
                Address = "122 Levington street, London",
                Code = "1455787955",
                Country = new Country
                {
                    Id = 2,
                    Name = "Germany",
                    InEU = true,
                    VAT = 20
                }
            },
            Service = new Service
            {
                Id = 1,
                Name = "Bananas",
                Quantity = 20,
                UnitPrice = 1.33
            }
        };
        Invoice SPandClientIsVatPayerDifferentCountry = new Invoice
        {
            Id = 1,
            ServiceProvider = new ServiceProvider
            {
                Id = 1,
                Name = "UAB Jack&Robin",
                Address = "122 Levington street, London",
                Code = "1455787955",
                VATCode = "LT123654789",
                Country = new Country
                {
                    Id = 1,
                    Name = "Lithuania",
                    InEU = true,
                    VAT = 21
                }
            },
            Client = new Client
            {
                Id = 2,
                Name = "UAB Smiltens&Co",
                Address = "122 Levington street, London",
                Code = "1455787955",
                VATCode = "DE100124586",
                Country = new Country
                {
                    Id = 2,
                    Name = "Germany",
                    InEU = true,
                    VAT = 20
                }
            },
            Service = new Service
            {
                Id = 1,
                Name = "Bananas",
                Quantity = 20,
                UnitPrice = 1.33
            }
        };
        Invoice SPandClientSameCountry = new Invoice
        {
            Id = 1,
            ServiceProvider = new ServiceProvider
            {
                Id = 1,
                Name = "UAB Jack&Robin",
                Address = "122 Levington street, London",
                Code = "1455787955",
                VATCode = "LT123654789",
                Country = new Country
                {
                    Id = 1,
                    Name = "Lithuania",
                    InEU = true,
                    VAT = 21
                }
            },
            Client = new Client
            {
                Id = 2,
                Name = "UAB Smiltens&Co",
                Address = "122 Levington street, London",
                Code = "1455787955",
                VATCode = "LT124856135543",
                Country = new Country
                {
                    Id = 2,
                    Name = "Lithuania",
                    InEU = true,
                    VAT = 21
                }
            },
            Service = new Service
            {
                Id = 1,
                Name = "Bananas",
                Quantity = 20,
                UnitPrice = 1.33
            }
        };


        // VAT calculator
        [Fact]
        public void VatIsAssignedCorrect_SPNotVatPayer()
        {
            SPNotVatPayer.ServiceProvider.VatPayerState(SPNotVatPayer);
            SPNotVatPayer.Client.VatPayerState(SPNotVatPayer);
            SPNotVatPayer.Service.VatCalculator(SPNotVatPayer);

            Assert.Equal(0, SPNotVatPayer.Service.VAT);
        }
        [Fact]
        public void VatIsAssignedCorrect_SPIsVatPayerClientIsNotInEu()
        {
            SPIsVatPayerClientIsNotInEu.ServiceProvider.VatPayerState(SPIsVatPayerClientIsNotInEu);
            SPIsVatPayerClientIsNotInEu.Client.VatPayerState(SPIsVatPayerClientIsNotInEu);
            SPIsVatPayerClientIsNotInEu.Service.VatCalculator(SPIsVatPayerClientIsNotInEu);

            Assert.Equal(0, SPIsVatPayerClientIsNotInEu.Service.VAT);
        }
        [Fact]
        public void VatIsAssignedCorrect_SPIsVatPayerClientIsNotVatPayerDifferentCountry()
        {
            SPIsVatPayerClientIsNotVatPayerDifferentCountry.ServiceProvider.VatPayerState(SPIsVatPayerClientIsNotVatPayerDifferentCountry);
            SPIsVatPayerClientIsNotVatPayerDifferentCountry.Client.VatPayerState(SPIsVatPayerClientIsNotVatPayerDifferentCountry);
            SPIsVatPayerClientIsNotVatPayerDifferentCountry.Service.VatCalculator(SPIsVatPayerClientIsNotVatPayerDifferentCountry);

            Assert.Equal(20, SPIsVatPayerClientIsNotVatPayerDifferentCountry.Service.VAT);
        }
        [Fact]
        public void VatIsAssignedCorrect_SPandClientIsVatPayerDifferentCountry()
        {
            SPandClientIsVatPayerDifferentCountry.ServiceProvider.VatPayerState(SPandClientIsVatPayerDifferentCountry);
            SPandClientIsVatPayerDifferentCountry.Client.VatPayerState(SPandClientIsVatPayerDifferentCountry);
            SPandClientIsVatPayerDifferentCountry.Service.VatCalculator(SPandClientIsVatPayerDifferentCountry);

            Assert.Equal(0, SPandClientIsVatPayerDifferentCountry.Service.VAT);
        }
        [Fact]
        public void VatIsAssignedCorrect_SPandClientSameCountry()
        {
            SPandClientSameCountry.ServiceProvider.VatPayerState(SPandClientSameCountry);
            SPandClientSameCountry.Client.VatPayerState(SPandClientSameCountry);
            SPandClientSameCountry.Service.VatCalculator(SPandClientSameCountry);

            Assert.Equal(21, SPandClientSameCountry.Service.VAT);
        }


        // Whole Price Calculator
        [Fact]
        public void WholePriceCalculatedRight_SPnotVatPayer()
        {
            SPNotVatPayer.Service.WholePriceCalculator(SPNotVatPayer);

            Assert.Equal(26.6,SPNotVatPayer.Service.WholePrice);
        }
        [Fact]
        public void WholePriceCalculatedRight_ClientNotEu()
        {
            SPIsVatPayerClientIsNotInEu.Service.WholePriceCalculator(SPIsVatPayerClientIsNotInEu);

            Assert.Equal(26.6, SPIsVatPayerClientIsNotInEu.Service.WholePrice);
        }
        [Fact]
        public void WholePriceCalculatedRight_SPIsVatPayerClientIsNotVatPayerDifferentCountry()
        {

            SPIsVatPayerClientIsNotVatPayerDifferentCountry.ServiceProvider.VatPayerState(SPIsVatPayerClientIsNotVatPayerDifferentCountry);
            SPIsVatPayerClientIsNotVatPayerDifferentCountry.Client.VatPayerState(SPIsVatPayerClientIsNotVatPayerDifferentCountry);
            SPIsVatPayerClientIsNotVatPayerDifferentCountry.Service.VatCalculator(SPIsVatPayerClientIsNotVatPayerDifferentCountry);
            SPIsVatPayerClientIsNotVatPayerDifferentCountry.Service.WholePriceCalculator(SPIsVatPayerClientIsNotVatPayerDifferentCountry);

            Assert.Equal(31.92, SPIsVatPayerClientIsNotVatPayerDifferentCountry.Service.WholePrice);
        }
    }
}
