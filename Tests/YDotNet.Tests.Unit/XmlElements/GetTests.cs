using NUnit.Framework;
using YDotNet.Document;
using YDotNet.Document.Types.XmlElements;

namespace YDotNet.Tests.Unit.XmlElements;

public class GetTests
{
    [Test]
    public void GetOutsideOfValidBounds()
    {
        // Arrange
        var doc = new Doc();
        var xmlFragment = doc.XmlFragment("xml-fragment");

        var transaction = doc.WriteTransaction();
        var xmlElement = xmlFragment.InsertElement(transaction, 0, "xml-element");
        transaction.Commit();

        // Act
        transaction = doc.WriteTransaction();
        var value = xmlElement.Get(transaction, index: 1);
        transaction.Commit();

        // Assert
        Assert.That(value, Is.Null);
    }

    [Test]
    public void GetXmlText()
    {
        // Arrange
        var (doc, xmlElement) = ArrangeDoc();

        // Act
        var transaction = doc.WriteTransaction();
        var output = xmlElement.Get(transaction, index: 1);
        transaction.Commit();

        // Assert
        Assert.That(output.XmlText, Is.Not.Null);
    }

    [Test]
    public void GetXmlElement()
    {
        // Arrange
        var (doc, xmlElement) = ArrangeDoc();

        // Act
        var transaction = doc.WriteTransaction();
        var output = xmlElement.Get(transaction, index: 2);
        transaction.Commit();

        // Assert
        Assert.That(output.XmlElement, Is.Not.Null);
    }

    private (Doc, XmlElement) ArrangeDoc()
    {
        var doc = new Doc();
        var xmlFragment = doc.XmlFragment("xml-fragment");

        var transaction = doc.WriteTransaction();
        var xmlElement = xmlFragment.InsertElement(transaction, 0, "xml-element");
        transaction.Commit();

        // Act
        transaction = doc.WriteTransaction();
        xmlElement.InsertText(transaction, index: 0);
        xmlElement.InsertText(transaction, index: 1);
        xmlElement.InsertElement(transaction, index: 2, "color");
        xmlElement.InsertText(transaction, index: 3);
        xmlElement.InsertElement(transaction, index: 4, "border");
        transaction.Commit();

        return (doc, xmlElement);
    }
}
