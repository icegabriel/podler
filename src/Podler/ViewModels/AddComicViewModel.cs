using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Podler.Models;
using Podler.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Podler.ViewModels
{
    public class AddComicViewModel
    {
        public Comic Comic { get; set; }

        [Required(ErrorMessage = "O autor é obrigatório")]
        public List<string> SelectedAuthors { get; set; }

        [Required(ErrorMessage = "A categoria e obrigatória")]
        public List<string> SelectedCategories { get; set; }

        [Required(ErrorMessage = "O desenhista é obrigatório")]
        public List<string> SelectedDesigners { get; set; }

        [Required(ErrorMessage = "A editora e obrigatória")]
        public string SelectedPublishers { get; set; }

        [Required(ErrorMessage = "A imagem de capa e obrigatória")]
        public IFormFile CoverImage { get; set; }

        public IEnumerable<SelectListItem> Categories { get; private set; }
        public IEnumerable<SelectListItem> Authors { get; private set; }
        public IEnumerable<SelectListItem> Designers { get; private set; }
        public IEnumerable<SelectListItem> Publishers { get; private set; }

        public AddComicViewModel()
        {
            Comic = new Comic();
        }

        public AddComicViewModel(IEnumerable<Category> categories, IEnumerable<Author> authors, IEnumerable<Designer> designers, IEnumerable<Publisher> publishers) : this()
        {
            Categories = GetSelectListItem(categories);
            Authors = GetSelectListItem(authors);
            Designers = GetSelectListItem(designers);
            Publishers = GetSelectListItem(publishers);
        }

        private IEnumerable<SelectListItem> GetSelectListItem<T>(IEnumerable<T> list) where T : ISelectListItem
        {
            var selectListItem = new List<SelectListItem>();

            foreach (var item in list)
                selectListItem.Add(new SelectListItem(item.Name, item.Id.ToString()));

            return selectListItem;
        }
    }

}
