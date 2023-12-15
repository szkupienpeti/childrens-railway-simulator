namespace Gyermekvasut.Grpc.Server.EventArgsNS;

public class GrpcVonatAllomaskozbeBelepEventArgs : GrpcRequestEventArgs<VonatAllomaskozbeBelepRequest>
{
    public GrpcVonatAllomaskozbeBelepEventArgs(VonatAllomaskozbeBelepRequest request) : base(request) { }
}
