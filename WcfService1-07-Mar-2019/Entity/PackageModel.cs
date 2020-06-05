using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
   public  class PackageModel
    {
        public string PackageName { set; get; }
        public string PackageId { set; get; }

    }

    public class PackageViewModel
    {
        public List<PackageModel> PackageList { set; get; }
    }

    public class Package
    {
        public PackageViewModel Packages { set; get; }
    }
}
