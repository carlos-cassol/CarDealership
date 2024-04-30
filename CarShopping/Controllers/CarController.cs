using CarShopping.Data.ValueObjects;
using CarShopping.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace CarShopping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private ICarRepository _carRepository;

        public CarController(ICarRepository carRepository)
        {
            _carRepository = carRepository ?? throw new
                ArgumentNullException(nameof(carRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarVO>>> FindAll()
        {
            var product = await _carRepository.FindAll();
            return Ok(product);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarVO>> FindById(long id)
        {
            var product = await _carRepository.FindById(id);
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<CarVO>> Create([FromBody] CarVO vo)
        {
            if (vo == null) BadRequest();
            var product = await _carRepository.Create(vo);
            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromBody] CarVO car)
        {
            var status = await _carRepository.Update(car);
            return Ok(status);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            var status = await _carRepository.Delete(id);
            if (!status) return BadRequest();
            return Ok(status);
        }



    }
}
