using C4Sharp.Diagrams;
using C4Sharp.Diagrams.Supplementary;
using C4Sharp.Models;
using C4Sharp.Models.Relationships;
using C4Sharp.Sample.Structures;

namespace C4Sharp.Sample.Diagrams;

using static Containers;
using static Components;

internal class DynamicDiagramBuildRunner : DiagramBuildRunner
{
    protected override string Title => "Dynamic diagram for Internet Banking System Authentication";

    protected override DiagramType DiagramType => DiagramType.Dynamic;

    protected override IEnumerable<Structure> Structures() => new Structure[]
    {
        Spa,
        new ContainerBoundary("Container", "API Application")
        {
            Components = new[]
            {
                Sign,
                Security,
            }
        },
        SqlDatabase,
    };

    protected override IEnumerable<Relationship> Relationships() => new Relationship[]
    {
        (Spa > Sign)["Submits credentials to", "JSON/HTTPS"][Position.Right],
        (Sign > Security)["Calls isAuthenticated() on"][DynamicDiagram.Index()-0],
        (Sign > SqlDatabase)["Logs call persistent to"][DynamicDiagram.LastIndex()-1],
        (Security > SqlDatabase)["select * from users where username = ?", "JDBC"][Position.Right],
    };
}