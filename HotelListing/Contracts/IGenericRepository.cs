namespace HotelListing.Contracts
{
    public partial interface IGenericRepository<T>where T : class
    {
        Task<T> GetAsync(int? id);
        Task<List<T>> GetAllAsync();

        Task<T>AddAsync(T entity);

        Task<T>DeleteAsync(int? id);
        Task<T> UpdateAsync(T entity);

        Task<bool> Exists(int id);

    }
}
