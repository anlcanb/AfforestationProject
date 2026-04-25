using Afforestation.WebUI.Models.ViewModels;
using System.Net.Http.Json;

namespace Afforestation.WebUI.Services
{
    public class SiteService : ISiteService
    {
        private readonly HttpClient _http;

        public SiteService(HttpClient http)
        {
            _http = http;
        }

        public async Task<IEnumerable<SiteListItemViewModel>> GetAllAsync()
        {
            var resp = await _http.GetFromJsonAsync<IEnumerable<SiteListItemViewModel>>("/api/sites");
            return resp ?? Enumerable.Empty<SiteListItemViewModel>();
        }

        public async Task<SiteDetailViewModel?> GetByIdAsync(int id)
        {
            var resp = await _http.GetFromJsonAsync<SiteDetailViewModel>($"/api/sites/{id}");
            return resp;
        }

        public async Task<bool> CreateAsync(SiteEditViewModel model)
        {
            var resp = await _http.PostAsJsonAsync("/api/sites", model);
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(int id, SiteEditViewModel model)
        {
            var resp = await _http.PutAsJsonAsync($"/api/sites/{id}", model);
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var resp = await _http.DeleteAsync($"/api/sites/{id}");
            return resp.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<ObservationViewModel>> GetObservationsAsync(int siteId)
        {
            var resp = await _http.GetFromJsonAsync<IEnumerable<ObservationViewModel>>($"/api/sites/{siteId}/observations");
            return resp ?? Enumerable.Empty<ObservationViewModel>();
        }

        public async Task<IEnumerable<SiteMapDataViewModel>> GetMapDataAsync()
        {
            var resp = await _http.GetFromJsonAsync<IEnumerable<SiteMapDataViewModel>>("/api/sites/map-data");
            return resp ?? Enumerable.Empty<SiteMapDataViewModel>();
        }

        public async Task<bool> CreateObservationAsync(ObservationViewModel model)
        {
            var resp = await _http.PostAsJsonAsync("/api/observations", model);
            return resp.IsSuccessStatusCode;
        }
    }
}