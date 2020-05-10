using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Podler.Models;
using Podler.Models.Interfaces;
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
        private const string RELATIVE_CATEGORIES_URI = "api/categories";
        private const string RELATIVE_AUTHORS_URI = "api/authors";
        private const string RELATIVE_DESIGNERS_URI = "api/designers";
        private const string RELATIVE_PUBLISHERS_URI = "api/publishers";

        private readonly Uri _categoriesUri;
        private readonly Uri _authorsUri;
        private readonly Uri _designersUri;
        private readonly Uri _publishersUri;

        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public PodlerApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

            var baseUri = new Uri(_configuration["DependencesServicesUrl:PodlerApi"]);

            _categoriesUri = new Uri(baseUri, RELATIVE_CATEGORIES_URI);
            _authorsUri = new Uri(baseUri, RELATIVE_AUTHORS_URI);
            _designersUri = new Uri(baseUri, RELATIVE_DESIGNERS_URI);
            _publishersUri = new Uri(baseUri, RELATIVE_PUBLISHERS_URI);
        }

        public async Task<List<T>> GetModelListAsync<T>(Uri uri) where T : BaseModel
        {
            var httpResponseMessage = await _httpClient.GetAsync(uri);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var json = await httpResponseMessage.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<List<T>>(json);

                return model;
            }

            return null;
        }

        public async Task<SelectAddResponse> PostSelectableItem<T>(T model, Uri uri, string successMessage, string existErrorMessage) where T : ISelectableItem
        {
            var json = JsonConvert.SerializeObject(model);

            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var httpResponseMessage = await _httpClient.PostAsync(uri, httpContent);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var modelDbJson = await httpResponseMessage.Content.ReadAsStringAsync();
                var modelDb = JsonConvert.DeserializeObject<T>(modelDbJson);

                return new SelectAddResponse(true, successMessage, modelDb);
            }
            if (httpResponseMessage.StatusCode == HttpStatusCode.Conflict)
                return new SelectAddResponse(false, existErrorMessage);
            else
                return new SelectAddResponse(false, "Error verifique os campos e tente novamente mais tarde.");
        }

        public async Task<SelectAddResponse> AddCategory(Category category)
        {
            return await PostSelectableItem(category,
                                            _categoriesUri,
                                            "Categoria adicionada com sucesso.",
                                            "Essa categoria ja existe.");
        }

        public async Task<SelectAddResponse> AddAuthor(Author author)
        {
            return await PostSelectableItem(author,
                                            _authorsUri,
                                            "Autor adicionado com sucesso.",
                                            "Esse author ja existe.");
        }

        public async Task<SelectAddResponse> AddDesigner(Designer designer)
        {
            return await PostSelectableItem(designer,
                                            _designersUri,
                                            "Desenhista adicionado com sucesso.",
                                            "Esse desenhista ja existe.");
        }

        public async Task<SelectAddResponse> AddPublisher(Publisher publisher)
        {
            return await PostSelectableItem(publisher,
                                            _publishersUri,
                                            "Editora adicionada com sucesso.",
                                            "Essa editora ja existe.");
        }

        public async Task<List<Author>> GetAuthorsAsync()
        {
            return await GetModelListAsync<Author>(_authorsUri);
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await GetModelListAsync<Category>(_categoriesUri);
        }

        public async Task<List<Designer>> GetDesignersAsync()
        {
            return await GetModelListAsync<Designer>(_designersUri);
        }

        public async Task<List<Publisher>> GetPublishersAsync()
        {
            return await GetModelListAsync<Publisher>(_publishersUri);
        }
    }

    public interface IPodlerApiService
    {
        Task<SelectAddResponse> AddAuthor(Author author);
        Task<SelectAddResponse> AddCategory(Category category);
        Task<SelectAddResponse> AddDesigner(Designer designer);
        Task<SelectAddResponse> AddPublisher(Publisher publisher);
        Task<List<Category>> GetCategoriesAsync();
        Task<List<Author>> GetAuthorsAsync();
        Task<List<Designer>> GetDesignersAsync();
        Task<List<Publisher>> GetPublishersAsync();
    }
}
