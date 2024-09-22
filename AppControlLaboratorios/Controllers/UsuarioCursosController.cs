using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppControlLaboratorios.Data;
using AppControlLaboratorios.Models;

namespace AppControlLaboratorios.Controllers
{
    public class UsuarioCursosController : Controller
    {
        private readonly BDContexto _context;

        public UsuarioCursosController(BDContexto context)
        {
            _context = context;
        }

        // GET: UsuarioCursos
        public async Task<IActionResult> Index()
        {
            var bDContexto = _context.UsuarioCursos.Include(u => u.Curso).Include(u => u.Usuario);
            return View(await bDContexto.ToListAsync());
        }

        // GET: UsuarioCursos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarioCurso = await _context.UsuarioCursos
                .Include(u => u.Curso)
                .Include(u => u.Usuario)
                .FirstOrDefaultAsync(m => m.UsuarioId == id);
            if (usuarioCurso == null)
            {
                return NotFound();
            }

            return View(usuarioCurso);
        }

        // GET: UsuarioCursos/Create
        public IActionResult Create()
        {
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Id");
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id");
            return View();
        }

        // POST: UsuarioCursos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UsuarioId,CursoId")] UsuarioCurso usuarioCurso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuarioCurso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Id", usuarioCurso.CursoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", usuarioCurso.UsuarioId);
            return View(usuarioCurso);
        }

        // GET: UsuarioCursos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarioCurso = await _context.UsuarioCursos.FindAsync(id);
            if (usuarioCurso == null)
            {
                return NotFound();
            }
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Id", usuarioCurso.CursoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", usuarioCurso.UsuarioId);
            return View(usuarioCurso);
        }

        // POST: UsuarioCursos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UsuarioId,CursoId")] UsuarioCurso usuarioCurso)
        {
            if (id != usuarioCurso.UsuarioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuarioCurso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioCursoExists(usuarioCurso.UsuarioId))
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
            ViewData["CursoId"] = new SelectList(_context.Cursos, "Id", "Id", usuarioCurso.CursoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", usuarioCurso.UsuarioId);
            return View(usuarioCurso);
        }

        // GET: UsuarioCursos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarioCurso = await _context.UsuarioCursos
                .Include(u => u.Curso)
                .Include(u => u.Usuario)
                .FirstOrDefaultAsync(m => m.UsuarioId == id);
            if (usuarioCurso == null)
            {
                return NotFound();
            }

            return View(usuarioCurso);
        }

        // POST: UsuarioCursos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuarioCurso = await _context.UsuarioCursos.FindAsync(id);
            if (usuarioCurso != null)
            {
                _context.UsuarioCursos.Remove(usuarioCurso);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioCursoExists(int id)
        {
            return _context.UsuarioCursos.Any(e => e.UsuarioId == id);
        }
    }
}
