using AutoMapper;
using Website.Articles;
using Website.Categories;
using Website.Web.Pages.Articles;

namespace Website.Web;

public class WebsiteWebAutoMapperProfile : Profile
{
    public WebsiteWebAutoMapperProfile()
    {
        //Define your AutoMapper configuration here for the Web project.
        CreateMap<CreateEditArticleViewModel,CreateUpdateArticleDto>();
        CreateMap<ArticleDto, CreateEditArticleViewModel>();
        CreateMap<CategoryDto, CreateUpdateCategoryDto>();
    }
}
