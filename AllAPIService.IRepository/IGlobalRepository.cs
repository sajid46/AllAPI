using System;
using AllAPIService.Models;
using Microsoft.AspNetCore.Mvc;

namespace AllAPIService.IRepository
{
    public interface IGlobalRepository
    {
        TaxModel GetTaxDetails([FromBody] TaxModel data);
        List<string> GetAgeEnum();
    }
}