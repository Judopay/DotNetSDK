namespace JudoPayDotNet.Errors
{
    public class FieldError
    {
        public int Code { get; set; }
        public string FieldName { get; set; }
        public string Message { get; set; }
        public string Detail { get; set; }
    }
}