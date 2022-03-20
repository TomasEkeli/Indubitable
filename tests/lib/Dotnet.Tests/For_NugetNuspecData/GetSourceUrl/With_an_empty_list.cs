namespace Dotnet.Tests.For_NugetNuspecData.GetSourceUrl;

public class With_an_empty_list
{
    [Test]
    public void Has_no_dependencies()
    {
        var nugetOrgData = Substitute.For<INugetOrgData>();

        var data = new NugetNuspecData(nugetOrgData);

        var packages = data.GetSourceUrl(new List<DependencyWithMetadata>());

        packages.Any().ShouldBeFalse();
    }
}
