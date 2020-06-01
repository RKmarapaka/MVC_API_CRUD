using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVC_CRUD.Helper;
using MVC_CRUD.Models;
using Newtonsoft.Json;

namespace MVC_CRUD.Controllers
{
    public class StudentController : Controller
    {
        StudentAPI _api = new StudentAPI();

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<StudentData> students = new List<StudentData>();
            HttpClient client = _api.Initial();
            HttpResponseMessage resp = await client.GetAsync("api/Student");
            if (resp.IsSuccessStatusCode)
            {
                var results = resp.Content.ReadAsStringAsync().Result;
                students = JsonConvert.DeserializeObject<List<StudentData>>(results);
            }

            return View(students);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Details(int Id)
        {
            var student = new StudentData();
            HttpClient client = _api.Initial();
            HttpResponseMessage resp = await client.GetAsync($"api/Student/{Id}");
            if (resp.IsSuccessStatusCode)
            {
                var results = resp.Content.ReadAsStringAsync().Result;
                student = JsonConvert.DeserializeObject<StudentData>(results);
            }
            return View(student);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(StudentData student)
        {
            HttpClient client = _api.Initial();

            var postTask = client.PostAsJsonAsync<StudentData>("api/student", student);
            postTask.Wait();

            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }


        public async Task<IActionResult> Delete(int id)
        {
            var student = new StudentData();
            HttpClient client = _api.Initial();

            HttpResponseMessage resp = await client.DeleteAsync($"api/student/{id}");

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var student = new StudentData();
            HttpClient client = _api.Initial();

            HttpResponseMessage resp = await client.GetAsync($"api/Student/{id}");

            if (resp.IsSuccessStatusCode)
            {
                var results = resp.Content.ReadAsStringAsync().Result;
                student = JsonConvert.DeserializeObject<StudentData>(results);
            }
            return View(student);
        }
        [HttpPost]
        public IActionResult Edit(StudentData student)
        {
            
            HttpClient client = _api.Initial();

            var postTask =client.PutAsJsonAsync<StudentData>("api/student", student);
            postTask.Wait();

            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}