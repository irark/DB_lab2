using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using DB_lab2.Models;

namespace DB_lab2.Controllers
{
    public class QueriesController : Controller
    {
        private const string CONNECTION_STRING = "Server=LAPTOP-VLHU3V0T;Database=DBFilms;Trusted_Connection=True;MultipleActiveResultSets=true";
        private const string ERR = "Немає результатів для даного запиту";
        private const string DEFAULT_PATH = @"C:\Labs\DB_lab2\DB_lab2\Queries\";
        private readonly DBFilmsContext _context;

        public QueriesController(DBFilmsContext context)
        {
            _context = context;
        }
        public IActionResult Index(int errorCode)
        {
            var actors = _context.Actors.Select(a => a.Name).Distinct().ToList();

            ViewBag.ActorsList = new SelectList(_context.Actors, "Name", "Name");
            ViewBag.GanresList = new SelectList(_context.Ganres, "Name", "Name");
            ViewBag.FilmsList = new SelectList(_context.Films, "Name", "Name");
            if(errorCode == 1)ViewBag.Error1 = "Недопустиме значення";
            if (errorCode == 2) ViewBag.Error2 = "Недопустиме значення";
            if (errorCode == 3) ViewBag.Error3 = "Недопустиме значення";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SimpleQuery1(Query queryModel)
        {
            string query = System.IO.File.ReadAllText(DEFAULT_PATH + "SIMPLE_QUERY_1.sql");
            query = query.Replace("ActorName", "N\'" + queryModel.ActorName + "\'");
            query = query.Replace("\r\n", " ");
            query = query.Replace('\t', ' ');

            queryModel.QueryId = "S1";
            queryModel.FilmsNames = new List<string>();

            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    using(var reader = command.ExecuteReader())
                    {
                        int flag = 0;
                        while(reader.Read())
                        {
                            queryModel.FilmsNames.Add(reader.GetString(0));
                            flag++;
                        }
                        if(flag == 0)
                        {
                            queryModel.ErrorFlag = 1;
                            queryModel.Error = ERR;
                        }
                    }
                    
                }
                connection.Close();
            }
            return RedirectToAction("Result", queryModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SimpleQuery2(Query queryModel)
        {
            if (ModelState.IsValid)
            {
                string query = System.IO.File.ReadAllText(DEFAULT_PATH + "SIMPLE_QUERY_2.sql");
                query = query.Replace("GanreName", "N\'" + queryModel.GanreName + "\'");
                query = query.Replace("FilmYear", queryModel.Year.ToString());
                query = query.Replace("\r\n", " ");
                query = query.Replace('\t', ' ');

                queryModel.QueryId = "S2";
                queryModel.FilmsNames = new List<string>();
                using (var connection = new SqlConnection(CONNECTION_STRING))
                {
                    connection.Open();
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                        using (var reader = command.ExecuteReader())
                        {
                            int flag = 0;
                            while (reader.Read())
                            {
                                queryModel.FilmsNames.Add(reader.GetString(0));
                                flag++;
                            }
                            if (flag == 0)
                            {
                                queryModel.ErrorFlag = 1;
                                queryModel.Error = ERR;
                            }
                        }

                    }
                    connection.Close();
                }
                return RedirectToAction("Result", queryModel);
            }
            return RedirectToAction("Index", new { errorCode = 1 });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SimpleQuery3(Query queryModel)
        {
            if (ModelState.IsValid)
            {
                string query = System.IO.File.ReadAllText(DEFAULT_PATH + "SIMPLE_QUERY_3.sql");
                query = query.Replace("CountOfGanres", queryModel.CountOfGanres.ToString());
                query = query.Replace("\r\n", " ");
                query = query.Replace('\t', ' ');

                queryModel.QueryId = "S3";
                queryModel.FilmsNames = new List<string>();

                using (var connection = new SqlConnection(CONNECTION_STRING))
                {
                    connection.Open();
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                        using (var reader = command.ExecuteReader())
                        {
                            int flag = 0;
                            while (reader.Read())
                            {
                                queryModel.FilmsNames.Add(reader.GetString(0));
                                flag++;
                            }
                            if (flag == 0)
                            {
                                queryModel.ErrorFlag = 1;
                                queryModel.Error = ERR;
                            }
                        }

                    }
                    connection.Close();
                }
                return RedirectToAction("Result", queryModel);
            }
            return RedirectToAction("Index", new { errorCode = 2 });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SimpleQuery4(Query queryModel)
        {
            string query = System.IO.File.ReadAllText(DEFAULT_PATH + "SIMPLE_QUERY_4.sql");
            query = query.Replace("GanreName", "N\'" + queryModel.GanreName + "\'");
            query = query.Replace("\r\n", " ");
            query = query.Replace('\t', ' ');

            queryModel.QueryId = "S4";
            queryModel.FilmsNames = new List<string>();
            queryModel.FilmsYears = new List<int>();
            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        int flag = 0;
                        while (reader.Read())
                        {
                            queryModel.FilmsNames.Add(reader.GetString(0));
                            queryModel.FilmsYears.Add(reader.GetInt32(1));
                            flag++;
                        }
                        if (flag == 0)
                        {
                            queryModel.ErrorFlag = 1;
                            queryModel.Error = ERR;
                        }
                    }

                }
                connection.Close();
            }
            return RedirectToAction("Result", queryModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SimpleQuery5(Query queryModel)
        {
            string query = System.IO.File.ReadAllText(DEFAULT_PATH + "SIMPLE_QUERY_5.sql");
            query = query.Replace("GanreName", "N\'" + queryModel.GanreName + "\'");
            query = query.Replace("\r\n", " ");
            query = query.Replace('\t', ' ');

            queryModel.QueryId = "S5";
            queryModel.CategoriesNames = new List<string>();

            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        int flag = 0;
                        while (reader.Read())
                        {
                            queryModel.CategoriesNames.Add(reader.GetString(0));
                            flag++;
                        }
                        if (flag == 0)
                        {
                            queryModel.ErrorFlag = 1;
                            queryModel.Error = ERR;
                        }
                    }

                }
                connection.Close();
            }
            return RedirectToAction("Result", queryModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MultipleQuery1(Query queryModel)
        {
            string query = System.IO.File.ReadAllText(DEFAULT_PATH + "MULTIPLE_QUERY_1.sql");
            query = query.Replace("ActorName", "N\'" + queryModel.ActorName + "\'");
            query = query.Replace("\r\n", " ");
            query = query.Replace('\t', ' ');

            queryModel.QueryId = "M1";
            queryModel.ActorsNames = new List<string>();

            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        int flag = 0;
                        while (reader.Read())
                        {
                            queryModel.ActorsNames.Add(reader.GetString(0));
                            flag++;
                        }
                        if (flag == 0)
                        {
                            queryModel.ErrorFlag = 1;
                            queryModel.Error = ERR;
                        }
                    }

                }
                connection.Close();
            }
            return RedirectToAction("Result", queryModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MultipleQuery2(Query queryModel)
        {
            string query = System.IO.File.ReadAllText(DEFAULT_PATH + "MULTIPLE_QUERY_2.sql");
            query = query.Replace("FilmName", "N\'" + queryModel.FilmName + "\'");
            query = query.Replace("\r\n", " ");
            query = query.Replace('\t', ' ');

            queryModel.QueryId = "M2";
            queryModel.FilmsNames = new List<string>();

            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        int flag = 0;
                        while (reader.Read())
                        {
                            queryModel.FilmsNames.Add(reader.GetString(0));
                            flag++;
                        }
                        if (flag == 0)
                        {
                            queryModel.ErrorFlag = 1;
                            queryModel.Error = ERR;
                        }
                    }

                }
                connection.Close();
            }
            return RedirectToAction("Result", queryModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MultipleQuery3(Query queryModel)
        {
            if (ModelState.IsValid)
            {
                string query = System.IO.File.ReadAllText(DEFAULT_PATH + "MULTIPLE_QUERY_3.sql");
            query = query.Replace("FilmYear", queryModel.Year.ToString());
            query = query.Replace("\r\n", " ");
            query = query.Replace('\t', ' ');

            queryModel.QueryId = "M3";
            queryModel.GanresNames = new List<string>();

            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        int flag = 0;
                        while (reader.Read())
                        {
                            queryModel.GanresNames.Add(reader.GetString(0));
                            flag++;
                        }
                        if (flag == 0)
                        {
                            queryModel.ErrorFlag = 1;
                            queryModel.Error = ERR;
                        }
                    }

                }
                connection.Close();
            }
            return RedirectToAction("Result", queryModel);
            }
            return RedirectToAction("Index", new { errorCode = 3 });
        }
        public IActionResult Result(Query queryResult)
        {
            return View(queryResult);
        }
    }
}
