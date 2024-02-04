using Google.Protobuf;

namespace Gyermekvasut.Halozat.Tests;

public static class HalozatAssertUtil
{
    public static void AssertExpectedGrpcRequestEventWasCaptured<TRequest>(GrpcRequestEventCapturer<TRequest> eventCapturer, TRequest expectedRequest)
        where TRequest : class, IMessage<TRequest>
    {
        Assert.IsTrue(eventCapturer.WasEventRaised);
        var eventArgsRequest = eventCapturer.CapturedRequest!;
        Assert.AreEqual(expectedRequest, eventArgsRequest);
    }
}
