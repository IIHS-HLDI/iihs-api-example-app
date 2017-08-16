using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using PCLStorage;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IIHSApiApp.Framework
{
    public class ApiCache
    {

        private static ApiCache current;
        public static ApiCache Current
        {
            get
            {
                if (current == null)
                {
                    current = new ApiCache();
                    return current;
                }
                return current;
            }
            set
            {
                current = value;
            }
        }

        CacheDb db;
        TimeSpan ExpirationTime;

        public ApiCache()
        {
            var task = Init(new TimeSpan(8,0,0));
            //task.Wait();
        }

        public ApiCache(TimeSpan expirationTime)
        {
            var task = Init(expirationTime);
            //task.Wait();
        }

        private async Task Init(TimeSpan expirationTime)
        {
            this.db = new CacheDb();
            this.ExpirationTime = expirationTime;
            await this.db.ProvisionTables();
        }

        public async Task<T> GetAsync<T>(string key) 
        {
            var everything = (await db.QueryAsync<LocalCache>("select * from LocalCache")).ToList();
            var localCache = await db.QueryAsync<LocalCache>("select * from LocalCache where Key = ? and Expiration > ?", key, DateTime.Now);

            if (localCache.Any())
            {
                var cache = localCache.First();
                return JsonConvert.DeserializeObject<T>(cache.Value);
            }
            else
            {
                return default(T);
            }
        }

        public async Task<T> SetAsync<T>(string key, T value)
        {
            var json = JsonConvert.SerializeObject(value);
            var cacheItem = new LocalCache {  Expiration = DateTime.Now.Add(this.ExpirationTime), Key = key, Value = json};

            var existingItem = await GetAsync<T>(key);

            if (existingItem != null)
                await this.db.DeleteAsync(existingItem);

            await this.db.InsertAsync(cacheItem);
            return value;
        }

        public async Task ClearAll()
        {
            await db.ExecuteAsync("delete from LocalCache");
        }

        public async Task CleanUpExpiredItems()
        {
            await db.ExecuteAsync("delete form LocalCache where Expiration < ?", DateTime.Now);
        }
    }

    public class LocalCache
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public string Key { get; set; }
        public DateTime Expiration { get; set; }
        public string Value { get; set; }
    }

    public class CacheDb:SQLiteAsyncConnection
    {
        public CacheDb(bool storeDateTimeAsTicks = false) : base(CreateDatabase(), storeDateTimeAsTicks)
        {            
        }
        
        public async Task ProvisionTables()
        {
            await this.CreateTableAsync<LocalCache>();
        }

        public static string CreateDatabase()
        {
            var dbFileName = "iihs-api-app.db";            
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            var databaseName = PortablePath.Combine(folder, dbFileName);

            var localStorage = FileSystem.Current.LocalStorage;

            var fileExistsTask = localStorage.CheckExistsAsync(databaseName);
            fileExistsTask.Wait();
            if (fileExistsTask.Result == ExistenceCheckResult.FileExists)
                return databaseName;
            else
            {
                var result = localStorage.CreateFileAsync(databaseName, CreationCollisionOption.FailIfExists);
                result.Wait();
                return databaseName;
            }
        }
    }

}