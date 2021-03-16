using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kerncentrale
{
    class Generator
    {
        private int stoom;

        public Generator(int stoom)
        {
            SetStoom(stoom);
        }

        public int GetStoom()
        {
            return stoom;
        }

        public void SetStoom(int value)
        {
            stoom = value;
        }

        public void GenerateHWH()
        {

        }
    }
}
