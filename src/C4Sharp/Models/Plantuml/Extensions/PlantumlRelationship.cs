using System;
using System.Linq;
using C4Sharp.Diagrams.Supplementary;
using C4Sharp.Models.Relationships;

namespace C4Sharp.Models.Plantuml.Extensions;

/// <summary>
/// Parser Relationship to PlantUML
/// </summary>
internal static class PlantumlRelationship
{
    /// <summary>
    /// Create PUML content from Relationship
    /// </summary>
    /// <param name="relationship"></param>
    /// <returns></returns>        
    public static string ToPumlString(this Relationship relationship)
    {
        var direction = relationship.ToPumlDirection();
        var position = relationship.Position.ToPumlPosition();
        var index = relationship.IndexExpression?.ToPumlIndexParameter();
        var protocol = relationship.Protocol.ToPumlProtocolParameter();
        var tags = relationship.Tags.ToPumlTagsParameter();

        return $"{direction}{position}({index}{relationship.From}, {relationship.To}, \"{relationship.Label}\"{protocol}{tags})";
    }

    private static string ToPumlDirection(this Relationship relationship)
    {
        var isIndexRelationship = relationship.IndexExpression is not null;
        var baseDirection = isIndexRelationship ? "RelIndex" : "Rel";

        return relationship.Direction switch
        {
            Direction.Back => $"{baseDirection}_Back",
            Direction.Forward => baseDirection,
            Direction.Bidirectional => isIndexRelationship
                ? throw new NotSupportedException("A bidirectional relationship is not supported when using index functions")
                : "BiRel",
            _ => baseDirection,
        };
    }

    private static string ToPumlPosition(this Position position)
        => position switch
        {
            Position.Down => "_D",
            Position.Up => "_U",
            Position.Left => "_L",
            Position.Right => "_R",
            Position.Neighbor => "_Neighbor",
            Position.None => "",
            _ => ""
        };


    private static string ToPumlIndexParameter(this string indexExpression)
    {
        if (string.IsNullOrEmpty(indexExpression))
        {
            return string.Empty;
        }

        // if using static is not used to import the Functions there may be DynamicDiagram in
        // the Expression which needs to be removed
        indexExpression = indexExpression.Replace($"{nameof(DynamicDiagram)}.", string.Empty);
        return $"{indexExpression}, ";
    }

    private static string ToPumlProtocolParameter(this string protocol)
        => !string.IsNullOrEmpty(protocol)
            ? $", \"{protocol}\""
            : string.Empty;

    private static string ToPumlTagsParameter(this string[] tags)
        => tags.Any()
            ? $", $tags={string.Join("+", tags)}"
            : string.Empty;
}