using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage2.Models;
using Garage2.Extensions;

namespace Garage2.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly Garage2Context _context;
        private MyExtensions _extensions = new MyExtensions();

        public VehiclesController(Garage2Context context)
        {
            _context = context;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            var vehicles = await _context.Vehicle.ToListAsync();

            var model = new VehicleViewModel()
            {
                Vehicles = vehicles,
                Types = await GetTypesAsync()
            };
            return View(model);
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // GET: Vehicles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RegNr,Color,Model,Manufacturer,NOWheels,Type")] Vehicle vehicle)
        {
            var time = DateTime.Now;
            if (ModelState.IsValid)
            {
                vehicle.BeginParking = _extensions.RoundDateTime(time);
                _context.Add(vehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RegNr,Color,Model,Manufacturer,NOWheels,Type")] Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var oldVehicle = _context.Vehicle.AsNoTracking().Where(ov => ov.Id == id).FirstOrDefault();
                    vehicle.BeginParking = oldVehicle.BeginParking;
                    
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicle = await _context.Vehicle.FindAsync(id);
            _context.Vehicle.Remove(vehicle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleExists(int id)
        {
            return _context.Vehicle.Any(e => e.Id == id);
        }

        //CUSTOM ACTIONRESULTS
        //[HttpGet]
        //public ActionResult GetVehicleViewModels()
        //{
        //    var vehicles = from v in _context.Vehicle select v;

        //    IEnumerable<VehicleViewModel> model = vehicles.Select(v => new VehicleViewModel
        //    {
        //        Id = v.Id,
        //        RegNr = v.RegNr,
        //        Manufacturer = v.Manufacturer,
        //        Type = v.Type,
        //        BeginParking = v.BeginParking
        //    });
        //    return View(nameof(Index), model);
        //}

        //Filters
    private async Task<IEnumerable<SelectListItem>> GetTypesAsync()
        {
            return await _context.Vehicle
                .Select(g => g.Type)
                .Distinct()
                .Select(m => new SelectListItem
                {
                    Text = m.ToString(),
                    Value = m.ToString()

                })
                .ToListAsync();
        }

        public async Task<IActionResult> Filter(VehicleViewModel viewModel)
        {
            var vehicles = string.IsNullOrWhiteSpace(viewModel.RegNr) ?
                await _context.Vehicle.ToListAsync() :
                await _context.Vehicle.Where(v => v.RegNr == viewModel.RegNr).ToListAsync();

            vehicles = viewModel.Type == null ?
               vehicles :
               vehicles.Where(v => v.Type == viewModel.Type).ToList();

            var model = new VehicleViewModel()
            {
                Vehicles = vehicles,
                Types = await GetTypesAsync()
            };

            return View(nameof(Index), model);
        }
    }
}
