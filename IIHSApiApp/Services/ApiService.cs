using System.Collections.Generic;
using System.Threading.Tasks;
using IIHSApiApp.Models;

namespace IIHSApiApp.Services
{
    /// <summary>
    /// Exposes many of the service methods that are available in the api.iihs.org v4 api web service.
    /// </summary>
    public class ApiService
    {
        ApiClientService client;

        public ApiService(string apiKey)
        {
            client = new ApiClientService(apiKey);
        }

        /// <summary>
        /// Gets all makes that have ratings.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Make>> GetMakes()
        {
            var url = $"ratings/all-makes";
            return await client.GetAsync<List<Make>>(url);
        }

        /// <summary>
        /// Gets all classes that have ratings.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Class>> GetClasses()
        {
            var url = $"ratings/all-classes";
            return await client.GetAsync<List<Class>>(url);
        }

        /// <summary>
        /// Gets all models associated with the provided class.
        /// </summary>
        /// <param name="slugClass">Class slug provided via the GetClasses service call</param>
        /// <returns></returns>
        public async Task<List<MakesModels>> GetMakesModels(string slugClass)
        {
            var url = $"ratings/makemodels-for-class/{slugClass}";
            return await client.GetAsync<List<MakesModels>>(url);
        }

        /// <summary>
        /// Gets all the series for a given make.
        /// </summary>
        /// <param name="makeSlug">Make slug can be retrieved via GetMakes service call</param>
        /// <returns></returns>
        public async Task<List<Series>> GetAllSeries(string makeSlug)
        {
            var url = $"ratings/all-series/{makeSlug}";
            return await client.GetAsync<List<Series>>(url);
        }

        /// <summary>
        /// Gets all the available model years for a given make and series.
        /// </summary>
        /// <param name="makeSlug"></param>
        /// <param name="slugSeries"></param>
        /// <returns></returns>
        public async Task<List<Year>> GetModelYearsForSeries(string makeSlug, string slugSeries)
        {
            var url = $"ratings/modelyears-for-series/{makeSlug}/{slugSeries}";
            return await client.GetAsync<List<Year>>(url);
        }

        /// <summary>
        /// Gets all the ratings available for a given year, make, series.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="makeSlug"></param>
        /// <param name="slugSeries"></param>
        /// <returns></returns>
        public async Task<List<SeriesRatingsData>> GetSeriesRatings(string year, string makeSlug, string slugSeries)
        {
            var url = $"ratings/single/{year}/{makeSlug}/{slugSeries}";
            return await client.GetAsync<List<SeriesRatingsData>>(url);
        }
    }
}