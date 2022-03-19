namespace Dotnet.Tests.For_CSharpDependencyLocator.AddProbableLocation;

public class With_one_dependency
{
    IEnumerable<DependencyWithLocation> _dependencies;

    [SetUp]
    public void Add_probable_location_to_a_single_dependency()
    {
        var locationLocator = new CSharpDependencyLocationLocator();

        _dependencies = locationLocator.DetermineLocationOf(
            new List<ParsedDependency>()
            {
                new("name", "1.0.2")
            }
        );
    }

    [Test]
    public void There_is_one_dependency() => _dependencies.Count().ShouldBe(1);

    [Test]
    public void Did_not_change_the_name() => _dependencies.Single().Name.ShouldBe("name");

    [Test]
    public void Did_not_change_the_version() => _dependencies.Single().Version.ShouldBe("1.0.2");

    [Test]
    public void Set_the_expected_location() =>
        _dependencies
            .Single()
            .PackageResourceLocation
            .ShouldBe(
                new Uri("https://www.nuget.org/packages/name/1.0.2")
            );
}
