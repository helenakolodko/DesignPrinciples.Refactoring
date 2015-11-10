using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BecomeSolid.Day1
{
    public class CommandParams
    {
        private string parameters;
        private IResponseStringBuilder builder;
        public CommandParams(string parameters, IResponseStringBuilder builder)
	    {
            this.parameters = parameters;
            this.builder = builder;
	    }

        //public IEnumerable<string> Parameters { get { return parameters; } }
        public IResponseStringBuilder Builder { get { return builder; } }
        
    }
}
