using System;
namespace MediatrCQRS.Logic
{
    public class CQRSQueryResponse<T> : CQRSResponse
    {
        public T QueryResult { get; init; }
    }
}
