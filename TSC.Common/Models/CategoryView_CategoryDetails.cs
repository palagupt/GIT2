using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSC.Common.Models
{
    public class CategoryView_CategoryDetails
    {
        [JsonProperty("recordSetComplete")]
        public string RecordSetComplete { get; set; }

        [JsonProperty("recordSetCount")]
        public string RecordSetCount { get; set; }

        [JsonProperty("recordSetStartNumber")]
        public string RecordSetStartNumber { get; set; }

        [JsonProperty("recordSetTotal")]
        public string RecordSetTotal { get; set; }

        [JsonProperty("resourceId")]
        public string ResourceId { get; set; }

        [JsonProperty("resourceName")]
        public string ResourceName { get; set; }

        [JsonProperty("CatalogGroupView")]
        public List<CatalogGroupView> CatalogGroupViewProperty { get; set; }

        //[JsonProperty("MetaData")]
        //public List<MetaData> MetaDataProperty { get; set; }



        //public class MetaData
        //{
        //    [JsonProperty("metaData")]
        //    public string MetaDataProperty { get; set; }

        //    [JsonProperty("metaKey")]
        //    public string MetaKey { get; set; }
        //}

        public class CatalogGroupView
        {

            [JsonProperty("shortDescription")]
            public string ShortDescription { get; set; }

            [JsonProperty("resourceId")]
            public string ResourceId { get; set; }
            [JsonProperty("identifier")]
            public string Identifier { get; set; }
            [JsonProperty("parentCatalogGroupID")]
            public List<string> ParentCatalogGroupID { get; set; }
            [JsonProperty("name")]
            public string Name { get; set; }
            [JsonProperty("uniqueID")]
            public string UniqueID { get; set; }
            [JsonProperty("storeID")]
            public string StoreID { get; set; }
            [JsonProperty("thumbnail")]
            public string Thumbnail { get; set; }
            [JsonProperty("childCatalogGroupID")]
            public List<string> ChildCatalogGroupID { get; set; }

            //[JsonProperty("fullImage")]
            //public string FullImage { get; set; }
            //[JsonProperty("fullImageAltDescription")]
            //public string FullImageAltDescription { get; set; }
            //[JsonProperty("identifier")]
            //public string Identifier { get; set; }
            //[JsonProperty("metaDescription")]
            //public string MetaDescription { get; set; }
            //[JsonProperty("metaKeyword")]
            //public string MetaKeyword { get; set; }
            //[JsonProperty("name")]
            //public string Name { get; set; }
            //[JsonProperty("productsURL")]
            //public string ProductsURL { get; set; }
            //[JsonProperty("resourceId")]
            //public string ResourceId { get; set; }
            //[JsonProperty("shortDescription")]
            //public string ShortDescription { get; set; }
            //[JsonProperty("thumbnail")]
            //public string Thumbnail { get; set; }
            //[JsonProperty("title")]
            //public string Title { get; set; }
            //[JsonProperty("uniqueID")]
            //public string UniqueID { get; set; }

            //public List<string> parentCatalogGroupID { get; set; }


        }

    }


}
