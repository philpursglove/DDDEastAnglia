namespace DDDEastAnglia.Helpers
{
    public interface ITicketProvider
    {
        bool TicketIsForOurEvent(string ticketNumber);
    }
}