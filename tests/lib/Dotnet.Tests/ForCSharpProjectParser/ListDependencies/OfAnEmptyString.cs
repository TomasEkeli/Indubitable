namespace Dotnet.Tests.ForCSharpProjectParser.ListDependencies;

public class OfAnEmptyString
{
    IEnumerable<ParsedDependency> _dependencies;

    [SetUp]
    public void Setup()
    {
        var projectReader = new CSharpProjectFileParser();
        _dependencies = projectReader.ParseDependencies("");
    }

    [Test]
    public void ThereAreNoDependencies() => _dependencies.Any().ShouldBeFalse();
}
