namespace Dotnet.Tests.For_NugetOrgData.GetSourceUrlFor;

public class Versions_that_do_not_exist
{
    [Test]
    [Category("Integration")]
    [TestCase("nunit", "833.13.2")]
    [TestCase("newtonsoft.json", "513.0.1")]
    public async Task There_is_no_metadata_for_versions_that_do_not_exist(
        string packageId,
        string version)
    {
        var data = new NugetOrgData();

        var packages = await data.GetSourceUrlFor(new (packageId, version));

        packages.ShouldBeNull();
    }
}
