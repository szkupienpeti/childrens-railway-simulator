namespace Gyermekvasut.Grpc.Server.EventArgsNS;

public class GrpcEngedelyAdasEventArgs : GrpcRequestEventArgs<EngedelyAdasRequest>
{
    public GrpcEngedelyAdasEventArgs(EngedelyAdasRequest request) : base(request) { }
}
