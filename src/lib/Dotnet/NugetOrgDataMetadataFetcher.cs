using NuGet.Common;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;

namespace Dotnet;

public class NugetOrgDataMetadataFetcher
{
    readonly INugetOrgData _nugetOrgData;

    public NugetOrgDataMetadataFetcher(INugetOrgData nugetOrgData)
    {
        _nugetOrgData = nugetOrgData;
    }

    public async Task<IEnumerable<DependencyWithMetadata>> FetchMetadataFor(
        List<DependencyWithLocation> dependencies,
        CancellationToken cancellationToken = default
    )
    {
        var tasks = dependencies
            .Select(dependency =>
                FetchMetadataFor(dependency, cancellationToken)
            );

        var results = await Task.WhenAll(tasks);
        return results;
    }

    async Task<DependencyWithMetadata> FetchMetadataFor(
        DependencyWithLocation dependency,
        CancellationToken cancellationToken
    )
    {
        var packages = await _nugetOrgData.FetchMetadataFor(
            dependency.Name,
            cancellationToken
        );

        var package = packages.FirstOrDefault(
            p => p.Identity.Version.ToString() == dependency.Version
        );

        var latestVersion = packages.OrderByDescending(
            p => p.Identity.Version
        )
        .FirstOrDefault();

        var owners = package
            ?.Owners
            ?.Split(',')
            .Select(owner => new Owner(owner.Trim()));

        var authors = package
            ?.Authors
            ?.Split(',')
            .Select(author => new Author(author.Trim()));

        return new(
            Name: dependency.Name,
            Version: dependency.Version,
            PackageResourceLocation: dependency.PackageResourceLocation,
            ProjectWebSite: package?.PackageDetailsUrl,
            LicenseUrl: package?.LicenseUrl,
            LatestVersion: latestVersion?.Identity.Version.ToString(),
            VersionPublished: package?.Published,
            LatestVersionPublished: latestVersion?.Published,
            Owners: owners ?? Enumerable.Empty<Owner>(),
            Authors: authors ?? Enumerable.Empty<Author>()
        );

    }
}
