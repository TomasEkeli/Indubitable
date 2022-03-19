namespace Dotnet.Tests.ForNugetOrgDataFetcher.FetchMetadata;

public class With_several_dependencies
{
    IEnumerable<DependencyWithMetadata> _dependencies;

    [SetUp]
    public async Task Fetch_metadata_for_a_list_of_dependencies()
    {
        var dataFetcher = new NugetOrgDataFetcher();

        _dependencies = await dataFetcher.FetchMetadataFor(
            new List<DependencyWithLocation>()
            {
                new(
                    "nunit",
                    "3.13.2",
                    new Uri("https://www.nuget.org/packages/nunit/3.13.2")
                ),
                new(
                    "shouldly",
                    "4.0.3",
                    new Uri("https://www.nuget.org/packages/shouldly/4.0.3")
                ),
                new(
                    "coverlet.collector",
                    "3.1.0",
                    new Uri("https://www.nuget.org/packages/coverlet.collector/3.1.0")
                )
            }
        );
    }

    [Test]
    public void Has_three_dependencies() => _dependencies.Count().ShouldBe(3);
}