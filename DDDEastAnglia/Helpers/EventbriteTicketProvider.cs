using System.Net;
using System.Security.Cryptography;
using System.Text;
using DDDEastAnglia.Helpers.AppSettings;
using Newtonsoft.Json.Linq;

namespace DDDEastAnglia.Helpers
{
    public class EventbriteTicketProvider : ITicketProvider
    {
        public bool TicketIsForOurEvent(string ticketNumber)
        {
            string eventBriteApiKey = new WebConfigurationAppSettingsProvider().GetSetting("EventbriteOAuthToken");
            string eventHashValue = "1c67b45e0cb0f83e2e5f39ca9261701edabb36ea24c0125bf11d477600dcbe1e";
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
                eventName = eventName.Substring(0, 15).ToLowerInvariant();

                byte[] eventNameBytes = Encoding.Unicode.GetBytes(eventName);
                HashAlgorithm sha256 = new SHA256CryptoServiceProvider();
                byte[] hashedEventNameBytes = sha256.ComputeHash(eventNameBytes);
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedEventNameBytes.Length; i++)
                {
                    builder.Append(hashedEventNameBytes[i].ToString("x2"));
                }
                string hashedEventName = builder.ToString();

                if (hashedEventName == eventHashValue)
                {
                    return true;
                }
                return false;
            }

        }
    }
}