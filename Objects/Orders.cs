using System;
using System.Text.Json.Serialization;

namespace DiscordNotifWHMCS
{
    class Orders
    {
        public String result { get; set; }
        public int totalresults { get; set; }
        public int startnumber { get; set; }
        public int numreturned { get; set; }
        [JsonConverter(typeof(InfoToStringConverter))]
        public String orders { get; set; }

        public Orders(String result, int totalresults, int startnumber, int numreturned, String orders)
        {
            this.result = result;
            this.totalresults = totalresults;
            this.startnumber = startnumber;
            this.numreturned = numreturned;
            this.orders = orders;
        }
    }
}