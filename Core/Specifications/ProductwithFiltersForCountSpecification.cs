using Core.Entities;

namespace Core.Specifications
{
    public class ProductwithFiltersForCountSpecification : BaseSpecification<Product>
    {
        public ProductwithFiltersForCountSpecification(ProductSpecParams productParams)
        : base(x =>
         (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId)
         && (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId))
        {
             
        }
    }
}