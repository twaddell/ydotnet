using NUnit.Framework;
using YDotNet.Document;

namespace YDotNet.Tests.Unit.XmlTexts;

public class IterateTests
{
    [Test]
    public void IteratesOnEmpty()
    {
        // Arrange
        var doc = new Doc();
        var xmlFragment = doc.XmlFragment("xml-fragment");

        var transaction = doc.WriteTransaction();
        var xmlText = xmlFragment.InsertText(transaction, index: 0);
        transaction.Commit();

        // Act
        transaction = doc.ReadTransaction();
        var iterator = xmlText.Iterate(transaction);
        var values = iterator.ToArray();
        transaction.Commit();

        // Assert
        Assert.That(values, Is.Empty);
    }

    [Test]
    public void IteratesOnSingleItem()
    {
        // Arrange
        var doc = new Doc();
        var xmlFragment = doc.XmlFragment("xml-fragment");

        var transaction = doc.WriteTransaction();
        var xmlText = xmlFragment.InsertText(transaction, index: 0);
        xmlText.InsertAttribute(transaction, "href", "https://lsviana.github.io/");
        transaction.Commit();

        // Act
        transaction = doc.ReadTransaction();
        var iterator = xmlText.Iterate(transaction);
        var values = iterator.ToArray();
        transaction.Commit();

        // Assert
        Assert.That(values.Length, Is.EqualTo(expected: 1));
        Assert.That(values.First(x => x.Key == "href").Value, Is.EqualTo("https://lsviana.github.io/"));
    }

    [Test]
    public void IteratesOnMultiItem()
    {
        // Arrange
        var doc = new Doc();
        var xmlFragment = doc.XmlFragment("xml-fragment");

        var transaction = doc.WriteTransaction();
        var xmlText = xmlFragment.InsertText(transaction, index: 0);
        xmlText.InsertAttribute(transaction, "href", "https://lsviana.github.io/");
        xmlText.InsertAttribute(transaction, "as", "document");
        xmlText.InsertAttribute(transaction, "rel", "preload");
        transaction.Commit();

        // Act
        transaction = doc.ReadTransaction();
        var iterator = xmlText.Iterate(transaction);
        var values = iterator.ToArray();
        transaction.Commit();

        // Assert
        Assert.That(values.Length, Is.EqualTo(expected: 3));
        Assert.That(values.First(x => x.Key == "href").Value, Is.EqualTo("https://lsviana.github.io/"));
        Assert.That(values.First(x => x.Key == "as").Value, Is.EqualTo("document"));
        Assert.That(values.First(x => x.Key == "rel").Value, Is.EqualTo("preload"));
    }
}
