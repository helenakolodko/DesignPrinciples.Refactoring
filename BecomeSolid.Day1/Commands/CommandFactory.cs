using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BecomeSolid.Day1
{
    public class CommandFactory
    {
        private readonly IDictionary<string, ICommand> commands;

        public void Register(string commandName, ICommand command)
        {
            commands.Add(commandName, command);
        }

        public ICommand GetCommand(string commandName)
        {
            commands[commandName];
        }
    }
}
