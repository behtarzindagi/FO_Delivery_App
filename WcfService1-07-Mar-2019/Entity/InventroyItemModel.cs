using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class InventroyItemViewModel
    {

        public string apiKey { get; set; }
        public int userId { get; set; }
        public int dealerId { get; set; }
        public List<InventroyItemModel> Items { get; set; }
    }
    public class InventroyItemModel
    {
        public string packageId { get; set; }
        public string qty { get; set; }
    }
}

