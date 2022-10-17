using AllAPIService.Models;
using AllAPIService.Repository;
using NUnit.Framework;

namespace AllAPIService.Tests
{
    public class GlobalRepositoryTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var repository=new GlobalRepository();
            var tax = new TaxModel();

            tax.GrossSalary = 24000;
            tax.PersonalAllownceCode = "1257L";
            tax.Pension = "0";

            var result = repository.GetTaxDetails(tax);
            
            Assert.IsNotNull(result);

        }
    }
}