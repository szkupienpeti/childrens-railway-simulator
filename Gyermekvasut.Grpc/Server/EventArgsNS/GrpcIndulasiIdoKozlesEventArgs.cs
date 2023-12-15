namespace Gyermekvasut.Grpc.Server.EventArgsNS;

public class GrpcIndulasiIdoKozlesEventArgs : GrpcRequestEventArgs<IndulasiIdoKozlesRequest>
{
    public GrpcIndulasiIdoKozlesEventArgs(IndulasiIdoKozlesRequest request) : base(request) { }
}
