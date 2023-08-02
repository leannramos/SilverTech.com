using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Common.Classes
{
    public abstract class DynamicContentService<T> where T : new()
    {
        public string ProviderName { get; set; }
        private string _transactionName;
        protected DynamicModuleManager _manager;
        private Type _contentType;

        public DynamicContentService(string type, string providerName = "", bool AutoInitialize = true)
        {
            try
            {
                _contentType = TypeResolutionService.ResolveType(type);
            }
            catch (Exception e)
            {
                Telerik.Sitefinity.Abstractions.Log.Write(e.Message);
            }
            if (AutoInitialize)
            {
                Initialize();
            }
        }

        public void Initialize()
        {
            try
            {
                _transactionName = (Guid.NewGuid()).ToString();
                _manager = DynamicModuleManager.GetManager(ProviderName, _transactionName);
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message);
            }
        }

        protected abstract T BuildModel(DynamicContent item);
        public T GetItem(Guid id)
        {
            T model = default(T);

            try
            {
                var item = GetDynamicContentItem(id);
                model = BuildModel(item);
            }
            catch (Exception e)
            {
                Log.Write(e.Message);
            }

            return model;
        }

        public List<T> GetItems(ContentLifecycleStatus lifecycleStatus = ContentLifecycleStatus.Live, bool visible = true)
        {
            List<T> models = new List<T>();
            try
            {
                var items = GetDynamicContentItems(lifecycleStatus, visible);
                foreach(var item in items)
                {
                    models.Add(BuildModel(item));
                }
            }
            catch (Exception e)
            {
                Log.Write(e.Message);
            }
            return models;
        }
        public DynamicContent GetDynamicContentItem(Guid id)
        {
            if (id == null || id == Guid.Empty)
            {
                return null;
            }

            try
            {
                DynamicContent item = _manager.GetDataItem(_contentType, id);
                if (item == null)
                {
                    item = _manager.GetDataItems(_contentType).FirstOrDefault(x => x.Id == id);
                }
                return item;
            }
            catch (Telerik.Sitefinity.SitefinityExceptions.ItemNotFoundException e)
            {
                Log.Write(e.Message);
            }

            return null;
        }


        public IEnumerable<DynamicContent> GetDynamicContentItems(ContentLifecycleStatus lifecycleStatus = ContentLifecycleStatus.Live, bool visible = true)
        {
            List<T> models = new List<T>();

            try
            {
                var items = _manager.GetDataItems(_contentType);
                items = items.Where(i => i.Status == lifecycleStatus && i.Visible == visible);
                return items;

            }
            catch (Exception e)
            {
                Log.Write(e.Message);
            }

            return null;
        }

        public List<T> GetSiblings(Guid parentId, ContentLifecycleStatus lifecycleStatus = ContentLifecycleStatus.Live, bool visible = true)
        {
            var modelList = new List<T>();

            try
            {
                var items = GetDynamicContentSiblings(parentId, lifecycleStatus, visible);
                foreach (var item in items)
                {
                    var model = BuildModel(item);
                    modelList.Add(model);
                }

                return modelList;
            }
            catch (Exception e)
            {
                Log.Write(e.Message);
            }

            return modelList;
        }

        public List<DynamicContent> GetDynamicContentSiblings(Guid parentId, ContentLifecycleStatus lifecycleStatus = ContentLifecycleStatus.Live, bool visible = true)
        {
            try
            {
                var items = _manager.GetDataItems(_contentType).Where(x => x.SystemParentId == parentId);

                items = items.Where(i => i.Status == lifecycleStatus && i.Visible == visible);

                return items.ToList();
            }
            catch (Exception e)
            {
                Log.Write(e.Message);
            }

            return new List<DynamicContent>();

        }

        public List<T> GetChildren(DynamicContent parentItem, ContentLifecycleStatus lifecycleStatus = ContentLifecycleStatus.Live, bool visible = true)
        {
            var modelList = new List<T>();
            try
            {
                var items = _manager.GetChildItems(parentItem, _contentType);
                items = items.Where(i => i.Status == lifecycleStatus && i.Visible == visible);

                foreach (var item in items)
                {
                    var model = BuildModel(item);
                    modelList.Add(model);
                }
            }
            catch (Exception e)
            {
                Log.Write(e.Message);
            }
            return modelList;
        }

        public T GetItem(Expression<Func<DynamicContent, bool>> predicate, ContentLifecycleStatus lifecycleStatus = ContentLifecycleStatus.Live)
        {
            try
            {
                var items = _manager?.GetDataItems(_contentType).Where(predicate) ?? null;
                items = items?.Where(i => i.Status == lifecycleStatus) ?? null;

                var item = items?.FirstOrDefault() ?? null;

                if (item == null)
                {
                    return default(T);
                }

                var model = BuildModel(item);

                return model;
            }
            catch (Exception e)
            {
                Log.Write(e.Message);
            }
            return default(T);
        }

        /// <summary>
        /// Used to get a list of dynamic content items, calls abstract Build Model to map dynamic content items to T
        /// </summary>
        /// <param name="predicate">Where condition for the query for the dynamic content items</param>
        /// <param name="lifecycleStatus">Defaults to Live to only grab published content</param>
        /// <returns>A list of the dynamic content objects, mapped to T</returns>
        public List<T> GetList(Expression<Func<DynamicContent, bool>> predicate, int Take = 0, ContentLifecycleStatus lifecycleStatus = ContentLifecycleStatus.Live)
        {
            var modelList = new List<T>();
            try
            {
                var items = _manager.GetDataItems(_contentType).Where(predicate);
                items = items.Where(i => i.Status == lifecycleStatus);
                if(Take > 0)
                {
                    items = items.Take(Take);
                }
                foreach (var item in items)
                {
                    var model = BuildModel(item);
                    modelList.Add(model);
                }
            }
            catch (Exception e)
            {
                Log.Write(e.Message);
            }

            return modelList;
        }
    }
}
