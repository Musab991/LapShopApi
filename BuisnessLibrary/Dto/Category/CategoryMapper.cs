using DomainLibrary.Entities;


namespace BuisnessLibrary.Dto.Category
{
    public static  class CategoryMapper
    {
        public static IEnumerable<CategoryDto> convertCategoriesToListOfCategoriesDto(IEnumerable<TbCategory> listCategories)
        {
            if (listCategories.Any())
            {


                return listCategories.Select(categ => new CategoryDto
                {
                    CategoryId = categ.CategoryId,
                    CurrentState = categ.CurrentState,
                    CategoryName = categ.CategoryName,
                    ImageName= categ.ImageName
                });
            }
            return null;

        }
        public static CategoryDto convertCategoryToCategoryDto(TbCategory category)
        {
            if (category != null)
            {

                CategoryDto categoryDto = new CategoryDto()
                {
                    CategoryId = category.CategoryId,
                    CurrentState = category.CurrentState,
                    CategoryName = category.CategoryName,
                    ImageName = category.ImageName
                };

                return categoryDto;
            }
            return null;

        }
    }
}
