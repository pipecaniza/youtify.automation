using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Linq;
using OpenQA.Selenium;
using Youtify.Automation.Common;
using Youtify.Automation.Extensions;

namespace Youtify.Automation.PageObjects
{
    public class YoutifyPageObject
    {
        protected readonly ChromeDriver _driver;

        public const string LoaderCss = "fa-spinner";

        public IWebElement SignInButton
        {
            get => _driver.WaitFor(".sign-in");
        }

        public IWebElement SignOutButton
        {
            get => _driver.WaitFor(".sign-out");
        }

        public IWebElement SearchField
        {
            get => _driver.WaitFor(".search-bar");
        }

        public IReadOnlyCollection<IWebElement> CurrentVideos
        {
            get
            {
                _driver.WaitFor(".play");
                return _driver.FindElementsByCssSelector(".play");
            }
        }

        public bool IsPlayerVisible
        {
            get
            {
                return _driver.FindElementsByCssSelector("#player").Count(e => e.IsVisible()) > 0;
            }
        }

        public IWebElement YoutubePlayButton
        {
            get => _driver.WaitFor(".ytp-large-play-button");
        }

        public IWebElement AddPlaylistButton
        {
            get => _driver.WaitFor("#sideNavbar button");
        }

        public IWebElement AddPlaylistInput
        {
            get => _driver.WaitFor(".add-playlist");
        }

        public const string PlaylistsCss = ".playlist";

        public IReadOnlyCollection<IWebElement> Playlists
        {
            get
            {
                _driver.WaitFor(".playlist");
                return _driver.FindElementsByCssSelector(PlaylistsCss);
            }
        }        

        public IReadOnlyCollection<IWebElement> RemovePlaylists
        {
            get
            {
                _driver.WaitFor(".remove-playlist");
                return _driver.FindElementsByCssSelector(".remove-playlist");
            }
        }

        public IReadOnlyCollection<IWebElement> VideoOptions
        {
            get
            {
                _driver.WaitFor(".dropdown-toggle");
                return _driver.FindElementsByCssSelector(".dropdown-toggle");
            }
        }

        public IReadOnlyCollection<IWebElement> VideoPlaylistsToAdd
        {
            get => _driver.FindElementsByCssSelector(".dropdown-item")
                .Where(e => e.IsVisible()).ToList();
            
        }

        public YoutifyPageObject(ChromeDriver chromeDriver)
        {
            _driver = chromeDriver;
            _driver.Manage().Window.Position = new Point(0, 0);
            _driver.Manage().Window.Size = new Size(1600, 700);
        }

        public void OpenSite()
        {
            _driver.Url = Globals.SITE_URL;
            _driver.Navigate();
        }

        public void Login(string email, string password)
        {
            SignInButton.Click();
            using (var googleAuth = new GoogleAuthPageObject(_driver))
            {
                googleAuth.Login(email, password);
            }
        }

        public void TryClearPlaylists()
        {
            try
            {
                foreach (var btn in RemovePlaylists)
                {
                    btn.Click();
                }
            }
            catch
            {
                // Just in case that there are no playlists
            }

            _driver.WaitUntilDisapear(PlaylistsCss);
        }

        public void WaitUntilLoads()
        {
            _driver.WaitUntilDisapear(LoaderCss);
        }

        public void WaitSecs(int sec)
        {
            System.Threading.Thread.Sleep(sec * 1000);
        }

        public void Logout()
        {
            SignOutButton.Click();
        }

        
    }
}
