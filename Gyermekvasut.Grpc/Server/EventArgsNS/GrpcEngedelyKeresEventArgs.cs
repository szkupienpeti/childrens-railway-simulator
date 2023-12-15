namespace Gyermekvasut.Grpc.Server.EventArgsNS;

public class GrpcEngedelyKeresEventArgs : GrpcRequestEventArgs<EngedelyKeresRequest>
{
    public GrpcEngedelyKeresEventArgs(EngedelyKeresRequest request) : base(request) { }
}
