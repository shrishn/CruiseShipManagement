using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CruiseShip.Data;
using CruiseShip.Models;
using CruiseShip.Data.Repository.IRepository;
using CruiseShip.Models.ViewModels;

namespace CruiseShip.Controllers.ApiController
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;

        public BillsController(IUnitOfWork unitOfWork,ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        // GET: api/Bills
        [HttpGet]
        public async Task<IEnumerable<Bill>> GetBills()
        {
            return await _unitOfWork.Bill.GetAll();
        }

        // GET: api/Bills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bill>> GetBill(int id)
        {
            var bill = await _unitOfWork.Bill.Get(b => b.Id == id);

            if (bill == null)
            {
                return NotFound();
            }

            return bill;
        }

        // PUT: api/Bills/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBill(int id, Bill bill)
        {
            if (id != bill.Id)
            {
                return BadRequest();
            }

            _unitOfWork.Bill.Update(bill);

            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillExists(id))
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

        // POST: api/Bills
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bill>> PostBill(Bill bill)
        {
            await _unitOfWork.Bill.Add(bill);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBill", new { id = bill.Id }, bill);
        }

        // DELETE: api/Bills/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBill(int id)
        {
            var bill = await _unitOfWork.Bill.Get(p => p.Id == id);
            if (bill == null)
            {
                return NotFound();
            }

            await _unitOfWork.Bill.Remove(bill);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        private bool BillExists(int id)
        {
            return _context.Bills.Any(e => e.Id == id);
        }
        // GET: api/Bills/user/7c171a6c-4639-4553-bfa8-17697505fec6
        [HttpGet("user/{id}")]
        public async Task<ActionResult<IEnumerable<Bill>>> GetuserBills(string id)
        {
            var allBills = await _unitOfWork.Bill.GetAll(includeProperties:"Booking,Booking.Facility");
            var billsList = allBills.Where(b => b.VoyagerId == id).ToList();

            if (billsList == null || !billsList.Any())
            {
                return NotFound();
            }

            return billsList;
        }
        // GET: api/Bills/TotalAmount
        [HttpGet("TotalAmount")]
        public async Task<ActionResult<decimal>> GetTotalBillAmount()
        {
            var totalAmount = await _context.Bills.SumAsync(b => b.Amount);

            return Ok(totalAmount);
        }
    }

}
