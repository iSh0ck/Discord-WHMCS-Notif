using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiscordNotifWHMCS
{
    class DiscordBot
    {
        private String botName = "SearchMe_ChangeBotName#9999";
        public static ulong channelTicketID = 012345678901234567; // SearchMe_ChangeTicketChannelID
        public static ulong channelOrderID = 012345678901234567; // SearchMe_ChangeOrderChannelID
        public static ulong serverID = 012345678901234567; // SearchMe_ChangeServerID
        private static int checkDelay = 900000; // SearchMe_ChangeUpdateDelay (900000ms = 15mn)

        public static DiscordSocketClient client;

        static void Main(String[] args) => new DiscordBot().RunBotAsync().GetAwaiter().GetResult();

        public async Task RunBotAsync()
        {
            client = new DiscordSocketClient();
            client.Log += Log;

            client.Ready += () =>
            {
                // Console.WriteLine("Bot is ready");
                return Task.CompletedTask;
            };

            await client.LoginAsync(TokenType.Bot, "SearchMe_ChangeWithYourDiscordBotToken");
            await client.StartAsync();

            client.MessageReceived += onMessage;

            await Task.Delay(-1);
        }

        private async Task onMessage(SocketMessage pMsg)
        {
            if ((pMsg.Channel.Id == channelTicketID || pMsg.Channel.Id == channelOrderID) && pMsg.Author.ToString() != botName)
            {
                // await update(); // Careful that's not working, you need to login your bot everytime before new update

                var task = Task.Run(() => update()); // Start update function in a new thread
                // task.Wait(); // Is you want cancel or pause a task

                await Task.Delay(1);
            }
        }

        private static async Task update()
        {
            while (true)
            {
                if (canUpdate())
                {
                    Console.WriteLine("Test update OK, go to delay");
                    // Deleting ticket notification messages
                    IEnumerable<IMessage> messageTicket = await client.GetGuild(serverID).GetTextChannel(channelTicketID).GetMessagesAsync(100).FlattenAsync();
                    await ((ITextChannel)client.GetGuild(serverID).GetTextChannel(channelTicketID)).DeleteMessagesAsync(messageTicket);

                    // Deleting orders notification messages
                    IEnumerable<IMessage> messagesOrder = await client.GetGuild(serverID).GetTextChannel(channelOrderID).GetMessagesAsync(100).FlattenAsync();
                    await ((ITextChannel)client.GetGuild(serverID).GetTextChannel(channelOrderID)).DeleteMessagesAsync(messagesOrder);

                    await Program.GetOpenTickets();
                    await Program.GetAnsweredTickets();
                    await Program.GetPendingOrders();
                }
                else
                {
                    Console.WriteLine("Test update failed, go to delay");
                }

                await Task.Delay(checkDelay);
            }
            
        }

        private static bool canUpdate()
        {
            // Get the today date
            DateTime now = DateTime.Now;

            // Check if the hour is between 23:00h and 09:00h
            if ((now.Hour >= 0 && now.Hour <= 8) || now.Hour >= 23)
            {
                // The program can't send notifications
                Console.WriteLine("Can't update");
                return false;
            }
            else
            {
                // The program can send notifications
                Console.WriteLine("Can update");
                return true;
            }
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
