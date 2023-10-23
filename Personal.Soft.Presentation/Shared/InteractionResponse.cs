namespace Personal.Soft.Presentation.Shared
{
    public class InteractionResponse<T>
    {

        public T? response;
        public Dictionary<string, string> errors { get; set; } = new Dictionary<string, string>();
        public int status { get;set; }
        public bool operation { get; set; }

    }
}
