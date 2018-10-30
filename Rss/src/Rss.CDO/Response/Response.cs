using Rss.CDO.Enums.Response;

namespace Rss.CDO.Response
{
    public class Response<T>
    {
        public T ResponseData { get; set; }
        public ResponseType Type { get; set; }
        public string Description { get; set; }

        private Response(T responseData, ResponseType type)
        {
            ResponseData = responseData;
            Type = type;
        }
        private Response(T responseData, ResponseType type, string description)
        {
            ResponseData = responseData;
            Type = type;
            Description = description;
        }

        public static Response<T> Create(T responseData, ResponseType type)
        {
            return new Response<T>(responseData, type);
        }
        public static Response<T> Create(T responseData, ResponseType type, string description)
        {
            return new Response<T>(responseData, type, description);
        }
    }
}
