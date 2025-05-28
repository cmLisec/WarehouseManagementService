namespace WarehouseManagementService.Domain.Utilities
{
    public static class ServiceExecutor
    {
        public static async Task<CommonResponseType<T>> TryExecuteAsync<T>(Func<Task<CommonResponseType<T>>> action, ILogger logger, string methodName) where T : class
        {
            try
            {
                return await action();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred in {MethodName}", methodName);
                return new CommonResponseType<T>("An unexpected error occurred.", StatusCodes.Status500InternalServerError);
            }
        }
    }
}
