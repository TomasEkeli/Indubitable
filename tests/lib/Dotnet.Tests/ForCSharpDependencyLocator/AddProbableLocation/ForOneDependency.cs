namespace Dotnet.Tests.ForCSharpDependencyLocator.AddProbableLocation;

public class ForOneDependency
{
    IEnumerable<DependencyWithLocation> _dependencies;

    [SetUp]
    public void AddProbableLocationsToAnEmptyList()
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
    public void ThereIsOneDependency() => _dependencies.Count().ShouldBe(1);

    [Test]
    public void DidNotChangedName() => _dependencies.Single().Name.ShouldBe("name");

    [Test]
    public void DidNotChangedVersion() => _dependencies.Single().Version.ShouldBe("1.0.2");

    [Test]
    public void SetTheExpectedLocation() => _dependencies.Single().Location.ShouldBe(
        new Uri("https://www.nuget.org/packages/name/1.0.2")
    );
}
