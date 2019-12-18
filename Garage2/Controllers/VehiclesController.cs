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

        // GET: Vehicles/Park
        public IActionResult Park()
        {
            return View();
        }

        // POST: Vehicles/Park
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Park([Bind("Id,RegNr,Color,Model,Manufacturer,NOWheels,Type")] Vehicle vehicle)
        {
            var time = DateTime.Now;
            if (ModelState.IsValid)
            {
                vehicle.BeginParking = time.RoundDateTime();
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

            //--
            var time = DateTime.Now.RoundDateTime();
            TimeSpan diff = time - vehicle.BeginParking;
            double period = diff.TotalHours;
            if(period < 1)
            {
                period = 1;
            }
            double price = 40 * period;

            //--
            UnparkInfoViewModel model = new UnparkInfoViewModel
            {
                        Id = vehicle.Id,
                        RegNr = vehicle.RegNr,
                        Manufacturer = vehicle.Manufacturer,
                        Type = vehicle.Type,
                        NOWheels = vehicle.NOWheels,
                        Color = vehicle.Color,
                        Model = vehicle.Model,
                        BeginParking = vehicle.BeginParking,
                        EndParking = time,
                        Period =  period,
                        Price = price
            };

            return View(model);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicle = await _context.Vehicle.FindAsync(id);
            _context.Vehicle.Remove(vehicle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Receipt), vehicle);
        }

        private bool VehicleExists(int id)
        {
            return _context.Vehicle.Any(e => e.Id == id);
        }

        public ActionResult Print()
        {
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Receipt(Vehicle vehicle)
        {
            //--
            var time = DateTime.Now.RoundDateTime();
            TimeSpan diff = time - vehicle.BeginParking;
            double period = diff.TotalHours;
            if (period < 1)
            {
                period = 1;
            }
            double price = 40 * period;
            //--
            UnparkInfoViewModel model = new UnparkInfoViewModel
            {
                Id = vehicle.Id,
                RegNr = vehicle.RegNr,
                Manufacturer = vehicle.Manufacturer,
                Type = vehicle.Type,
                NOWheels = vehicle.NOWheels,
                Color = vehicle.Color,
                Model = vehicle.Model,
                BeginParking = vehicle.BeginParking,
                EndParking = time,
                Period = period,
                Price = price
            };

            return View(model);
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
                await _context.Vehicle.Where(v => v.RegNr.ToLower().Contains(viewModel.RegNr.ToLower())).ToListAsync();

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

        public async Task<IActionResult> Statistics()
        {
            var vehicles = await _context.Vehicle.ToListAsync();

            var amountSedan = 0;
            var amountSUV = 0;
            var amountMC = 0;
            var amountBus = 0;
            var amountLimo = 0;
            var amountWheels = 0;

            var totalHours = 0;
            var totalPrice = 0;

            var time = DateTime.Now.RoundDateTime();
            

            foreach (var v in vehicles) {
                TimeSpan diff = time - v.BeginParking;
                double period = diff.TotalHours;

                if (period < 1)
                {
                    period = 1;
                }

                double price = 40 * period;

                totalHours += (int) period;
                totalPrice += (int) price;

                switch (v.Type)
                {
                    case TypeOfVehicle.Sedan:
                        amountSedan++;
                        break;
                    case TypeOfVehicle.SUV:
                        amountSUV++;
                        break;
                    case TypeOfVehicle.MC:
                        amountMC++;
                        break;
                    case TypeOfVehicle.Bus:
                        amountBus++;
                        break;
                    case TypeOfVehicle.Limo:
                        amountLimo++;
                        break;
                }
                amountWheels += v.NOWheels;
            }


            var stats = new StatisticsViewModel()
            {
                TotalWheels = amountWheels,
                TotalVehicles = vehicles.Count(),
                AmountSedan = amountSedan,
                AmountSUV = amountSUV,
                AmountMC = amountMC,
                AmountBus = amountBus,
                AmountLimo = amountLimo,
                TotalHours = totalHours,
                TotalPrice = totalPrice
            };
            return View(stats);

        }
    }
}
