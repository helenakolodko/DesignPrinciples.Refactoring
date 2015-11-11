using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BecomeSolid.Day1
{
    public class CommandFactory
    {
        private readonly Dictionary<string, ICommand> commands = new Dictionary<string, ICommand>();

        public void Register(string commandName, ICommand command)
        {
            commands.Add(commandName, command);
        }

        public ICommand GetCommand(string commandName)
        {
            ICommand result;
            if (commands.TryGetValue(commandName, out result))
                return result;
            else
                return null;
        }
    }
}
