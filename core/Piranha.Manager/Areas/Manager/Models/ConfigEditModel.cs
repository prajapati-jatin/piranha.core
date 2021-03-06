/*
 * Copyright (c) 2017 Håkan Edling
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 * 
 * https://github.com/piranhacms/piranha.core
 * 
 */

namespace Piranha.Areas.Manager.Models
{
    public class ConfigEditModel
    {
        /// <summary>
        /// Inner class for cache configuration settings.
        /// </summary>
        public class CacheConfig
        {
            /// <summary>
            /// Gets/sets the cache expiration for media.
            /// </summary>
            public int MediaExpires { get; set; }

            /// <summary>
            /// Gets/sets the cache expiration for pages.
            /// </summary>
            public int PagesExpires { get; set; }
        }

        /// <summary>
        /// Inner class for general configuration settings.
        /// </summary>
        public class GeneralConfig
        {
            /// <summary>
            /// Gets/sets if hierarchical slugs should be used.
            /// </summary>
            public bool HierarchicalPageSlugs { get; set; }

            /// <summary>
            /// Gets/sets the default expanded levels in the manager sitemap.
            /// </summary>
            public int ExpandedSitemapLevels { get; set; }
        }

        /// <summary>
        /// Gets/sets the cache config.
        /// </summary>
        public CacheConfig Cache { get; set; }

        /// <summary>
        /// Gets/sets the general config.
        /// </summary>
        public GeneralConfig General { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ConfigEditModel() {
            Cache = new CacheConfig();
            General = new GeneralConfig();
        }

        /// <summary>
        /// Gets the cache edit model.
        /// </summary>
        /// <param name="api">The current api</param>
        /// <returns>The model</returns>
        public static ConfigEditModel Get(Api api) {
            var model = new ConfigEditModel();

            using (var config = new Config(api)) {
                model.Cache.MediaExpires = config.CacheExpiresMedia;
                model.Cache.PagesExpires = config.CacheExpiresPages;

                model.General.HierarchicalPageSlugs = config.HierarchicalPageSlugs;
                model.General.ExpandedSitemapLevels = config.ManagerExpandedSitemapLevels;
            }
            return model;
        }

        /// <summary>
        /// Saves the current config model.
        /// </summary>
        /// <param name="api">The current api</param>
        public void Save(Api api) {
            using (var config = new Config(api)) {
                config.CacheExpiresMedia = Cache.MediaExpires;
                config.CacheExpiresPages = Cache.PagesExpires;
                config.HierarchicalPageSlugs = General.HierarchicalPageSlugs;
                config.ManagerExpandedSitemapLevels = General.ExpandedSitemapLevels;
            }
        }
    }
}