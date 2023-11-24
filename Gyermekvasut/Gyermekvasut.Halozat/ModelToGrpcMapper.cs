using Google.Protobuf.Collections;
using Gyermekvasut.Grpc;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek.Telefon;

namespace Gyermekvasut.Halozat;

public static class ModelToGrpcMapper
{
    public static GrpcAllomasNev MapAllomasNev(AllomasNev allomasNev)
    {
        return allomasNev switch
        {
            AllomasNev.Szechenyihegy => GrpcAllomasNev.Szechenyihegy,
            AllomasNev.Csilleberc => GrpcAllomasNev.Csilleberc,
            AllomasNev.Viragvolgy => GrpcAllomasNev.Viragvolgy,
            AllomasNev.Janoshegy => GrpcAllomasNev.Janoshegy,
            AllomasNev.Szepjuhaszne => GrpcAllomasNev.Szepjuhaszne,
            AllomasNev.Harshegy => GrpcAllomasNev.Harshegy,
            AllomasNev.Huvosvolgy => GrpcAllomasNev.Huvosvolgy,
            _ => throw new ArgumentException(nameof(allomasNev))
        };
    }

    public static GrpcCsengetes MapCsengetes(Csengetes csengetes)
    {
        return csengetes switch
        {
            Csengetes.Rovid => GrpcCsengetes.Rovid,
            Csengetes.Hosszu => GrpcCsengetes.Hosszu,
            _ => throw new ArgumentException(nameof(csengetes))
        };
    }

    public static void FillRepeated<TGrpc, TModel>(RepeatedField<TGrpc> grpcRepeatedField, List<TModel> modelList, Func<TModel, TGrpc> mapper)
    {
        modelList.ForEach(model => grpcRepeatedField.Add(mapper(model)));
    }
}
