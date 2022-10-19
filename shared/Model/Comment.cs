namespace shared.Model
{

    public class Comment
    {
       
        public int CommentId { get; set; }
        public string ?Content { get; set; }
        public DateTime ?Date { get; set; }
        public string ?Author { get; set; }
        public int PostId { get; set; }
        public int ?Upvotes { get; set; }
        public int ?Downvotes { get; set; }

      
    
    }
      
    }


