using System.Collections.Generic;
using System.Threading.Tasks;
using IIHSApiApp.Models;

namespace IIHSApiApp.Services
{
    public class ApiService
    {
        ApiClientService client;

        public ApiService(string apiKey)
        {
            client = new ApiClientService(apiKey);
        }
        public async Task<List<Make>> GetMakes()
        {
            var url = $"ratings/all-makes";
            return await client.GetAsync<List<Make>>(url);
        }
        public async Task<List<Class>> GetClasses()
        {
            var url = $"ratings/all-classes";
            return await client.GetAsync<List<Class>>(url);
        }
        public async Task<List<MakesModels>> GetMakesModels(string slugClass)
        {
            var url = $"ratings/makemodels-for-class/{slugClass}";
            return await client.GetAsync<List<MakesModels>>(url);
        }
        public async Task<List<Year>> GetYears()
        {
            var url = $"ratings/modelyears";
            return await client.GetAsync<List<Year>>(url);
        }
        public async Task<List<Series>> GetSeries(string year, string makeSlug)
        {
            var url = $"ratings/series/{year}/{makeSlug}";
            return await client.GetAsync<List<Series>>(url);
        }
        public async Task<List<Series>> GetAllSeries(string makeSlug)
        {
            var url = $"ratings/all-series/{makeSlug}";
            return await client.GetAsync<List<Series>>(url);
        }
        public async Task<List<Year>> GetModelYearsForSeries(string makeSlug, string slugSeries)
        {
            var url = $"ratings/modelyears-for-series/{makeSlug}/{slugSeries}";
            return await client.GetAsync<List<Year>>(url);
        }
        public async Task<List<SeriesRatingsData>> GetSeriesRatings(string year, string makeSlug, string slugSeries)
        {
            var url = $"ratings/single/{year}/{makeSlug}/{slugSeries}";
            return await client.GetAsync<List<SeriesRatingsData>>(url);
        }
        public async Task<List<SeriesRatingsData>> GetClassSeriesRatings(string year, string makeSlug, string seriesSlug)
        {
            var url = $"ratings/single/{year}/{makeSlug}/{seriesSlug}";
            return await client.GetAsync<List<SeriesRatingsData>>(url);
        }
    }
}