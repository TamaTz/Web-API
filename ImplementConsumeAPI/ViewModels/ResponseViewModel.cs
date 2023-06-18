namespace ImplementConsumeAPI.ViewModels
{
    public class ResponseViewModel<AllEntity>
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public AllEntity Data { get; set; } 
    }
}
