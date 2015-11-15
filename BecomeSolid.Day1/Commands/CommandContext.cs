using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BecomeSolid.Commands
{
    public class CommandContext
    {
        private IEnumerable<string> parameters;
        private CustomBot bot;
        private int chatId;
        public CommandContext(IEnumerable<string> parameters, int chatId, CustomBot bot)
	    {
            this.parameters = parameters;
            this.bot = bot;
            this.chatId = chatId;
	    }

        public IEnumerable<string> Parameters { get { return parameters; } }
        public CustomBot Bot { get { return bot; } }
        public int ChatId { get { return chatId; } }
        
    }
}
