namespace Dotnet.Tests.For_CSharpDependencyLocator.AddProbableLocation;

public class With_several_dependencies
{
    IEnumerable<DependencyWithLocation> _dependencies;

    [SetUp]
    public void Add_probable_location_to_several_dependencies()
    {
        var locationLocator = new CSharpDependencyLocationLocator();

        _dependencies = locationLocator.DetermineLocationOf(
            new List<ParsedDependency>()
            {
                new("nunit", "1.0.2"),
                new("shouldly", "4.5.6"),
                new("xunit", "2.1.0")
            }
        );
    }

    [Test]
    public void There_are_three_dependencies() => _dependencies.Count().ShouldBe(3);

    [Test]
    public void Set_the_expected_location_on_nunit() =>
        _dependencies
            .First()
            .PackageResourceLocation
            .ShouldBe(
                new Uri("https://www.nuget.org/packages/nunit/1.0.2")
            );

    [Test]
    public void Set_the_expected_location_on_shouldly() =>
        _dependencies
            .Skip(1)
            .First()
            .PackageResourceLocation
            .ShouldBe(
                new Uri("https://www.nuget.org/packages/shouldly/4.5.6")
            );

    [Test]
    public void Set_the_expected_location_on_xunit() =>
        _dependencies
            .Skip(2)
            .First()
            .PackageResourceLocation
            .ShouldBe(
                new Uri("https://www.nuget.org/packages/xunit/2.1.0")
            );
}
