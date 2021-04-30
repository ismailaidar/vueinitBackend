using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using patients.Models;
using patients.Wrappers;
using patients.Filters;
using patients.Services;
using patients.Helpers;
using Microsoft.AspNetCore.Cors;

namespace patients.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly PatientDbContext _context;
        private readonly IUriService uriService;

        public PatientsController(PatientDbContext context, IUriService uriService)
        {
            _context = context;
            this.uriService = uriService;
        }

        // GET: api/Patients
        [HttpGet]
        public async Task<ActionResult> GetPatient([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize, filter.SearchText);
            var patients = await _context.Patient.ToListAsync();
            
            var totalRecords = 0;
            // Filtering
            if (!filter.SearchText.Equals(""))
            {
                patients = FilterPatient(patients, filter.SearchText);
                totalRecords = patients.Count();
            }
            else
            {
                totalRecords = await _context.Patient.CountAsync();
            }

            // Sorting
            if(filter.Sort != null && filter.Sort.Length != 0)
            {
                patients = SortPatient(patients, filter.Sort);
            }

            patients = patients.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                                                   .Take(validFilter.PageSize)
                                                   .ToList();

            var pagedReponse = PaginationHelper.CreatePagedReponse<Patient>(patients, validFilter, totalRecords, uriService, route);
            return Ok(pagedReponse);
        }

        private Random gen = new Random();
        DateTime RandomDay()
        {
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
        }

        List<Patient> FilterPatient(IEnumerable<Patient> patients, string seachText)
        {
            return patients
                    .Where(p => p.LastName.Contains(seachText) ||
                                p.FirstName.Contains(seachText))
                    .ToList();

        }

        List<Patient> SortPatient(IEnumerable<Patient> patients, string[] sortBy)
        {
            foreach (var item in sortBy)
            {
                // id,asc => [id][asc]
                var key = item.Split(",")[0];
                var sortType = item.Split(",")[1];

                if (sortType.Equals("asc"))
                {
                    patients = patients
                                    .OrderBy(p => p[key])
                                    .ToList();
                }

                if (sortType.Equals("desc"))
                {
                    patients = patients
                                    .OrderByDescending(p => p[key])
                                    .ToList();
                }
            }
            return patients.ToList();
        }

        // GET: api/Patients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatient(int id)
        {
            var patient = await _context.Patient.FindAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            return Ok(new Response<Patient>(patient));
        }

        // PUT: api/Patients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatient(int id, Patient patient)
        {
            if (id != patient.Id)
            {
                return BadRequest();
            }

            _context.Entry(patient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
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

        // POST: api/Patients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Patient>> PostPatient(Patient patient)
        {
            _context.Patient.Add(patient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPatient", new { id = patient.Id }, patient);
        }

        // DELETE: api/Patients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var patient = await _context.Patient.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            _context.Patient.Remove(patient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PatientExists(int id)
        {
            return _context.Patient.Any(e => e.Id == id);
        }
    }
}
