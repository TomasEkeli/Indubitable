namespace Dotnet.Tests.ForProjectReader.ListDependencies;
public class WithOneDependency
{
    IEnumerable<Dependency> _dependencies;

    [SetUp]
    public void Setup()
    {
        var projectReader = new ProjectReader();
        _dependencies = projectReader.ListDependencies(
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
    public void ThereIsOneDependency() => _dependencies.Count().ShouldBe(1);

    [Test]
    public void TheDependencyIsDotnetTest() => _dependencies.Single().Name.ShouldBe("NUnit");

    [Test]
    public void TheDependencyIsOfTheCorrectVersion() => _dependencies.Single().Version.ShouldBe("3.13.2");
}