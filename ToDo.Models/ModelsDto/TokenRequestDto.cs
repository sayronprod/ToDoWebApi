﻿using System.ComponentModel.DataAnnotations;

namespace ToDo.Models.ModelsDto
{
    public class TokenRequestDto
    {
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Login { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Password { get; set; }
    }
}
