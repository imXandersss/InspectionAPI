using InspectionAPI.Data;
using InspectionAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InspectionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {

        private readonly DataContext _dataContext;

        public StatusController(DataContext dataContext)
        {
           this._dataContext=dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Status>>> getAllStatus()
        {
            return await _dataContext.Status.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Status>> getOnlyStatus(int id)
        {
            var status = await _dataContext.Status.FindAsync(id);
            if(Object.Equals(status, null))
            {
                return NotFound("Not found");
            }

            return Ok(status);
        }

        [HttpPost]
        public async Task<IActionResult> insertStatus(Status status)
        {
           _dataContext.Status.Add(status);
            await _dataContext.SaveChangesAsync();
            return CreatedAtAction("Get Status: ", status.id, status);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> updateStatus(int id, Status status)
        {
            if (isExistStatus(id))
            {
                return BadRequest("Not found");
            }

            if (Object.Equals(status, null))
            {
                return BadRequest("Not fund");
            }
            _dataContext.Entry(status).State = EntityState.Modified;

            try
            {
                 await _dataContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (isExistStatus(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteStatus(int id)
        {
            if (isExistStatus(id))
            {
                return NotFound();
            }

            var status= await _dataContext.Status.FindAsync(id);

            if (Object.Equals(status, null))
            {
                return NotFound();
            }

            _dataContext.Status.Remove(status);
            await _dataContext.SaveChangesAsync();
            return NoContent();
        }

        private bool isExistStatus(int id)
        {
            return !_dataContext.Status.Any(status => status.id == id);
        }
    }
}
