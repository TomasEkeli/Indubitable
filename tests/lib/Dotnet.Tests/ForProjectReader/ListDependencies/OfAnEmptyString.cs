namespace Dotnet.Tests.ForProjectReader.ListDependencies;

public class OfAnEmptyString
{
    IEnumerable<Dependency> _dependencies;

    [SetUp]
    public void Setup()
    {
        var projectReader = new ProjectReader();
        _dependencies = projectReader.ListDependencies("");
    }

    [Test]
    public void ThereAreNoDependencies() => _dependencies.Any().ShouldBeFalse();
}