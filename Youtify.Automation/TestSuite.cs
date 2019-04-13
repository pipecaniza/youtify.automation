using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Youtify.Automation.Common;
using Youtify.Automation.Extensions;
using Youtify.Automation.PageObjects;
using System.Linq;
using OpenQA.Selenium;

namespace Youtify.Automation
{
    public class TestSuite
    {
        [Fact]
        public void GoogleAuth_Success()
        {
            using (var driver = new ChromeDriver("./"))
            {
                var youtify = new YoutifyPageObject(driver);

                youtify.OpenSite();
                youtify.Login(Globals.USER_EMAIL, Globals.USER_PASSWORD);
                Assert.True(youtify.SignOutButton.IsVisible());

                youtify.Logout();
                Assert.True(youtify.SignInButton.IsVisible());
            }
        }

        [Fact]
        public void YotubeNotReady_NoVideoPlaying_Fails()
        {
            using (var driver = new ChromeDriver("./"))
            {
                var youtify = new YoutifyPageObject(driver);

                youtify.OpenSite();

                Assert.False(youtify.IsPlayerVisible);
                Assert.Throws<WebDriverTimeoutException>(() => youtify.YoutubePlayButton);
            }
        }

        [Fact]
        public void SearchAndPlay_Success()
        {
            using (var driver = new ChromeDriver("./"))
            {
                var youtify = new YoutifyPageObject(driver);

                youtify.OpenSite();

                youtify.SearchField.SendKeys("avicci");
                youtify.SearchField.Submit();
                youtify.CurrentVideos.First().Click();

                Assert.True(youtify.IsPlayerVisible);
            }
        }

        [Fact]
        public void CreatePlaylist_AddVideo_Success()
        {
            using (var driver = new ChromeDriver("./"))
            {
                var youtify = new YoutifyPageObject(driver);

                youtify.OpenSite();
                youtify.Login(Globals.USER_EMAIL, Globals.USER_PASSWORD);
                Assert.True(youtify.SignOutButton.IsVisible());

                youtify.TryClearPlaylists();
               
                // Create a playlist
                youtify.AddPlaylistButton.Click();
                youtify.AddPlaylistInput.SendKeys("Favorites");
                youtify.AddPlaylistInput.Submit();
                youtify.WaitUntilLoads();
                Assert.Equal(1, youtify.Playlists.Count);

                // Add video to a playlist
                youtify.VideoOptions.First().Click();
                youtify.VideoPlaylistsToAdd.First().Click();
                youtify.WaitSecs(1);

                youtify.Playlists.First().Click();
                youtify.WaitSecs(1);
                Assert.Equal(1, youtify.CurrentVideos.Count);

                youtify.TryClearPlaylists();
            }
        }
    }
}
