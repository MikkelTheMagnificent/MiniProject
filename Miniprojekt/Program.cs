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

// Middlware der kører før hver request. Sætter ContentType for alle responses til "JSON".
app.Use(async (context, next) =>
{
    context.Response.ContentType = "application/json; charset=utf-8";
    await next(context);
});



//Henter alle posts
app.MapGet("/api/posts", (DataService service) =>
{
    return service.GetPosts().Select(p => new { 
        PostId = p.PostId, 
        title = p.Title, 
        content = p.Content,
        author = p.Author,
        date = p.Date,
        upvotes = p.Upvotes,
        downvotes = p.Downvotes
    });
      
});

///Post sektion

//Henter en post ud fra id 
app.MapGet("/api/posts/{id}", (DataService service, int id) => {
    return service.GetPost(id);
});

//Poster en ny post
app.MapPost("/api/posts", (DataService service, NewPostData data) =>
{
    string result = service.CreatePost(data.PostId, data.Title, data.Content, data.Author, data.Date, data.Upvotes, data.Downvotes);
    return new { message = result };
});


///Comment sektion

//Henter comments ud fra id
app.MapGet("/api/comments/{id}", (DataService service, int id) => 
{
    return service.GetComment(id);
});

//Poster ny comment
app.MapPost("/api/comment", (DataService service, NewCommentData data) =>
{   
    string result = service.CreateComment(data.CommentId, data.Content, data.Date, data.Author, data.PostId);
    return new { message = result };
});



app.Run();




record NewPostData(int PostId, string Title, string Content, string Author, DateTime Date, int Upvotes, int Downvotes);
record NewCommentData(int CommentId, string Content, DateTime Date, string Author, int PostId);



