using Microsoft.AspNetCore.Mvc;

namespace demowithsieve;

public class PostController(IPostService postService) : ControllerBase
{
    [HttpGet(nameof(GetPosts))]
    public async Task<List<Post>> GetPosts()

    {
        return await postService.GetPosts();
    }
}