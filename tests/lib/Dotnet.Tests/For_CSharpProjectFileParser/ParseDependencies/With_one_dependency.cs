namespace Dotnet.Tests.For_CSharpProjectParser.ParseDependencies;
public class With_one_dependency
{
    IEnumerable<ParsedDependency> _dependencies;

    [SetUp]
    public void Parse_a_project_xml_with_one_package_reference()
    {
        var projectReader = new CSharpProjectFileParser();
        _dependencies = projectReader.ParseDependencies(
@"<Project Sdk=""Microsoft.NET.Sdk"">

  <PropertyGroup>
    <TargetFramework>net6.0</TargetFramework>
    <Nullable>enable</Nullable>

    <IsPackable>false</IsPackable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include=""NUnit"" Version=""3.13.2"" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include=""..\..\..\src\lib\Dotnet\Dotnet.csproj"" />
  </ItemGroup>

</Project>"
        );
    }

    [Test]
    public void Has_one_dependency() => _dependencies.Count().ShouldBe(1);

    [Test]
    public void The_dependency_is_nunit() => _dependencies.Single().Name.ShouldBe("NUnit");

    [Test]
    public void The_dependency_version_is_correct() =>
        _dependencies
            .Single()
            .Version
            .ShouldBe("3.13.2");
}