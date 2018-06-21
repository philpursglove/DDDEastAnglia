namespace DDDEastAnglia.Helpers
{
    public class EventbriteTicketProvider : ITicketProvider
    {
        public bool TicketIsForOurEvent(string ticketNumber)
        {
            return true;
        }
    }
}