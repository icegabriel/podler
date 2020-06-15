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
        private const string RELATIVE_COMICS_URI = "api/comics";

        private readonly Uri _categoriesUri;
        private readonly Uri _authorsUri;
        private readonly Uri _designersUri;
        private readonly Uri _publishersUri;
        private readonly Uri _comicsUri;

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
            _comicsUri = new Uri(baseUri, RELATIVE_COMICS_URI);
        }

        private async Task<List<T>> GetAllModelsAsync<T>(Uri uri) where T : BaseModel
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

        private async Task<T> GetModelAsync<T>(Uri uri) where T : BaseModel
        {
            var httpResponseMessage = await _httpClient.GetAsync(uri);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var json = await httpResponseMessage.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<T>(json);

                return model;
            }

            return null;
        }

        private async Task<SelectAddResponse> PostSelectableItem<T>(T model, Uri uri, string successMessage) where T : ISelectableItem
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
            else
                return new SelectAddResponse(false, await httpResponseMessage.Content.ReadAsStringAsync());
        }

        public async Task<AddComicResponse> PostComic(ComicUpload comic)
        {
            var json = JsonConvert.SerializeObject(comic);

            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var httpResponseMessage = await _httpClient.PostAsync(_comicsUri, httpContent);

            if (httpResponseMessage.IsSuccessStatusCode)
                return new AddComicResponse(true, "Quadrinho adicionado com sucesso.");

            else
                return new AddComicResponse(false, await httpResponseMessage.Content.ReadAsStringAsync());
        }


        public async Task<SelectAddResponse> AddCategoryAsync(Category category) =>
            await PostSelectableItem(category, _categoriesUri, "Categoria adicionada com sucesso.");

        public async Task<SelectAddResponse> AddAuthorAsync(Author author) =>
            await PostSelectableItem(author, _authorsUri, "Autor adicionado com sucesso.");

        public async Task<SelectAddResponse> AddDesignerAsync(Designer designer) =>
            await PostSelectableItem(designer, _designersUri, "Desenhista adicionado com sucesso.");

        public async Task<SelectAddResponse> AddPublisherAsync(Publisher publisher) =>
            await PostSelectableItem(publisher,_publishersUri, "Editora adicionada com sucesso.");


        public async Task<List<AuthorApi>> GetAuthorsAsync() =>
            await GetAllModelsAsync<AuthorApi>(_authorsUri);

        public async Task<List<CategoryApi>> GetCategoriesAsync() =>
            await GetAllModelsAsync<CategoryApi>(_categoriesUri);

        public async Task<List<DesignerApi>> GetDesignersAsync() =>
            await GetAllModelsAsync<DesignerApi>(_designersUri);

        public async Task<List<PublisherApi>> GetPublishersAsync() =>
            await GetAllModelsAsync<PublisherApi>(_publishersUri);


        public async Task<AuthorApi> GetAuthorAsync(int id)
        {
            var authorUri = new Uri(_authorsUri.ToString() + $"/{id}");

            return await GetModelAsync<AuthorApi>(authorUri);
        }

        public async Task<CategoryApi> GetCategoryAsync(int id)
        {
            var categoryUri = new Uri(_categoriesUri.ToString() + $"/{id}");

            return await GetModelAsync<CategoryApi>(categoryUri);
        }

        public async Task<DesignerApi> GetDesignerAsync(int id)
        {
            var designerUri = new Uri(_designersUri.ToString() + $"/{id}");

            return await GetModelAsync<DesignerApi>(designerUri);
        }

        public async Task<PublisherApi> GetPublisherAsync(int id)
        {
            var publisherUri = new Uri(_publishersUri.ToString() + $"/{id}");

            return await GetModelAsync<PublisherApi>(publisherUri);
        }

        public async Task<ComicApi> GetComicAsync(int id)
        {
            var comicUri = new Uri(_comicsUri.ToString() + $"/{id}");

            return await GetModelAsync<ComicApi>(comicUri);
        }
    }

    public interface IPodlerApiService
    {
        Task<SelectAddResponse> AddAuthorAsync(Author author);
        Task<SelectAddResponse> AddCategoryAsync(Category category);
        Task<SelectAddResponse> AddDesignerAsync(Designer designer);
        Task<SelectAddResponse> AddPublisherAsync(Publisher publisher);

        Task<List<CategoryApi>> GetCategoriesAsync();
        Task<List<AuthorApi>> GetAuthorsAsync();
        Task<List<DesignerApi>> GetDesignersAsync();
        Task<List<PublisherApi>> GetPublishersAsync();

        Task<ComicApi> GetComicAsync(int id);
        Task<AuthorApi> GetAuthorAsync(int id);
        Task<CategoryApi> GetCategoryAsync(int id);
        Task<DesignerApi> GetDesignerAsync(int id);
        Task<PublisherApi> GetPublisherAsync(int id);

        Task<AddComicResponse> PostComic(ComicUpload comic);
    }
}
