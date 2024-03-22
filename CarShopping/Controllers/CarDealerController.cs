using CarShopping.Data.ValueObjects;
using CarShopping.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarShopping.Controllers
{
    [Route("api/dealer/[controller]")]
    [ApiController]
    public class CarDealerController : ControllerBase
    {

        private ICarDealerRepository _carDealerRepository;
        public CarDealerController(ICarDealerRepository repository)
        {
            _carDealerRepository = repository 
                ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        public async Task<ActionResult<CarDealerVO>> Get()
        {
            if (_carDealerRepository.Find().Result == null)
            {
                return BadRequest("Não existem revendedores cadastrados no sistema.");
            }
            await _carDealerRepository.Update();

            CarDealerVO vo = await _carDealerRepository.Find();
            
            return Ok(vo);
        }

        [HttpPost]
        public async Task<ActionResult<CarDealerVO>> Create(CarDealerVO vo)
        {

            if (_carDealerRepository.Find().Result != null)
            {
                return BadRequest("Já existe um revendedor cadastrado no sistema.");
            }
            var dealer = await _carDealerRepository.Create(vo);
            return Ok(dealer);
        }
        [HttpPut]
        private async Task<ActionResult<CarDealerVO>> Update()
        {
            var dealer = await _carDealerRepository.Update();
            return Ok(dealer);
        }
    }
}
