namespace Gyermekvasut.Tests.Util;

public class EventCapturer<TEventArgs>
    where TEventArgs : EventArgs
{
    public TEventArgs? CapturedEventArgs { get; private set; }
    public bool WasEventRaised => CapturedEventArgs != null;

    public EventCapturer(Action<EventHandler<TEventArgs>> subscriber)
    {
        subscriber.Invoke(EventSubscriberHandler);
    }

    private void EventSubscriberHandler(object? sender, TEventArgs e)
    {
        CapturedEventArgs = e;
    }
}
