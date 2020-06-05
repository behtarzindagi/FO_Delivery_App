using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class GeoTagCategoryViewModel
    {
        public List<GeoTagCategoryModel> Category { get; set; }
    }
    public class GeoTagCategoryModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<GeoTagSubCategoryModel> SubCategory { get; set; }
    }
    public class GeoTagSubCategoryModel
    {
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
    }

    public class GeoTaggingDataViewModel
    {
        public List<GeoTaggingDataModel> GeoDataList { get; set; }
    }
    public class GeoTaggingDataModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lat { get; set; }
        public string Longitute { get; set; }
    }
    public class GeoTaggingDataWithVillageViewModel
    {
        public List<GeoTaggingDataWithVillageModel> GeoVillageList { get; set; }
    }
        public class GeoTaggingDataWithVillageModel
    {
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public int VillageId { get; set; }
        public string VillageName { get; set; }
    }
}
