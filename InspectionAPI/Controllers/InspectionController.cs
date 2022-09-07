using InspectionAPI.Data;
using InspectionAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InspectionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InspectionController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public InspectionController(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inspection>>> getAllInspections()
        {
            return await _dataContext.Inspections.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Inspection>> getOnlyInspection(int id)
        {
            if (inspectionExist(id))
            {
                return BadRequest("Not found");
            }

            var inspection= await _dataContext.Inspections.FindAsync(id);
            if (Object.Equals(inspection, null)){
                return NotFound("itsn't found the register");
            }
            return Ok(inspection);
        }

 
        [HttpPut("{id}")]
        public async Task<IActionResult> updateInspection(int id, Inspection inspection)
        {
            if (inspectionExist(id))
            {
                return BadRequest();
            }

            _dataContext.Entry(inspection).State = EntityState.Modified;

            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (inspectionExist(id))
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
        public async Task<IActionResult> insertInspection(Inspection inspection)
        {
            {
                _dataContext.Inspections.Add(inspection);
                await _dataContext.SaveChangesAsync();
                return CreatedAtAction("Get Inspection: ", new { id = inspection.id, inspection });
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteInspection(int id)
        {

            var inspection = await _dataContext.Inspections.FindAsync(id);
            if (Object.Equals(inspection, null))
            {
                return BadRequest("Not found");
            }

            _dataContext.Inspections.Remove(inspection);
            await _dataContext.SaveChangesAsync();
            return NoContent();
        }

        private bool inspectionExist(int id)
        {
            return !_dataContext.Inspections.Any(inspection => inspection.id == id);
        }
    }
}
