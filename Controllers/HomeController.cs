using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace NewsWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string category)
        {
            ViewData["TitlePage"] = "Επικαιρότητα";
            if (!string.IsNullOrEmpty(category))
            {
                switch (category)
                {
                    case "business":
                        ViewData["TitlePage"] = "Οικονομία";
                        ViewData["pressed"] = "1";
                        break;
                    case "science":
                        ViewData["TitlePage"] = "Επιστήμη";
                        ViewData["pressed"] = "2";
                        break;
                    case "sports":
                        ViewData["TitlePage"] = "Αθλητικά";
                        ViewData["pressed"] = "3";
                        break;
                    case "technology":
                        ViewData["TitlePage"] = "Τεχνολογία";
                        ViewData["pressed"] = "4";
                        break;
                    case "entertainment":
                        ViewData["TitlePage"] = "Διασκέδαση";
                        ViewData["pressed"] = "5";
                        break;
                }
            }

            if (string.IsNullOrEmpty(category))
            {
                ViewData["pressed"] = "0";

                var newsApiClient = new NewsApiClient("e395280492df4a15a47d689f26fd729f");
                var articlesResponse = newsApiClient.GetEverything(new EverythingRequest
                {
                    Q = "Greece",
                    SortBy = SortBys.Popularity,
                    Language = Languages.EL,
                    From = DateTime.Today
                });

                if (articlesResponse.Status == Statuses.Ok)
                {
                    if (articlesResponse.Articles.Count() <= 15)
                    {
                        articlesResponse = newsApiClient.GetEverything(new EverythingRequest
                        {
                            Q = "Greece",
                            SortBy = SortBys.Popularity,
                            Language = Languages.EL,
                            From = DateTime.Today.AddDays(-1)
                        });

                        if (articlesResponse.Status == Statuses.Ok)
                        {
                            ViewData["topNews"] = articlesResponse.Articles;
                        }
                    }

                    ViewData["topNews"] = articlesResponse.Articles;
                }
            } else
            {
                string json = new WebClient().DownloadString("http://newsapi.org/v2/everything?q=" + category + "&apiKey=e395280492df4a15a47d689f26fd729f");
                int index = json.IndexOf("articles");
                var objects = JArray.Parse(json.Substring(index + 10, json.Length - (index + 10) - 1));

                List<Article> articles = new List<Article>();

                string title = null;
                string author = null;
                string description = null;
                string urlToImage = null;
                string url = null;

                foreach (JObject root in objects)
                {
                    foreach (KeyValuePair<String, JToken> pair in root)
                    {

                        if (pair.Key.Equals("author"))
                            author = (string)pair.Value;

                        if (pair.Key.Equals("title"))
                            title = (string)pair.Value;

                        if (pair.Key.Equals("description"))
                            description = (string)pair.Value;

                        if (pair.Key.Equals("urlToImage"))
                            urlToImage = (string)pair.Value;

                        if (pair.Key.Equals("url"))
                            url = (string)pair.Value;
                    }

                    articles.Add(new Article
                    {
                        Title = title,
                        Author = author,
                        Description = description,
                        UrlToImage = urlToImage,
                        Url = url
                    });
                }

                ViewData["topNews"] = articles;
            }

            return View();
        }

    }
}
