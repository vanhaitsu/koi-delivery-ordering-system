using KoiDeliveryOrderingSystem.Service;
using KoiDeliveryOrderingSystem.Service.Base;
using Microsoft.AspNetCore.Mvc;

namespace KoiDeliveryOrderingSystem.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalTypeController : ControllerBase
    {
        private readonly AnimalTypeService _animalTypeService;

        public AnimalTypeController(AnimalTypeService animalTypeService)
        {
            _animalTypeService = animalTypeService;
        }

        // GET: api/Shippers
        [HttpGet]
        public async Task<IBusinessResult> GetAnimalTypes()
        {
            return await _animalTypeService.GetAll();
        }
    }
}
