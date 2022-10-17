namespace shared.Model
{

    public class Post
    {
        public int PostId { get; set; }
        public string ?Title { get; set; }
        public string ?Content { get; set; }
        public string ?Author { get; set; }
        public DateTime ?Date { get; set; }
        public List<Comment> ?Comments { get; set; } = new List<Comment>();
        public int ?Upvotes { get; set; }
        public int ?Downvotes { get; set; }

        public Post(string author = "", string title = "", string content = "", int upvotes = 0, int downvotes = 0) {
        Title = title;
        Content = content;
        Upvotes = upvotes;
        Downvotes = downvotes;
        Author = author;

    }


         public Post() {
        PostId = 0;
        Title = "";
        Content = "";
        Upvotes = 0;
        Downvotes = 0;
        Author = "";
    }

}
}