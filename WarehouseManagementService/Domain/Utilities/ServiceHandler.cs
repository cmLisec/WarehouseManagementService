namespace WarehouseManagementService.Domain.Utilities
{
    public static class ServiceHandler
    {
        public static async Task<T> ExecuteAsync<T>(Func<Task<T>> func, ILogger logger, string contextMessage)
        {
            try
            {
                return await func();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in {ContextMessage}", contextMessage);
                throw;
            }
        }

        public static async Task ExecuteAsync(Func<Task> func, ILogger logger, string contextMessage)
        {
            try
            {
                await func();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in {ContextMessage}", contextMessage);
                throw;
            }
        }
    }
}
