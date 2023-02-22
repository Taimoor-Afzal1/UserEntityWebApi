using System.Collections.Generic;

namespace UserEntity.Model
{
    public class Response
    {
        public enum RequestStatus { Success = 0, Error = 1 }
        public RequestStatus Status { get; set; } = RequestStatus.Success;
        public string Message { get; set; }
        public Dictionary<string, object> Payload { get; set; }
        public object Error { get; set; }
    }
}
