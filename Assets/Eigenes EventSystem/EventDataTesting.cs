using System.Diagnostics;

public class EventDataTesting
{
    /// <summary>
    /// Type of the event. Every event must declare an event type.
    /// </summary>
    public EventDataTypeTesting Type { get; private set; }

    /// <summary>
    /// Data stored within the event. Some events may not include any kind of data.
    /// </summary>
    public object Data { get; private set; }

    /// <summary>
    /// The source of the event. Used for tracking purpose during debugging.
    /// </summary>
    public string Source { get; private set; }

    public EventDataTesting(EventDataTypeTesting type) : this(type, null)
    {
    }

    public EventDataTesting(EventDataTypeTesting type, object data)
    {
        Type = type;
        Data = data;
        Source = null;

#if UNITY_EDITOR
        StackFrame sourceFrame = new StackFrame(2);
        Source = sourceFrame.GetMethod().ReflectedType.Name + "." + sourceFrame.GetMethod().Name;
#endif
    }
}
