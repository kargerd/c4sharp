using C4Sharp.Diagrams.Supplementary;
using C4Sharp.Models;
using C4Sharp.Models.Relationships;
using C4Sharp.Sample.Structures;

namespace C4Sharp.Sample.Diagrams
{
    using static Containers;
    using static Components;

    using static DynamicDiagram;

    internal class DynamicDiagramBuilder
    {
        public static DynamicDiagram Build()
        {
            return new()
            {
                ShowLegend = true,
                Title = "Dynamic diagram for Internet Banking System Authentication",
                Structures = new Structure[]
                {
                    Spa,
                    ApiApplicationBoundary(),
                    SqlDatabase,
                },
                Relationships = new[]
                {
                    (Spa > Sign)["Submits credentials to", "JSON/HTTPS"][Position.Right],
                    (Sign > Security)["Calls isAuthenticated() on"][Index()-0],
                    (Sign > SqlDatabase)["Logs call persistent to"][LastIndex()-1],
                    (Security > SqlDatabase)["select * from users where username = ?", "JDBC"][Position.Right],
                }
            };
        }

        private static ContainerBoundary ApiApplicationBoundary()
        {
            return new("Container", "API Application")
            {
                Components = new[]
                {
                    Sign,
                    Security,
                },
            };
        }
    }
}
