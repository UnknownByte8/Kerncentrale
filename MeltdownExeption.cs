using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kerncentrale
{
public class MeltdownExeption : Exception
    {
        public MeltdownExeption()
        {
        }

        public MeltdownExeption(string message)
            : base(message)
        {
        }

        public MeltdownExeption(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

}
