using NUnit.Framework;
using YDotNet.Document;
using YDotNet.Document.Cells;
using YDotNet.Document.StickyIndexes;

namespace YDotNet.Tests.Unit.StickyIndexes;

public class ReadTests
{
    [Test]
    public void ReadIndexFromText()
    {
        // Arrange
        var doc = new Doc();
        var text = doc.Text("text");

        var transaction = doc.WriteTransaction();
        text.Insert(transaction, index: 0, "Lucas");
        var stickyIndexBefore = text.StickyIndex(transaction, index: 3, StickyAssociationType.Before);
        var stickyIndexAfter = text.StickyIndex(transaction, index: 3, StickyAssociationType.After);
        transaction.Commit();

        // Act
        transaction = doc.ReadTransaction();
        var beforeIndex = stickyIndexBefore.Read(transaction);
        var afterIndex = stickyIndexAfter.Read(transaction);
        transaction.Commit();

        // Assert
        Assert.That(beforeIndex, Is.EqualTo(expected: 3));
        Assert.That(afterIndex, Is.EqualTo(expected: 3));

        // Act
        transaction = doc.WriteTransaction();
        text.Insert(transaction, index: 3, "(");
        text.Insert(transaction, index: 5, ")");
        text.Insert(transaction, index: 7, " Viana");
        text.Insert(transaction, index: 0, "Hello, ");
        beforeIndex = stickyIndexBefore.Read(transaction);
        afterIndex = stickyIndexAfter.Read(transaction);
        transaction.Commit();

        // Assert
        Assert.That(beforeIndex, Is.EqualTo(expected: 10));
        Assert.That(afterIndex, Is.EqualTo(expected: 11));
    }

    [Test]
    public void ReadIndexFromArray()
    {
        // Arrange
        var doc = new Doc();
        var array = doc.Array("array");

        var transaction = doc.WriteTransaction();
        array.InsertRange(
            transaction, index: 0, Input.Long(value: 2469L), Input.Null(), Input.Boolean(value: false));
        var stickyIndexBefore = array.StickyIndex(transaction, index: 1, StickyAssociationType.Before);
        var stickyIndexAfter = array.StickyIndex(transaction, index: 1, StickyAssociationType.After);
        transaction.Commit();

        // Act
        transaction = doc.ReadTransaction();
        var beforeIndex = stickyIndexBefore.Read(transaction);
        var afterIndex = stickyIndexAfter.Read(transaction);
        transaction.Commit();

        // Assert
        Assert.That(beforeIndex, Is.EqualTo(expected: 1));
        Assert.That(afterIndex, Is.EqualTo(expected: 1));

        // Act
        transaction = doc.WriteTransaction();
        array.InsertRange(transaction, index: 1, Input.String("("));
        array.InsertRange(transaction, index: 3, Input.String(")"));
        array.InsertRange(transaction, index: 4, Input.String(" Viana"));
        array.InsertRange(transaction, index: 0, Input.String("Hello, "));
        beforeIndex = stickyIndexBefore.Read(transaction);
        afterIndex = stickyIndexAfter.Read(transaction);
        transaction.Commit();

        // Assert
        Assert.That(beforeIndex, Is.EqualTo(expected: 2));
        Assert.That(afterIndex, Is.EqualTo(expected: 3));
    }

    [Test]
    public void ReadIndexFromXmlText()
    {
        // Arrange
        var doc = new Doc();
        var xmlFragment = doc.XmlFragment("xml-fragment");

        var transaction = doc.WriteTransaction();
        var xmlText = xmlFragment.InsertText(transaction, index: 0);
        transaction.Commit();

        // Act
        transaction = doc.WriteTransaction();
        xmlText.Insert(transaction, index: 0, "Lucas");
        var stickyIndexBefore = xmlText.StickyIndex(transaction, index: 3, StickyAssociationType.Before);
        var stickyIndexAfter = xmlText.StickyIndex(transaction, index: 3, StickyAssociationType.After);
        transaction.Commit();

        // Act
        transaction = doc.ReadTransaction();
        var beforeIndex = stickyIndexBefore.Read(transaction);
        var afterIndex = stickyIndexAfter.Read(transaction);
        transaction.Commit();

        // Assert
        Assert.That(beforeIndex, Is.EqualTo(expected: 3));
        Assert.That(afterIndex, Is.EqualTo(expected: 3));

        // Act
        transaction = doc.WriteTransaction();
        xmlText.InsertAttribute(transaction, "bold", "true");
        xmlText.Insert(transaction, index: 3, "(");
        xmlText.Insert(transaction, index: 5, ")");
        xmlText.Insert(transaction, index: 7, " Viana");
        xmlText.Insert(transaction, index: 0, "Hello, ");
        xmlText.InsertAttribute(transaction, "italics", "false");
        beforeIndex = stickyIndexBefore.Read(transaction);
        afterIndex = stickyIndexAfter.Read(transaction);
        transaction.Commit();

        // Assert
        Assert.That(beforeIndex, Is.EqualTo(expected: 10));
        Assert.That(afterIndex, Is.EqualTo(expected: 11));
    }
}
