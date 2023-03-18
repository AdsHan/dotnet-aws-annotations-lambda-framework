namespace Lambda.Products.Function;

public class BaseResult
{
    public List<string> Errors { get; set; }
    public object Response { get; set; }

    public BaseResult()
    {
        Errors = new List<string>();
        Response = null;
    }

    public bool IsValid()
    {
        return !Errors.Any();
    }
}
