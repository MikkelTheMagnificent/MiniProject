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

        public Comment(string author = "", string content = "", int upvotes = 0, int downvotes = 0) {
        Content = content;
        Upvotes = upvotes;
        Downvotes = downvotes;
        Author = author;
    }
         public Comment() {
        CommentId = 0;
        Content = "";
        Upvotes = 0;
        Downvotes = 0;
    }
}
};
