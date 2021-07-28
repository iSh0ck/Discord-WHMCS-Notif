using System;
using System.Text.Json.Serialization;

namespace DiscordNotifWHMCS
{
    public class Tickets
    {
        public String result { get; set; }
        public int totalresults { get; set; }
        public int startnumber { get; set; }
        public int numreturned { get; set; }
        [JsonConverter(typeof(InfoToStringConverter))]
        public String tickets { get; set; }
        public String requestor_name { get; set; }
        public String requestor_type { get; set; }
        public String requestor_email { get; set; }
        public String owner_name { get; set; }
        
        public Tickets(String result, int totalresults, int startnumber,
                        int numreturned, String tickets, String requestor_name, 
                        String requestor_type, String requestor_email, String owner_name)
        {
            this.result = result;
            this.totalresults = totalresults;
            this.startnumber = startnumber;
            this.numreturned = numreturned;
            this.tickets = tickets;
            this.requestor_name = requestor_name;
            this.requestor_type = requestor_type;
            this.requestor_email = requestor_email;
            this.owner_name = owner_name;
        }
    }

}
