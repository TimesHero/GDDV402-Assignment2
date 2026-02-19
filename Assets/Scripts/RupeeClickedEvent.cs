using Unity.Services.Analytics;

public class RupeeClickedEvent : Event
{
    public RupeeClickedEvent() : base("rupeeClicked") {}

    public string RupeeColour {set { SetParameter("rupeeColour", value);}}
}
