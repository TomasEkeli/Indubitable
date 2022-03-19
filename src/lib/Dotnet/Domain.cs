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
    Uri PackagePage,
    Uri ProjectWebSite,
    Uri Source,
    License License,
    string LatestVersion,
    DateTimeOffset VersionPublished,
    DateTimeOffset LatestVersionPublished,
    IEnumerable<Owner> Owners,
    IEnumerable<Author> Authors);

public record License(string Name, Uri Url);

public record Person(string Name, IEnumerable<Uri> Links);

public record Owner(string Name, IEnumerable<Uri> Links) : Person(Name, Links);

public record Author(string Name, IEnumerable<Uri> Links) : Person(Name, Links);