﻿using System;
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
    public class UsuariosController : Controller
    {
        private readonly BDContexto _context;

        public UsuariosController(BDContexto context)
        {
            _context = context;
        }

        //Validaciones
        public bool IsCreatingUser { get; set; }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            var bDContexto = _context.Usuarios.Include(u => u.Rol);
            return View(await bDContexto.ToListAsync());
        }


        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            IsCreatingUser = true;
            ViewData["RolId"] = new SelectList(_context.Roles, "Id", "Id");
            return View();
        }


        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellidos,Correo,Contrasena,RolId")] Usuario usuario)
        {
            IsCreatingUser = false;
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RolId"] = new SelectList(_context.Roles, "Id", "Id", usuario.RolId);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewData["RolId"] = new SelectList(_context.Roles, "Id", "Id", usuario.RolId);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellidos,Correo,Contrasena,RolId")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
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
            ViewData["RolId"] = new SelectList(_context.Roles, "Id", "Id", usuario.RolId);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Usuario usuario)
        {
            IsCreatingUser = false;
            if (ModelState.IsValid)
            {
                // Se busca el usuario en la base de datos por correo y contraseña
                var usuarioValido = await _context.Usuarios
                    .Include(u => u.Rol) // Incluir el rol del usuario
                    .FirstOrDefaultAsync(u => u.Correo == usuario.Correo && u.Contrasena == usuario.Contrasena);

                if (usuarioValido != null)
                {
                    // Dependiendo del Rol, se muestra una vista diferente
                    if (usuarioValido.Rol.Nombre == "Docente")
                    {
                        return View("VistaDocente", usuarioValido);
                    }
                    else if (usuarioValido.Rol.Nombre == "Estudiante")
                    {
                        return View("VistaEstudiante", usuarioValido);
                    }
                    else if (usuarioValido.Rol.Nombre == "Administrativo")
                    {
                        return View("VistaAdministrador", usuarioValido);
                    }
                }

                ModelState.AddModelError(string.Empty, "Intento de inicio de sesión no válido.");
            }
            return View(usuario); // Si algo falla, vuelve a mostrar la vista de login
        }
        public IActionResult VistaEstudiante(int idUsuario)
        {
            // Obtener el usuario desde la base de datos
            var usuario = _context.Usuarios.Find(idUsuario);

            // Verificar si el usuario fue encontrado
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }
        public IActionResult VistaDocente(int idUsuario)
        {
            // Obtener el usuario desde la base de datos
            var usuario = _context.Usuarios.Find(idUsuario);

            // Verificar si el usuario fue encontrado
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}
