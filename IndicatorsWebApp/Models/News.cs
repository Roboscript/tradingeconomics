namespace IndicatorsWebApp.Models
{
    public class News
    {
        public int? Id { get; set; } = 0;
        public string Title { get; set; } = "";
        public DateTime? Date { get; set; } = DateTime.Now;
        public string Description { get; set; } = "";
        public string Country { get; set; } = "";
        public string Category { get; set; } = "";
        public string Symbol { get; set; } = "";
        public string Url { get; set; } = "";
        public int? Importance { get; set; } = 0;
    }
}
