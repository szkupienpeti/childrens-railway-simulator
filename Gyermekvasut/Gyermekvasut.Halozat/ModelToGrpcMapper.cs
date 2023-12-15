using Google.Protobuf.Collections;
using Gyermekvasut.Grpc;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek.Telefon;
using Gyermekvasut.Modellek.VonatNS;

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
            _ => throw new ArgumentException($"Illegal {nameof(AllomasNev)}: {allomasNev}")
        };
    }

    public static GrpcCsengetes MapCsengetes(Csengetes csengetes)
    {
        return csengetes switch
        {
            Csengetes.Rovid => GrpcCsengetes.Rovid,
            Csengetes.Hosszu => GrpcCsengetes.Hosszu,
            _ => throw new ArgumentException($"Illegal {nameof(Csengetes)}: {csengetes}")
        };
    }

    public static GrpcIdo MapTimeOnly(TimeOnly timeOnly)
    {
        return new()
        {
            Ora = timeOnly.Hour,
            Perc = timeOnly.Minute
        };
    }

    public static GrpcEngedelyKeresTipus MapEngedelyKeresTipus(EngedelyKeresTipus engedelyKeresTipus)
    {
        return engedelyKeresTipus switch
        {
            EngedelyKeresTipus.AzonosIranyuVolt => GrpcEngedelyKeresTipus.AzonosIranyuVolt,
            EngedelyKeresTipus.EllenkezoIranyuVolt => GrpcEngedelyKeresTipus.EllenkezoIranyuVolt,
            EngedelyKeresTipus.EllenkezoIranyuVan => GrpcEngedelyKeresTipus.EllenkezoIranyuVan,
            _ => throw new ArgumentException($"Illegal {nameof(EngedelyKeresTipus)}: {engedelyKeresTipus}")
        };
    }

    public static GrpcEngedelyAdasTipus MapEngedelyAdasTipus(EngedelyAdasTipus engedelyAdasTipus)
    {
        return engedelyAdasTipus switch
        {
            EngedelyAdasTipus.AzonosIranyu => GrpcEngedelyAdasTipus.AzonosIranyu,
            EngedelyAdasTipus.EllenkezoIranyu => GrpcEngedelyAdasTipus.EllenkezoIranyu,
            _ => throw new ArgumentException($"Illegal {nameof(EngedelyAdasTipus)}: {engedelyAdasTipus}")
        };
    }

    public static GrpcVonat MapVonat(Vonat vonat)
    {
        // TODO Map Vonat
        throw new NotImplementedException();
    }

    public static void FillRepeated<TGrpc, TModel>(RepeatedField<TGrpc> grpcRepeatedField, List<TModel> modelList, Func<TModel, TGrpc> mapper)
        => modelList.ForEach(model => grpcRepeatedField.Add(mapper(model)));
}
