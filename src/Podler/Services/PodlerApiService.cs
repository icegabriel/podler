using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Podler.Models;
using Podler.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Podler.Services
{
    public class PodlerApiService : IPodlerApiService
    {
        private const string RELATIVE_CATEGORY_URI = "api/categories";

        private readonly Uri _categoryUri;

        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public PodlerApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

            var baseCategoryUri = new Uri(_configuration["DependencesServicesUrl:PodlerApi"]);
            _categoryUri = new Uri(baseCategoryUri, RELATIVE_CATEGORY_URI);
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            var httpResponseMessage = await _httpClient.GetAsync(_categoryUri);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var json = await httpResponseMessage.Content.ReadAsStringAsync();
                var categories = JsonConvert.DeserializeObject<List<Category>>(json);

                return categories;
            }

            return null;
        }

        public async Task<AddCategoryResponse> AddCategory(Category category)
        {
            var json = JsonConvert.SerializeObject(category);

            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var httpResponseMessage = await _httpClient.PostAsync(_categoryUri, httpContent);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var categoryDbJson = await httpResponseMessage.Content.ReadAsStringAsync();
                var categoryDb = JsonConvert.DeserializeObject<Category>(categoryDbJson);

                return new AddCategoryResponse(true, "Categoria adicionada com sucesso.", categoryDb);
            }
            if (httpResponseMessage.StatusCode == HttpStatusCode.Conflict)
                return new AddCategoryResponse(false, "Essa categoria ja existe.");
            else
                return new AddCategoryResponse(false, "Error verifique os campos e tente novamente mais tarde.");
        }
    }

    public interface IPodlerApiService
    {
        Task<AddCategoryResponse> AddCategory(Category category);
        Task<List<Category>> GetCategoriesAsync();
    }
}
