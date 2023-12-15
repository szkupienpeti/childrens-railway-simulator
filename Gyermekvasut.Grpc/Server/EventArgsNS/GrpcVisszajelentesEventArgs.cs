namespace Gyermekvasut.Grpc.Server.EventArgsNS;

public class GrpcVisszajelentesEventArgs : GrpcRequestEventArgs<VisszajelentesRequest>
{
    public GrpcVisszajelentesEventArgs(VisszajelentesRequest request) : base(request) { }
}
