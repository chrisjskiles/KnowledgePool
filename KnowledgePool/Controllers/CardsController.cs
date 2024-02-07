using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KnowledgePool.Models;
using KnowledgePool.Models.OtherModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KnowledgePool.Controllers
{
    public class CardsController : Controller
    {
        private readonly AllPrintingsContext _context;

        public CardsController(AllPrintingsContext context)
        {
            _context = context;
        }

        // GET: Cards
        public async Task<IActionResult> Index()
        {
              return _context.Cards != null ? 
                          View(await _context.Cards.ToListAsync()) :
                          Problem("Entity set 'AllPrintingsContext.Cards'  is null.");
        }

        // GET: Cards/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Cards == null)
            {
                return NotFound();
            }

            var card = await _context.Cards
                .FirstOrDefaultAsync(m => m.Uuid == id);
            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        // GET: Cards/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Artist,ArtistIds,AsciiName,AttractionLights,Availability,BoosterTypes,BorderColor,CardParts,ColorIdentity,ColorIndicator,Colors,Defense,DuelDeck,EdhrecRank,EdhrecSaltiness,FaceConvertedManaCost,FaceFlavorName,FaceManaValue,FaceName,Finishes,FlavorName,FlavorText,FrameEffects,FrameVersion,Hand,HasAlternativeDeckLimit,HasContentWarning,HasFoil,HasNonFoil,IsAlternative,IsFullArt,IsFunny,IsOnlineOnly,IsOversized,IsPromo,IsRebalanced,IsReprint,IsReserved,IsStarter,IsStorySpotlight,IsTextless,IsTimeshifted,Keywords,Language,Layout,LeadershipSkills,Life,Loyalty,ManaCost,ManaValue,Name,Number,OriginalPrintings,OriginalReleaseDate,OriginalText,OriginalType,OtherFaceIds,Power,Printings,PromoTypes,Rarity,RebalancedPrintings,RelatedCards,SecurityStamp,SetCode,Side,Signature,SourceProducts,Subsets,Subtypes,Supertypes,Text,Toughness,Type,Types,Uuid,Variations,Watermark")] Card card)
        {
            if (ModelState.IsValid)
            {
                _context.Add(card);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(card);
        }

        // GET: Cards/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Cards == null)
            {
                return NotFound();
            }

            var card = await _context.Cards.FindAsync(id);
            if (card == null)
            {
                return NotFound();
            }
            return View(card);
        }

        // POST: Cards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Artist,ArtistIds,AsciiName,AttractionLights,Availability,BoosterTypes,BorderColor,CardParts,ColorIdentity,ColorIndicator,Colors,Defense,DuelDeck,EdhrecRank,EdhrecSaltiness,FaceConvertedManaCost,FaceFlavorName,FaceManaValue,FaceName,Finishes,FlavorName,FlavorText,FrameEffects,FrameVersion,Hand,HasAlternativeDeckLimit,HasContentWarning,HasFoil,HasNonFoil,IsAlternative,IsFullArt,IsFunny,IsOnlineOnly,IsOversized,IsPromo,IsRebalanced,IsReprint,IsReserved,IsStarter,IsStorySpotlight,IsTextless,IsTimeshifted,Keywords,Language,Layout,LeadershipSkills,Life,Loyalty,ManaCost,ManaValue,Name,Number,OriginalPrintings,OriginalReleaseDate,OriginalText,OriginalType,OtherFaceIds,Power,Printings,PromoTypes,Rarity,RebalancedPrintings,RelatedCards,SecurityStamp,SetCode,Side,Signature,SourceProducts,Subsets,Subtypes,Supertypes,Text,Toughness,Type,Types,Uuid,Variations,Watermark")] Card card)
        {
            if (id != card.Uuid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(card);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CardExists(card.Uuid))
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
            return View(card);
        }

        // GET: Cards/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Cards == null)
            {
                return NotFound();
            }

            var card = await _context.Cards
                .FirstOrDefaultAsync(m => m.Uuid == id);
            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        // POST: Cards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Cards == null)
            {
                return Problem("Entity set 'AllPrintingsContext.Cards'  is null.");
            }
            var card = await _context.Cards.FindAsync(id);
            if (card != null)
            {
                _context.Cards.Remove(card);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CardExists(string id)
        {
          return (_context.Cards?.Any(e => e.Uuid == id)).GetValueOrDefault();
        }

        public ActionResult UpdateDatabase(FileUpload upload)
        {
            if (upload.File != null && upload.File.FileName.EndsWith(".json"))
            {
                var setCode = upload.File.FileName.Substring(0, 3);

                var lines = new List<string>();
                using (var reader = new StreamReader(upload.File.OpenReadStream()))
                {
                    while (reader.Peek() >= 0)
                        lines.Add(reader.ReadLine());
                }

                var fullString = string.Join(' ', lines);

                try
                {
                    //var result = JsonConvert.DeserializeObject<SetJson>(fullString);
                    var jObject = JObject.Parse(fullString);
                    var setJson = jObject["data"];
                    var cardsJson = setJson["cards"] as JArray;

                    var set = new Set(setJson);

                    var cards = cardsJson.Select(_ => new Card(_));

                    _context.AddRange(cards);
                    _context.Add(set);
                    _context.SaveChanges();
                }

                catch (Exception ex) 
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
