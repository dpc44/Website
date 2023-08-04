using AutoMapper;
using Volo.Abp.Identity;
using Website.Articles;
using Website.Categories;
using Website.Files;
using Website.Users;

namespace Website;

public class WebsiteApplicationAutoMapperProfile : Profile
{
    public WebsiteApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Article, ArticleDto>();
        CreateMap<Category, CategoryLookUpDto>();
        CreateMap<User, UserLookUpDto>();
        CreateMap<IdentityUser, UserLookUpDto>();
        CreateMap<CreateUpdateArticleDto, Article>();
        CreateMap<CreateUpdateCategoryDto, Category>();
        CreateMap<Category, CategoryDto>();
        CreateMap<CSV, CSVDto>();
        CreateMap<CSVDto, CSV>();
        CreateMap<TableListDto, TableList>();
    }
}
