using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.OpenAccess;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity
{
    public static class DynamicContentExtensions
    {
        /// <summary>
        /// Get a single image from a content link
        /// ** Sitefinitysteve.com Extension **
        /// </summary>
        /// <returns>Telerik.Sitefinity.Libraries.Model.Image object</returns>
        public static Image GetImage(this DynamicContent item, string fieldName){
            var contentLinks = (ContentLink[])item.GetValue(fieldName);
            ContentLink imageContentLink = contentLinks.FirstOrDefault();

            return (imageContentLink == null) ? null : imageContentLink.ChildItemId.GetImage();
        }

        /// <summary>
        /// Gets the images from a content link array
        /// ** Sitefinitysteve.com Extension **
        /// </summary>
        /// <returns>IQueryable Telerik.Sitefinity.Libraries.Model.Image</returns>
        public static IQueryable<Image> GetImages(this DynamicContent item, string fieldName)
        {
            var contentLinks = (ContentLink[])item.GetValue(fieldName);

            var images = from i in contentLinks
                         select i.ChildItemId.GetImage();
            
            return images.AsQueryable();
        }

        /// <summary>
        /// Get a single document from a content link
        /// ** Sitefinitysteve.com Extension **
        /// </summary>
        /// <returns>Telerik.Sitefinity.Libraries.Model.Image object</returns>
        public static Document GetDocument(this DynamicContent item, string fieldName){
            var contentLinks = (ContentLink[])item.GetValue(fieldName);
            ContentLink docContentLink = contentLinks.FirstOrDefault();

            return (docContentLink == null) ? null : docContentLink.ChildItemId.GetDocument();
        }

        /// <summary>
        /// Gets the documents from a content link array
        /// ** Sitefinitysteve.com Extension **
        /// </summary>
        /// <returns>IQueryable Telerik.Sitefinity.Libraries.Model.Image</returns>
        public static IQueryable<Document> GetDocuments(this DynamicContent item, string fieldName)
        {
            var contentLinks = (ContentLink[])item.GetValue(fieldName);

            var docs = from i in contentLinks
                         select i.ChildItemId.GetDocument();
            
            return docs.AsQueryable();
        }

        /// <summary>
        /// Get the Live Visible items
        /// ** Sitefinitysteve.com Extension **
        /// </summary>
        /// <returns>IQueryable<DynamicContent></returns>
        public static IQueryable<DynamicContent> Live(this IQueryable<DynamicContent> items){
            return items.Where(x => x.Status == GenericContent.Model.ContentLifecycleStatus.Live && x.Visible == true);
        }

        /// <summary>
        /// Get the Master Visible items
        /// ** Sitefinitysteve.com Extension **
        /// </summary>
        /// <returns>IQueryable<DynamicContent></returns>
        public static IQueryable<DynamicContent> Master(this IQueryable<DynamicContent> items)
        {
            return items.Where(x => x.Status == GenericContent.Model.ContentLifecycleStatus.Master);
        }

        /// <summary>
        /// Get the Temp Visible items
        /// ** Sitefinitysteve.com Extension **
        /// </summary>
        /// <returns>IQueryable<DynamicContent></returns>
        public static IQueryable<DynamicContent> Temp(this IQueryable<DynamicContent> items)
        {
            return items.Where(x => x.Status == GenericContent.Model.ContentLifecycleStatus.Temp);
        }

        /// <summary>
        /// Generic Taxon control, use GetCategories or GetTags for the defaults
        /// </summary>
        /// <returns>IQueryable<HierarchicalTaxon></returns>
        public static List<HierarchicalTaxon> GetHierarchicalTaxons(this DynamicContent item, string fieldName, string taxonomyName)
        {
            var categories = item.GetValue<TrackedList<Guid>>(fieldName);

            TaxonomyManager manager = TaxonomyManager.GetManager();

            var taxonomyParent = manager.GetTaxonomies<HierarchicalTaxonomy>().SingleOrDefault(x => x.Name == taxonomyName);
            
            var taxons = taxonomyParent.Taxa.Where(x => categories.Contains(x.Id)).Select(x => (HierarchicalTaxon)x);
            
            return taxons.ToList();
        }

        /// <summary>
        /// Generic Taxon control, use GetCategories or GetTags for the defaults
        /// </summary>
        /// <returns>IQueryable<HierarchicalTaxon></returns>
        public static List<Taxon> GetFlatTaxons(this DynamicContent item, string fieldName)
        {
            var categories = item.GetValue<TrackedList<Guid>>(fieldName);

            TaxonomyManager manager = TaxonomyManager.GetManager();

            var taxonomyParent = manager.GetTaxonomies<Taxonomy>().FirstOrDefault(x => x.Name == fieldName);

            var taxons = taxonomyParent.Taxa.Where(x => categories.Contains(x.Id)).Select(x => x);

            return taxons.ToList();
        }

        /// <summary>
        /// Get the linked Categories
        /// </summary>
        /// <returns>IQueryable<HierarchicalTaxon></returns>
        public static List<HierarchicalTaxon> GetCategories(this DynamicContent item)
        {
            var categories = item.GetValue<TrackedList<Guid>>("Category");

            TaxonomyManager manager = TaxonomyManager.GetManager();

            var taxonomyParent = manager.GetTaxonomy<HierarchicalTaxonomy>(TaxonomyManager.CategoriesTaxonomyId);

            var taxons = taxonomyParent.Taxa.Where(x => categories.Contains(x.Id)).Select(x => (HierarchicalTaxon)x);

            return taxons.ToList();
        }

        /// <summary>
        /// Get the linked Tags
        /// </summary>
        /// <returns>IQueryable<HierarchicalTaxon></returns>
        public static List<Taxon> GetTags(this DynamicContent item)
        {
            var tags = item.GetValue<TrackedList<Guid>>("Tags");

            TaxonomyManager manager = TaxonomyManager.GetManager();

            var taxonomyParent = manager.GetTaxonomy<Taxonomy>(TaxonomyManager.TagsTaxonomyId);
            var taxons = taxonomyParent.Taxa.Where(x => tags.Contains(x.Id)).ToList();

            return taxons;
        }

        /// <summary>
        /// Returns the linked DynamicContent objects
        /// ** Sitefinitysteve.com Extension **
        /// </summary>
        public static IQueryable<DynamicContent> GetRelatedContentItems(this DynamicContent dataItem, string fieldName, string type)
        {
            var contentLinks = dataItem.GetValue<Guid[]>(fieldName);
            
            var items = contentLinks.GetDynamicContentItems(type);
            
            return items;
        }

        /// <summary>
        /// Returns the linked DynamicContent object
        /// ** Sitefinitysteve.com Extension **
        /// </summary>
        public static DynamicContent GetRelatedContentItem(this DynamicContent dataItem, string fieldName, string type)
        {
            var contentLink = dataItem.GetValue<Guid>(fieldName);
            
            return contentLink.GetDynamicContent(type);
        }
        

        /// <summary>
        /// Returns the Child Items of the current DataItem
        /// ** Sitefinitysteve.com Extension **
        /// </summary>
        public static IQueryable<DynamicContent> GetChildren(this DynamicContent dataItem, Type type = null)
        {
            var manager = DynamicModuleManager.GetManager();
            return manager.GetChildItems(dataItem, type);
        }

        /// <summary>
        /// Returns the Child Items of the current DataItems Parent
        /// ** Sitefinitysteve.com Extension **
        /// </summary>
        public static IQueryable<DynamicContent> GetChildrenFromParent(this DynamicContent dataItem, Type type = null)
        {
            var manager = DynamicModuleManager.GetManager();
            return manager.GetChildItems(dataItem.SystemParentItem, type);
        }
    }
}