using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEH_OSSimulator
{
    public static class Singleton
    {
        private static int zindex = 100;
        public static int ZIndex { 
            get {
                zindex++;
                return zindex;
            }
            set { zindex = value; }
        }
    }
}
