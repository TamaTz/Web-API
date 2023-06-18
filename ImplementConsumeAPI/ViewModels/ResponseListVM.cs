namespace ImplementConsumeAPI.ViewModels
{
    public class ResponseListVM<AllEntity>
    {
        public string StatusCode { get; set; }
        public string Message { get; set; }
        public  List<AllEntity>? Data { get; set;}
    }
}
