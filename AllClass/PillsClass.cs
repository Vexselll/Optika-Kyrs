using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Optika.AllClass
{
    class PillsClass
    {
        public int idPills { get; set;}
        public string Name { get; set; }
        public BitmapImage img { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public string Option { get; set; }
    }
}
