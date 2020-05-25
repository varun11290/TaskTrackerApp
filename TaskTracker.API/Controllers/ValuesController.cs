using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskTracker.API.Data;

namespace TaskTracker.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ValuesController : ControllerBase
    {

        private ValueDBContext _context { get; }
        public ValuesController(ValueDBContext context)
        {
            _context = context;
        }


        // api/values
        [HttpGet]
        public async Task<IActionResult> GetValues()
        {
            var value = await _context.Values.ToListAsync();
            return Ok(value);
        }

        // api/values/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue(int id)
        {
            var value = await _context.Values.FirstOrDefaultAsync(v => v.Id==id);
            return Ok(value);
        }
    }
}