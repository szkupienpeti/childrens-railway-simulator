using Google.Protobuf;
using Gyermekvasut.Grpc.Server.EventArgsNS;
namespace Gyermekvasut.Tests.Util;

public class GrpcRequestEventCapturer<TRequest> : EventCapturer<GrpcRequestEventArgs<TRequest>>
    where TRequest : class, IMessage<TRequest>
{
    public GrpcRequestEventCapturer(Action<EventHandler<GrpcRequestEventArgs<TRequest>>> subscriber)
        : base(subscriber)
    { }

    public TRequest? CapturedRequest
        => CapturedEventArgs?.Request;
}
