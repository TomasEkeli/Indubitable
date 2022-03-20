namespace Dotnet.Tests.For_NugetNuspecData.GetSourceUrl;

public class With_one_dependency
{
    IEnumerable<Dependency> _packages;

    [SetUp]
    public void Get_source_url_for_one_dependency()
    {
        var dependency = new DependencyWithMetadata(
            Name: "name",
            Version: "1.0.0",
            PackageResourceLocation: new Uri("https://example.com/package.nupkg"),
            ProjectWebSite: null,
            LicenseUrl: null,
            LatestVersion: null,
            VersionPublished: null,
            LatestVersionPublished: null,
            Owners: null,
            Authors: null
        );

        var nugetOrgData = Substitute.For<INugetOrgData>();
        nugetOrgData
            .GetSourceUrlFor(new("name", "1.0.0"))
            .Returns(new Uri("https://www.github.com/name/name"));

        var data = new NugetNuspecData(nugetOrgData);

        _packages = data.GetSourceUrl(new List<DependencyWithMetadata>()
        {
            dependency
        });

    }
    [Test]
    public void Has_one_dependency() => _packages.Count().ShouldBe(1);
}