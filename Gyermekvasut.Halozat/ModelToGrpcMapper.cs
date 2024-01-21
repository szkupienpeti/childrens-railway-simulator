using Google.Protobuf.Collections;
using Gyermekvasut.Grpc;
using Gyermekvasut.Modellek;
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
        => new()
        {
            Ora = timeOnly.Hour,
            Perc = timeOnly.Minute
        };

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
        GrpcVonat grpcVonat = new()
        {
            Nev = vonat.Nev,
            Irany = MapIrany(vonat.Irany),
            Hossz = vonat.Hossz
        };
        FillRepeated(grpcVonat.Jarmuvek, vonat.Jarmuvek, MapJarmu);
        FillRepeated(grpcVonat.Menetrendek, vonat.Menetrendek, MapMenetrend);
        return grpcVonat;
    }

    public static GrpcIrany MapIrany(Irany irany)
        => irany switch
        {
            Irany.KezdopontFele => GrpcIrany.KezdopontFele,
            Irany.VegpontFele => GrpcIrany.VegpontFele,
            _ => throw new ArgumentException($"Illegal {nameof(Irany)}: {irany}")
        };

    public static GrpcJarmu MapJarmu(Jarmu jarmu)
        => new()
        {
            Nev = jarmu.Nev,
            Tipus = MapJarmuTipus(jarmu.Tipus)
        };

    public static string MapJarmuTipus(JarmuTipus jarmuTipus)
        => Enum.GetName(jarmuTipus)!;

    public static GrpcMenetrend MapMenetrend(Menetrend menetrend)
    {
        GrpcMenetrend grpcMenetrend = new()
        {
            Vonatszam = menetrend.Vonatszam,
            Irany = MapVonatIrany(menetrend.Irany),
            Koruljar = menetrend.Koruljar
        };
        FillRepeated(grpcMenetrend.Sorok, menetrend.Sorok, MapAllomasiMenetrendSor);
        return grpcMenetrend;
    }

    public static GrpcVonatIrany MapVonatIrany(VonatIrany vonatIrany)
        => vonatIrany switch
        {
            VonatIrany.Paratlan => GrpcVonatIrany.Paratlan,
            VonatIrany.Paros => GrpcVonatIrany.Paros,
            _ => throw new ArgumentException($"Illegal {nameof(VonatIrany)}: {vonatIrany}")
        };

    public static GrpcAllomasiMenetrendSor MapAllomasiMenetrendSor(AllomasiMenetrendSor allomasiMenetrendSor)
        => new()
        {
            Allomas = MapAllomasNev(allomasiMenetrendSor.Allomas),
            Erkezes = MapOptionalStruct(allomasiMenetrendSor.Erkezes, MapTimeOnly),
            Indulas = MapTimeOnly(allomasiMenetrendSor.Indulas)
        };

    public static void FillRepeated<TGrpc, TModel>(RepeatedField<TGrpc> grpcRepeatedField, List<TModel> modelList, Func<TModel, TGrpc> mapper)
        => modelList.ForEach(model => grpcRepeatedField.Add(mapper(model)));

    public static List<TGrpc> MapList<TModel, TGrpc>(List<TModel> modelList, Func<TModel, TGrpc> mapper)
        => modelList.Select(mapper).ToList();

    public static TGrpc? MapOptionalStruct<TModel, TGrpc>(TModel? modelField, Func<TModel, TGrpc> mapper)
            where TModel : struct
            where TGrpc : class
        => modelField.HasValue ? mapper(modelField.Value) : null;
}
