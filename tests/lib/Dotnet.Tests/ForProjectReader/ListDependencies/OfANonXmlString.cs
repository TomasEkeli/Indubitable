namespace Dotnet.Tests.ForProjectReader.ListDependencies;

public class OfANonXmlString
{
    IEnumerable<Dependency> _dependencies;

    [SetUp]
    public void Setup()
    {
        var projectReader = new ProjectReader();
        _dependencies = projectReader.ListDependencies("<not xml, but starts and ends like it>");
    }

    [Test]
    public void ThereAreNoDependencies() => _dependencies.Any().ShouldBeFalse();
}
