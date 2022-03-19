namespace Dotnet;

public record ParsedDependency(string Name, string Version);

public record DependencyWithLocation(string Name, string Version, Uri Location);

public record DependencyWithLocationAndSource(
    string Name,
    string Version,
    Uri Location,
    Uri Source);

public record Dependency(
    string Name,
    string Version,
    Uri Location,
    Uri Source,
    IEnumerable<Owner> Owners);

public record Owner(string Name, IEnumerable<Uri> Links);