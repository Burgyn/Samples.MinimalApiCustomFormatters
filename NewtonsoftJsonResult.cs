using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

namespace Samples.MinimalApiCustomFormatters;

internal class NewtonsoftJsonResult<T> : IResult
{
    private readonly T _result;

    public NewtonsoftJsonResult(T result)
    {
        _result = result;
    }

    public async Task ExecuteAsync(HttpContext httpContext)
    {
        using var stream = new FileBufferingWriteStream();
        using var streamWriter = new StreamWriter(stream);
        using var jsonTextWriter = new JsonTextWriter(streamWriter);

        var serializer = new JsonSerializer();
        serializer.Serialize(jsonTextWriter, _result);
        await jsonTextWriter.FlushAsync();

        httpContext.Response.ContentType = System.Net.Mime.MediaTypeNames.Application.Json;
        await stream.DrainBufferAsync(httpContext.Response.Body);
    }
}

public static class NewtonsoftJsonResultExtensions
{
    public static IResult NJson<T>(this IResultExtensions _, T result)
        => new NewtonsoftJsonResult<T>(result);
}