using Google.Protobuf.Collections;
using Gyermekvasut.Grpc;
using Gyermekvasut.Modellek;
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
        Irany irany = MapIrany(grpcVonat.Irany);
        Jarmu[] jarmuvek = MapRepeated(grpcVonat.Jarmuvek, MapJarmu).ToArray();
        Menetrend[] menetrendek = MapRepeated(grpcVonat.Menetrendek, MapMenetrend).ToArray();
        Vonat vonat = new(grpcVonat.Nev, irany, jarmuvek, menetrendek);
        if (grpcVonat.Hossz != vonat.Hossz)
        {
            throw new InvalidOperationException(
                $"A gRPC-n küldtt vonathossz ({grpcVonat.Hossz}) és a felépített vonathossz ({vonat.Hossz}) eltérnek.");
        }
        return vonat;
    }

    public static Irany MapIrany(GrpcIrany grpcIrany)
        => grpcIrany switch
        {
            GrpcIrany.KezdopontFele => Irany.KezdopontFele,
            GrpcIrany.VegpontFele => Irany.VegpontFele,
            _ => throw new ArgumentException($"Illegal {nameof(GrpcIrany)}: {grpcIrany}")
        };

    public static Jarmu MapJarmu(GrpcJarmu grpcJarmu)
        => new(grpcJarmu.Nev, MapJarmuTipus(grpcJarmu.Tipus));

    public static JarmuTipus MapJarmuTipus(string grpcJarmuTipus)
        => Enum.Parse<JarmuTipus>(grpcJarmuTipus);

    public static Menetrend MapMenetrend(GrpcMenetrend grpcMenetrend)
        => new(grpcMenetrend.Vonatszam, MapVonatIrany(grpcMenetrend.Irany), grpcMenetrend.Koruljar,
            MapRepeated(grpcMenetrend.Sorok, MapAllomasiMenetrendSor).ToArray());

    public static VonatIrany MapVonatIrany(GrpcVonatIrany grpcVonatIrany)
        => grpcVonatIrany switch
        {
            GrpcVonatIrany.Paratlan => VonatIrany.Paratlan,
            GrpcVonatIrany.Paros => VonatIrany.Paros,
            _ => throw new ArgumentException($"Illegal {nameof(GrpcVonatIrany)}: {grpcVonatIrany}")
        };

    public static AllomasiMenetrendSor MapAllomasiMenetrendSor(GrpcAllomasiMenetrendSor grpcAllomasiMenetrendSor)
        => new(MapAllomasNev(grpcAllomasiMenetrendSor.Allomas),
            MapIdo(grpcAllomasiMenetrendSor.Erkezes), MapIdo(grpcAllomasiMenetrendSor.Indulas));

    public static List<TModel> MapRepeated<TGrpc, TModel>(RepeatedField<TGrpc> repeatedField, Func<TGrpc, TModel> mapper)
        => repeatedField.Select(mapper).ToList();
}
