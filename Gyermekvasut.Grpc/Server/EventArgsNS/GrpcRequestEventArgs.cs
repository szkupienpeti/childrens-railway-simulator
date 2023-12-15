using Google.Protobuf;

namespace Gyermekvasut.Grpc.Server.EventArgsNS;

public abstract class GrpcRequestEventArgs<TRequest> : EventArgs where TRequest : IMessage<TRequest>
{
    public TRequest Request { get; }
    protected GrpcRequestEventArgs(TRequest request)
    {
        Request = request;
    }
}
