namespace PollsApi.Models
{
    public class Vote
    {
        public int Id { get; set; }
        public int PollId { get; set; }
        public int OptionId { get; set; }
        public string SessionId { get; set; } = string.Empty;
        public DateTime VotedAt { get; set; } = DateTime.UtcNow;

        public Poll? Poll { get; set; }
        public Option? Option { get; set; }
    }
}