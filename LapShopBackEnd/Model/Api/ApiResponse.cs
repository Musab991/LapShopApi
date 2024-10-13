using System.Text.Json.Serialization;

namespace LapShop.Model.Api
{
    public class ApiResponse
    {
        public object Data { get; set; }
        public ResponseStatus Status { get; set; }
        public IEnumerable<string> Errors { get; set; } = new List<string>();

        public ApiResponse(object data, ResponseStatus status)
        {
            Data = data;
            Status = status;
        }

        public ApiResponse(ResponseStatus status, IEnumerable<string> errors)
        {
            Status = status;
            Errors = errors;
        }
    }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ResponseStatus
    {
        Success,
        Error,
        NotFound,
        Unauthorized,
        NotValid
    }
}
