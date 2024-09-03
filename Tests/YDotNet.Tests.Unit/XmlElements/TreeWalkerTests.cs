using NUnit.Framework;
using YDotNet.Document;

namespace YDotNet.Tests.Unit.XmlElements;

public class TreeWalkerTests
{
    [Test]
    public void WalksOnEmptyTree()
    {
        // Arrange
        var doc = new Doc();
        var xmlFragment = doc.XmlFragment("xml-fragment");

        var transaction = doc.WriteTransaction();
        var xmlElement = xmlFragment.InsertElement(transaction, index: 0, "xml-element");
        transaction.Commit();

        // Act
        transaction = doc.ReadTransaction();
        var xmlTreeWalker = xmlElement.TreeWalker(transaction);
        transaction.Commit();

        // Assert
        Assert.That(xmlTreeWalker, Is.Not.Null);
        Assert.That(xmlTreeWalker.ToArray().Length, Is.EqualTo(expected: 0));
    }

    [Test]
    public void WalksOnTreeWithSingleLevelOfDepth()
    {
        // Arrange
        var doc = new Doc();
        var xmlFragment = doc.XmlFragment("xml-fragment");

        var transaction = doc.WriteTransaction();
        var xmlElement = xmlFragment.InsertElement(transaction, index: 0, "xml-element");
        transaction.Commit();

        // Act
        transaction = doc.WriteTransaction();
        xmlElement.InsertText(transaction, index: 0);
        xmlElement.InsertElement(transaction, index: 1, "color");
        xmlElement.InsertText(transaction, index: 2);
        transaction.Commit();

        // Act
        transaction = doc.ReadTransaction();
        var xmlTreeWalker = xmlElement.TreeWalker(transaction);

        // Assert
        var xmlNodes = xmlTreeWalker.ToArray();

        Assert.That(xmlTreeWalker, Is.Not.Null);
        Assert.That(xmlNodes.Length, Is.EqualTo(expected: 3));
        Assert.That(xmlNodes.ElementAt(index: 0).XmlText, Is.Not.Null);
        Assert.That(xmlNodes.ElementAt(index: 1).XmlElement, Is.Not.Null);
        Assert.That(xmlNodes.ElementAt(index: 1).XmlElement.Tag(transaction), Is.EqualTo("color"));
        Assert.That(xmlNodes.ElementAt(index: 2).XmlText, Is.Not.Null);

        transaction.Commit();
    }

    [Test]
    public void WalksOnTreeWithMultipleLevelsOfDepth()
    {
        // Arrange
        var doc = new Doc();
        var xmlFragment = doc.XmlFragment("xml-fragment");

        var transaction = doc.WriteTransaction();
        var xmlElement = xmlFragment.InsertElement(transaction, index: 0, "xml-element");
        transaction.Commit();

        // Act
        transaction = doc.WriteTransaction();
        xmlElement.InsertText(transaction, index: 0);
        xmlElement.InsertElement(transaction, index: 1, "color");
        var childXmlElement = xmlElement.Get(transaction, index: 1).XmlElement;
        childXmlElement.InsertElement(transaction, index: 0, "alpha");
        childXmlElement.InsertElement(transaction, index: 1, "hex");
        childXmlElement.InsertText(transaction, index: 2);
        transaction.Commit();

        // Act
        transaction = doc.ReadTransaction();
        var xmlTreeWalker = xmlElement.TreeWalker(transaction);

        // Assert
        var xmlNodes = xmlTreeWalker.ToArray();

        Assert.That(xmlTreeWalker, Is.Not.Null);
        Assert.That(xmlNodes.Length, Is.EqualTo(expected: 5));
        Assert.That(xmlNodes.ElementAt(index: 0).XmlText, Is.Not.Null);
        Assert.That(xmlNodes.ElementAt(index: 1).XmlElement, Is.Not.Null);
        Assert.That(xmlNodes.ElementAt(index: 1).XmlElement.Tag(transaction), Is.EqualTo("color"));
        Assert.That(xmlNodes.ElementAt(index: 2).XmlElement, Is.Not.Null);
        Assert.That(xmlNodes.ElementAt(index: 2).XmlElement.Tag(transaction), Is.EqualTo("alpha"));
        Assert.That(xmlNodes.ElementAt(index: 3).XmlElement, Is.Not.Null);
        Assert.That(xmlNodes.ElementAt(index: 3).XmlElement.Tag(transaction), Is.EqualTo("hex"));
        Assert.That(xmlNodes.ElementAt(index: 4).XmlText, Is.Not.Null);

        transaction.Commit();
    }
}
