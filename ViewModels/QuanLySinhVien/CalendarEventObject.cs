
public class CalendarEventObject {
    // Variables
    public string? Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? GroupId { get; set; }
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
    public Boolean? DisplayEventTime { get; set; }
    // ToString
    public override string ToString() {
        return $"title: {Title}, description: {Description}, start: {Start}, end: {End}";
    }

}