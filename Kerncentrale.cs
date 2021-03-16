using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kerncentrale
{
    class Kerncentrale
    {
        private Controlroom controlroom;
        private List<Reactor> reactors;
        private Generator generator;
        private Koelsysteem koelsysteem;

        public Kerncentrale(Controlroom controlroom, List<Reactor> reactors, Generator generator, Koelsysteem koelsysteem)
        {
            this.controlroom = controlroom;
            this.reactors = reactors;
            this.generator = generator;
            this.koelsysteem = koelsysteem;
        }

        public int AddThreads(int amount)
        {
            return amount;
        }
    }
}
