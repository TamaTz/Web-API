using ImplementConsumeAPI.ViewModels;

namespace ImplementConsumeAPI.Repositories.Interface
{
    public interface IRepository<T, X> where T : class
    {
        Task<ResponseListVM<T>> Get();
        Task<ResponseViewModel<T>> Get(X Guid);
        Task<ResponseMessageVM> Post(T AllEntity);
        Task<ResponseMessageVM> Put(T AllEntity);
        Task<ResponseMessageVM> Delete(X Guid);
    }
}
