using Sieve.Models;
using Sieve.Services;

namespace demowithsieve;

public class PostService(MyDbContext ctx, ISieveProcessor processor) : IPostService
{
    public async Task<List<Post>> GetPosts(SieveModel sieveModel)
    {
        IQueryable<Post> query = ctx.Posts;
        
        //trigger all of the logic
        query = processor.Apply(sieveModel, query);

        return query.ToList();
    }
}

public interface IPostService
{
    Task<List<Post>> GetPosts(SieveModel sieveModel);
}