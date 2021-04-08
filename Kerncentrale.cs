﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
        private Controlroom controlroom;
        private List<Reactor> reactors;
        private Generator generator;
        private Koelsysteem koelsysteem;
        private ThreadingType threadingType;

        public Kerncentrale(Controlroom controlroom, List<Reactor> reactors, Generator generator, Koelsysteem koelsysteem)
        {
            this.controlroom = controlroom;
            this.reactors = reactors;
            this.generator = generator;
            this.koelsysteem = koelsysteem;
            this.threadingType = ThreadingType.SingleThreading;
            this.initializeTmpReactors();
            this.generateThreads();
        }

        public void initializeTmpReactors()
        {
            Random rnd = new Random();
            for (int i = 0; i < 100; i++)
            {
                Reactor reactor = new Reactor();
                reactor.setSelectedThreadingType(this.threadingType);
                int rndNumber = rnd.Next(0, 25);
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
