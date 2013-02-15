using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Messaging.Events;
using Orchard.Messaging.Models;

namespace XODB.Events
{
    public class EmailMessageHandler : IMessageEventHandler
    {
        public const string DEFAULT_XODB_EMAIL_TYPE = "XODB-Services-UserNotification";
        
        public void Sending(MessageContext context)
        {
            if (context.MessagePrepared)
                return;
            switch (context.Type)
            {
                case DEFAULT_XODB_EMAIL_TYPE:
                    context.MailMessage.Subject = context.Properties["Subject"];
                    context.MailMessage.Body = context.Properties["Body"];
                    context.MessagePrepared = true;
                    break;
            }
        }

        //we don't care about this right now 
        public void Sent(MessageContext context) { }

    }
}