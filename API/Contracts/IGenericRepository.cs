﻿namespace API.Contracts
{
    public interface IGenericRepository<AllEntity> where AllEntity : class
    {
        AllEntity? Create(AllEntity entity);
        bool Update(AllEntity entity);
        bool Delete(Guid guid);
        IEnumerable<AllEntity> GetAll();
        AllEntity? GetByGuid(Guid guid);

    }
}
