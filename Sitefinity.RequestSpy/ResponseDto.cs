namespace Sitefinity.RequestSpy
{
    public class ResponseDto
    {
        public ResponseDto() { }
        public ResponseDto(int statusCode)
        {
            this.statusCode = statusCode;
        }

        public int StatusCode
        {
            get 
            { 
                return this.statusCode; 
            }

            set 
            { 
                this.statusCode = value; 
            }
        }

        private int statusCode;
    }
}