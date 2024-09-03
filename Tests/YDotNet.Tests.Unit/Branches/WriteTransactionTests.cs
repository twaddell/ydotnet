using NUnit.Framework;
using YDotNet.Document;
using YDotNet.Document.Types.Branches;

namespace YDotNet.Tests.Unit.Branches;

public class WriteTransactionTests
{
    [Test]
    public void StartSingleWriteTransaction()
    {
        // Arrange
        var doc = new Doc();
        Branch branch = doc.Text("branch");

        // Act
        var transaction = branch.WriteTransaction();

        // Assert
        Assert.That(transaction, Is.Not.Null);
    }

    [Test]
    public void StartMultipleWriteTransactions()
    {
        // Arrange
        var doc = new Doc();
        Branch branch = doc.Array("branch");

        // Act
        var transaction1 = branch.WriteTransaction();

        // Assert
        Assert.Throws<YDotNetException>(() => branch.WriteTransaction());
        Assert.That(transaction1, Is.Not.Null);
    }

    [Test]
    public void StartWriteTransactionWhileReadTransactionIsOpen()
    {
        // Arrange
        var doc = new Doc();
        Branch branch = doc.Array("branch");

        // Act
        var readTransaction = branch.ReadTransaction();

        // Assert
        Assert.Throws<YDotNetException>(() => branch.WriteTransaction());
        Assert.That(readTransaction, Is.Not.Null);
    }

    [Test]
    public void StartWriteTransactionWhileDocumentReadTransactionIsOpen()
    {
        // Arrange
        var doc = new Doc();
        Branch branch = doc.Array("branch");

        // Act
        var readTransaction = doc.ReadTransaction();

        // Assert
        Assert.Throws<YDotNetException>(() => branch.WriteTransaction());
        Assert.That(readTransaction, Is.Not.Null);
    }

    [Test]
    public void StartWriteTransactionWhileDocumentWriteTransactionIsOpen()
    {
        // Arrange
        var doc = new Doc();
        Branch branch = doc.Array("branch");

        // Act
        var transaction1 = doc.WriteTransaction();

        // Assert
        Assert.Throws<YDotNetException>(() => branch.WriteTransaction());
        Assert.That(transaction1, Is.Not.Null);
    }

    [Test]
    public void GetRootMapWithOpenBranchTransactionNotAllowed()
    {
        // Arrange
        var doc = new Doc();
        var map = doc.Map("Map");

        // Keep the transaction open.
        map.Length(map.WriteTransaction());

        // Assert
        Assert.Throws<YDotNetException>(() => doc.Map("Item"));
    }

    [Test]
    public void GetRootArrayWithOpenTransactionNotAllowed()
    {
        // Arrange
        var doc = new Doc();
        var map = doc.Map("Map");

        // Keep the transaction open.
        map.Length(map.WriteTransaction());

        // Assert
        Assert.Throws<YDotNetException>(() => doc.Array("Item"));
    }

    [Test]
    public void GetRootTextWithOpenTransactionNotAllowed()
    {
        // Arrange
        var doc = new Doc();
        var map = doc.Map("Map");

        // Keep the transaction open.
        map.Length(map.WriteTransaction());

        // Assert
        Assert.Throws<YDotNetException>(() => doc.Text("Item"));
    }

    [Test]
    public void GetRootXmlFragmentWithOpenTransactionNotAllowed()
    {
        // Arrange
        var doc = new Doc();
        var map = doc.Map("Map");

        // Keep the transaction open.
        map.Length(map.WriteTransaction());

        // Assert
        Assert.Throws<YDotNetException>(() => doc.XmlFragment("xml-fragment"));
    }
}
