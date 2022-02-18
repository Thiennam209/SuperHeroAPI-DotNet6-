using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext dataContext;

        public SuperHeroController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await dataContext.SuperHeroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var dbHero = await dataContext.SuperHeroes.FindAsync(id);
            if (dbHero == null)
                return BadRequest("Hero not found!");
            return Ok(dbHero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero dbHero)
        {
            dataContext.SuperHeroes.Add(dbHero);
            await dataContext.SaveChangesAsync();

            return Ok(await dataContext.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var dbHero = await dataContext.SuperHeroes.FindAsync(request.Id);
            if (dbHero == null)
                return BadRequest("Hero not found!");
                
            dbHero.Name = request.Name;
            dbHero.FirstName = request.FirstName;
            dbHero.LastName = request.LastName;
            dbHero.Place = request.Place;

            await dataContext.SaveChangesAsync();

            return Ok(await dataContext.SuperHeroes.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {
            var dbHero = await dataContext.SuperHeroes.FindAsync(id);
            if (dbHero == null)
                return BadRequest("Hero not found!");

            dataContext.SuperHeroes.Remove(dbHero);
            await dataContext.SaveChangesAsync();

            return Ok(await dataContext.SuperHeroes.ToListAsync());
        }
    }
}
