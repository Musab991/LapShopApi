namespace LapShop.Model.Api
{
    public class ApiResponse<T>
    {

        public T Data { get; set; }
        public string ResponseStatus { get; set; } = null!;
        public IEnumerable<string> listErrors{ get; set; }


    }
}
