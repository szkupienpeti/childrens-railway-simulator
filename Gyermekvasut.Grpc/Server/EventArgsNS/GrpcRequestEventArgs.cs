using Google.Protobuf;

namespace Gyermekvasut.Grpc.Server.EventArgsNS;

public class GrpcRequestEventArgs<TRequest> : EventArgs
    where TRequest : IMessage<TRequest>
{
    public TRequest Request { get; }
    public GrpcRequestEventArgs(TRequest request)
    {
        Request = request;
    }
}
