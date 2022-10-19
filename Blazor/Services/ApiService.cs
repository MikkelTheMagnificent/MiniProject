using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

using shared.Model;

namespace Blazor.Data;

public class ApiService
{
    private readonly HttpClient http;
    private readonly IConfiguration configuration;
    private readonly string baseAPI = "";

    public ApiService(HttpClient http, IConfiguration configuration)
    {
        this.http = http;
        this.configuration = configuration;
        this.baseAPI = configuration["baseAPI"];
    }


    public async Task<Post[]> GetPostData()
    {
        string url = $"{baseAPI}/api/posts/";
        return await http.GetFromJsonAsync<Post[]>(url);
    }

    public async Task<Post> GetPostById(int id)
    {
        string url = $"{baseAPI}/api/posts/{id}";
        return await http.GetFromJsonAsync<Post>(url);
    }


    public async void CreatePost(Post post)
    {
        string url = $"{baseAPI}/api/post/";
        await http.PostAsJsonAsync(url, post);
    }
 
     public async Task<Comment[]> GetCommentById(int id)
    {
        string url = $"{baseAPI}/api/comments/{id}";
        return await http.GetFromJsonAsync<Comment[]>(url);
    }



//skal laves om
  public async void CreateComment(InputComment comment)
    {
        string url = $"{baseAPI}/api/comment/";
        await http.PostAsJsonAsync(url, comment);
    }



      public async void UpvotePost(int id){
      string url = $"{baseAPI}/api/upvote/post/{id}";
      await http.PutAsJsonAsync(url, " ");
      }

        public async void DownvotePost(int id){
        string url = $"{baseAPI}/api/downvote/post/{id}";
        await http.PutAsJsonAsync(url, " ");
      }

        public async void UpvoteComment(int id){
        string url = $"{baseAPI}/api/upvote/comment/{id}";
        await http.PutAsJsonAsync(url, " ");
      }

        public async void DownvoteComment(int id){
        string url = $"{baseAPI}/api/downvote/comment/{id}";
        await http.PutAsJsonAsync(url, " ");
      }


    }

    



//mangler comment, upvote og downvote