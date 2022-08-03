namespace Membership.Application.DTO.Widgets;

public class WidgetDto
{
    public int No { get; set; }
    public string Title { get; set; }
    public Int32? SummaryValue { get; set; }
    public string SummaryText { get; set; }
    public IEnumerable<WidgetDetailDto> Details { get; set; }
}

public class WidgetDetailDto
{
    public string Text { get; set; }
    public Int32? IntValue { get; set; }
    public string TextValue { get; set; }
}