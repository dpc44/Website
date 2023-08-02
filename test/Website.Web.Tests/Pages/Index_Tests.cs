using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Website.Pages;

public class Index_Tests : WebsiteWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
