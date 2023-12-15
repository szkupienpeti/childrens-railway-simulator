namespace Gyermekvasut.Grpc.Server.EventArgsNS;

public class GrpcIndulasiIdoKozlesVetelEventArgs : GrpcRequestEventArgs<IndulasiIdoKozlesVetelRequest>
{
    public GrpcIndulasiIdoKozlesVetelEventArgs(IndulasiIdoKozlesVetelRequest request) : base(request) { }
}
