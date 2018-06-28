using System.Net;
using DDDEastAnglia.Helpers.AppSettings;
using Newtonsoft.Json.Linq;

namespace DDDEastAnglia.Helpers
{
    public class EventbriteTicketProvider : ITicketProvider
    {
        public bool TicketIsForOurEvent(string ticketNumber)
        {
            string eventBriteApiKey = new WebConfigurationAppSettingsProvider().GetSetting("EventbriteAPIKey");
            using (WebClient webClient = new WebClient())
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                string ticketJson = webClient.DownloadString(string.Format("https://www.eventbriteapi.com/v3/orders/{0}?token={1}", ticketNumber, eventBriteApiKey));
                JToken ticketObject = JToken.Parse(ticketJson);

                // If the order is not "placed" then it's probably cancelled
                if (ticketObject.SelectToken("status").Value<string>() != "placed")
                {
                    return false;
                }

                string eventJson = webClient.DownloadString(string.Format(
                    "https://www.eventbriteapi.com/v3/events/{0}?token={1}", ticketObject.SelectToken("event_id"),
                    eventBriteApiKey));
                JToken eventObject = JToken.Parse(eventJson);
                string eventName = eventObject.SelectToken("name.text").Value<string>();
                if (eventName.ToLowerInvariant().StartsWith("ddd east anglia"))
                {
                    return true;
                }
                return false;
            }

        }
    }
}