using Discord;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace DiscordNotifWHMCS
{
    class Program
    {
        private static HttpClient client = new HttpClient();

        public static async Task GetAnsweredTickets()
        {
            // Create the request to list all opened tickets
            var requestContent = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("identifier", "SearchMe_ChangeWithYourWHMCSIdentifier"),
                new KeyValuePair<string, string>("secret", "SearchMe_ChangeWithYourWHMCSSecret"),
                new KeyValuePair<string, string>("accesskey", "SearchMe_ChangeWithYourAccessKey"),
                new KeyValuePair<string, string>("action", "GetTickets"),
                new KeyValuePair<string, string>("status", "Customer-Reply"),
                new KeyValuePair<string, string>("responsetype","json"),
            });

            // Get the response.
            HttpResponseMessage response = await client.PostAsync("https://www.tld.net/whmcs_path/includes/api.php", requestContent);

            // Get the response content.
            HttpContent responseContent = response.Content;

            // Get the stream of the content.
            using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
            {
                // Store the JSON response in string var
                String jsonStringTickets = await reader.ReadToEndAsync();

                // Create a tickets object from our response
                Tickets tickets = System.Text.Json.JsonSerializer.Deserialize<Tickets>(jsonStringTickets);

                // Create a ticket object that store the information about our ticket
                try
                {
                    String jsonStringTicket = tickets.tickets.Substring(10).Replace("}]}", "}]");

                    // Store all ticket into a list
                    var list = JsonConvert.DeserializeObject<List<Ticket>>(jsonStringTicket);

                    // Console.WriteLine("Liste des tickets en attente de réponse (" + list.Count + "):");

                    await DiscordBot.client.GetGuild(DiscordBot.serverID).GetTextChannel(DiscordBot.channelTicketID).SendMessageAsync("||<@&869020841432715276> || \nListe des tickets en attente de réponse (" + list.Count + "):");
                    
                    // Builder header
                    EmbedBuilder builderWaitingForReply = new EmbedBuilder();
                    builderWaitingForReply.WithColor(Color.Blue);
                    builderWaitingForReply.WithFooter("Last update of ticket");

                    Ticket ticket;
                    for (int i = 0; i < list.Count; i++)
                    {
                        ticket = list.ToArray()[i];

                        // Builder body
                        builderWaitingForReply.WithTitle($"Subject: {ticket.subject} [#{ticket.ticketid}]");
                        builderWaitingForReply.WithDescription(
                            $"**Department**: {ticket.deptname} \n " +
                            $"**Client Name**: {ticket.owner_name} \n " +
                            $"**Status**: {ticket.status} \n " +
                            $"**Priority**: {ticket.priority} \n\n " +
                            $"[Open ticket](https://www.tld.net/whmcs_path/admin/supporttickets.php?action=view&id={ticket.ticketid})");
                        builderWaitingForReply.WithTimestamp(DateTime.Parse(ticket.lastreply));

                        await DiscordBot.client.GetGuild(DiscordBot.serverID).GetTextChannel(DiscordBot.channelTicketID).SendMessageAsync("", false, builderWaitingForReply.Build());
                        await Task.Delay(500);

                        // Console.WriteLine($"Owner: {ticket.owner_name} \t Subject: {ticket.subject} \t Last reply: {ticket.lastreply}");
                    }

                } 
                catch (Exception) 
                {
                    // Console.WriteLine("Aucun ticket en attente de réponse");

                    await DiscordBot.client.GetGuild(DiscordBot.serverID).GetTextChannel(DiscordBot.channelTicketID).SendMessageAsync("Liste des tickets en attente de réponse ouverts (0):");

                    EmbedBuilder builderNoTicket = new EmbedBuilder();
                    builderNoTicket.WithColor(Color.Red);
                    builderNoTicket.WithFooter("Good job everyone! :D");
                    builderNoTicket.WithTitle("No ticket in waiting for reply");
                    builderNoTicket.WithDescription("DediGo don't have ticket in waiting for reply");
                    builderNoTicket.WithTimestamp(DateTime.Now);

                    await DiscordBot.client.GetGuild(DiscordBot.serverID).GetTextChannel(DiscordBot.channelTicketID).SendMessageAsync("", false, builderNoTicket.Build());
                }
            }
        }

        public static async Task GetOpenTickets()
        {
            // Create the request to list all opened tickets
            var requestContent = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("identifier", "SearchMe_ChangeWithYourWHMCSIdentifier"),
                new KeyValuePair<string, string>("secret", "SearchMe_ChangeWithYourWHMCSSecret"),
                new KeyValuePair<string, string>("accesskey", "SearchMe_ChangeWithYourAccessKey"),
                new KeyValuePair<string, string>("action", "GetTickets"),
                new KeyValuePair<string, string>("status", "Open"),
                new KeyValuePair<string, string>("responsetype","json"),
            });

            // Get the response.
            HttpResponseMessage response = await client.PostAsync("https://www.tld.net/whmcs_path/includes/api.php",requestContent);

            // Get the response content.
            HttpContent responseContent = response.Content;

            // Get the stream of the content.
            using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
            {
                // Store the JSON response in string var
                String jsonStringTickets = await reader.ReadToEndAsync();

                // Create a tickets object from our response
                Tickets tickets = System.Text.Json.JsonSerializer.Deserialize<Tickets>(jsonStringTickets);

                try
                {
                    // Create a ticket object that store the information about our ticket
                    String jsonStringTicket = tickets.tickets.Substring(10).Replace("}]}", "}]");

                    // Store all ticket into a list
                    var list = JsonConvert.DeserializeObject<List<Ticket>>(jsonStringTicket);

                    // Console.WriteLine("Liste des tickets ouverts (" + list.Count + "):");

                    await DiscordBot.client.GetGuild(DiscordBot.serverID).GetTextChannel(DiscordBot.channelTicketID).SendMessageAsync("||<@&869020841432715276> || \nListe des tickets ouverts (" + list.Count + "):");

                    // Builder header
                    EmbedBuilder builderJustOpen = new EmbedBuilder();
                    builderJustOpen.WithColor(Color.Green);
                    builderJustOpen.WithFooter("Last update of ticket");

                    Ticket ticket;
                    for (int i = 0; i < list.Count; i++)
                    {
                        ticket = list.ToArray()[i];

                        // Builder body
                        builderJustOpen.WithTitle($"Subject: {ticket.subject} [#{ticket.ticketid}]");
                        builderJustOpen.WithDescription(
                            $"**Department**: {ticket.deptname} \n " +
                            $"**Client Name**: {ticket.owner_name} \n " +
                            $"**Status**: {ticket.status} \n " +
                            $"**Priority**: {ticket.priority} \n\n " +
                            $"[Open ticket](https://www.tld.net/whmcs_path/admin/supporttickets.php?action=view&id={ticket.ticketid})");
                        builderJustOpen.WithTimestamp(DateTime.Parse(ticket.lastreply));

                        await DiscordBot.client.GetGuild(DiscordBot.serverID).GetTextChannel(DiscordBot.channelTicketID).SendMessageAsync("", false, builderJustOpen.Build());
                        await Task.Delay(500);

                        // Console.WriteLine($"Owner: {ticket.owner_name} \t Subject: {ticket.subject} \t Last reply: {ticket.lastreply} " +
                        //    $"\t Status: {ticket.status} \t Département: {ticket.deptname}");
                    }
                } 
                catch(Exception) 
                {
                    // Console.WriteLine("Aucun ticket récement ouvert"); 

                    await DiscordBot.client.GetGuild(DiscordBot.serverID).GetTextChannel(DiscordBot.channelTicketID).SendMessageAsync("Liste des tickets ouverts (0):");

                    EmbedBuilder builderNoTicket = new EmbedBuilder();
                    builderNoTicket.WithColor(Color.Red);
                    builderNoTicket.WithFooter("Good job everyone! :D");
                    builderNoTicket.WithTitle("No ticket recently opened");
                    builderNoTicket.WithDescription("DediGo don't have recent open ticket");
                    builderNoTicket.WithTimestamp(DateTime.Now);

                    await DiscordBot.client.GetGuild(DiscordBot.serverID).GetTextChannel(DiscordBot.channelTicketID).SendMessageAsync("", false, builderNoTicket.Build());
                }
            }
        }

        public static async Task GetPendingOrders()
        {
            // Create the request to list all opened tickets
            var requestContent = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("identifier", "SearchMe_ChangeWithYourWHMCSIdentifier"),
                new KeyValuePair<string, string>("secret", "SearchMe_ChangeWithYourWHMCSSecret"),
                new KeyValuePair<string, string>("accesskey", "SearchMe_ChangeWithYourAccessKey"),
                new KeyValuePair<string, string>("action", "GetOrders"),
                new KeyValuePair<string, string>("status", "Pending"),
                new KeyValuePair<string, string>("responsetype","json"),
            });

            // Get the response.
            HttpResponseMessage response = await client.PostAsync("https://www.tld.net/whmcs_path/includes/api.php", requestContent);

            // Get the response content.
            HttpContent responseContent = response.Content;

            using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
            {
                // Store the JSON response in string var
                String jsonStringOrders = await reader.ReadToEndAsync();

                // Create a orders object from our response
                Orders orders = System.Text.Json.JsonSerializer.Deserialize<Orders>(jsonStringOrders);

                try
                {
                    // Create a order object that store the information about our order
                    
                    String jsonStringOrder = orders.orders.Substring(9).Replace("]}}]}", "]}}]");
                    

                    // Store all ticket into a list
                    var list = JsonConvert.DeserializeObject<List<Order>>(jsonStringOrder);

                    // Console.WriteLine("Nombre de commande(s): " + list.Count);

                    Order order;

                    for (int i = 0; i < list.Count; i++)
                    {
                        order = list.ToArray()[i];

                        if (order.paymentstatus != "Unpaid" && order.paymentstatus != "Payment Pending")
                        {
                            await DiscordBot.client.GetGuild(DiscordBot.serverID).GetTextChannel(DiscordBot.channelOrderID).SendMessageAsync("||<@&868955351461019708> || \nCommande payée en attente:");

                            // Builder header
                            EmbedBuilder builderPendingPaid = new EmbedBuilder();
                            builderPendingPaid.WithColor(Color.Green);
                            builderPendingPaid.WithFooter("Date of order");

                            // Builder body
                            builderPendingPaid.WithTitle($"Order: #{order.ordernum}");
                            builderPendingPaid.WithDescription(
                                $"**Client name**: {order.name} \n" +
                                $"**Amount**: {order.amount}{order.currencyprefix} \n " +
                                $"**Payment method**: {order.paymentmethodname} \n " +
                                $"**Status**: {order.status} \n " +
                                $"**Payment status**: {order.paymentstatus} \n\n " +
                                $"[Open order](https://www.tld.net/whmcs_path/admin/orders.php?action=view&id={order.id})");
                            builderPendingPaid.WithTimestamp(DateTime.Parse(order.date));

                            await DiscordBot.client.GetGuild(DiscordBot.serverID).GetTextChannel(DiscordBot.channelOrderID).SendMessageAsync("", false, builderPendingPaid.Build());
                            await Task.Delay(500);
                        } 
                        else
                        {
                            await DiscordBot.client.GetGuild(DiscordBot.serverID).GetTextChannel(DiscordBot.channelOrderID).SendMessageAsync("Commande non payée en attente:");

                            // Builder header
                            EmbedBuilder builderPendingUnpaid = new EmbedBuilder();
                            builderPendingUnpaid.WithColor(Color.Red);
                            builderPendingUnpaid.WithFooter("Date of order");

                            // Builder body
                            builderPendingUnpaid.WithTitle($"Order: #{order.ordernum}");
                            builderPendingUnpaid.WithDescription(
                                $"**Client name**: {order.name} \n" +
                                $"**Amount**: {order.amount}{order.currencyprefix} \n " +
                                $"**Payment method**: {order.paymentmethodname} \n " +
                                $"**Status**: {order.status} \n " +
                                $"**Payment status**: {order.paymentstatus} \n\n " +
                                $"[Open order](https://www.tld.net/whmcs_path/admin/orders.php?action=view&id={order.id})");
                            builderPendingUnpaid.WithTimestamp(DateTime.Parse(order.date));

                            await DiscordBot.client.GetGuild(DiscordBot.serverID).GetTextChannel(DiscordBot.channelOrderID).SendMessageAsync("", false, builderPendingUnpaid.Build());
                            await Task.Delay(500);
                        }
                    }
                }
                catch (Exception)
                {
                    await DiscordBot.client.GetGuild(DiscordBot.serverID).GetTextChannel(DiscordBot.channelOrderID).SendMessageAsync("Liste des commandes en attentes:");

                    EmbedBuilder builderNoOrder = new EmbedBuilder();
                    builderNoOrder.WithColor(Color.Blue);
                    builderNoOrder.WithFooter("Good job everyone! :D");
                    builderNoOrder.WithTitle("No orders recently created");
                    builderNoOrder.WithDescription("DediGo don't have recent orders");
                    builderNoOrder.WithTimestamp(DateTime.Now);

                    await DiscordBot.client.GetGuild(DiscordBot.serverID).GetTextChannel(DiscordBot.channelOrderID).SendMessageAsync("", false, builderNoOrder.Build());
                }
            }
        }
    }
}
