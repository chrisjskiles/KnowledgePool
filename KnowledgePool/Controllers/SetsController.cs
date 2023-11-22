using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KnowledgePool.Models;

namespace KnowledgePool.Controllers
{
    public class SetsController : Controller
    {
        private readonly AllPrintingsContext _context;

        public SetsController(AllPrintingsContext context)
        {
            _context = context;
        }

        // GET: Sets
        public async Task<IActionResult> Index()
        {
              return _context.Sets != null ? 
                          View(await _context.Sets.ToListAsync()) :
                          Problem("Entity set 'AllPrintingsContext.Sets'  is null.");
        }

        // GET: Sets/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Sets == null)
            {
                return NotFound();
            }

            var @set = await _context.Sets
                .FirstOrDefaultAsync(m => m.Code == id);
            if (@set == null)
            {
                return NotFound();
            }

            return View(@set);
        }

        // GET: Sets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BaseSetSize,Block,CardsphereSetId,Code,IsFoilOnly,IsForeignOnly,IsNonFoilOnly,IsOnlineOnly,IsPartialPreview,KeyruneCode,Languages,McmId,McmIdExtras,McmName,MtgoCode,Name,ParentCode,ReleaseDate,TcgplayerGroupId,TokenSetCode,TotalSetSize,Type")] Set @set)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@set);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@set);
        }

        // GET: Sets/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Sets == null)
            {
                return NotFound();
            }

            var @set = await _context.Sets.FindAsync(id);
            if (@set == null)
            {
                return NotFound();
            }
            return View(@set);
        }

        // POST: Sets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("BaseSetSize,Block,CardsphereSetId,Code,IsFoilOnly,IsForeignOnly,IsNonFoilOnly,IsOnlineOnly,IsPartialPreview,KeyruneCode,Languages,McmId,McmIdExtras,McmName,MtgoCode,Name,ParentCode,ReleaseDate,TcgplayerGroupId,TokenSetCode,TotalSetSize,Type")] Set @set)
        {
            if (id != @set.Code)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@set);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SetExists(@set.Code))
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
            return View(@set);
        }

        // GET: Sets/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Sets == null)
            {
                return NotFound();
            }

            var @set = await _context.Sets
                .FirstOrDefaultAsync(m => m.Code == id);
            if (@set == null)
            {
                return NotFound();
            }

            return View(@set);
        }

        // POST: Sets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Sets == null)
            {
                return Problem("Entity set 'AllPrintingsContext.Sets'  is null.");
            }
            var @set = await _context.Sets.FindAsync(id);
            if (@set != null)
            {
                _context.Sets.Remove(@set);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SetExists(string id)
        {
          return (_context.Sets?.Any(e => e.Code == id)).GetValueOrDefault();
        }
    }
}
