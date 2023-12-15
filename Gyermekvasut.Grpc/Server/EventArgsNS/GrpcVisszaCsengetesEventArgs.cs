namespace Gyermekvasut.Grpc.Server.EventArgsNS;

public class GrpcVisszaCsengetesEventArgs : GrpcRequestEventArgs<VisszaCsengetesRequest>
{
    public GrpcVisszaCsengetesEventArgs(VisszaCsengetesRequest request) : base(request) { }
}
