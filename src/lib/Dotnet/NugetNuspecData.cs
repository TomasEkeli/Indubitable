namespace Dotnet;

public class NugetNuspecData
{
    // readonly INugetOrgData _nugetOrgData;

    public NugetNuspecData(INugetOrgData nugetOrgData)
    {
        // _nugetOrgData = nugetOrgData;
    }

    public IEnumerable<Dependency> GetSourceUrl(
        List<DependencyWithMetadata> dependencies
    )
    {
        return dependencies
            .Select(dependency =>
                new Dependency(
                    Name: dependency.Name,
                    Version: dependency.Version,
                    PackageResourceLocation: dependency.PackageResourceLocation,
                    ProjectWebSite: dependency.ProjectWebSite,
                    LicenseUrl: dependency.LicenseUrl,
                    LatestVersion: dependency.LatestVersion,
                    VersionPublished: dependency.VersionPublished,
                    LatestVersionPublished: dependency.LatestVersionPublished,
                    Owners: dependency.Owners,
                    Authors: dependency.Authors,
                    SourceUrl: dependency.PackageResourceLocation
                )
            );
    }
}