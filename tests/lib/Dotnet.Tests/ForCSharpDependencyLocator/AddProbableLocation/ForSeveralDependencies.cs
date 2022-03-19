namespace Dotnet.Tests.ForCSharpDependencyLocator.AddProbableLocation;

public class ForSeveralDependencies
{
    IEnumerable<DependencyWithLocation> _dependencies;

    [SetUp]
    public void AddProbableLocationsToAnEmptyList()
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
    public void ThereAreThreeDependencies() => _dependencies.Count().ShouldBe(3);

    [Test]
    public void DidNotChangedName() => _dependencies.First().Name.ShouldBe("nunit");

    [Test]
    public void DidNotChangedVersion() => _dependencies.First().Version.ShouldBe("1.0.2");

    [Test]
    public void SetTheExpectedLocation() =>
        _dependencies
            .First()
            .Location
            .ShouldBe(
                new Uri("https://www.nuget.org/packages/nunit/1.0.2")
            );

    [Test]
    public void SetsTheExpectedLocationOnShouldly() =>
        _dependencies
            .Skip(1)
            .First()
            .Location
            .ShouldBe(
                new Uri("https://www.nuget.org/packages/shouldly/4.5.6")
            );

    [Test]
    public void SetsTheExpectedLocationOnXunit() =>
        _dependencies
            .Skip(2)
            .First()
            .Location
            .ShouldBe(
                new Uri("https://www.nuget.org/packages/xunit/2.1.0")
            );
}