using InspectionAPI.Data;
using InspectionAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InspectionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InspectionTypeController : ControllerBase
    {

        private readonly DataContext _dataContext;

        public InspectionTypeController(DataContext dataContext)
        {
           this._dataContext= dataContext;  
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InspectionType>>> getAllInspectionType()
        {
            return await _dataContext.InspectionType.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InspectionType>> getOnlyInspectionType(int id)
        {
           if(isExistInspectionType(id))
            {
                return NotFound("Not found");
            }

           var inspectionType= await _dataContext.InspectionType.FindAsync(id);
           if(Object.Equals(inspectionType, null))
            {
                return BadRequest("itsn't found the register");
            }
           return Ok(inspectionType);   
        }
        
        [HttpPost]
        public async Task<IActionResult> insertInspectionType(InspectionType inspectionType)
        {
            _dataContext.InspectionType.Add(inspectionType);
            await _dataContext.SaveChangesAsync();
            return CreatedAtAction("Get InspectionType: ", new { id = inspectionType.id, inspectionType });
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> updateInspectionType(int id, InspectionType inspectionType)
        {
            if (isExistInspectionType(id))
            {
                return BadRequest("Not found");
            }

            _dataContext.Entry(inspectionType).State = EntityState.Modified;
            try
            {
              await _dataContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (isExistInspectionType(id))
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
        public async Task<IActionResult> deleteInspectionType(int id)
        {
            if (isExistInspectionType(id))
            {
                return NotFound("Not found");
            }

            var inspectionType = await _dataContext.InspectionType.FindAsync(id);

            if(Object.Equals(inspectionType, null))
            {
                return BadRequest("itsnt found");
            }

             _dataContext.InspectionType.Remove(inspectionType);
            await _dataContext.SaveChangesAsync();
            return NoContent();
        
        }



        private bool isExistInspectionType(int id)
        {
            return !_dataContext.InspectionType.Any(inspectionType=> inspectionType.id== id);
        }




    }
}
