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

    public async void PostPostData(string author, string title, string content)
    {
        string url = $"{baseAPI}/api/post/";
        Post post = new Post{ Author = author, Title = title, Content = content };
        await http.PostAsJsonAsync(url, post);
    }
 
     public async Task<Comment[]> GetCommentData()
    {
        string url = $"{baseAPI}/api/comments/";
        return await http.GetFromJsonAsync<Comment[]>(url);
    }


//skal laves om
 public async Task<Comment> CreateComment(string content, int postId, string Author)
    {
        string url = $"{baseAPI}/api/posts/{postId}/comments";
     
        // Post JSON to API, save the HttpResponseMessage
        HttpResponseMessage msg = await http.PostAsJsonAsync(url, new { content, Author });

        // Get the JSON string from the response
        string json = msg.Content.ReadAsStringAsync().Result;

        // Deserialize the JSON string to a Comment object
        Comment? newComment = JsonSerializer.Deserialize<Comment>(json, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true // Ignore case when matching JSON properties to C# properties 
        });

        // Return the new comment 
        return newComment;
    }



      public async Task<Post> UpvotePost(int id)
    {
        string url = $"{baseAPI}/api/posts/{id}/upvote/";

        // Post JSON to API, save the HttpResponseMessage
        HttpResponseMessage msg = await http.PutAsJsonAsync(url, "");

        // Get the JSON string from the response
        string json = msg.Content.ReadAsStringAsync().Result;

        // Deserialize the JSON string to a Post object
        Post? updatedPost = JsonSerializer.Deserialize<Post>(json, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true // Ignore case when matching JSON properties to C# properties
        });

        // Return the updated post (vote increased)
        return updatedPost;
    }

     public async Task<Post> DownvotePost(int id)
    {
        string url = $"{baseAPI}/api/posts/{id}/downvote/";

        // Post JSON to API, save the HttpResponseMessage
        HttpResponseMessage msg = await http.PutAsJsonAsync(url, "");

        // Get the JSON string from the response
        string json = msg.Content.ReadAsStringAsync().Result;

        // Deserialize the JSON string to a Post object
        Post? updatedPost = JsonSerializer.Deserialize<Post>(json, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true // Ignore case when matching JSON properties to C# properties
        });

        // Return the updated post (vote increased)
        return updatedPost;
    }
}



//mangler comment, upvote og downvote