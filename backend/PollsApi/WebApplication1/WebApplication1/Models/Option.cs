namespace PollsApi.Models
{
    public class Option
    {
        public int Id { get; set; }
        public string OptionText { get; set; } = string.Empty;
        public int PollId { get; set; }

        public Poll? Poll { get; set; }
        public List<Vote> Votes { get; set; } = new();
    }
}