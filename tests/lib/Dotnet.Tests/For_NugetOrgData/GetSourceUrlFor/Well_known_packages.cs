namespace Dotnet.Tests.For_NugetOrgData.GetSourceUrlFor;

public class Well_known_packages
{
    [Test]
    [Category("Integration")]
    [TestCase("nunit", "3.13.2", "https://github.com/nunit/nunit")]
    [TestCase("newtonsoft.json", "13.0.1", "https://github.com/JamesNK/Newtonsoft.Json")]
    public async Task There_is_metadata_for_well_known_packages(
        string packageId,
        string version,
        string expected_source_url)
    {
        var data = new NugetOrgData();

        var packages = await data.GetSourceUrlFor(new (packageId, version));

        packages.ShouldBe(new Uri(expected_source_url));
    }
}
