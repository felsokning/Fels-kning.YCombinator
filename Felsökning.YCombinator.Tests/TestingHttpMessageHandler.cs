// ----------------------------------------------------------------------
// <copyright file="TestingHttpMessageHandler.cs" company="Felsökning">
//      Copyright © Felsökning. All rights reserved.
// </copyright>
// <author>John Bailey</author>
// ----------------------------------------------------------------------
namespace Felsökning.YCombinator.Tests
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="TestingHttpMessageHandler"/> class,
    ///     which is used to model HTTP Response messages back to the caller, based on URL.
    /// </summary>
    /// <inheritdoc cref="HttpMessageHandler"/>
    [ExcludeFromCodeCoverage]
    internal class TestingHttpMessageHandler : HttpMessageHandler
    {
        /// <summary>
        ///     Overrides the <see cref="SendAsync(HttpRequestMessage, CancellationToken)"/> method in <see cref="HttpMessageHandler"/>
        ///     to return a specified <see cref="HttpResponseMessage"/>, based on the URL called.
        /// </summary>
        /// <param name="httpRequestMessage"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A <see cref="Task{TResult}"/> of <see cref="HttpResponseMessage"/> for the test class[es] to consume.</returns>
        /// <exception cref="HttpRequestException">A response to mock exceptions thrown on request.</exception>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken)
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();

            if (httpRequestMessage?.RequestUri?.AbsoluteUri == "https://hacker-news.firebaseio.com/v0/topstories.json?print=pretty")
            {
                responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
                responseMessage.StatusCode = HttpStatusCode.OK;
                responseMessage.Content = new StringContent("[ 32649091 ]");

                return Task<HttpResponseMessage>.Factory.StartNew(() => responseMessage, cancellationToken);
            }

            if (httpRequestMessage?.RequestUri?.AbsoluteUri == "https://hacker-news.firebaseio.com/v0/item/32649091.json?print=pretty")
            {
                responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
                responseMessage.StatusCode = HttpStatusCode.OK;
                responseMessage.Content = new StringContent("{\r\n  \"by\" : \"vinnyglennon\",\r\n  \"descendants\" : 36,\r\n  \"id\" : 32649091,\r\n  \"kids\" : [ 32650627, 32652889, 32651364, 32651377, 32655335, 32652375, 32652023, 32649456, 32651630, 32649718, 32653799, 32650386, 32657988, 32650124 ],\r\n  \"score\" : 162,\r\n  \"time\" : 1661859463,\r\n  \"title\" : \"Wikipedia Recent Changes Map\",\r\n  \"type\" : \"story\",\r\n  \"url\" : \"http://rcmap.hatnote.com/#en\"\r\n}");

                return Task<HttpResponseMessage>.Factory.StartNew(() => responseMessage, cancellationToken);
            }

            throw new HttpRequestException("Resource Not Found", null, System.Net.HttpStatusCode.NotFound);
        }
    }
}