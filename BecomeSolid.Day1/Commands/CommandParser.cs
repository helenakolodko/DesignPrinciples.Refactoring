using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BecomeSolid.Commands
{
    public class CommandParser
    {
        private string commandToken;

        public CommandParser():
            this("/")
	    {
	    }

        public CommandParser(string commandToken)
        {
            this.commandToken = commandToken;
        }

        public string GetCommandName(string query)
        {
            string firstWord = query.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).First();
            if (firstWord.StartsWith(commandToken))
                return firstWord.Substring(1);
            else 
                return null;
        }

        public IEnumerable<string> GetCommandParameters(string query)
        {
            return query.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Skip(1);
        }
    }
}
