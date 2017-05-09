using System;
using System.Collections.Generic;
using System.Text;

namespace Inrotech.Domain.Components.Graphs
{
    public class tal_test
    {
        private int tal { get; set; }
        public tal_test()
        {
            this.tal = 0;
        }
        public void inc_tal()
        {
            this.tal++;
        }
        public int getTal() {
            return tal;
        }
    }
}
