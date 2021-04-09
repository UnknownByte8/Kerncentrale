using System;

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
