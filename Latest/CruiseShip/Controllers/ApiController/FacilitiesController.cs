using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CruiseShip.Data;
using CruiseShip.Models;
using Microsoft.AspNetCore.Authorization;
using CruiseShip.Data.Repository.IRepository;
using Microsoft.AspNetCore.Hosting;

namespace CruiseShip.Controllers.ApiController
{
    [Route("api/[controller]")]
    [ApiController]

    
    public class FacilitiesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FacilitiesController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: api/Facilities
        [HttpGet]
        public async Task<IEnumerable<Facility>> GetFacilities()
        {
            return await _unitOfWork.Facility.GetAll(includeProperties: "CreatedByUser");
        }

        // GET: api/Facilities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Facility>> GetFacility(int id)
        {
            var facility = await _unitOfWork.Facility.Get(b=>b.Id==id);

            if (facility == null)
            {
                return NotFound();
            }

            return facility;
        }

        // PUT: api/Facilities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFacility(int id, Facility facility)
        {
            if (id != facility.Id)
            {
                return BadRequest();
            }

            _unitOfWork.Facility.Update(facility);

            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacilityExists(id))
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

        // POST: api/Facilities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Facility>> PostFacility(Facility facility)
        {
            await _unitOfWork.Facility.Add(facility);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        // DELETE: api/Facilities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFacility(int id)
        {
            var facility = await _unitOfWork.Facility.Get(p=>p.Id==id);
           
            if (facility == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Error while deleting"
                });
            }
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string facilityPath = Path.Combine(wwwRootPath, @"images\facility");
            var ImagePath = Path.Combine(wwwRootPath, facility.ImageURL.TrimStart('\\'));
            if (System.IO.File.Exists(ImagePath))
                System.IO.File.Delete(ImagePath);

            await _unitOfWork.Facility.Remove(facility);
            await _unitOfWork.SaveAsync();

            return Ok(new { success = true, message = "Delete Successful" });

        }

        private bool FacilityExists(int id)
        {
            return _context.Facilities.Find(id) != null;
        }
    }
}
