namespace Ciroque.Foundations.FeatureStore.Data.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class InMemoryStorageContainerTests : StorageContainerTests
    {
        public InMemoryStorageContainerTests()
        {
            m_StorageContainer = new InMemoryStorageContainer();
        }
    }
}