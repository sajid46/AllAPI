using AllAPIService.IRepository;
using AllAPIService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AllAPIService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllAPIController : ControllerBase
    {
        private IGlobalRepository _repository;

        public AllAPIController(IGlobalRepository repository)
        {
            this._repository = repository;
        }

        [HttpGet]
        [Route("getPAYETax")]
        public TaxModel getPAYETax([FromBody] TaxModel data)
        {
            return _repository.GetTaxDetails(data);
        }

        [HttpGet]
        [Route("getageenum")] 
        public List<string> getAgeEnum()
        {
            var o = _repository.GetAgeEnum();
            return o;
        }

        [HttpGet]
        [Route("get")]
        public TaxModel get()
        {
            return null;
        }
    }
}
