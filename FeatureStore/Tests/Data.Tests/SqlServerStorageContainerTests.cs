namespace Ciroque.Foundations.FeatureStore.Data.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SqlServerStorageContainerTests : StorageContainerTests
    {
        private const string SqlConnectionStringKey = "Ciroque.Foundations.FeatureStore.Database";

        public SqlServerStorageContainerTests()
        {
            m_StorageContainer = new SqlServerStorageContainer(SqlConnectionStringKey);
        }
    }
}