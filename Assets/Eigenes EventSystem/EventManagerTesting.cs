using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Basic implementation of the event manager.
/// Simple and straight forward without fancy optimizations.
/// </summary>
public class EventManagerTesting : MonoBehaviour
{
    public static EventManagerTesting MainStatic;
    /// <summary>
    /// Currently registered listeners.
    /// Any incoming event will be delegated to these listeners.
    /// </summary>
    List<EventListenerTesting> listeners;

    /// <summary>
    /// Collection of events fired during the last frame.
    /// </summary>
    List<EventDataTesting> events;

    private void Awake()
    {
        MainStatic = this;
    }

    /// <summary>
    /// Usually replaced by a script that initializes all standard systems
    /// </summary>
    private void Start()
    {
        Initialize();
    }

    /// <summary>
    /// Event manager setup.
    /// </summary>
    public void Initialize()
    {
        listeners = new List<EventListenerTesting>();
        events = new List<EventDataTesting>();
    }

    /// <summary>
    /// Add a listener that will receive events.
    /// </summary>
    /// <param name="listener">The event listener to be added.</param>
    public void Update()
    {
        // Make sure no new events are added to the list that is now executed.
        List<EventDataTesting> eventsToFireNow = events;
        events = new List<EventDataTesting>();

        foreach (EventDataTesting eventToFire in eventsToFireNow)
        {
            foreach (EventListenerTesting listener in listeners)
            {
                listener.OnEventReceived(eventToFire);
            }
        }
    }

    /// <summary>
    /// Add a listener that will receive events.
    /// </summary>
    /// <param name="listener">The event listener to be added.</param>
    public void AddListener(EventListenerTesting listener)
    {
        if (listener != null && !listeners.Contains(listener))
        {
            listeners.Add(listener);
        }
    }

    /// <summary>
    /// Remove an existing listener so that it will not receive events anymore.
    /// </summary>
    /// <param name="listener">The event listener to be removed.</param>
    public void RemoveListener(EventListenerTesting listener)
    {
        listeners.Remove(listener);
    }

    /// <summary>
    /// Queue an event for execution during the next update cycle.
    /// </summary>
    /// <param name="eventToFire">Event to be delegated to its listeners.</param>
    public void FireEvent(EventDataTesting eventToFire)
    {
        events.Add(eventToFire);
    }
}
