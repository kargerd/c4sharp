using C4Sharp.Diagrams.Supplementary;
using C4Sharp.IntegratedTests.Stubs.Models;
using C4Sharp.Models;
using C4Sharp.Models.Relationships;

using static C4Sharp.Diagrams.Supplementary.DynamicDiagram;

namespace C4Sharp.IntegratedTests.Stubs.Diagrams
{
    public static class DynamicDiagramBuilder
    {
        public static DynamicDiagram Build()
        {
            return new()
            {
                ShowLegend = true,
                Title = "Dynamic diagram for Internet Banking System Authentication",
                Structures = new Structure[]
                {
                    Containers.Spa,
                    ApiApplicationBoundary(),
                    Containers.SqlDatabase,
                },
                Relationships = new[]
                {
                    (Containers.Spa > Components.Sign)["Submits credentials to", "JSON/HTTPS"][Position.Right],
                    (Components.Sign > Components.Security)["Calls isAuthenticated() on"][Index()-0],
                    (Components.Sign > Containers.SqlDatabase)["Logs call persistent to"][LastIndex()-1],
                    (Components.Security > Containers.SqlDatabase)["select * from users where username = ?", "JDBC"][Position.Right],
                }
            };
        }

        private static ContainerBoundary ApiApplicationBoundary()
        {
            return new("Container", "API Application")
            {
                Components = new[]
                {
                    Components.Sign,
                    Components.Security,
                },
            };
        }
    }
}
