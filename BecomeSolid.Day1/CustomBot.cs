using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BecomeSolid.Day1
{
    public class CustomBot
    {
        private Api api;
        private int offset;
        private int index;
        private Update[] updates = new Update[0];

        public CustomBot():
            this(ConfigurationManager.AppSettings["TelegramBotApiKey"])
        {
        }
        public CustomBot(string apiKey)
        {
            api = new Api(apiKey);
        }

        public async Task<Update> NextUpdate()
        {
            if (index >= updates.Length)
	        {
                do
                    updates = await api.GetUpdates(offset);
                while (updates.Length == 0);
		        index = 0;
	        }
            offset = updates[index].Id + 1;
            return updates[index++];
        }

        public async Task<Message> NextTextMessage()
        {
            Update update;
            do
                update = await NextUpdate();
            while(update.Message.Type != MessageType.TextMessage);
            return update.Message;
        }

        public async Task SendTextMessage(int chatId, string message)
        {
            await api.SendTextMessage(chatId, message);
        }

        public async Task SendChatAction(int chatId, ChatAction action)
        {
            await api.SendChatAction(chatId, action);
        }
    }
}
