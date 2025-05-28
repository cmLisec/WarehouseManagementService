namespace WarehouseManagementService.Domain.Utilities
{
    public class CommonResponseType<T> where T : class
    {
        public T Resource { get; set; }

        public int StatusCode { get; set; }

        public string Message { get; set; }

        public CommonResponseType()
        {
            Resource = default(T);
            StatusCode = 200;
        }

        public CommonResponseType(T resource, int statusCode)
        {
            Resource = resource;
            StatusCode = statusCode;
        }

        public CommonResponseType(string message, int statusCode)
        {
            Message = message;
            StatusCode = statusCode;
        }
        public bool IsSuccessStatusCode()
        {
            if (StatusCode != 200)
            {
                return StatusCode == 201;
            }

            return true;
        }
    }
}
