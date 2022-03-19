namespace Dotnet.Tests.ForCSharpDependencyLocator.AddProbableLocation;

public class ForNoDependencies
{
    IEnumerable<DependencyWithLocation> _dependencies;

    [SetUp]
    public void AddProbableLocationsToAnEmptyList()
    {
        var locationLocator = new CSharpDependencyLocationLocator();

        _dependencies = locationLocator.DetermineLocationOf(
            new List<ParsedDependency>()
        );
    }

    [Test]
    public void IsAnEmptyList() => _dependencies.Any().ShouldBeFalse();
}
