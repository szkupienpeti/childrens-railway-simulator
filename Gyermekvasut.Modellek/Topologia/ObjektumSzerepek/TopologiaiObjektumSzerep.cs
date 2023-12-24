namespace Gyermekvasut.Modellek.Topologia.ObjektumSzerepek;

/// <summary>
/// Common base class for topological object roles which are implemented as static fields.<br />
/// It is meant to implement a similar behavior as Java enums, with the following limitations:
/// <list type="bullet">
/// <item>Can not iterate over 'literals', like <c>Enum.GetValues&lt;T&gt;()</c></item>
/// <item>Without a static constructor, the static field initializers are executed at an<br />
/// implementation-dependent time prior to the first use of a static field of that class.<br />
/// As a result, if a topological object type class is not used at all, it is not guaranteed,<br />
/// that the fields are initialized, i.e., their constructor has already run.</item>
/// </list>
/// </summary>
public abstract class TopologiaiObjektumSzerep
{
    public string Nev { get; }

    protected TopologiaiObjektumSzerep(string nev)
    {
        Nev = nev;
    }

    public override string ToString() => Nev;
}
