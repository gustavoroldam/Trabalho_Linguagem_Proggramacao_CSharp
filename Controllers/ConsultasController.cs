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
    public class ConsultasController : Controller
    {
        private readonly Contexto _context;

        public ConsultasController(Contexto context)
        {
            _context = context;
        }

        // GET: Consultas
        public async Task<IActionResult> Index()
        {
            var contexto = _context.Consulta.Include(c => c.Medicamento_Injetaveis).Include(c => c.medico).Include(c => c.paciente);
            return View(await contexto.ToListAsync());
        }

        // GET: Consultas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Consulta == null)
            {
                return NotFound();
            }

            var consulta = await _context.Consulta
                .Include(c => c.Medicamento_Injetaveis)
                .Include(c => c.medico)
                .Include(c => c.paciente)
                .FirstOrDefaultAsync(m => m.id == id);
            if (consulta == null)
            {
                return NotFound();
            }

            return View(consulta);
        }

        // GET: Consultas/Create
        public IActionResult Create()
        {
            ViewData["MedicamentoId"] = new SelectList(_context.Medicamento_Injetaveis, "codigo", "nome");
            ViewData["MadicoId"] = new SelectList(_context.Medicos, "crm", "nome");
            ViewData["PacienteID"] = new SelectList(_context.Paciente, "cpf", "nome");
            return View();
        }

        // POST: Consultas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,PacienteID,MadicoId,MedicamentoId,Qtde_Vacina")] Consulta consulta)
        {
            if (ModelState.IsValid)
            {
                Medicamento_Injetaveis medicamento_Injetaveis = await _context.Medicamento_Injetaveis.FindAsync(consulta.MedicamentoId);
                medicamento_Injetaveis.Qtde_Estoque = medicamento_Injetaveis.Qtde_Estoque - consulta.Qtde_Vacina;
                _context.Update(medicamento_Injetaveis);
                await _context.SaveChangesAsync();
                _context.Add(consulta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["MedicamentoId"] = new SelectList(_context.Medicamento_Injetaveis, "codigo", "nome", consulta.MedicamentoId);
            ViewData["MadicoId"] = new SelectList(_context.Medicos, "crm", "especialidade", consulta.MadicoId);
            ViewData["PacienteID"] = new SelectList(_context.Paciente, "cpf", "nome", consulta.PacienteID);
            return View(consulta);
        }

        // GET: Consultas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Consulta == null)
            {
                return NotFound();
            }

            var consulta = await _context.Consulta.FindAsync(id);
            if (consulta == null)
            {
                return NotFound();
            }
            ViewData["MedicamentoId"] = new SelectList(_context.Medicamento_Injetaveis, "codigo", "nome", consulta.MedicamentoId);
            ViewData["MadicoId"] = new SelectList(_context.Medicos, "crm", "especialidade", consulta.MadicoId);
            ViewData["PacienteID"] = new SelectList(_context.Paciente, "cpf", "nome", consulta.PacienteID);
            return View(consulta);
        }

        // POST: Consultas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,PacienteID,MadicoId,MedicamentoId,Qtde_Vacina")] Consulta consulta)
        {
            if (id != consulta.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var consulta_Aux = await _context.Consulta.FindAsync(id);

                    if (consulta_Aux.MedicamentoId == consulta.MedicamentoId)
                    {
                        Medicamento_Injetaveis medicamento_Injetaveis = await _context.Medicamento_Injetaveis.FindAsync(consulta_Aux.MedicamentoId);
                        _context.Entry(medicamento_Injetaveis).State = EntityState.Detached; // Desanexa a entidade
                        medicamento_Injetaveis.Qtde_Estoque = (medicamento_Injetaveis.Qtde_Estoque + consulta_Aux.Qtde_Vacina) - consulta.Qtde_Vacina;
                        _context.Update(medicamento_Injetaveis); // Anexa a entidade de volta ao contexto
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        Medicamento_Injetaveis medicamento_Injetaveis = await _context.Medicamento_Injetaveis.FindAsync(consulta_Aux.MedicamentoId);
                        _context.Entry(medicamento_Injetaveis).State = EntityState.Detached; // Desanexa a entidade
                        medicamento_Injetaveis.Qtde_Estoque = medicamento_Injetaveis.Qtde_Estoque + consulta_Aux.Qtde_Vacina;
                        _context.Update(medicamento_Injetaveis); // Anexa a entidade de volta ao contexto
                        await _context.SaveChangesAsync();

                        Medicamento_Injetaveis medicamento_Injetaveis_Novo = await _context.Medicamento_Injetaveis.FindAsync(consulta.MedicamentoId);
                        _context.Entry(medicamento_Injetaveis_Novo).State = EntityState.Detached; // Desanexa a entidade
                        medicamento_Injetaveis_Novo.Qtde_Estoque = medicamento_Injetaveis_Novo.Qtde_Estoque - consulta.Qtde_Vacina;
                        _context.Update(medicamento_Injetaveis_Novo); // Anexa a entidade de volta ao contexto
                        await _context.SaveChangesAsync();
                    }


                    _context.Update(consulta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsultaExists(consulta.id))
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
            ViewData["MedicamentoId"] = new SelectList(_context.Medicamento_Injetaveis, "codigo", "nome", consulta.MedicamentoId);
            ViewData["MadicoId"] = new SelectList(_context.Medicos, "crm", "especialidade", consulta.MadicoId);
            ViewData["PacienteID"] = new SelectList(_context.Paciente, "cpf", "nome", consulta.PacienteID);
            return View(consulta);
        }

        // GET: Consultas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Consulta == null)
            {
                return NotFound();
            }

            var consulta = await _context.Consulta
                .Include(c => c.Medicamento_Injetaveis)
                .Include(c => c.medico)
                .Include(c => c.paciente)
                .FirstOrDefaultAsync(m => m.id == id);
            if (consulta == null)
            {
                return NotFound();
            }

            return View(consulta);
        }

        // POST: Consultas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Consulta == null)
            {
                return Problem("Entity set 'Contexto.Consulta'  is null.");
            }
            var consulta = await _context.Consulta.FindAsync(id);
            if (consulta != null)
            {
                _context.Consulta.Remove(consulta);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsultaExists(int id)
        {
          return _context.Consulta.Any(e => e.id == id);
        }
    }
}
