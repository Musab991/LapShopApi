using BuisnessLibrary.Dto.Item;
using DomainLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BuisnessLibrary.Dto.Category
{
    public static  class CategoryDtoMethods
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
