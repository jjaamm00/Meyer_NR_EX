using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Octokit;

namespace GitHubAPI.Controllers
{
    public class HomeController : Controller
    {
        //GitHubClient githubClient = new GitHubClient(new ProductHeaderValue("my-cool-app"));
        GitHubClient client = new GitHubClient(new ProductHeaderValue("jjaamm00"));

        public ActionResult Index()
        {
            //var basicAuth = new Credentials("jjaamm00", "N0pp@r@t"); // NOTE: not real credentials
            //client.Credentials = basicAuth;

            return View();
        }

        public ActionResult _SearchUser()
        {
            return PartialView();
        }

        public ActionResult _SearchRepo()
        {
            //ViewBag.flag = flag;
            return PartialView();
        }

        public ActionResult _SearchRepoOnly()
        {
            return PartialView();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult GetUser(string user_filter)
        {
            var request = new SearchUsersRequest(user_filter);
            var result = client.Search.SearchUsers(request).Result;

            var request_repo = new SearchRepositoriesRequest("test")
            {
                User = user_filter
            };
            var result_repo = client.Search.SearchRepo(request_repo).Result;

            return Json(result); 
        }

        [HttpPost]
        public ActionResult GetRepo(string user_filter, string search_crit)
        {
            if (!string.IsNullOrEmpty(search_crit))
            {
                var request_repo = new SearchRepositoriesRequest(search_crit)
                {
                    User = user_filter
                };
                var result_repo = client.Search.SearchRepo(request_repo).Result;

                return Json(result_repo);
            }
            else
            {

                var request_repo = new SearchRepositoriesRequest()
                {
                    User = user_filter
                };
                var result_repo = client.Search.SearchRepo(request_repo).Result;

                return Json(result_repo);
            }
        }

        [HttpPost]
        public ActionResult GetRepoOnly(string search_crit)
        {
            var request_repo = new SearchRepositoriesRequest(search_crit);
            var result_repo = client.Search.SearchRepo(request_repo).Result;
            return Json(result_repo);
        }
    }
}