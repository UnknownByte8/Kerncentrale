using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

            this.initializeTmpReactors();
            this.generateThreads();
        }

        public void initializeTmpReactors()
        {
            Random rnd = new Random();
            Reactor reactor = new Reactor();
            for(int i = 0; i < 100; i++)
            {
                int rndNumber = rnd.Next(0, 25);
                for (int j = 0; j < rndNumber; j++)
                {
                    reactor.addFuelRod(new Uranium());
                }
            }
            reactors.Add(reactor);
        }

        public int AddThreads(int amount)
        {
            return amount;
        }

        public void generateThreads()
        {
            foreach(Reactor reactor in this.reactors)
            {
                ThreadPool.QueueUserWorkItem(reactor.threadProcess);
            }
        }
    }
}
