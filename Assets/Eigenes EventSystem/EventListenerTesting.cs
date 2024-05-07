

/// <summary>
/// Required by any component (system or otherwise) which is supposed to receive events from the event manager.
/// </summary>
public interface EventListenerTesting
{
    /// <summary>
    /// Callback for any received event.
    /// </summary>
    /// <param name="receivedEvent">The event that was send by the event manager. Use the event type to react accordingly.</param>
    void OnEventReceived(EventDataTesting receivedEvent);
}
