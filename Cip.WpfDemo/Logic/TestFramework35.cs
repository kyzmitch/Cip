using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cip.WpfDemo.Logic
{
    public class CipPoint20
    {
        private int _x;
        private int _y;

        public int X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }
        public int Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }
    }

    public class CipPoint35
    {
        public int X
        {
            get;
            set;
        }
        public int Y
        {
            get;
            set;
        }
    }
}
