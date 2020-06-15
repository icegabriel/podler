using Podler.Models;
using System;
using System.Collections.Generic;

namespace Podler.ViewModels
{
    public class HomeViewModel
    {
        public Uri BaseApiUri { get; }
        public List<CategoryApi> Categories { get; set; }

        public HomeViewModel(string baseApiUrl, List<CategoryApi> categories)
        {
            BaseApiUri = new Uri(baseApiUrl);
            Categories = categories;
        }
    }
}
