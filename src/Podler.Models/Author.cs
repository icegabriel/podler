﻿using Podler.Models.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Podler.Models
{
    public class Author : BaseModel, ISelectListItem
    {
        [Required(ErrorMessage = "O nome do autor é obrigatório")]
        [StringLength(40, ErrorMessage = "O campo {0} precisa ter no mínimo {2} e no máximo {1} caracteres.", MinimumLength = 1)]
        public string Name { get; set; }

        public List<ComicAuthor> ComicAuthors { get; set; }

        public Author()
        {
            ComicAuthors = new List<ComicAuthor>();
        }
    }
}