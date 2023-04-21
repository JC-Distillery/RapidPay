using System.Net;


namespace BussinesLogic.Dtos
{
    public class CommonResponseDto<T>
    {
        public T Response { get; set; }
        public HttpStatusCode HttpStatusCode {get;set;}
        public string Message { get; set; }
    }
}
