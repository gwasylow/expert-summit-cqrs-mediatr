using System;
namespace MediatrCQRS.Logic
{
    public class CQRSCommandResponse : CQRSResponse
    {
        public int? ReturnedId { get; init; } = null;
    }
}
