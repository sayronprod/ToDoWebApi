using System.ComponentModel.DataAnnotations;

namespace ToDo.Models.ModelsDto
{
    public class DeleteRoleInUserByIdRequestDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 1)]
        public string RoleName { get; set; }
    }
}
