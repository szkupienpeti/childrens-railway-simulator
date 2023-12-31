using Gyermekvasut.Grpc;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek.Telefon;
using Gyermekvasut.Modellek.VonatNS;

namespace Gyermekvasut.Halozat.Factory;

public static class GrpcRequestFactory
{
    public static CsengetesRequest CreateCsengetesRequest(AllomasNev kuldo, List<Csengetes> csengetesek)
    {
        var request = new CsengetesRequest()
        {
            Kuldo = ModelToGrpcMapper.MapAllomasNev(kuldo)
        };
        ModelToGrpcMapper.FillRepeated(request.Csengetesek, csengetesek, ModelToGrpcMapper.MapCsengetes);
        return request;
    }

    public static VisszaCsengetesRequest CreateVisszaCsengetesRequest(AllomasNev kuldo, List<Csengetes> csengetesek)
    {
        var request = new VisszaCsengetesRequest()
        {
            Kuldo = ModelToGrpcMapper.MapAllomasNev(kuldo)
        };
        ModelToGrpcMapper.FillRepeated(request.Csengetesek, csengetesek, ModelToGrpcMapper.MapCsengetes);
        return request;
    }

    public static EngedelyKeresRequest CreateEngedelyKeresRequest(AllomasNev kuldo, EngedelyKeresTipus tipus, string utolsoVonat, string vonatszam, TimeOnly ido, string nev)
    {
        var request = new EngedelyKeresRequest()
        {
            Kuldo = ModelToGrpcMapper.MapAllomasNev(kuldo),
            Tipus = ModelToGrpcMapper.MapEngedelyKeresTipus(tipus),
            UtolsoVonat = utolsoVonat,
            Vonatszam = vonatszam,
            Ido = ModelToGrpcMapper.MapTimeOnly(ido),
            Nev = nev
        };
        return request;
    }

    public static EngedelyAdasRequest CreateEngedelyAdasRequest(AllomasNev kuldo, EngedelyAdasTipus tipus, string utolsoVonat, string vonatszam, string nev)
    {
        var request = new EngedelyAdasRequest()
        {
            Kuldo = ModelToGrpcMapper.MapAllomasNev(kuldo),
            Tipus = ModelToGrpcMapper.MapEngedelyAdasTipus(tipus),
            UtolsoVonat = utolsoVonat,
            Vonatszam = vonatszam,
            Nev = nev
        };
        return request;
    }

    public static EngedelyMegtagadasRequest CreateEngedelyMegtagadasRequest(AllomasNev kuldo, string vonatszam, string ok, int percMulva, string nev)
    {
        var request = new EngedelyMegtagadasRequest()
        {
            Kuldo = ModelToGrpcMapper.MapAllomasNev(kuldo),
            Vonatszam = vonatszam,
            Ok = ok,
            PercMulva = percMulva,
            Nev = nev
        };
        return request;
    }

    public static IndulasiIdoKozlesRequest CreateIndulasiIdoKozlesRequest(AllomasNev kuldo, string vonatszam, TimeOnly ido, string nev)
    {
        var request = new IndulasiIdoKozlesRequest()
        {
            Kuldo = ModelToGrpcMapper.MapAllomasNev(kuldo),
            Vonatszam = vonatszam,
            Ido = ModelToGrpcMapper.MapTimeOnly(ido),
            Nev = nev
        };
        return request;
    }

    public static IndulasiIdoKozlesVetelRequest CreateIndulasiIdoKozlesVetelRequest(AllomasNev kuldo, string vonatszam, TimeOnly ido, string nev)
    {
        var request = new IndulasiIdoKozlesVetelRequest()
        {
            Kuldo = ModelToGrpcMapper.MapAllomasNev(kuldo),
            Vonatszam = vonatszam,
            Ido = ModelToGrpcMapper.MapTimeOnly(ido),
            Nev = nev
        };
        return request;
    }

    public static VisszajelentesRequest CreateVisszajelentesRequest(AllomasNev kuldo, string vonatszam, string nev)
    {
        var request = new VisszajelentesRequest()
        {
            Kuldo = ModelToGrpcMapper.MapAllomasNev(kuldo),
            Vonatszam = vonatszam,
            Nev = nev
        };
        return request;
    }

    public static VisszajelentesVetelRequest CreateVisszajelentesVetelRequest(AllomasNev kuldo, string vonatszam, string nev)
    {
        var request = new VisszajelentesVetelRequest()
        {
            Kuldo = ModelToGrpcMapper.MapAllomasNev(kuldo),
            Vonatszam = vonatszam,
            Nev = nev
        };
        return request;
    }

    public static VonatAllomaskozbeBelepRequest CreateVonatAllomaskozbeBelepRequest(AllomasNev kuldo, Vonat vonat)
    {
        var request = new VonatAllomaskozbeBelepRequest()
        {
            Kuldo = ModelToGrpcMapper.MapAllomasNev(kuldo),
            Vonat = ModelToGrpcMapper.MapVonat(vonat)
        };
        return request;
    }

    public static VonatAllomaskozbolKilepRequest CreateVonatAllomaskozbolKilepRequest(AllomasNev kuldo, string vonatszam)
    {
        var request = new VonatAllomaskozbolKilepRequest()
        {
            Kuldo = ModelToGrpcMapper.MapAllomasNev(kuldo),
            Vonatszam = vonatszam
        };
        return request;
    }
}
