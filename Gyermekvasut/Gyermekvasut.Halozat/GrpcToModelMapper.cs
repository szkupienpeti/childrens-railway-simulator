using Google.Protobuf.Collections;
using Gyermekvasut.Grpc;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek.Telefon;

namespace Gyermekvasut.Halozat;

public static class GrpcToModelMapper
{
    public static AllomasNev MapAllomasNev(GrpcAllomasNev grpcAllomasNev)
    {
        return grpcAllomasNev switch
        {
            GrpcAllomasNev.Szechenyihegy => AllomasNev.Szechenyihegy,
            GrpcAllomasNev.Csilleberc => AllomasNev.Csilleberc,
            GrpcAllomasNev.Viragvolgy => AllomasNev.Viragvolgy,
            GrpcAllomasNev.Janoshegy => AllomasNev.Janoshegy,
            GrpcAllomasNev.Szepjuhaszne => AllomasNev.Szepjuhaszne,
            GrpcAllomasNev.Harshegy => AllomasNev.Harshegy,
            GrpcAllomasNev.Huvosvolgy => AllomasNev.Huvosvolgy,
            _ => throw new ArgumentException(nameof(grpcAllomasNev))
        };
    }

    public static Csengetes MapCsengetes(GrpcCsengetes grpcCsengetes)
    {
        return grpcCsengetes switch
        {
            GrpcCsengetes.Rovid => Csengetes.Rovid,
            GrpcCsengetes.Hosszu => Csengetes.Hosszu,
            _ => throw new ArgumentException(nameof(grpcCsengetes))
        };
    }

    public static List<TModel> MapRepeated<TGrpc, TModel>(RepeatedField<TGrpc> repeatedField, Func<TGrpc, TModel> mapper)
    {
        return repeatedField.Select(mapper).ToList();
    }
}
