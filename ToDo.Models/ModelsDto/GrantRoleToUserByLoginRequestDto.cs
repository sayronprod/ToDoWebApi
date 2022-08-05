using System.ComponentModel.DataAnnotations;

namespace ToDo.Models.ModelsDto
{
    public class GrantRoleToUserByLoginRequestDto
    {
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string UserLogin { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 1)]
        public string RoleName { get; set; }
    }
}
