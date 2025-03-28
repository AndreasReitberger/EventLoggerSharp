using AndreasReitberger.Logging;
using Newtonsoft.Json;

namespace RestApiClientSharp.Test.NUnit
{
    public class Tests
    {
        private EventLogger? logger;

        #region Setup
        [SetUp]
        public void Setup()
        {
            logger = new EventLogger.EventLogInstanceBuilder()
                .WithFilePath(@"C:\temp\Log")
                .WithFileExtension(".dat")
                .Build();
            logger.Error += (sender, args) =>
            {
                Assert.Fail($"Error: {args?.ToString()}");
            };
        }
        #endregion

        #region JSON
        [Test]
        public void TestJsonSerialization()
        {
            try
            {
                string? json = JsonConvert.SerializeObject(logger, Formatting.Indented);
                Assert.That(!string.IsNullOrEmpty(json));

                EventLogger? client2 = JsonConvert.DeserializeObject<EventLogger>(json);
                Assert.That(client2, Is.Not.EqualTo(null));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        #endregion

        #region Logging
        [Test]
        public async Task TestLoggingAsync()
        {
            try
            {
                if (logger is null) throw new NullReferenceException($"The logger was null!");
                // Do some tests here

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        #endregion

        #region Cleanup
        [TearDown]
        public void BaseTearDown()
        {
            logger?.Dispose();
        }
        #endregion
    }
}