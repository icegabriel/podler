using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Podler.Models;
using Podler.Models.Interfaces;
using Podler.Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;

namespace Podler.ViewModels
{
    public class AddComicViewModel
    {
        public ComicUpload Comic { get; set; }

        [Required(ErrorMessage = "A imagem de capa e obrigatória")]
        public IFormFile Cover { get; set; }

        public IEnumerable<SelectListItem> Categories { get; private set; }
        public IEnumerable<SelectListItem> Authors { get; private set; }
        public IEnumerable<SelectListItem> Designers { get; private set; }
        public IEnumerable<SelectListItem> Publishers { get; private set; }

        public AddComicViewModel()
        {
            Comic = new ComicUpload();
        }

        public AddComicViewModel(IEnumerable<CategoryApi> categories,
                                 IEnumerable<AuthorApi> authors,
                                 IEnumerable<DesignerApi> designers,
                                 IEnumerable<PublisherApi> publishers) : this()
        {
            Categories = GetSelectListItem(categories);
            Authors = GetSelectListItem(authors);
            Designers = GetSelectListItem(designers);
            Publishers = GetSelectListItem(publishers);
        }

        private IEnumerable<SelectListItem> GetSelectListItem<T>(IEnumerable<T> list) where T : ISelectableItem
        {
            var selectListItem = new List<SelectListItem>();

            foreach (var item in list)
                selectListItem.Add(new SelectListItem(item.Name, item.Id.ToString()));

            return selectListItem;
        }

        public void SetComicUploadCover() => Comic.Cover = ConvertToBytes(Cover);

        private byte[] ConvertToBytes(IFormFile image)
        {
            if (image == null)
            {
                return null;
            }

            using (var inputStream = image.OpenReadStream())
            using (var stream = new MemoryStream())
            {
                inputStream.CopyTo(stream);
                return stream.ToArray();
            }
        }
    }

}
