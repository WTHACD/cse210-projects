using System;
using System;
using System.Collections.Generic;

class Comment
{
    public string CommenterName { get; set; }
    public string CommentText { get; set; }

    public Comment(string commenterName, string commentText)
    {
        CommenterName = commenterName;
        CommentText = commentText;
    }

    public void DisplayComment()
    {
        Console.WriteLine($"- {CommenterName}: {CommentText}");
    }
}

class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Length { get; set; } // Length in seconds
    private List<Comment> Comments { get; set; }

    public Video(string title, string author, int length)
    {
        Title = title;
        Author = author;
        Length = length;
        Comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    public int GetCommentCount()
    {
        return Comments.Count;
    }

    public void DisplayVideoDetails()
    {
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Author: {Author}");
        Console.WriteLine($"Length: {Length} seconds");
        Console.WriteLine($"Number of Comments: {GetCommentCount()}");
        Console.WriteLine("Comments:");
        foreach (var comment in Comments)
        {
            comment.DisplayComment();
        }
        Console.WriteLine(new string('-', 40));
    }
}

class Program
{
    static void Main()
    {
        // Creating videos
        Video video1 = new Video("Learn C# in One Hour", "Tech Guru", 3600);
        Video video2 = new Video("OOP Principles Explained", "Code Master", 1800);
        Video video3 = new Video("C# Abstraction Tutorial", "Dev Corner", 900);

        // Adding comments to video1
        video1.AddComment(new Comment("Alice", "This is really helpful!"));
        video1.AddComment(new Comment("Bob", "Great explanation."));
        video1.AddComment(new Comment("Charlie", "Could you make a part 2?"));

        // Adding comments to video2
        video2.AddComment(new Comment("David", "I finally understand OOP!"));
        video2.AddComment(new Comment("Eve", "Very well explained."));
        video2.AddComment(new Comment("Frank", "Thanks for this!"));

        // Adding comments to video3
        video3.AddComment(new Comment("Grace", "This was so clear, thanks!"));
        video3.AddComment(new Comment("Hank", "I needed this tutorial."));
        video3.AddComment(new Comment("Ivy", "Really useful content."));

        // Storing videos in a list
        List<Video> videos = new List<Video> { video1, video2, video3 };

        // Displaying video details
        foreach (var video in videos)
        {
            video.DisplayVideoDetails();
        }
    }
}
