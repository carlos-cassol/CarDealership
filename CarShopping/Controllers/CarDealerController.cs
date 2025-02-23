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
        public async Task<ActionResult<CarDealerVO>> Get(Guid id)
        {
            var carDealer = _carDealerRepository.Find(id).Result;
            if (carDealer is null)
            {
                return BadRequest("Não existem revendedores cadastrados no sistema.");
            }

            var updatedDealer = await _carDealerRepository.UpdateValues(carDealer.Id);

            return Ok(updatedDealer);
        }

        [HttpPost]
        public async Task<ActionResult<CarDealerVO>> Create([FromBody] CarDealerVO vo)
        {
            var dealer = await _carDealerRepository.Create(vo);
            return Ok(dealer);
        }

        [HttpPut]
        public async Task<ActionResult<CarDealerVO>> Update([FromBody] CarDealerVO vo)
        {
            var dealer = await _carDealerRepository.Update(vo);
            return Ok(dealer);
        }

        [HttpPut("UpdateValues")]
        public async Task<ActionResult<CarDealerVO>> UpdateValues([FromBody] CarDealerVO vo)
        {
            var dealer = await _carDealerRepository.UpdateValues(vo.Id);
            return Ok(dealer);
        }
    }
}
