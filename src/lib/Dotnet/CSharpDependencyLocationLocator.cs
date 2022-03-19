namespace Dotnet;

public class CSharpDependencyLocationLocator
{
    public IEnumerable<DependencyWithLocation> DetermineLocationOf(
        List<ParsedDependency> dependencies
    )
    {
        return dependencies
            .Select(dependency =>
                new DependencyWithLocation(
                    dependency.Name,
                    dependency.Version,
                    new Uri($"https://www.nuget.org/packages/{dependency.Name}/{dependency.Version}")
                )
            );
    }
}
