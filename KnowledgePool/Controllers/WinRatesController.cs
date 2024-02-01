using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KnowledgePool.Models;
using KnowledgePool.Models.OtherModels;
using System.Text;
using SQLitePCL;

namespace KnowledgePool.Controllers
{
    public class WinRatesController : Controller
    {
        private readonly AllPrintingsContext _context;

        public WinRatesController(AllPrintingsContext context)
        {
            _context = context;
        }

        // GET: WinRates
        public async Task<IActionResult> Index()
        {
              return _context.WinRates != null ? 
                          View(await _context.WinRates.ToListAsync()) :
                          Problem("Entity set 'AllPrintingsContext.WinRates'  is null.");
        }

        // GET: WinRates/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.WinRates == null)
            {
                return NotFound();
            }

            var winRate = await _context.WinRates
                .FirstOrDefaultAsync(m => m.Uuid == id);
            if (winRate == null)
            {
                return NotFound();
            }

            return View(winRate);
        }

        // GET: WinRates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WinRates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Uuid,Name,Set,Color,Rarity,OhWr,GdWr,GihWr,Iwd")] WinRate winRate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(winRate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(winRate);
        }

        // GET: WinRates/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.WinRates == null)
            {
                return NotFound();
            }

            var winRate = await _context.WinRates.FindAsync(id);
            if (winRate == null)
            {
                return NotFound();
            }
            return View(winRate);
        }

        // POST: WinRates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Uuid,Name,Set,Color,Rarity,OhWr,GdWr,GihWr,Iwd")] WinRate winRate)
        {
            if (id != winRate.Uuid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(winRate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WinRateExists(winRate.Uuid))
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
            return View(winRate);
        }

        // GET: WinRates/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.WinRates == null)
            {
                return NotFound();
            }

            var winRate = await _context.WinRates
                .FirstOrDefaultAsync(m => m.Uuid == id);
            if (winRate == null)
            {
                return NotFound();
            }

            return View(winRate);
        }

        // POST: WinRates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.WinRates == null)
            {
                return Problem("Entity set 'AllPrintingsContext.WinRates'  is null.");
            }
            var winRate = await _context.WinRates.FindAsync(id);
            if (winRate != null)
            {
                _context.WinRates.Remove(winRate);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WinRateExists(string id)
        {
          return (_context.WinRates?.Any(e => e.Uuid == id)).GetValueOrDefault();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadWinRates(FileUpload upload)
        {
            if (upload.File != null && upload.File.FileName.EndsWith(".csv"))
            {
                var setCode = upload.File.FileName.Substring(0, 3);

                var lines = new List<string>();
                using (var reader = new StreamReader(upload.File.OpenReadStream()))
                {
                    while (reader.Peek() >= 0)
                        lines.Add(reader.ReadLine());
                }

                var winRates = lines
                    .Skip(1)
                    .Select(_ => _.Split(new string[] { "\",\"" }, StringSplitOptions.None))
                    .Select(_ => new WinRate
                    {
                        Uuid = GetUuid(_[0].Substring(1), setCode),
                        Name = _[0].Substring(1),
                        Set = setCode,
                        Color = _[1],
                        Rarity = _[2],
                        OhWr = ConvertRate(_[11], 1),
                        GdWr = ConvertRate(_[13], 1),
                        GihWr = ConvertRate(_[15], 1),
                        Iwd = ConvertRate(_[18], 3),

                    })
                    .ToList();

                _context.WinRates.AddRange(winRates);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        public decimal? ConvertRate(string value, int endOfInput)
        {
            if (value.Length < 4) return null;

            else return Convert.ToDecimal(value.Substring(0, value.Length - endOfInput));
        }

        public string GetUuid(string cardName, string setCode)
        {
            try
            {
                var bonusSheetCode = _context.Sets.Any(_ => _.ParentCode == setCode && _.Type == "masterpiece") ? _context.Sets.First(_ => _.ParentCode == setCode && _.Type == "masterpiece").Code : string.Empty;
                return _context.Cards.First(_ => 
                    _.Name.StartsWith(cardName) && 
                    ((_.SetCode == setCode && _.PromoTypes == null) || 
                    _.SetCode == bonusSheetCode))
                    .Uuid;

            }
            catch (Exception e)
            {
                throw;
            }        
        }
    }
}
