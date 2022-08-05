using System.ComponentModel.DataAnnotations;

namespace ToDo.Models.ModelsDto
{
    public class DeleteRoleRequestDto
    {
        [Required]
        [StringLength(30, MinimumLength = 1)]
        public string RoleName { get; set; }
    }
}
