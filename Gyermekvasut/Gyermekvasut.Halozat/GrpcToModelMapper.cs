using Google.Protobuf.Collections;
using Gyermekvasut.Grpc;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek.Telefon;
using Gyermekvasut.Modellek.VonatNS;

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
            _ => throw new ArgumentException($"Illegal {nameof(GrpcAllomasNev)}: {grpcAllomasNev}")
        };
    }

    public static Csengetes MapCsengetes(GrpcCsengetes grpcCsengetes)
    {
        return grpcCsengetes switch
        {
            GrpcCsengetes.Rovid => Csengetes.Rovid,
            GrpcCsengetes.Hosszu => Csengetes.Hosszu,
            _ => throw new ArgumentException($"Illegal {nameof(GrpcCsengetes)}: {grpcCsengetes}")
        };
    }

    public static TimeOnly MapIdo(GrpcIdo grpcIdo)
        => new(grpcIdo.Ora, grpcIdo.Perc);

    public static EngedelyKeresTipus MapEngedelyKeresTipus(GrpcEngedelyKeresTipus grpcEngedelyKeresTipus)
    {
        return grpcEngedelyKeresTipus switch
        {
            GrpcEngedelyKeresTipus.AzonosIranyuVolt => EngedelyKeresTipus.AzonosIranyuVolt,
            GrpcEngedelyKeresTipus.EllenkezoIranyuVolt => EngedelyKeresTipus.EllenkezoIranyuVolt,
            GrpcEngedelyKeresTipus.EllenkezoIranyuVan => EngedelyKeresTipus.EllenkezoIranyuVan,
            _ => throw new ArgumentException($"Illegal {nameof(GrpcEngedelyKeresTipus)}: {grpcEngedelyKeresTipus}")
        };
    }

    public static EngedelyAdasTipus MapEngedelyAdasTipus(GrpcEngedelyAdasTipus grpcEngedelyAdasTipus)
    {
        return grpcEngedelyAdasTipus switch
        {
            GrpcEngedelyAdasTipus.AzonosIranyu => EngedelyAdasTipus.AzonosIranyu,
            GrpcEngedelyAdasTipus.EllenkezoIranyu => EngedelyAdasTipus.EllenkezoIranyu,
            _ => throw new ArgumentException($"Illegal {nameof(GrpcEngedelyAdasTipus)}: {grpcEngedelyAdasTipus}")
        };
    }

    public static Vonat MapVonat(GrpcVonat grpcVonat)
    {
        // TODO Map Vonat
        throw new NotImplementedException();
    }

    public static List<TModel> MapRepeated<TGrpc, TModel>(RepeatedField<TGrpc> repeatedField, Func<TGrpc, TModel> mapper)
        => repeatedField.Select(mapper).ToList();
}
