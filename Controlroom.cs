using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kerncentrale
{
    class Controlroom
    {
        Reactor selectedReactor = new Reactor();

        internal Reactor SelectedReactor { get => selectedReactor; set => selectedReactor = value; }

        public void InitializeButtons()
        {

        }
    }
}
