namespace PetFam.Api.Response
{
    public record Envelope
    {
        public object? Result { get; }
        public IEnumerable<ResponseError> Errors { get; }
        public DateTime TimeGenerated { get; }

        private Envelope(object? result, IEnumerable<ResponseError> errors)
        {
            Result = result;
            Errors = errors.ToList();
            TimeGenerated = DateTime.Now;
        }

        public static Envelope Ok(object? result = null) =>
        new(result, []);

        public static Envelope Error(IEnumerable<ResponseError> errors) =>
            new(null, errors);
    }
}
