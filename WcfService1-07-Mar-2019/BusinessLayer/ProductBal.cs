using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.Data;
using DataLayer;

namespace BusinessLayer
{
    public class ProductBal
    {
        ProductDal _prodDal;
        public ProductBal()
        {
            _prodDal = new ProductDal();
        }
        public Prod_ProductViewModel GetProduct_Master()
        {
            DataSet DS = _prodDal.GetProduct_Master();
            Prod_ProductViewModel _viewModel = new Prod_ProductViewModel();

            if (DS != null && DS.Tables[0] != null)
            {
                if (DS.Tables[0].Rows.Count > 0)
                {
                    // List<Prod_ProductModel> _listModel = new List<Prod_ProductModel>();

                    var _listModel = (from p in DS.Tables[0].AsEnumerable()
                                      select new Prod_ProductModel
                                      {
                                          ProductId = p.Field<int>("ProductID"),
                                          ProductName = p.Field<string>("ProductName"),
                                          CategoryName = p.Field<string>("CategoryName"),
                                          TechnicalName = p.Field<string>("TechnicalName_Name"),
                                          CompanyName = p.Field<string>("OrganisationName"),
                                      }).ToList();

                    _viewModel.Product = _listModel;

                }
            }

            return _viewModel;
        }

        public List<GenericModel> Get_Master_Data(int id, string type)
        {
            DataSet DS = _prodDal.Get_Master_Data(id, type);

            List<GenericModel> _viewModel = new List<GenericModel>();

            if (DS != null && DS.Tables[0] != null)
            {
                if (DS.Tables[0].Rows.Count > 0)
                {

                    var _listModel = (from p in DS.Tables[0].AsEnumerable()
                                      select new GenericModel
                                      {
                                          Id = p.Field<int>("Id"),
                                          Name = p.Field<string>("Name"),

                                      }).ToList();

                    _viewModel = _listModel;
                }
            }

            return _viewModel;
        }

        public Prod_Product_Detail_Model Get_Product_Detail(int productId)
        {
            DataSet DS = _prodDal.Get_Product_Detail(productId);

            Prod_Product_Detail_Model _viewModel = new Prod_Product_Detail_Model();

            if (DS != null && DS.Tables[0] != null)
            {
                if (DS.Tables[0].Rows.Count > 0)
                {
                    var _listModel = (from p in DS.Tables[0].AsEnumerable()
                                      select new Prod_Product_Detail_Model
                                      {
                                          ProductId = p.Field<int>("ProductID"),
                                          ProductName = p.Field<string>("ProductName"),
                                          Dosage = p.Field<string>("DosageDescription"),
                                          Category = new GenericModel { Id = p.Field<int>("CategoryID"), Name = p.Field<string>("CategoryName") },
                                          SubCategory = new GenericModel { Id = p.Field<int>("SubCategoryID"), Name = p.Field<string>("SubCategoryName") },
                                          TechnicalName = new GenericModel { Id = p.Field<int>("TechNameID"), Name = p.Field<string>("TechnicalName") },
                                          Brand = new GenericModel { Id = p.Field<int>("BrandID"), Name = p.Field<string>("BrandName") },
                                          Company = new GenericModel { Id = p.Field<int>("CompanyID"), Name = p.Field<string>("OrganisationName") },
                                          PackageType = new GenericModel { Id = p.Field<int>("PckTypeID"), Name = p.Field<string>("PckName") },
                                          Quality = new GenericModel { Id = p.Field<int>("QualityID"), Name = p.Field<string>("QualityName") }
                                      }).FirstOrDefault();

                    var pkglist = (from q in DS.Tables[1].AsEnumerable()
                                   select new Prod_Package
                                   {
                                       PackageId = q.Field<int>("PackageID"),
                                       Qty = q.Field<decimal>("Amount"),
                                       Unit = new GenericModel { Id = q.Field<int>("UnitID"), Name = q.Field<string>("UnitName") },
                                       MRP = q.Field<decimal>("MRP"),
                                   }).ToList();

                    _listModel.Packahes = pkglist;

                    _viewModel = _listModel;
                }
            }

            return _viewModel;
        }
    }
}
