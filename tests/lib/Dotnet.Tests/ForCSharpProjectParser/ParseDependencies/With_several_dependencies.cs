namespace Dotnet.Tests.For_CSharpProjectParser.ParseDependencies;

public class With_several_dependencies
{
    IEnumerable<ParsedDependency> _dependencies;

    [SetUp]
    public void Parse_a_project_xml_with_several_package_references()
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
    <PackageReference Include=""coverlet.msbuild"" Version=""3.1.2"">
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
      <PrivateAssets>all</PrivateAssets>
    </PackageReference>
    <PackageReference Include=""Microsoft.NET.Test.Sdk"" Version=""16.11.0"" />
    <PackageReference Include=""NUnit"" Version=""3.13.2"" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include=""NUnit3TestAdapter"" Version=""4.0.0"" />
    <PackageReference Include=""coverlet.collector"" Version=""3.1.0"" />
    <PackageReference Include=""shouldly"" Version=""4.0.3"" />
    <PackageReference WillNotBeReturned=""because it's wrong""/>
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include=""..\..\..\src\lib\Dotnet\Dotnet.csproj"" />
  </ItemGroup>

</Project>"
        );
    }

    [Test]
    public void There_are_six_dependencies() => _dependencies.Count().ShouldBe(6);

    [Test]
    public void Depends_on_coverlet_msbuild() =>
        _dependencies
            .Select(_ => _.Name)
            .ShouldContain("coverlet.msbuild");

    [Test]
    public void Depends_on_microsoft_net_test_sdk() =>
        _dependencies
            .Select(_ => _.Name)
            .ShouldContain("Microsoft.NET.Test.Sdk");

    [Test]
    public void Depends_on_nunit() =>
        _dependencies
            .Select(_ => _.Name)
            .ShouldContain("NUnit");

    [Test]
    public void Depends_on_nunit3_test_adapter() =>
        _dependencies
            .Select(_ => _.Name)
            .ShouldContain("NUnit3TestAdapter");

    [Test]
    public void Depends_on_coverlet_collector() =>
        _dependencies
            .Select(_ => _.Name)
            .ShouldContain("coverlet.collector");

    [Test]
    public void Depends_on_shouldly() =>
        _dependencies
            .Select(_ => _.Name)
            .ShouldContain("shouldly");

    [Test]
    public void Does_not_depend_on_a_package_that_is_not_in_the_project() =>
        _dependencies
            .Select(_ => _.Name)
            .ShouldNotContain("will_not_be_returned");
}
