namespace Gyermekvasut.Grpc.Server.EventArgsNS;

public class GrpcEngedelyMegtagadasEventArgs : GrpcRequestEventArgs<EngedelyMegtagadasRequest>
{
    public GrpcEngedelyMegtagadasEventArgs(EngedelyMegtagadasRequest request) : base(request) { }
}
