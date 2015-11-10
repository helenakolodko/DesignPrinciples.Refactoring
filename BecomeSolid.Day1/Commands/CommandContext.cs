using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BecomeSolid.Day1
{
    public class CommandContext
    {
        private IEnumerable<string> parameters;
        private CustomBot bot;
        public CommandContext(IEnumerable<string> parameters, CustomBot bot)
	    {
            this.parameters = parameters;
            this.bot = bot;
	    }

        public IEnumerable<string> Parameters { get { return parameters; } }
        public CustomBot Bot { get { return bot; } }
        
    }
}
