namespace Dotnet.Tests.ForProjectReader.ListDependencies;

public class WithSeveralDependencies
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
    public void ThereAreSixDependencies() => _dependencies.Count().ShouldBe(6);

    [Test]
    public void DependsOnCoverletMsBuild() =>
        _dependencies
            .Select(_ => _.Name)
            .ShouldContain("coverlet.msbuild");

    [Test]
    public void DependsOnNUnit() =>
        _dependencies
            .Select(_ => _.Name)
            .ShouldContain("NUnit");

    [Test]
    public void DependsOnNUnit3TestAdapter() =>
        _dependencies
            .Select(_ => _.Name)
            .ShouldContain("NUnit3TestAdapter");

    [Test]
    public void DependsOnCoverletCollector() =>
        _dependencies
            .Select(_ => _.Name)
            .ShouldContain("coverlet.collector");

    [Test]
    public void DependsOnShouldly() =>
        _dependencies
            .Select(_ => _.Name)
            .ShouldContain("shouldly");

    [Test]
    public void DependsOnDotnetTestSdk() =>
        _dependencies
            .Select(_ => _.Name)
            .ShouldContain("Microsoft.NET.Test.Sdk");
}
