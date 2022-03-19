namespace Dotnet.Tests.For_CSharpProjectParser.ParseDependencies;

public class Of_a_non_xml_string
{
    IEnumerable<ParsedDependency> _dependencies;

    [SetUp]
    public void Parse_a_string_that_is_not_xml()
    {
        var projectReader = new CSharpProjectFileParser();
        _dependencies = projectReader.ParseDependencies("<not xml, but starts and ends like it>");
    }

    [Test]
    public void Has_no_dependencies() => _dependencies.Any().ShouldBeFalse();
}
