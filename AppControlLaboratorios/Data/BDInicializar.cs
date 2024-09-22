using AppControlLaboratorios.Models;
using Microsoft.EntityFrameworkCore;

namespace AppControlLaboratorios.Data
{
    public class BDInicializar
    {
        public static void Registrar(BDContexto contexto)
        {
            // Verificar si ya existen roles en la base de datos
            if (!contexto.Roles.Any())
            {
                var roles = new List<Rol>
                {
                    new Rol { Nombre = "Estudiante" },
                    new Rol { Nombre = "Docente" },
                    new Rol { Nombre = "Administrativo" }
                };

                contexto.Roles.AddRange(roles);
                contexto.SaveChanges();
            }

            // Verificar si ya existen usuarios en la base de datos
            if (!contexto.Usuarios.Any())
            {
                var usuarios = new List<Usuario>
                {
                    new Usuario
                    {
                        Nombre = "Juan",
                        Apellidos = "Pérez",
                        Correo = "juan.perez@ejemplo.com",
                        Contrasena = "12345",
                        RolId = contexto.Roles.First(r => r.Nombre == "Estudiante").Id
                    },
                    new Usuario
                    {
                        Nombre = "Ana",
                        Apellidos = "García",
                        Correo = "ana.garcia@ejemplo.com",
                        Contrasena = "67890", // Contraseña en texto plano
                        RolId = contexto.Roles.First(r => r.Nombre == "Docente").Id
                    },
                    new Usuario
                    {
                        Nombre = "Carlos",
                        Apellidos = "Lopez",
                        Correo = "carlos.lopez@ejemplo.com",
                        Contrasena = "admin123", // Contraseña en texto plano
                        RolId = contexto.Roles.First(r => r.Nombre == "Administrativo").Id
                    }
                };

                contexto.Usuarios.AddRange(usuarios);
                contexto.SaveChanges();
            }

            // Verificar si ya existen laboratorios en la base de datos
            if (!contexto.Laboratorios.Any())
            {
                var laboratorios = new List<Laboratorio>
                {
                    new Laboratorio { LaboratorioNombre = "Laboratorio 01" },
                    new Laboratorio { LaboratorioNombre = "Laboratorio 02" },
                    new Laboratorio { LaboratorioNombre = "Laboratorio 03" },
                    new Laboratorio { LaboratorioNombre = "Laboratorio 04" }
                };

                contexto.Laboratorios.AddRange(laboratorios);
                contexto.SaveChanges();
            }

            if (!contexto.Cursos.Any())
            {
                var cursos = new List<Curso>
                {
                    new Curso {CursoNombre = "Programación Aplicada III"},
                    new Curso {CursoNombre = "Inteligencia de negocios" },
                    new Curso {CursoNombre = "Sistemas inteligentes" },
                    new Curso {CursoNombre = "Fundamentos de los sistemas de información" },
                };
                contexto.Cursos.AddRange(cursos);
                contexto.SaveChanges();
            }
            if (!contexto.Maquinas.Any())
            {
                var maquinas = new List<Maquina>();

                // Inicializar máquinas para el Laboratorio 1 (35 máquinas)
                for (int i = 1; i <= 35; i++)
                {
                    maquinas.Add(new Maquina { NumSerie = $"NumSerie{i}", LaboratorioId = contexto.Laboratorios.First(l => l.Id == 1).Id });
                }

                // Inicializar máquinas para el Laboratorio 2 (32 máquinas)
                for (int i = 1; i <= 32; i++)
                {
                    maquinas.Add(new Maquina { NumSerie = $"NumSerie{i}", LaboratorioId = contexto.Laboratorios.First(l => l.Id == 2).Id });
                }

                // Inicializar máquinas para el Laboratorio 3 (35 máquinas)
                for (int i = 1; i <= 35; i++)
                {
                    maquinas.Add(new Maquina { NumSerie = $"NumSerie{i}", LaboratorioId = contexto.Laboratorios.First(l => l.Id == 3).Id });
                }

                // Inicializar máquinas para el Laboratorio 4 (24 máquinas)
                for (int i = 1; i <= 24; i++)
                {
                    maquinas.Add(new Maquina { NumSerie = $"NumSerie{i}", LaboratorioId = contexto.Laboratorios.First(l => l.Id == 4).Id });
                }

                contexto.Maquinas.AddRange(maquinas);
                contexto.SaveChanges();
            }
        }
    }
}
