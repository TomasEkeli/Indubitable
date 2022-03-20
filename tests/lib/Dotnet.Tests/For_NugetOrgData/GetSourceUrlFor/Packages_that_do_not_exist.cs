namespace Dotnet.Tests.For_NugetOrgData.GetSourceUrlFor;

public class Packages_that_do_not_exist
{
    [Test]
    [Category("Integration")]
    [TestCase("172961B2-69C6-46A6-B6AF-93B4598EDF1B")]
    [TestCase("1761EABF-6C71-4A84-B5B3-952A6D645C64")]
    public async Task There_is_no_metadata_for_packages_that_do_not_exist(
        string packageId
    )
    {
        var data = new NugetOrgData();

        var packages = await data.GetSourceUrlFor(new (packageId, "1.0.0"));

        packages.ShouldBeNull();
    }
}