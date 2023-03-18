using Amazon.Lambda.Annotations.APIGateway;
using System.Net;

namespace Lambda.Products.Function;

public abstract class BaseFunction
{

    protected List<string> Errors = new List<string>();

    protected IHttpResult CustomResponse(object result = null)
    {
        if (ValidateOperation())
        {
            return HttpResults.Ok(result);
        }

        return HttpResults.BadRequest(Errors.ToArray());
    }

    protected IHttpResult CustomResponse(BaseResult baseResult)
    {
        foreach (string error in baseResult.Errors)
        {
            AddError(error);
        }

        return CustomResponse(baseResult.Response);
    }

    protected IHttpResult NoContent()
    {
        return HttpResults.NewResult(HttpStatusCode.NoContent);
    }
    protected bool ValidateOperation()
    {
        return !Errors.Any();
    }

    protected void AddError(string error)
    {
        Errors.Add(error);
    }

}
