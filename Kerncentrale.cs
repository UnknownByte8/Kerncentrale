using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Kerncentrale
{
    class Kerncentrale
    {
        private List<Reactor> reactors;
        private ThreadingType threadingType;
        private int reactorAmount;

        public Kerncentrale(ThreadingType threadingType)
        {
            this.reactors = new List<Reactor>();
            this.threadingType = threadingType;
            this.InitializeTmpReactors();
        }

        public List<Reactor> GetReactors()
        {
            return this.reactors;
        }

        /*
         * Set random fuelRods to the reactor
         */
        public void InitializeTmpReactors()
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

        /*
         * setup for threads
         */
        public async Task<bool> GenerateThreads()
        {
            try
            {
                int i = 1;
                foreach (Reactor reactor in this.reactors)
                {
                    switch (reactor.selectedThreadingType)
                    {
                        case ThreadingType.SingleThreading:
                            reactor.ExecuteThread();
                            break;
                        case ThreadingType.MultiThreading:
                            Thread thread = new Thread(reactor.ExecuteThread)
                            {
                                Name = reactor.ToString() + i
                            };
                            thread.Start();
                            break;
                        case ThreadingType.ThreadPool:
                            ThreadPool.QueueUserWorkItem(reactor.ThreadProcess);
                            break;

                    }
                    Thread.Sleep(100);
                    i++;
                }
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("\n\n\n\nException\nmessage: {0}\nStacktrace: {1}", e.Message,e.StackTrace);
                return false;
            }
            
        }
    }
}
