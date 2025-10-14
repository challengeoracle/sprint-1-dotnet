using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Medix.Data;
using Medix.Models;

namespace Medix.Controllers
{
    [Authorize]
    public class UnidadesMedicasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UnidadesMedicasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UnidadesMedicas
        public async Task<IActionResult> Index()
        {
            return View(await _context.UnidadesMedicas.ToListAsync());
        }

        // GET: UnidadesMedicas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unidadeMedica = await _context.UnidadesMedicas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (unidadeMedica == null)
            {
                return NotFound();
            }

            return View(unidadeMedica);
        }

        // GET: UnidadesMedicas/Create
        public IActionResult Create()
        {
            // A linha abaixo cria uma lista com os nomes do enum "StatusUnidade"
            // e a envia para a View através do ViewBag.
            ViewBag.StatusOptions = new SelectList(Enum.GetValues(typeof(StatusUnidade)));
            return View();
        }

        // POST: UnidadesMedicas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,CNPJ,Endereco,Telefone,EmailAdmin,Status,DataCadastro")] UnidadeMedica unidadeMedica)
        {
            if (ModelState.IsValid)
            {
                _context.Add(unidadeMedica);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(unidadeMedica);
        }

        // GET: UnidadesMedicas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unidadeMedica = await _context.UnidadesMedicas.FindAsync(id);
            if (unidadeMedica == null)
            {
                return NotFound();
            }

            // Envia as opções de status para a view de edição
            ViewBag.StatusOptions = new SelectList(Enum.GetValues(typeof(StatusUnidade)));

            return View(unidadeMedica);
        }

        // POST: UnidadesMedicas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,CNPJ,Endereco,Telefone,EmailAdmin,Status,DataCadastro")] UnidadeMedica unidadeMedica)
        {
            if (id != unidadeMedica.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(unidadeMedica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnidadeMedicaExists(unidadeMedica.Id))
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
            return View(unidadeMedica);
        }

        // GET: UnidadesMedicas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unidadeMedica = await _context.UnidadesMedicas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (unidadeMedica == null)
            {
                return NotFound();
            }

            return View(unidadeMedica);
        }

        // POST: UnidadesMedicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var unidadeMedica = await _context.UnidadesMedicas.FindAsync(id);
            if (unidadeMedica != null)
            {
                _context.UnidadesMedicas.Remove(unidadeMedica);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UnidadeMedicaExists(int id)
        {
            return _context.UnidadesMedicas.Any(e => e.Id == id);
        }
    }
}
