using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithFiltersForCountSpecification : BaseSpecification<Product>
    {
       // obținerea numărului de articole, astfel încât să  putem 
       // popula clasa noastră de paginare
        public ProductWithFiltersForCountSpecification(ProductSpecParams productParams)
        : base(x => 
         (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search))
          &&
         (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
         (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
        )
        {

        }
    }
}