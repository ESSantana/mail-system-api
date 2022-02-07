namespace MailSystem.API.DTO
{
    public class DocumentDTO
    {
        public long Id { get; set; }
        public long ReceiverId { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }

        public virtual ReceiverDTO Receiver { get; set; }
    }
}
