using Microsoft.AspNetCore.Routing.Patterns;
using System.Text;

var template =@"weather/{city:regex(^0\d{{2,3}}$)=010}/{days:int:range(1,4)=4}/{detailed?}";
var pattern = RoutePatternFactory.Parse(
    pattern: template,
    defaults: null,
    parameterPolicies: null,
    requiredValues: new { city = "010", days = 4 });

var app = WebApplication.Create();
app.MapGet("/", () => Format(pattern));
app.Run();

static string Format(RoutePattern pattern)
{
    var builder = new StringBuilder();
    builder.AppendLine($"RawText:{pattern.RawText}");
    builder.AppendLine($"InboundPrecedence:{pattern.InboundPrecedence}");
    builder.AppendLine($"OutboundPrecedence:{pattern.OutboundPrecedence}");
    var segments = pattern.PathSegments;
    builder.AppendLine("Segments");
    foreach (var segment in segments)
    {
        foreach (var part in segment.Parts)
        {
            builder.AppendLine($"\t{ToString(part)}");
        }
    }
    builder.AppendLine("Defaults");
    foreach (var @default in pattern.Defaults)
    {
        builder.AppendLine($"\t{@default.Key} = {@default.Value}");
    }

    builder.AppendLine("ParameterPolicies ");
    foreach (var policy in pattern.ParameterPolicies)
    {
        builder.AppendLine($"\t{policy.Key} = {string.Join(',',policy.Value.Select(it => it.Content))}");
    }

    builder.AppendLine("RequiredValues");
    foreach (var required in pattern.RequiredValues)
    {
        builder.AppendLine($"\t{required.Key} = {required.Value}");
    }

    return builder.ToString();

    static string ToString(RoutePatternPart part)
        => part switch
        {
            RoutePatternLiteralPart literal => $"Literal: {literal.Content}",
            RoutePatternSeparatorPart separator => $"Separator: {separator.Content}",
            RoutePatternParameterPart parameter => @$"Parameter: Name = {parameter.Name}; Default = {parameter.Default}; IsOptional = { parameter.IsOptional}; IsCatchAll = { parameter.IsCatchAll};ParameterKind = { parameter.ParameterKind}",
            _ => throw new ArgumentException("Invalid RoutePatternPart.")
        };
}
