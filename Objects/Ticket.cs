using System;

namespace DiscordNotifWHMCS
{
    class Ticket
    {
        public int id { get; set; }
        public int ticketid { get; set; }
        public String tid { get; set; }
        public String c { get; set; }
        public int deptid { get; set; }
        public String deptname { get; set; }
        public int userid { get; set; }
        public int contactid { get; set; }
        public String name { get; set; }
        public String owner_name { get; set; }
        public String email { get; set; }
        public String requestor_name { get; set; }
        public String requestor_email { get; set; }
        public String requestor_type { get; set; }
        public String cc { get; set; }
        public String date { get; set; }
        public String subject { get; set; }
        public String status { get; set; }
        public String priority { get; set; }
        public String admin { get; set; }
        public bool attachments_removed { get; set; }
        public String lastreply { get; set; }
        public int flag { get; set; }
        public String service { get; set; }

        public Ticket(int id, int ticketid, String tid, String c, int deptid, 
                        String deptname, int userid, int contactid, String name, 
                        String owner_name, String email, String requestor_name, 
                        String requestor_email, String requestor_type, String cc, String date, 
                        String subject, String status, String priority, String admin, 
                        bool attachments_removed, string lastreply, int flag, String service)
        {
            this.id = id;
            this.ticketid = ticketid;
            this.tid = tid;
            this.c = c;
            this.deptid = deptid;
            this.deptname = deptname;
            this.userid = userid;
            this.contactid = contactid;
            this.name = name;
            this.owner_name = owner_name;
            this.email = email;
            this.requestor_name = requestor_name;
            this.requestor_email = requestor_email;
            this.requestor_type = requestor_type;
            this.cc = cc;
            this.date = date;
            this.subject = subject;
            this.status = status;
            this.priority = priority;
            this.admin = admin;
            this.attachments_removed = attachments_removed;
            this.lastreply = lastreply;
            this.flag = flag;
            this.service = service;
        }
    }

}
