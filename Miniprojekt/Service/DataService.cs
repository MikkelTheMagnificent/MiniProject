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
        return db.posts.ToList();
    }

    public Post GetPost(int id) {
        return db.posts.Include(p => p.PostId).FirstOrDefault(p => p.PostId == id);
    }

    public List<Comment> GetComments() {
        return db.Comments.ToList();
    }

    public Comment GetComment(int id) {
        return db.Comments.Include(c => c.CommentId).FirstOrDefault(c => c.CommentId == id);
    }

    ///Create post og comment

    public string CreatePost(int postId, string title, string content, string author, DateTime date, int upvotes, int downvotes) {
        Post Post = db.posts.FirstOrDefault(p => p.PostId == postId);
        db.posts.Add(new Post { PostId = postId, Title = title, Content = content, Author = author, Date = date, Upvotes = upvotes, Downvotes = downvotes });
        db.SaveChanges();
        return "Post created";
    }

    public string CreateComment(int commentId, string content, DateTime date, string author, int postId) {
        var Comment = db.Comments.FirstOrDefault(c => c.CommentId == commentId);
        db.Comments.Add(new Comment { CommentId = commentId, Content = content, Date = date, Author = author, PostId = postId });
        db.SaveChanges();
        return "Comment created";
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


 