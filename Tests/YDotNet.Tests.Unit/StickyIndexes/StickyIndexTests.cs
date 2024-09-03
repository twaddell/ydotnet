using NUnit.Framework;
using YDotNet.Document;
using YDotNet.Document.StickyIndexes;

namespace YDotNet.Tests.Unit.StickyIndexes;

public class StickyIndexTests
{
    [Test]
    public void CreateFromText()
    {
        // Arrange
        var doc = new Doc();
        var text = doc.Text("text");

        // Act
        var transaction = doc.WriteTransaction();
        var stickyIndexAfter = text.StickyIndex(transaction, index: 0, StickyAssociationType.After);
        var stickyIndexBefore = text.StickyIndex(transaction, index: 0, StickyAssociationType.Before);
        transaction.Commit();

        // Assert
        Assert.That(stickyIndexAfter, Is.Null);
        Assert.That(stickyIndexBefore, Is.Not.Null);
        Assert.That(stickyIndexBefore.Handle, Is.GreaterThan(nint.Zero));
    }

    [Test]
    public void CreateFromArray()
    {
        // Arrange
        var doc = new Doc();
        var array = doc.Array("array");

        // Act
        var transaction = doc.WriteTransaction();
        var stickyIndexAfter = array.StickyIndex(transaction, index: 0, StickyAssociationType.After);
        var stickyIndexBefore = array.StickyIndex(transaction, index: 0, StickyAssociationType.Before);
        transaction.Commit();

        // Assert
        Assert.That(stickyIndexAfter, Is.Null);
        Assert.That(stickyIndexBefore, Is.Not.Null);
        Assert.That(stickyIndexBefore.Handle, Is.GreaterThan(nint.Zero));
    }

    [Test]
    public void CreateFromXmlText()
    {
        // Arrange
        var doc = new Doc();
        var xmlFragment = doc.XmlFragment("xml-fragment");

        var transaction = doc.WriteTransaction();
        var xmlText = xmlFragment.InsertText(transaction, index: 0);
        transaction.Commit();

        // Act
        transaction = doc.WriteTransaction();
        var stickyIndexAfter = xmlText.StickyIndex(transaction, index: 0, StickyAssociationType.After);
        var stickyIndexBefore = xmlText.StickyIndex(transaction, index: 0, StickyAssociationType.Before);
        transaction.Commit();

        // Assert
        Assert.That(stickyIndexAfter, Is.Null);
        Assert.That(stickyIndexBefore, Is.Not.Null);
        Assert.That(stickyIndexBefore.Handle, Is.GreaterThan(nint.Zero));
    }
}
