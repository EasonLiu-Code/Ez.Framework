namespace Ez.Domain.Event.Events;

/// <summary>
/// TestMessageEvent
/// </summary>
public record TestMessageEvent
{
    /// <summary>
    /// DateTime
    /// </summary>
    public DateTime DateTime { get; set; }
}