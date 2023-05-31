using System;
using System.Collections.Generic;

namespace BookStoreWeb.Models;

public partial class Comment
{
    public string IdComment { get; set; } = null!;

    public string? CommentContent { get; set; }

    public DateTime? CommentTime { get; set; }

    public string? IdUser { get; set; }

    public string? IdBook { get; set; }

    public int? StarRating { get; set; }

    public virtual Book? IdBookNavigation { get; set; }

    public virtual Account? IdUserNavigation { get; set; }

    public Comment(string idComment, string? commentContent, string? idUser, string? idBook)
    {
        IdComment = idComment;
        CommentContent = commentContent;
        IdUser = idUser;
        IdBook = idBook;
    }
    public Comment() { }
    public static List<Comment> listComment(string id_book)
    {
        BookstoreContext db = new BookstoreContext();
        var getAllComment = (from item in db.Comments
                             where item.IdBook == id_book
                             select item).ToList();
        return getAllComment;
    }
    public static void CreateComment(string comment_content, string id_user, string id_book, int star)
    {
        BookstoreContext db = new BookstoreContext();
        string id_comment = Guid.NewGuid().ToString();
        Comment comment = new Comment(id_comment, comment_content, id_user, id_book);
        comment.StarRating = star;
        db.Comments.Add(comment);
        db.SaveChanges();
    }
}
