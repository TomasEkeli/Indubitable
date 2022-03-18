namespace Dotnet.Tests.ForCSharpProjectParser.ListDependencies;

public class OfANonXmlString
{
    IEnumerable<ParsedDependency> _dependencies;

    [SetUp]
    public void Setup()
    {
        var projectReader = new CSharpProjectFileParser();
        _dependencies = projectReader.ParseDependencies("<not xml, but starts and ends like it>");
    }

    [Test]
    public void ThereAreNoDependencies() => _dependencies.Any().ShouldBeFalse();
}
