using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;
using StarChart.Models;

namespace StarChart.Controllers
{
    [Route(""), ApiController]
    public class CelestialObjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CelestialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {

            foreach (var obj in _context.CelestialObjects)
            {
                if (obj.Id != id)
                {
                    return NotFound();
                }
                else
                {
                    obj.Satellites = new System.Collections.Generic.List<CelestialObject>();
                    obj.Satellites.Add(obj);
                    return Ok(obj);
                }
            }
            return Ok();
        }

        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {

            var data = _context.CelestialObjects.Where(x => x.Name == name).ToList();
            if (data.Count==0)
            {
                return NotFound();
            }                                                                                                                                              

            foreach (var obj in _context.CelestialObjects)
            {
                if (_context.CelestialObjects.Where(x => x.OrbitedObjectId == obj.Id).Any())
                {
                    obj.Satellites = new System.Collections.Generic.List<CelestialObject>();
                    obj.Satellites.Add(obj);
                    
                }
               
            }

            return Ok(_context.CelestialObjects);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            foreach (var obj in _context.CelestialObjects)
            {
               
                    obj.Satellites = new System.Collections.Generic.List<CelestialObject>();
                    obj.Satellites.Add(obj);

            }

            return Ok(_context.CelestialObjects);
        }
        }
}