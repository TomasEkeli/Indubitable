using NuGet.Protocol.Core.Types;

namespace Dotnet.Tests.ForNugetOrgDataFetcher.FetchMetadata;

public class With_several_dependencies
{
    IEnumerable<DependencyWithMetadata> _dependencies;

    [SetUp]
    public async Task Fetch_metadata_for_a_list_of_dependencies()
    {
        var nugetOrgData = Substitute.For<INugetOrgData>();
        nugetOrgData
            .FetchMetadataFor("nunit")
            .Returns(new IPackageSearchMetadata[]
                {
                    new TestPackageSearchMetadata
                    {
                        Authors = "george, ringo",
                        Owners = "john, paul",
                        Identity = new("nunit", new("3.13.2")),
                        Published = new(2018, 1, 1, 0, 0, 0, TimeSpan.Zero),
                        PackageDetailsUrl = new("https://github.com/nunit/nunit"),
                    },
                    new TestPackageSearchMetadata
                    {
                        Authors = "george, ringo",
                        Owners = "john, paul",
                        Identity = new("nunit", new("4.0.1")),
                        Published = new(2019, 1, 1, 0, 0, 0, TimeSpan.Zero),
                    },
                }
            );

        nugetOrgData
            .FetchMetadataFor("shouldly")
            .Returns(new IPackageSearchMetadata[]
            {
                new TestPackageSearchMetadata
                {
                    Authors = "gamma, helm",
                    Owners = "johnsson, vlissides",
                    Identity = new("shouldly", new("3.0.1")),
                    Published = new(2018, 1, 1, 0, 0, 0, TimeSpan.Zero),
                    PackageDetailsUrl = new("https://github.com/shouldly/shouldly"),
                },
                new TestPackageSearchMetadata
                {
                    Authors = "gamma, helm",
                    Owners = "johnsson, vlissides",
                    Identity = new("shouldly", new("4.0.3")),
                    Published = new(2021, 1, 1, 0, 0, 0, TimeSpan.Zero),
                },
            });

        var dataFetcher = new NugetOrgDataMetadataFetcher(nugetOrgData);

        _dependencies = await dataFetcher.FetchMetadataFor(
            new List<DependencyWithLocation>()
            {
                new(
                    "nunit",
                    "3.13.2",
                    new("https://www.nuget.org/packages/nunit/3.13.2")
                ),
                new(
                    "shouldly",
                    "4.0.3",
                    new("https://www.nuget.org/packages/shouldly/4.0.3")
                ),
                new(
                    "coverlet.collector",
                    "3.1.0",
                    new("https://www.nuget.org/packages/coverlet.collector/3.1.0")
                )
            }
        );


    }

    [Test]
    public void Has_three_dependencies() => _dependencies.Count().ShouldBe(3);

    [Test]
    public void Nunit_has_two_authors() =>
        _dependencies
            .First(d => d.Name == "nunit")
            .Authors
            .Count()
            .ShouldBe(2);

    [Test]
    public void Nunit_has_author_george() =>
        _dependencies
            .First(d => d.Name == "nunit")
            .Authors
            .Select(_ => _.Name)
            .ShouldContain("george");

    [Test]
    public void Nunit_has_author_ringo() =>
        _dependencies
            .First(d => d.Name == "nunit")
            .Authors
            .Select(_ => _.Name)
            .ShouldContain("ringo");

    [Test]
    public void Nunit_has_ringo_owners() =>
        _dependencies
            .First(d => d.Name == "nunit")
            .Owners
            .Count()
            .ShouldBe(2);

    [Test]
    public void Nunit_has_owner_john() =>
        _dependencies
            .First(d => d.Name == "nunit")
            .Owners
            .Select(_ => _.Name)
            .ShouldContain("john");

    [Test]
    public void Nunit_has_owner_paul() =>
        _dependencies
            .First(d => d.Name == "nunit")
            .Owners
            .Select(_ => _.Name)
            .ShouldContain("paul");

    [Test]
    public void Nunit_has_a_version_of_3_13_2() =>
        _dependencies
            .First(d => d.Name == "nunit")
            .Version
            .ShouldBe("3.13.2");

    [Test]
    public void Nunit_has_a_latest_version_of_4_0_1() =>
        _dependencies
            .First(d => d.Name == "nunit")
            .LatestVersion
            .ShouldBe("4.0.1");

    [Test]
    public void Nunit_has_a_published_time_as_expected() =>
        _dependencies
            .First(d => d.Name == "nunit")
            .VersionPublished
            .ShouldBe(new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Utc));

    [Test]
    public void Nunit_has_a_latest_published_time_as_expected() =>
        _dependencies
            .First(d => d.Name == "nunit")
            .LatestVersionPublished
            .ShouldBe(new DateTime(2019, 1, 1, 0, 0, 0, DateTimeKind.Utc));

    [Test]
    public void Nunit_has_a_project_website_as_expected() =>
        _dependencies
            .First(d => d.Name == "nunit")
            .ProjectWebSite
            .ShouldBe(new Uri("https://github.com/nunit/nunit"));

    [Test]
    public void Nunit_has_a_package_resource_location_unchanged() =>
        _dependencies
            .First(d => d.Name == "nunit")
            .PackageResourceLocation
            .ShouldBe(new Uri("https://www.nuget.org/packages/nunit/3.13.2"));

    [Test]
    public void Shouldly_has_two_authors() =>
        _dependencies
            .First(d => d.Name == "shouldly")
            .Authors
            .Count()
            .ShouldBe(2);



}
