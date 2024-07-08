using System;

namespace SocialMediaApis.CommonMethod
{
    public class CommonModel
    {
        public class JsonModel
        {
            public string Message { get; set; }
            public int StatusCode { get; set; }
            public Object Data { get; set; }
            public string AccessToken { get; set; }
            public PaginationMeta Pagination { get; set; }
        }

        public class PaginationMeta
        {
            public int TotalItems { get; set; }
            public int PageSize { get; set; }
            public int CurrentPage { get; set; }
            public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
        }
    }
}
