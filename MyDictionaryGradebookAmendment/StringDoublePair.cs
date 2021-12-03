using System;
using System.Collections.Generic;
using System.Text;

namespace MyDictionaryGradebookAmendment
{
    internal class StringDoublePair
    {
        private string str;
        public string Str
        {
            get { return str; }
        }

        private double dbl;
        public double Dbl
        {
            get { return dbl; }
        }

        public StringDoublePair(string str, double dbl)
        {
            this.str = str;
            this.dbl = dbl;
        }

    }
}
