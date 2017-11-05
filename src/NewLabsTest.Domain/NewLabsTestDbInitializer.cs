namespace NewLabsTest.Domain
{
    public static class NewLabsTestDbInitializer
    {
        public static void Initialize(NewLabsTestContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
