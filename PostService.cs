namespace demowithsieve;

public class PostService(MyDbContext ctx) : IPostService
{
    public async Task<List<Post>> GetPosts()
    {
        IQueryable<Post> query = ctx.Posts;
        
    
        return query.ToList();
    }
}

public interface IPostService
{
    Task<List<Post>> GetPosts();
}