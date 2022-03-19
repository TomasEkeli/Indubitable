using NuGet.Packaging;
using NuGet.Packaging.Core;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;

namespace Dotnet.Tests.ForNugetOrgDataFetcher.FetchMetadata;

public class TestPackageSearchMetadata : IPackageSearchMetadata
{
    public string Authors { get; set; }

    public IEnumerable<PackageDependencyGroup> DependencySets { get; set; }

    public string Description { get; set; }

    public long? DownloadCount { get; set; }

    public Uri IconUrl { get; set; }

    public PackageIdentity Identity { get; set; }

    public Uri LicenseUrl { get; set; }

    public Uri ProjectUrl { get; set; }

    public Uri ReadmeUrl { get; set; }

    public Uri ReportAbuseUrl { get; set; }

    public Uri PackageDetailsUrl { get; set; }

    public DateTimeOffset? Published { get; set; }

    public string Owners { get; set; }

    public bool RequireLicenseAcceptance { get; set; }

    public string Summary { get; set; }

    public string Tags { get; set; }

    public string Title { get; set; }

    public bool IsListed { get; set; }

    public bool PrefixReserved { get; set; }

    public LicenseMetadata LicenseMetadata { get; set; }

    public IEnumerable<PackageVulnerabilityMetadata> Vulnerabilities { get; set; }

    public Task<PackageDeprecationMetadata> GetDeprecationMetadataAsync()
    {
        return Task.FromResult<PackageDeprecationMetadata>(null);
    }

    public Task<IEnumerable<VersionInfo>> GetVersionsAsync()
    {
        return Task.FromResult<IEnumerable<VersionInfo>>(null);
    }
}