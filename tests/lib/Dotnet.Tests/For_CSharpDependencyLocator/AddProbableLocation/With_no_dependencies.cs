namespace Dotnet.Tests.For_CSharpDependencyLocator.AddProbableLocation;

public class With_no_dependencies
{
    IEnumerable<DependencyWithLocation> _dependencies;

    [SetUp]
    public void Adding_probable_location_to_an_empty_list()
    {
        var locationLocator = new CSharpDependencyLocationLocator();

        _dependencies = locationLocator.DetermineLocationOf(
            new List<ParsedDependency>()
        );
    }

    [Test]
    public void Is_an_empty_list() => _dependencies.Any().ShouldBeFalse();
}
