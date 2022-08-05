using System.ComponentModel.DataAnnotations;

namespace ToDo.Models.ModelsDto
{
    public class AddRoleRequestDto
    {
        [Required]
        [StringLength(30, MinimumLength = 1)]
        public string RoleName { get; set; }
    }
}
