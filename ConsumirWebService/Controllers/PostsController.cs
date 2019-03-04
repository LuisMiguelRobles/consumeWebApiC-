using ConsumirWebService.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ConsumirWebService.Controllers
{

    public class PostsController : Controller
    {
        private HttpClient client;
        private string url = "https://localhost:44365/api/posts";

        public PostsController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // Sincrono
        /*public ActionResult Index()
        {
            IEnumerable<Post> posts = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44365/api/");
                var response = client.GetAsync("posts");
                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Post>>();
                    readTask.Wait();
                    posts = readTask.Result;
                }

                return View(posts);
            }
        }*/


       
        //Asincrono
        public async Task<ActionResult> Index() {

            /*using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44365/api/");
                HttpResponseMessage responseMessage = await client.GetAsync("posts");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessagehttps://localhost:44365/api/.Content.ReadAsStringAsync().Result;
                    var post = JsonConvert.DeserializeObject<List<Post>>(responseData);
                    return View(post);
                }
                return Json(new { post = "Error" }, JsonRequestBehavior.AllowGet);
            }*/
            HttpResponseMessage responseMessage = await client.GetAsync(url);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                var post = JsonConvert.DeserializeObject<List<Post>>(responseData);
                return View(post);
            }
            return Json(new { post = "Error" }, JsonRequestBehavior.AllowGet);

        }
        /*public ActionResult Index()
        {
            IEnumerable<Post> posts = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(UrlBase);

                //HTTP GET
                try
                {


                    var response = client.GetAsync("posts");
                    response.Wait();

                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IList<Post>>();
                        readTask.Wait();
                        posts = readTask.Result;
                    }
                    else
                    {
                        posts = Enumerable.Empty<Post>();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");

                    }
                }
                catch (Exception ex) {
                    posts = Enumerable.Empty<Post>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(posts);
            //return Json(posts, JsonRequestBehavior.AllowGet);
            
        }*/

        public ActionResult Create() {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(Post post) {
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(url,post);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return Json(new { post = "Error" }, JsonRequestBehavior.AllowGet);
        }

        /*[HttpPost]
        public ActionResult Create(Post post)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44365/api/posts");

                var postTask =  client.PostAsJsonAsync<Post>("posts", post);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

                return View(post);
            }
        }*/

        public async Task<ActionResult> Edit(int id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var post = JsonConvert.DeserializeObject<Post>(responseData);

                return View(post);
            }
            return Json(new { post = "Error" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, Post post)
        {

            HttpResponseMessage responseMessage = await client.PutAsJsonAsync(url + "/" + id, post);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return Json(new { post = "Error" }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url + "/" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var Employee = JsonConvert.DeserializeObject<Post>(responseData);

                return View(Employee);
            }
            return Json(new { post = "Error" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {

            HttpResponseMessage responseMessage = await client.DeleteAsync(url + "/" + id);
            var status = responseMessage.StatusCode;
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return Json(new { post = "Error" }, JsonRequestBehavior.AllowGet);
        }



        public async Task<ActionResult> GetPost()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
                HttpResponseMessage responseMessage = await client.GetAsync("posts");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var post = JsonConvert.DeserializeObject<List<Post>>(responseData);
                    return Json(post, JsonRequestBehavior.AllowGet);
                }
                return Json(new { post = "Error" }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}