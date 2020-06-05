using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class WordMeaningModel
    {
        public string Word { get; set; }
        public string Meaning { get; set; }
    }
    public class WordMeaningViewModel
    {
        public List<WordMeaningModel> List { get; set; }

    }
}
