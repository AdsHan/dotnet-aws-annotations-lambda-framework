using Lambda.Products.Function;

namespace Lambda.Products.Data.Repositories
{
    public abstract class BaseRepository
    {
        protected BaseResult BaseResult;

        protected BaseRepository()
        {
            BaseResult = new BaseResult();
        }

        protected void AddError(string message)
        {
            BaseResult.Errors.Add(message);
        }
    }
}
