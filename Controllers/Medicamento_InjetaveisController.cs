using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Trabalho_Gustavo_Karoline.Models;

namespace Trabalho_Gustavo_Karoline.Controllers
{
    public class Medicamento_InjetaveisController : Controller
    {
        private readonly Contexto _context;

        public Medicamento_InjetaveisController(Contexto context)
        {
            _context = context;
        }

        // GET: Medicamento_Injetaveis
        public async Task<IActionResult> Index()
        {
              return View(await _context.Medicamento_Injetaveis.ToListAsync());
        }

        // GET: Medicamento_Injetaveis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Medicamento_Injetaveis == null)
            {
                return NotFound();
            }

            var medicamento_Injetaveis = await _context.Medicamento_Injetaveis
                .FirstOrDefaultAsync(m => m.codigo == id);
            if (medicamento_Injetaveis == null)
            {
                return NotFound();
            }

            return View(medicamento_Injetaveis);
        }

        // GET: Medicamento_Injetaveis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Medicamento_Injetaveis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("codigo,nome,unidade,Qtde_Estoque")] Medicamento_Injetaveis medicamento_Injetaveis)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medicamento_Injetaveis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(medicamento_Injetaveis);
        }

        // GET: Medicamento_Injetaveis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Medicamento_Injetaveis == null)
            {
                return NotFound();
            }

            var medicamento_Injetaveis = await _context.Medicamento_Injetaveis.FindAsync(id);
            if (medicamento_Injetaveis == null)
            {
                return NotFound();
            }
            return View(medicamento_Injetaveis);
        }

        // POST: Medicamento_Injetaveis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("codigo,nome,unidade,Qtde_Estoque")] Medicamento_Injetaveis medicamento_Injetaveis)
        {
            if (id != medicamento_Injetaveis.codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicamento_Injetaveis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Medicamento_InjetaveisExists(medicamento_Injetaveis.codigo))
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
            return View(medicamento_Injetaveis);
        }

        // GET: Medicamento_Injetaveis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Medicamento_Injetaveis == null)
            {
                return NotFound();
            }

            var medicamento_Injetaveis = await _context.Medicamento_Injetaveis
                .FirstOrDefaultAsync(m => m.codigo == id);
            if (medicamento_Injetaveis == null)
            {
                return NotFound();
            }

            return View(medicamento_Injetaveis);
        }

        // POST: Medicamento_Injetaveis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Medicamento_Injetaveis == null)
            {
                return Problem("Entity set 'Contexto.Medicamento_Injetaveis'  is null.");
            }
            var medicamento_Injetaveis = await _context.Medicamento_Injetaveis.FindAsync(id);
            if (medicamento_Injetaveis != null)
            {
                _context.Medicamento_Injetaveis.Remove(medicamento_Injetaveis);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Medicamento_InjetaveisExists(int id)
        {
          return _context.Medicamento_Injetaveis.Any(e => e.codigo == id);
        }
    }
}
