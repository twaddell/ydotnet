using NUnit.Framework;
using YDotNet.Document;
using YDotNet.Document.Types.Branches;

namespace YDotNet.Tests.Unit.Branches;

public class ReadTransactionTests
{
    [Test]
    public void StartSingleReadTransaction()
    {
        // Arrange
        var doc = new Doc();
        Branch branch = doc.Text("branch");

        // Act
        var transaction = branch.ReadTransaction();

        // Assert
        Assert.That(transaction, Is.Not.Null);
    }

    [Test]
    public void StartMultipleReadTransaction()
    {
        // Arrange
        var doc = new Doc();
        Branch branch = doc.Array("branch");

        // Act
        var transaction1 = branch.ReadTransaction();
        var transaction2 = branch.ReadTransaction();

        // Assert
        Assert.That(transaction1, Is.Not.Null);
        Assert.That(transaction2, Is.Not.Null);
    }

    [Test]
    public void StartReadTransactionWhileDocumentReadTransactionIsOpen()
    {
        // Arrange
        var doc = new Doc();
        Branch branch = doc.Map("branch");

        // Act
        var documentTransaction = doc.ReadTransaction();
        var branchTransaction = branch.ReadTransaction();

        // Assert
        Assert.That(documentTransaction, Is.Not.Null);
        Assert.That(branchTransaction, Is.Not.Null);
    }

    [Test]
    public void StartReadTransactionWhileWriteTransactionIsOpen()
    {
        // Arrange
        var doc = new Doc();
        Branch branch = doc.XmlFragment("branch");

        // Act
        var writeTransaction = branch.WriteTransaction();

        // Assert
        Assert.Throws<YDotNetException>(() => branch.ReadTransaction());
        Assert.That(writeTransaction, Is.Not.Null);
    }

    [Test]
    public void StartReadTransactionWhileDocumentWriteTransactionIsOpen()
    {
        // Arrange
        var doc = new Doc();
        Branch branch = doc.XmlFragment("branch");

        // Act
        var documentTransaction = doc.WriteTransaction();

        // Assert
        Assert.Throws<YDotNetException>(() => branch.ReadTransaction());
        Assert.That(documentTransaction, Is.Not.Null);
    }

    [Test]
    public void GetRootMapWithOpenBranchTransactionNotAllowed()
    {
        // Arrange
        var doc = new Doc();
        var map = doc.Map("Map");

        // Keep the transaction open.
        map.Length(map.ReadTransaction());

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
        map.Length(map.ReadTransaction());

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
        map.Length(map.ReadTransaction());

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
        map.Length(map.ReadTransaction());

        // Assert
        Assert.Throws<YDotNetException>(() => doc.XmlFragment("xml-fragment"));
    }
}
