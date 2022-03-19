using System.Xml;
using System.Xml.Linq;

namespace Dotnet;

public class CSharpProjectFileParser
{
    public IEnumerable<ParsedDependency> ParseDependencies(string projectFileContents)
    {
        if (string.IsNullOrWhiteSpace(projectFileContents))
        {
            return Enumerable.Empty<ParsedDependency>();
        }

        try
        {
            var xml = XDocument.Parse(projectFileContents);

            var packageReferences = xml.Descendants("PackageReference");

            return packageReferences
                .Where(x => x.Attribute("Include") != null && x.Attribute("Version") != null)
                .Select(packageReference =>
                    new ParsedDependency(
                        packageReference.Attribute("Include")!.Value,
                        packageReference.Attribute("Version")!.Value
                    )
                );
        }
        catch (XmlException)
        {
            return Enumerable.Empty<ParsedDependency>();
        }
    }
}
