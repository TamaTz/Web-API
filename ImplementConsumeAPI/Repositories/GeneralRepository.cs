using ImplementConsumeAPI.Repositories.Interface;
using ImplementConsumeAPI.ViewModels;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ImplementConsumeAPI.Repositories
{
    public class GeneralRepository<AllEntity, Tid> : IRepository<AllEntity, Tid> where AllEntity : class
    {
        private readonly string request;
        private readonly HttpClient httpClient;
        private readonly IHttpContextAccessor contextAccessor;
        public GeneralRepository(string request)
        {
            this.request = request;
          /*  contextAccessor = new HttpContextAccessor();
          */  httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7065/api/")
            };
            // Ini yg bawah skip dulu
       /*     httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", contextAccessor.HttpContext?.Session.GetString("JWToken"));
        */}
        public async Task<ResponseMessageVM> Deletes(Tid Guid)
        {
            ResponseMessageVM entityVM = null;
            using (var response = httpClient.DeleteAsync(request + Guid).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseMessageVM>(apiResponse);
            }
            return entityVM;
        }

        public async Task<ResponseListVM<AllEntity>> Get()
        {
            ResponseListVM<AllEntity> entityVM = null;
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", JWTokenVM);
            using (var response = await httpClient.GetAsync(request))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseListVM<AllEntity>>(apiResponse);
            }
            return entityVM;
        }

        public async Task<ResponseViewModel<AllEntity>> Get(Tid id)
        {
            ResponseViewModel<AllEntity> allentity = null;

            using (var response = await httpClient.GetAsync(request + id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                allentity = JsonConvert.DeserializeObject<ResponseViewModel<AllEntity>>(apiResponse);
            }
            return allentity;
        }

        public async Task<ResponseMessageVM> Post(AllEntity AllEntity)
        {
            ResponseMessageVM entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(AllEntity), Encoding.UTF8, "application/json");
            using (var response = httpClient.PostAsync(request, content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseMessageVM>(apiResponse);
            }
            return entityVM;
        }

        public async Task<ResponseMessageVM> Put(AllEntity AllEntity)
        {
            ResponseMessageVM entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(AllEntity), Encoding.UTF8, "application/json");
            using (var response = httpClient.PutAsync(request, content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseMessageVM>(apiResponse);
            }
            return entityVM;
        }

    }
}
