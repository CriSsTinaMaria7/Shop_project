namespace Core.Specifications
{
    public class ProductSpecParams
    {    private const  int MaxPageSize = 50;
        // in mod implicit va fi returnata prima pagina
        public int PageIndex {get;set;} = 1;
        // clientul poate deasi acest maximul de 6
        private int _pageSize = 6;
        public int PageSize 
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        public int? BrandId;
        public int? TypeId;
        public string Sort{get; set;}

        private string _search;

        public string Search {
             get => _search;
             set => _search = value.ToLower();
       }
       

    }
}