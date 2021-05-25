using System;

namespace Kerncentrale
{
    public class MeltdownExeption : Exception
    {
        /// <summary>
        /// Meltdown exeption is made when fuelrod goes over maximal tempreature
        /// </summary>
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
