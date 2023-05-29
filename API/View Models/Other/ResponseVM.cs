namespace API.View_Models.Other
{
    public class ResponseVM<AllEntity>
    {
        public int Code { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public AllEntity? Data { get; set; }
    }
}
