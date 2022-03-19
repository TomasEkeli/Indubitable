namespace Dotnet;

public record ParsedDependency(string Name, string Version);

public record DependencyWithLocation(string Name, string Version, Uri PackageResourceLocation)
    : ParsedDependency(Name, Version);

public record DependencyWithMetadata(
    string Name,
    string Version,
    Uri PackageResourceLocation,
    Uri? ProjectWebSite,
    Uri? LicenseUrl,
    string? LatestVersion,
    DateTimeOffset? VersionPublished,
    DateTimeOffset? LatestVersionPublished,
    IEnumerable<Owner> Owners,
    IEnumerable<Author> Authors)
    : DependencyWithLocation(Name, Version, PackageResourceLocation);

public record Dependency(
    string Name,
    string Version,
    Uri PackageResourceLocation,
    Uri? ProjectWebSite,
    Uri? LicenseUrl,
    string? LatestVersion,
    DateTimeOffset? VersionPublished,
    DateTimeOffset? LatestVersionPublished,
    IEnumerable<Owner> Owners,
    IEnumerable<Author> Authors,
    Uri? SourceUrl)
    : DependencyWithMetadata(
        Name,
        Version,
        PackageResourceLocation,
        ProjectWebSite,
        LicenseUrl,
        LatestVersion,
        VersionPublished,
        LatestVersionPublished,
        Owners,
        Authors);



public record Owner(string Name);
public record Author(string Name);