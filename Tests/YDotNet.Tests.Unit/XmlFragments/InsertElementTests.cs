using NUnit.Framework;
using YDotNet.Document;

namespace YDotNet.Tests.Unit.XmlFragments;

public class InsertElementTests
{
    [Test]
    public void InsertSingleElement()
    {
        // Arrange
        var doc = new Doc();
        var xmlFragment = doc.XmlFragment("xml-fragment");

        // Act
        var transaction = doc.WriteTransaction();
        var addedXmlElement = xmlFragment.InsertElement(transaction, index: 0, "color");
        var childLength = xmlFragment.ChildLength(transaction);

        // Assert
        Assert.That(addedXmlElement.Handle, Is.Not.EqualTo(nint.Zero));
        Assert.That(addedXmlElement.Tag(transaction), Is.EqualTo("color"));
        Assert.That(childLength, Is.EqualTo(expected: 1));

        transaction.Commit();
    }

    [Test]
    public void InsertMultipleElements()
    {
        // Arrange
        var doc = new Doc();
        var xmlFragment = doc.XmlFragment("xml-fragment");

        // Act
        var transaction = doc.WriteTransaction();
        var xmlElement1 = xmlFragment.InsertElement(transaction, index: 0, "color");
        var xmlElement2 = xmlFragment.InsertElement(transaction, index: 0, "width");
        var xmlElement3 = xmlFragment.InsertElement(transaction, index: 0, "height");
        var childLength = xmlFragment.ChildLength(transaction);

        // Assert
        Assert.That(xmlElement1.Tag(transaction), Is.EqualTo("color"));
        Assert.That(xmlElement2.Tag(transaction), Is.EqualTo("width"));
        Assert.That(xmlElement3.Tag(transaction), Is.EqualTo("height"));
        Assert.That(childLength, Is.EqualTo(expected: 3));

        transaction.Commit();
    }
}
