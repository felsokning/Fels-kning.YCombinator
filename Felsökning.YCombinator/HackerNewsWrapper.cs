// ----------------------------------------------------------------------
// <copyright file="HackerNewsWrapper.cs" company="Felsökning">
//      Copyright © Felsökning. All rights reserved.
// </copyright>
// <author>John Bailey</author>
// ----------------------------------------------------------------------
namespace Felsökning.YCombinator
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="HackerNewsWrapper"/> class,
    ///     which is used to wrap calls to the Firebase-implemented Hacker News API.
    /// </summary>
    /// <inheritdoc cref="HttpBase"/>
    /// <inheritdoc cref="IHackerNewsWrapper"/>
    public class HackerNewsWrapper : HttpBase, IHackerNewsWrapper
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="HackerNewsWrapper"/> class,
        ///     which is used to wrap calls to the Firebase-implemented Hacker News API.
        /// </summary>
        /// <inheritdoc cref="HttpBase"/>
        /// <inheritdoc cref="IHackerNewsWrapper"/>
        public HackerNewsWrapper()
            : base(HttpVersion.Version11, "Felsökning.Utilities.YCombinator")
        {
        }

        /// <summary>
        ///     Gets the list of top stories (ordered in descending order by time) from the Firebase-implemented Hacker News API.
        /// </summary>
        /// <param name="numberOfStoriesToReturn">The numer of stories to return, to reduce overhead of processing 500 items - if not needed.</param>
        /// <returns>A <see cref="List{T}"/> of <see cref="Story"/></returns>
        /// <exception cref="StatusException">An error occurred on the Http Request.</exception>
        public async Task<List<Story>> GetTopStoriesAsync([Optional] int numberOfStoriesToReturn)
        {
#pragma warning disable S1075 // URIs should not be hardcoded   
            var resultIds = await this.HttpClient.GetAsync<List<int>>("https://hacker-news.firebaseio.com/v0/topstories.json?print=pretty");
#pragma warning restore S1075 // URIs should not be hardcoded   
            if (numberOfStoriesToReturn > 0)
            {
                resultIds = resultIds.OrderByDescending(x => x).Take(numberOfStoriesToReturn).ToList();

                // Asynchronous Calls require either async collections or to await all of the results.
                var tasks = resultIds.Select(r => this.HttpClient.GetAsync<Story>($"https://hacker-news.firebaseio.com/v0/item/{r}.json?print=pretty")).ToList();
                var stories = await Task.WhenAll(tasks!);
                return stories.OrderByDescending(x => x.Time).ToList();
            }
            else
            {
                resultIds = resultIds.OrderByDescending(x => x).ToList();

                // Asynchronous Calls require either async collections or to await all of the results.
                var tasks = resultIds.Select(r => this.HttpClient.GetAsync<Story>($"https://hacker-news.firebaseio.com/v0/item/{r}.json?print=pretty")).ToList();
                var stories = await Task.WhenAll(tasks!);
                return stories.OrderByDescending(x => x.Time).ToList();
            }
        }

        /// <summary>
        ///     Gets the list of stories (ordered in descending order by time) from the Firebase-implemented Hacker News API.
        /// </summary>
        /// <param name="numberOfStoriesToReturn">The numer of stories to return, to reduce overhead of processing 200 items - if not needed.</param>
        /// <returns>A <see cref="List{T}"/> of <see cref="Story"/></returns>
        /// <exception cref="StatusException">An error occurred on the Http Request.</exception>
        public async Task<List<Story>> ShowTopStoriesAsync([Optional] int numberOfStoriesToReturn)
        {
#pragma warning disable S1075 // URIs should not be hardcoded   
            var resultIds = await this.HttpClient.GetAsync<List<int>>("https://hacker-news.firebaseio.com/v0/showstories.json?print=pretty");
#pragma warning restore S1075 // URIs should not be hardcoded   

            if (numberOfStoriesToReturn > 0)
            {
                resultIds = resultIds.OrderByDescending(x => x).Take(numberOfStoriesToReturn).ToList();

                // Asynchronous Calls require either async collections or to await all of the results.
                var tasks = resultIds.Select(r => this.HttpClient.GetAsync<Story>($"https://hacker-news.firebaseio.com/v0/item/{r}.json?print=pretty")).ToList();
                var stories = await Task.WhenAll(tasks!);
                return stories.OrderByDescending(x => x.Time).ToList();
            }
            else
            {
                resultIds = resultIds.OrderByDescending(x => x).ToList();

                // Asynchronous Calls require either async collections or to await all of the results.
                var tasks = resultIds.Select(r => this.HttpClient.GetAsync<Story>($"https://hacker-news.firebaseio.com/v0/item/{r}.json?print=pretty")).ToList();
                var stories = await Task.WhenAll(tasks!);
                return stories.OrderByDescending(x => x.Time).ToList();
            }
        }
    }
}