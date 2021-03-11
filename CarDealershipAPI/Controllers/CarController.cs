using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarDealershipAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarDealershipAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : Controller
    {
        private readonly CarDBContext _carContext;
        public CarController(CarDBContext context)
        {
            _carContext = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            return await _carContext.Cars.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await _carContext.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            return car;
        }

        [HttpGet("make/{make}")]
        public async Task<ActionResult<IEnumerable<Car>>> GetMake(string make)
        {
            var car = await _carContext.Cars.Where(x => x.Make.ToLower() == make).ToListAsync();
            if (car == null)
            {
                return NotFound();
            }
            return car;
        }

        [HttpGet("model/{model}")]
        public async Task<ActionResult<IEnumerable<Car>>> GetModel(string model)
        {
            var car = await _carContext.Cars.Where(x => x.Model.ToLower() == model).ToListAsync();
            if (car == null)
            {
                return NotFound();
            }
            return car;
        }

        [HttpGet("year/{year}")]
        public async Task<ActionResult<IEnumerable<Car>>> GetYear(int year)
        {
            var car = await _carContext.Cars.Where(x => x.Year == year).ToListAsync();
            if (car == null)
            {
                return NotFound();
            }
            return car;
        }

        [HttpGet("color/{color}")]
        public async Task<ActionResult<IEnumerable<Car>>> GetColor(string color)
        {
            var car = await _carContext.Cars.Where(x => x.Color.ToLower() == color).ToListAsync();
            if (car == null)
            {
                return NotFound();
            }
            return car;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, Car car)
        {
            if (id != car.Id)
            {
                return BadRequest();
            }
            _carContext.Entry(car).State = EntityState.Modified;

            try
            {
                await _carContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            _carContext.Cars.Add(car);
            await _carContext.SaveChangesAsync();

            return CreatedAtAction("GetCar", new { id = car.Id }, car);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Car>> DeleteCar(int id)
        {
            var car = await _carContext.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            _carContext.Cars.Remove(car);
            await _carContext.SaveChangesAsync();

            return NoContent();
        }

        private bool CarExists(int id)
        {
            return _carContext.Cars.Any(e => e.Id == id);
        }
    }
}
