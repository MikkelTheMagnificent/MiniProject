using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using shared.Model;
using Data;
using Service;

var builder = WebApplication.CreateBuilder(args);

// Sætter CORS så API'en kan bruges fra andre domæner
var AllowSomeStuff = "_AllowSomeStuff";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowSomeStuff, builder => {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

// Tilføj DbContext factory som service.
builder.Services.AddDbContext<dbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ContextSQLite")));

// Tilføj DataService så den kan bruges i endpoints
builder.Services.AddScoped<DataService>();

// Dette kode kan bruges til at fjerne "cykler" i JSON objekterne.
/*
builder.Services.Configure<JsonOptions>(options =>
{
    // Her kan man fjerne fejl der opstår, når man returnerer JSON med objekter,
    // der refererer til hinanden i en cykel.
    // (altså dobbelrettede associeringer)
    options.SerializerOptions.ReferenceHandler = 
        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});
*/

var app = builder.Build();

// Seed data hvis nødvendigt.
using (var scope = app.Services.CreateScope())
{
    var dataService = scope.ServiceProvider.GetRequiredService<DataService>();
    dataService.SeedData(); // Fylder data på, hvis databasen er tom. Ellers ikke.
}

app.UseHttpsRedirection();
app.UseCors(AllowSomeStuff);


//APi endpoints

///Post sektion

//Henter alle posts (virker)
app.MapGet("/api/posts/", (DataService service) =>
{
    return service.GetPosts();
});

//Henter en post ud fra id (virker)
app.MapGet("/api/posts/{id}", (DataService service, int id) => {
    return service .GetPostById(id);
});

//Poster en ny post (virker)
app.MapPost("/api/post/", (DataService service, InputPost post) =>
{
    service.CreatePost(post);

});


///Comment sektion

app.MapGet("/api/comments/", (DataService service) =>
{
    return service.GetComments();
});

//Henter comments ud fra id (virker)
app.MapGet("/api/comments/{id}", (DataService service, int id) => 
{
    return service.GetCommentById(id);
});

//Poster ny comment (virker)
app.MapPost("/api/comment/", (DataService service, InputComment comment) =>
{   
    service.CreateComment(comment);

});

app.MapPut("/api/upvote/post/{id}", (DataService service, int id) =>
{
    service.UpvotePost(id);
});

app.MapPut("/api/downvote/post/{id}", (DataService service, int id) =>
{
    service.DownvotePost(id);
});

app.MapPut("/api/upvote/comment/{id}", (DataService service, int id) =>
{
    service.UpvoteComment(id);
});

app.MapPut("/api/downvote/comment/{id}", (DataService service, int id) =>
{
    service.DownvoteComment(id);
});

app.Run();




record NewPostData(int PostId, string Title, string Content, string Author, DateTime Date, int Upvotes, int Downvotes);
record NewCommentData(string Content, DateTime Date, string Author, int PostId);



