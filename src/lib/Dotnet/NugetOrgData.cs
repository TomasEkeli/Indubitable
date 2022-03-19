using NuGet.Common;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;

namespace Dotnet;

public interface INugetOrgData
{
    Task<IEnumerable<IPackageSearchMetadata>> FetchMetadataFor(
        string packageId,
        CancellationToken cancellationToken = default);
}

public class NugetOrgData : INugetOrgData
{
    readonly SourceCacheContext _cache;
    readonly SourceRepository _repository;

    public NugetOrgData()
    {
        _cache = new SourceCacheContext();
        _repository = Repository.Factory.GetCoreV3("https://api.nuget.org/v3/index.json");
    }

    public async Task<IEnumerable<IPackageSearchMetadata>> FetchMetadataFor(
        string packageId,
        CancellationToken cancellationToken = default
    )
    {
        var resource = await _repository
            .GetResourceAsync<PackageMetadataResource>(cancellationToken);

        return await resource.GetMetadataAsync(
            packageId: packageId,
            includePrerelease: true,
            includeUnlisted: true,
            sourceCacheContext: _cache,
            log: NullLogger.Instance,
            token: cancellationToken
        )
        ?? Array.Empty<IPackageSearchMetadata>();
    }
}
