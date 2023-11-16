// ----------------------------------------------------------------------
// <copyright file="HackerNewsWrapperTests.cs" company="Felsökning">
//      Copyright © Felsökning. All rights reserved.
// </copyright>
// <author>John Bailey</author>
// ----------------------------------------------------------------------
namespace Felsökning.YCombinator.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class HackerNewsWrapperTests
    {
        [TestMethod]
        public void HackerNewsWrapper_ctor()
        {
            var sut = new HackerNewsWrapper();

            sut.Should().NotBeNull();
            sut.HttpClient.Should().NotBeNull();

            sut = new HackerNewsWrapper();

            sut.Should().NotBeNull();
            sut.HttpClient.Should().NotBeNull();
        }

        [TestMethod]
        public async Task HackerNewsWrapper_GetTopStoriesAsync_Faked_Succeeds()
        {

            var sut = new HackerNewsWrapper()
            {
                HttpClient = new HttpClient(new TestingHttpMessageHandler())
            };

            var results = await sut.GetTopStoriesAsync();

            results.Should().NotBeNullOrEmpty();
            results.Count.Should().Be(1);
            var result = results.FirstOrDefault();
            result?.By.Should().NotBeNullOrWhiteSpace();
            result?.By.Should().Be("vinnyglennon");
            result?.Id.Should().Be(32649091);
            result?.Descendants.Should().BeGreaterThan(0);
            result?.Descendants.Should().Be(36);
            result?.Kids.Should().NotBeNull();
            result?.Score.Should().BeGreaterThanOrEqualTo(0);
            result?.Time.Should().Be(1661859463);
            result?.Title.Should().NotBeNullOrWhiteSpace();
            result?.Title.Should().Be("Wikipedia Recent Changes Map");
            result?.Type.Should().NotBeNullOrWhiteSpace();
            result?.Type.Should().Be("story");
            result?.Url.Should().NotBeNullOrWhiteSpace();

            sut = new HackerNewsWrapper()
            {
                HttpClient = new HttpClient(new TestingHttpMessageHandler())
            };

            results = await sut.GetTopStoriesAsync();

            results.Should().NotBeNullOrEmpty();
            results.Count.Should().Be(1);
            result = results.FirstOrDefault();
            result?.By.Should().NotBeNullOrWhiteSpace();
            result?.By.Should().Be("vinnyglennon");
            result?.Id.Should().Be(32649091);
            result?.Descendants.Should().BeGreaterThan(1);
            result?.Descendants.Should().Be(36);
            result?.Kids.Should().NotBeNull();
            result?.Time.Should().Be(1661859463);
            result?.Score.Should().BeGreaterThanOrEqualTo(0);
            result?.Title.Should().NotBeNullOrWhiteSpace();
            result?.Title.Should().Be("Wikipedia Recent Changes Map");
            result?.Type.Should().NotBeNullOrWhiteSpace();
            result?.Type.Should().Be("story");
            result?.Url.Should().NotBeNullOrWhiteSpace();
        }

        [TestMethod]
        public async Task HackerNewsWrapper_GetTopStoriesAsync_Real_Succeeds()
        {
            var sut = new HackerNewsWrapper();

            var results = await sut.GetTopStoriesAsync();

            results.Should().NotBeNull();
            results.Count.Should().BeGreaterThan(1);

            results = await sut.ShowTopStoriesAsync();

            results.Should().NotBeNull();
            results.Count.Should().BeGreaterThan(1);

            results = await sut.GetTopStoriesAsync(3);

            results.Should().NotBeNull();
            results.Count.Should().BeGreaterThan(1);
            results.Count.Should().BeLessThan(4);

            results = await sut.ShowTopStoriesAsync(3);

            results.Should().NotBeNull();
            results.Count.Should().BeGreaterThan(1);
            results.Count.Should().BeLessThan(4);
        }
    }
}