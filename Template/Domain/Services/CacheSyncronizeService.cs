namespace Domain
{
    using System;
    using System.Threading.Tasks;
    using Olive;
    using Olive.Entities;
    using Olive.Entities.Data;

    /// <summary>
    /// Provides synchronization service to enable server farm or multi-process caching.
    /// </summary>
    public class CacheSyncronizeService
    {
        static IDatabase Database => Context.Current.Database();

        static CacheSyncronizeService()
        {
            // Watch every change in the database, and increase the cache version accordingly.
            Context.Current.Database().Updated += 
                ev => ev.Do(() => IncreaseCacheVersion(ev.Args?.GetType()));
        }

        static async Task IncreaseCacheVersion(Type modifiedObjectType)
        {
            if (modifiedObjectType == null) return;
            if (modifiedObjectType == typeof(Settings)) return;
            if (!modifiedObjectType.IsCacheable()) return;

            // Note: If the updated version in the database was the same as my in-memory version, the job will be done.
            // But if another instance has already in the meantime increased the version, then my cache version will be behind the database's.
            // In which case my next call to ValidateCache() will bring this instance up to date.

            // Increment the in-memory version to avoid unnecessary refresh in this instance.
            Settings.Current.CacheVersion++;

            // Ensure the database is universally incremented, even if my version was already stale.
            await Database.GetAccess<Settings>().ExecuteNonQuery("UPDATE Settings set CacheVersion = CacheVersion + 1");
        }

        /// <summary>
        /// This will ensure that the cache is valid and synchronised across multiple servers / processes.
        /// To enable this, add an Automated Task to call this every 100ms.
        /// Also it is recommended to disable caching for specific entities that change (insert, update or delete) frequently.
        /// </summary>
        public static async Task ValidateCache()
        {
            var cacheVersion = await Database.GetAccess<Settings>()
                .ExecuteScalar<int>("SELECT TOP 1 CacheVersion FROM Settings");

            if (cacheVersion != Settings.Current.CacheVersion)
            {
                // A change is made by another process / server.
                await Database.Refresh();
            }
        }
    }
}