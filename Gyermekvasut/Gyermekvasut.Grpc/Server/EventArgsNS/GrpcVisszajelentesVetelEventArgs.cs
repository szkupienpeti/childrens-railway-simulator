namespace Gyermekvasut.Grpc.Server.EventArgsNS;

public class GrpcVisszajelentesVetelEventArgs : GrpcRequestEventArgs<VisszajelentesVetelRequest>
{
    public GrpcVisszajelentesVetelEventArgs(VisszajelentesVetelRequest request) : base(request) { }
}
