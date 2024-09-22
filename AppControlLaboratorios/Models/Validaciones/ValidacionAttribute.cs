using AppControlLaboratorios.Controllers;
using System.ComponentModel.DataAnnotations;

namespace AppControlLaboratorios.Models.Validaciones
{
    public class ValidacionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Intenta obtener el controlador
            var controller = validationContext.GetService(typeof(UsuariosController)) as UsuariosController;

            // Asegúrate de que el controlador no sea null
            if (controller != null && controller.IsCreatingUser)
            {
                if (string.IsNullOrEmpty(value?.ToString()))
                {
                    return new ValidationResult(ErrorMessage ?? "El nombre es obligatorio.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
