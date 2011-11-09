using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace JiraRestClient
{
    /// <summary>
    /// Base for classes that are readon-only wrappers for JSON data.
    /// </summary>
    abstract public class JsonWrapper
    {
        #region Delegates

        /// <summary>
        /// Delegate for Get() to create the object being requested if it doesn't exist in the cache.
        /// </summary>
        /// <typeparam name="T">The type of data to create</typeparam>
        /// <returns>The newly created object</returns>
        protected delegate T CreatorDelegate<T>();

        #endregion

        #region Protected Properties

        /// <summary>
        /// The underlying JSON data
        /// </summary>
        protected JObject JObject { get; private set; }

        #endregion

        #region Private Fields

        /// <summary>
        /// Internal cache for avoiding re-looking up in the JSON tree.
        /// </summary>
        private readonly Dictionary<string, object> _cache = new Dictionary<string, object>();

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for derived classes.
        /// </summary>
        /// <param name="jObject">Underlying JSON data</param>
        protected JsonWrapper(JObject jObject)
        {
            JObject = jObject;
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Try to follow a path in the JSON data tree and return the token found if successful.
        /// </summary>
        /// <param name="token">The token found at the specified path</param>
        /// <param name="paths">A list of paths (node names) to traverse in the JSON data tree</param>
        /// <returns>True if the path is found (token will be not null)</returns>
        protected bool TryGetPath(out JToken token, params object[] paths)
        {
            token = JObject;

            foreach (object path in paths)
            {
                if (token == null) return false;

                token = token[path];
            }

            return token != null;
        }

        /// <summary>
        /// Get the item at the specified path, giving it the specified key
        /// </summary>
        /// <typeparam name="T">Data type that the item will be converted to</typeparam>
        /// <param name="key">Key for this item, for the cache</param>
        /// <param name="paths">Paths to traverse to get to the item</param>
        /// <returns>The item or default(T) if not found</returns>
        protected T Get<T>(string key, params object[] paths)
        {
            T item;
            if (TryGetCached(key, out item))
            {
                return item;
            }
            JToken token;
            item = TryGetPath(out token, paths) ? token.Value<T>() : default(T);
            return AddCache(key, item);
        }

        /// <summary>
        /// Same as Get() but path points to an array of items that get returned.
        /// </summary>
        /// <typeparam name="T">Data type of the array elements</typeparam>
        /// <param name="key">Key for this array, for the cache</param>
        /// <param name="paths">Paths to traverse to get to the array</param>
        /// <returns>The array of items found (or an empty list if not found)</returns>
        protected IEnumerable<T> GetList<T>(string key, params object[] paths)
        {
            IEnumerable<T> item;
            if (TryGetCached(key, out item))
            {
                return item;
            }
            JToken token;
            item = TryGetPath(out token, paths)
                ? token.Children().Values<T>()
                : new List<T>();
            return AddCache(key, item);
        }

        /// <summary>
        /// A helper method for the derived class to allow if to use the internal cache for items it may need.
        /// </summary>
        /// <typeparam name="T">The data type of the item</typeparam>
        /// <param name="key">The key in the cache</param>
        /// <param name="creator">The delegate that gets called to create the item if it does not exist in the cache</param>
        /// <returns>The item from the cache (or newly created)</returns>
        protected T Get<T>(string key, CreatorDelegate<T> creator)
        {
            T item;
            if (TryGetCached(key, out item))
            {
                return item;
            }
            return AddCache(key, creator());
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Add the specified item to the internal cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="item"></param>
        /// <returns>Returns the same item for simplyfying calling code</returns>
        private T AddCache<T>(string key, T item)
        {
            _cache[key] = item;
            return item;
        }

        /// <summary>
        /// Try to get the item specified from the cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="item"></param>
        /// <returns>True if found (item will be set)</returns>
        private bool TryGetCached<T>(string key, out T item) 
        {
            object itemObject;
            bool got = _cache.TryGetValue(key, out itemObject);
            item = got ? (T)itemObject : default(T);
            return got;
        }

        #endregion

    }
}
