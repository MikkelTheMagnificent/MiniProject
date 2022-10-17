using Microsoft.EntityFrameworkCore;
using System.Text.Json;

using Data;
using shared.Model;

namespace Service;

public class DataService
{
    private dbContext db { get; }

    public DataService(dbContext db) {
        this.db = db;
    }
    /// <summary>
    /// Seeder noget nyt data i databasen hvis det er nødvendigt.
    /// </summary>
    public void SeedData() {
        
        Post post = db.posts.FirstOrDefault()!;
        if (post == null) {
            post = new Post { Title = "I'm addicted to WOW", Content = "i cant STOP, IM FUCKING ADDICTED", Author = "Tyler1", Date = DateTime.Now, Upvotes = 0, Downvotes = 0 };
            db.posts.Add(post);
            db.posts.Add(new Post { Title = "BIG DICK ENERGY", Content = "Mit nye Arcane spec rykker røvhul", Author = "Olcastnblast", Date = DateTime.Now, Upvotes = 0, Downvotes = 0 });
            db.posts.Add(new Post { Title = "DK's er den mest broken class in wow history", Content = "Jeg kommer til at skade mere end Consolelog i raids", Author = "Olcastnblast", Date = DateTime.Now, Upvotes = 0, Downvotes = 0 });
        db.SaveChanges();
        }
        

        Comment comment = db.Comments.FirstOrDefault()!;
        if (comment == null)
        { Post p1 = db.posts.FirstOrDefault()!;
            db.Comments.Add(new Comment { Content = "That is asually truf", Date = DateTime.Now, Author = "Ole", PostId = post.PostId});
            db.Comments.Add(new Comment { Content = "I luv mages", Date = DateTime.Now, Author = "Kenneth", PostId = post.PostId });
            db.Comments.Add(new Comment { Content = "I luv mages too!", Date = DateTime.Now, Author = "Svend", PostId = post.PostId});
        db.SaveChanges();
        }
        
    }


     public List<Post> GetPosts() {
        return db.posts
            .Include(c => c.Comments)
            .ToList();
    }

    public Post GetPostById(int id) {
        var post = db.posts
        .Where(p => p.PostId == id)
        .Include(c => c.Comments)
        .FirstOrDefault();
        return post;
    }

    public List<Comment> GetComments() {
        return db.Comments.ToList();
    }

    public Comment GetCommentById(int id) {
        var comment = db.Comments
        .Where(c => c.CommentId == id)
        .FirstOrDefault();
        return comment;
    }

    ///Create post og comment

  
    public void CreatePost(string title, string content, string author) {
        db.posts.Add(new Post { Content = content, Date = DateTime.Now, Author = author, Title = title, Upvotes = 0, Downvotes = 0 });
        db.SaveChanges();
    }

    public void CreateComment(string content, string author, int postId) {
        db.Comments.Add(new Comment { Content = content, Date = DateTime.Now, Author = author, Upvotes = 0, Downvotes = 0, PostId = postId });
        db.SaveChanges();
    }


    /// Upvote og downvote på post og comments

    public void UpvotePost(int id) {
        Post post = db.posts.FirstOrDefault(p => p.PostId == id);
        post.Upvotes++;
        db.SaveChanges();
    }

    public void DownvotePost(int id) {
        Post post = db.posts.FirstOrDefault(p => p.PostId == id);
        post.Downvotes++;
        db.SaveChanges();
    }

    public void UpvoteComment(int id) {
        Comment comment = db.Comments.FirstOrDefault(c => c.CommentId == id);
        comment.Upvotes++;
        db.SaveChanges();
    }

    public void DownvoteComment(int id) {
        Comment comment = db.Comments.FirstOrDefault(c => c.CommentId == id);
        comment.Downvotes++;
        db.SaveChanges();
    }
    
   

}


 