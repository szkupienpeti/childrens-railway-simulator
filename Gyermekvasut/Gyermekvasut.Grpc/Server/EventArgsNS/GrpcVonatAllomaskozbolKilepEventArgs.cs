namespace Gyermekvasut.Grpc.Server.EventArgsNS;

public class GrpcVonatAllomaskozbolKilepEventArgs : GrpcRequestEventArgs<VonatAllomaskozbolKilepRequest>
{
    public GrpcVonatAllomaskozbolKilepEventArgs(VonatAllomaskozbolKilepRequest request) : base(request) { }
}
