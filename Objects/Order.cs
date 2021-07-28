using System;

namespace DiscordNotifWHMCS
{
    class Order
    {
        public int id { get; set; }
        public ulong ordernum { get; set; }
        public int userid { get; set; }
        public int contactid { get; set; }
        public int requestor_id { get; set; }
        public String date { get; set; }
        public String nameservers { get; set; }
        public String transfersecret { get; set; }
        public String renewals { get; set; }
        public String promocode { get; set; }
        public String promotype { get; set; }
        public String promovalue { get; set; }
        public String orderdata { get; set; }
        public String amount { get; set; }
        public String paymentmethod { get; set; }
        public int invoiceid { get; set; }
        public String status { get; set; }
        public String ipaddress { get; set; }
        public String fraudmodule { get; set; }
        public String fraudoutput { get; set; }
        public String notes { get; set; }
        public String paymentmethodname { get; set; }
        public String paymentstatus { get; set; }
        public String name { get; set; }
        public String currencyprefix { get; set; }
        public String currencysuffix { get; set; }

        public Order(int id, ulong ordernum, int userid,
                        int contactid, int requestor_id, String date,
                        String nameservers, String transfersecret,
                        String renewals, String promocode, String promotype,
                        String promovalue, String orderdata, String amount,
                        String paymentmethod, int invoiceid, String status, 
                        String ipaddress, String fraudmodule, String fraudoutput, 
                        String notes, String paymentmethodname, String paymentstatus, 
                        String name, String currencyprefix, String currencysuffix)
        {
            this.id = id;
            this.ordernum = ordernum;
            this.userid = userid;
            this.contactid = contactid;
            this.requestor_id = requestor_id;
            this.date = date;
            this.nameservers = nameservers;
            this.transfersecret = transfersecret;
            this.renewals = renewals;
            this.promocode = promocode;
            this.promotype = promotype;
            this.promovalue = promovalue;
            this.orderdata = orderdata;
            this.amount = amount;
            this.paymentmethod = paymentmethod;
            this.invoiceid = invoiceid;
            this.status = status;
            this.ipaddress = ipaddress;
            this.fraudmodule = fraudmodule;
            this.fraudoutput = fraudoutput;
            this.notes = notes;
            this.paymentmethodname = paymentmethodname;
            this.paymentstatus = paymentstatus;
            this.name = name;
            this.currencyprefix = currencyprefix;
            this.currencysuffix = currencysuffix;
        }
    }
}
