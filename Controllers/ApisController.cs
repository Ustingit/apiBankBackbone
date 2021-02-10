using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ApiBankBackBone.Data;
using ApiBankBackBone.Models.Apis;
using AutoMapper;

namespace ApiBankBackBone.Controllers
{
    public class ApisController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ApisController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Apis
        public async Task<IActionResult> Index()
        {
            return View(await _context.Apis.ToListAsync());
        }

        // GET: Apis/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var api = await _context.Apis.FirstOrDefaultAsync(m => m.Id == id);

            if (api == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<ApiDto>(api);
            result.Methods = await _context.Methods.Where(m => m.ApiId == id).ToListAsync() ?? new List<Method>();

            return View(result);
        }

        // GET: Apis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Apis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,IsFree,AccessCost,MonthlyCost,AdditionalAccessRules,License")] Api api)
        {
            if (ModelState.IsValid)
            {
                api.Id = Guid.NewGuid();
                _context.Add(api);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(api);
        }

        // GET: Apis/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var api = await _context.Apis.FindAsync(id);
            if (api == null)
            {
                return NotFound();
            }
            return View(api);
        }

        // POST: Apis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description,IsFree,AccessCost,MonthlyCost,AdditionalAccessRules,License")] Api api)
        {
            if (id != api.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(api);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApiExists(api.Id))
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
            return View(api);
        }

        // GET: Apis/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var api = await _context.Apis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (api == null)
            {
                return NotFound();
            }

            return View(api);
        }

        // POST: Apis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var api = await _context.Apis.FindAsync(id);
            _context.Apis.Remove(api);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApiExists(Guid id)
        {
            return _context.Apis.Any(e => e.Id == id);
        }
    }
}
