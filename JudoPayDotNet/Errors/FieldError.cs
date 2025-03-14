namespace JudoPayDotNet.Errors
{
    public class FieldError
    {
        public int Code { get; set; }
        public string FieldName { get; set; }
        public string Message { get; set; }
        public string Detail { get; set; }

        public FieldError(int code, string fieldName, string message, string detail)
        {
            Code = code;
            FieldName = fieldName;
            Message = message;
            Detail = detail;
        }

        public FieldError()
        {
        }
    }
}