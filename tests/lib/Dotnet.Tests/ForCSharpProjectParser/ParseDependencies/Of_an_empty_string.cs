namespace Dotnet.Tests.For_CSharpProjectParser.ParseDependencies;

public class Of_an_empty_string
{
    IEnumerable<ParsedDependency> _dependencies;

    [SetUp]
    public void Parse_the_empty_string()
    {
        var projectReader = new CSharpProjectFileParser();
        _dependencies = projectReader.ParseDependencies("");
    }

    [Test]
    public void Has_no_dependencies() => _dependencies.Any().ShouldBeFalse();
}
