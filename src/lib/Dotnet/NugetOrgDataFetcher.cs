using NuGet.Common;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;

namespace Dotnet;

public class NugetOrgDataFetcher
{

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
        var cache = new SourceCacheContext();
        var repository = Repository.Factory.GetCoreV3("https://api.nuget.org/v3/index.json");
        var resource = await repository.GetResourceAsync<PackageMetadataResource>();

        var packages = await resource.GetMetadataAsync(
            dependency.Name,
            true,
            false,
            cache,
            NullLogger.Instance,
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
            dependency.Name,
            dependency.Version,
            dependency.PackageResourceLocation,
            package?.PackageDetailsUrl,
            package?.LicenseUrl,
            latestVersion?.Identity.Version.ToString(),
            package?.Published,
            latestVersion?.Published,
            owners ?? Enumerable.Empty<Owner>(),
            authors ?? Enumerable.Empty<Author>()
        );

    }
}
