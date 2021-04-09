using System;
using System.Collections.Generic;
using System.Threading;

namespace Kerncentrale
{
    public enum ThreadingType
    {
        ThreadPool,
        MultiThreading,
        SingleThreading
    }
    class Kerncentrale
    {
        private List<Reactor> reactors;
        private ThreadingType threadingType;

        public Kerncentrale()
        {
            this.reactors = new List<Reactor>();
            this.threadingType = ThreadingType.MultiThreading;
            this.initializeTmpReactors();
            this.generateThreads();
        }

        public List<Reactor> getReactors()
        {
            return this.reactors;
        }

        public void initializeTmpReactors()
        {
            Random rnd = new Random();
            for (int i = 0; i < 20; i++)
            {
                Reactor reactor = new Reactor();
                reactor.setSelectedThreadingType(this.threadingType);
                int rndNumber = rnd.Next(5, 25);
                for (int j = 0; j < rndNumber; j++)
                {
                    if (rndNumber % 2 == 1)
                        reactor.addFuelRod(new FuelRod.Uranium());
                    else
                        reactor.addFuelRod(new FuelRod.Plutonium());
                }
                reactors.Add(reactor);
            }
        }

        public int AddThreads(int amount)
        {
            return amount;
        }

        public void generateThreads()
        {
            foreach (Reactor reactor in this.reactors)
            {
                switch (this.threadingType)
                {
                    case ThreadingType.SingleThreading:
                        reactor.executeThread();
                        break;
                    case ThreadingType.MultiThreading:

                        Thread thread = new Thread(reactor.executeThread);
                        thread.Name = reactor.ToString();
                        thread.Start();
                        break;
                    case ThreadingType.ThreadPool:
                        ThreadPool.QueueUserWorkItem(reactor.threadProcess);
                        break;

                }

            }
        }
    }
}
