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
    public class AsistenciasController : Controller
    {
        private readonly BDContexto _context;

        public AsistenciasController(BDContexto context)
        {
            _context = context;
        }

        // GET: Asistencias
        public async Task<IActionResult> Index()
        {
            var bDContexto = _context.Asistencias.Include(a => a.Horario).Include(a => a.Maquina).Include(a => a.Usuario);
            return View(await bDContexto.ToListAsync());
        }

        // GET: Asistencias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asistencia = await _context.Asistencias
                .Include(a => a.Horario)
                .Include(a => a.Maquina)
                .Include(a => a.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asistencia == null)
            {
                return NotFound();
            }

            return View(asistencia);
        }

        // GET: Asistencias/Create
        public IActionResult Create(int usuarioId, int maquinaId)
        {
            ViewData["HorarioId"] = new SelectList(_context.Horarios, "Id", "Id");
            var usuario = _context.Usuarios.Find(usuarioId);
            var maquina = _context.Maquinas.Find(maquinaId);
            var asistencia = new Asistencia
            {
                Usuario = usuario,
                Maquina = maquina,
                UsuarioId = usuarioId,
                MaquinaId = maquinaId
            };

            return View(asistencia);
        }


        // POST: Asistencias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fecha,Observaciones,UsuarioId,MaquinaId,HorarioId")] Asistencia asistencia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(asistencia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HorarioId"] = new SelectList(_context.Horarios, "Id", "Id", asistencia.HorarioId);
            return View(asistencia);
        }

        // GET: Asistencias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asistencia = await _context.Asistencias.FindAsync(id);
            if (asistencia == null)
            {
                return NotFound();
            }
            ViewData["HorarioId"] = new SelectList(_context.Horarios, "Id", "Id", asistencia.HorarioId);
            ViewData["MaquinaId"] = new SelectList(_context.Maquinas, "Id", "Id", asistencia.MaquinaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", asistencia.UsuarioId);
            return View(asistencia);
        }

        // POST: Asistencias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,Observaciones,UsuarioId,MaquinaId,HorarioId")] Asistencia asistencia)
        {
            if (id != asistencia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asistencia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AsistenciaExists(asistencia.Id))
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
            ViewData["HorarioId"] = new SelectList(_context.Horarios, "Id", "Id", asistencia.HorarioId);
            ViewData["MaquinaId"] = new SelectList(_context.Maquinas, "Id", "Id", asistencia.MaquinaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", asistencia.UsuarioId);
            return View(asistencia);
        }

        // GET: Asistencias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asistencia = await _context.Asistencias
                .Include(a => a.Horario)
                .Include(a => a.Maquina)
                .Include(a => a.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asistencia == null)
            {
                return NotFound();
            }

            return View(asistencia);
        }

        // POST: Asistencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var asistencia = await _context.Asistencias.FindAsync(id);
            if (asistencia != null)
            {
                _context.Asistencias.Remove(asistencia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Finalizar));
        }

        private bool AsistenciaExists(int id)
        {
            return _context.Asistencias.Any(e => e.Id == id);
        }

        //Selecionar laboratorios
        public IActionResult SeleccionLab(int id, int idlab)
        {

            var maquinas = _context.Maquinas.Where(m => m.LaboratorioId == idlab).ToList(); // Cargar máquinas del laboratorio
            if (!maquinas.Any())
            {
                // Manejo de error o mensaje
                ViewBag.Mensaje = "No hay máquinas disponibles en este laboratorio.";
            }
            var usuario = _context.Usuarios.Find(id);
            var asistencia = new Asistencia
            {
                UsuarioId = id,
                Usuario = usuario
            };
            ViewBag.LaboratorioId = idlab;
            ViewBag.Maquinas = maquinas; // Pasar la lista de máquinas para que tambien vaya a la vista
            return View(asistencia);
        }
        public IActionResult Finalizar()
        {
            return View();
        }

    }
}
