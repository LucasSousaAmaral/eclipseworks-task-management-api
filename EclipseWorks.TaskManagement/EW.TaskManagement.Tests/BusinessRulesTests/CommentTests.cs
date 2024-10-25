using EW.TaskManagement.Domain.Entities;

namespace EW.TaskManagement.Tests.BusinessRulesTests;

public class CommentTests
{
    [Fact]
    public void AddComment_Should_Add_Comment_And_Record_History()
    {
        // Arrange
        var taskItem = new TaskItem("Título", "Descrição", DateTime.UtcNow.AddDays(1), TaskPriority.Medium, 1);
        var content = "Este é um comentário de teste.";
        var userId = 1;

        // Act
        taskItem.AddComment(content, userId);

        // Assert
        Assert.Single(taskItem.Comments);
        var comment = taskItem.Comments.First();
        Assert.Equal(content, comment.Content);
        Assert.Equal(userId, comment.UserId);

        Assert.Single(taskItem.Histories);
        var history = taskItem.Histories.First();
        Assert.Contains("Comentário adicionado", history.Changes);
        Assert.Equal(userId, history.ModifiedByUserId);
    }

    [Fact]
    public void AddComment_Should_Throw_Exception_When_Content_Is_Invalid()
    {
        // Arrange
        var taskItem = new TaskItem("Título", "Descrição", DateTime.UtcNow.AddDays(1), TaskPriority.Medium, 1);
        var content = "";
        var userId = 1;

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => taskItem.AddComment(content, userId));
        Assert.Equal("O conteúdo do comentário não pode ser vazio. (Parameter 'content')", exception.Message);
    }
}