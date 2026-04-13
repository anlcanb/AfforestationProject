using System.Collections.Generic;
using System.Threading.Tasks;
using Afforestation.WebUI.Models.ViewModels;

namespace Afforestation.WebUI.Services
{
    public interface ISiteService
    {
        Task<IEnumerable<SiteListItemViewModel>> GetAllAsync();
        Task<SiteDetailViewModel?> GetByIdAsync(int id);
        Task<bool> CreateAsync(SiteEditViewModel model);
        Task<bool> UpdateAsync(int id, SiteEditViewModel model);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<ObservationViewModel>> GetObservationsAsync(int siteId);
        Task<IEnumerable<SiteMapDataViewModel>> GetMapDataAsync();
    }
}