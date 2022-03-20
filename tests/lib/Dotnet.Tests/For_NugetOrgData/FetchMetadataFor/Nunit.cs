namespace Dotnet.Tests.For_NugetOrgData.FetchMetadataFor;

public class Nunit
{
    [Test]
    // [Category("Integration")]
    public async Task There_is_metadata_for_nunit()
    {
        var data = new NugetOrgData();

        var packages = await data.FetchMetadataFor("nunit");

        packages.Any().ShouldBeTrue();
    }
}