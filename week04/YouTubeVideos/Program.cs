using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Video> videos = CreateSampleVideos();

        foreach (Video video in videos)
        {
            video.Display();
        }
    }

    static List<Video> CreateSampleVideos()
    {
        List<Video> videos = new List<Video>();

        Video video1 = new Video("C# Abstraction Explained", "Code Academy", 642);
        video1.AddComment(new Comment("Alice", "This made abstraction click for me."));
        video1.AddComment(new Comment("Bob", "Great examples, thanks!"));
        video1.AddComment(new Comment("Carol", "The video length is just right."));
        videos.Add(video1);

        Video video2 = new Video("Object-Oriented Design Basics", "Dev University", 915);
        video2.AddComment(new Comment("Diana", "I finally understand classes and objects."));
        video2.AddComment(new Comment("Edward", "Clear and easy to follow."));
        video2.AddComment(new Comment("Fiona", "The diagrams helped a lot."));
        video2.AddComment(new Comment("George", "Please make more videos like this."));
        videos.Add(video2);

        Video video3 = new Video("Encapsulation in Practice", "Programming with Classes", 528);
        video3.AddComment(new Comment("Hannah", "Private fields make so much more sense now."));
        video3.AddComment(new Comment("Ian", "Good explanation of getters and setters."));
        video3.AddComment(new Comment("Julia", "Short but very useful."));
        videos.Add(video3);

        Video video4 = new Video("Testing Your First C# Classes", "BYU-I CSE", 377);
        video4.AddComment(new Comment("Kevin", "The example output was easy to check."));
        video4.AddComment(new Comment("Laura", "Thanks for the step-by-step walkthrough."));
        video4.AddComment(new Comment("Miles", "This saved me a lot of time."));
        videos.Add(video4);

        return videos;
    }
}
