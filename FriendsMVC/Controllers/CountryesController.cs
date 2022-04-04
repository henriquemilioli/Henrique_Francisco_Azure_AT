using FriendsMVC.Data;
using FriendsMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsMVC.Controllers
{
    public class CountryesController : Controller
    {
        private readonly AppDbContext _context;

        public CountryesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Friends
        public ActionResult Index()
        {
            var countryes = new List<Countryes>();

            var connectionString = "Server=tcp:milioliserver.database.windows.net,1433;Initial Catalog=TP03;Persist Security Info=False;User ID=milioli85;Password=Francisco85!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            using (var connection = new SqlConnection(connectionString))
            {
                var storagep = "GetCountry";
                var sqlCommand = new SqlCommand(storagep, connection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                try
                {
                    connection.Open();

                    using (var reader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            var country = new Countryes
                            {
                                IdCountry = (int)reader["IdCountry"],
                                CountryName = reader["CountryName"].ToString(),
                                CountryPhoto = reader["CountryPhoto"].ToString(),
                            };
                            countryes.Add(country);
                        }
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
            return View(countryes);
        }

        // GET: Friends/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friends = await _context.Countryes
                .FirstOrDefaultAsync(m => m.IdCountry == id);
            if (friends == null)
            {
                return NotFound();
            }

            return View(friends);
        }

        // GET: Friends/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Friends/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Countryes countryes)
        {
            var picture = EnviarImagem(countryes.Picture);
            if (ModelState.IsValid)
            {
                var connectionString = "Server=tcp:milioliserver.database.windows.net,1433;Initial Catalog=TP03;Persist Security Info=False;User ID=milioli85;Password=Francisco85!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

                using (var connection = new SqlConnection(connectionString))
                {
                    var storagep = "AddCountry";
                    var sqlCommand = new SqlCommand(storagep, connection);

                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@CountryName", countryes.CountryName);
                    sqlCommand.Parameters.AddWithValue("@CountryPhoto", await picture);                    


                    try
                    {
                        connection.Open();
                        sqlCommand.ExecuteNonQuery();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(countryes);
        }

        // GET: Friends/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friend = await _context.Countryes.FindAsync(id);
            if (friend == null)
            {
                return NotFound();
            }
            return View(friend);
        }

        // POST: Friends/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Countryes countryes)
        {
            string connectionString = "Server=tcp:milioliserver.database.windows.net,1433;Initial Catalog=TP03;Persist Security Info=False;User ID=milioli85;Password=Francisco85!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string consult = "EditCountry";
                SqlCommand sqlCommand = new SqlCommand(consult, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@IdCountry", countryes.IdCountry);               
                sqlCommand.Parameters.AddWithValue("@CountryName", countryes.CountryName);
                sqlCommand.Parameters.AddWithValue("@CountryPhoto", countryes.CountryPhoto);

                sqlCommand.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Friends/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friend = await _context.Countryes
                .FirstOrDefaultAsync(m => m.IdCountry == id);
            if (friend == null)
            {
                return NotFound();
            }

            return View(friend);
        }

        // POST: Friends/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            string connectionString = "Server=tcp:milioliserver.database.windows.net,1433;Initial Catalog=TP03;Persist Security Info=False;User ID=milioli85;Password=Francisco85!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string consult = "DeleteCountry";
                SqlCommand cmd = new SqlCommand(consult, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdCountry", id);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CountryExists(int id)
        {
            return _context.Countryes.Any(e => e.IdCountry == id);
        }

        public async Task<string> EnviarImagem(IFormFile imageFile)
        {

            var reader = imageFile.OpenReadStream();
            var cloundStorageAccount = CloudStorageAccount.Parse(@"DefaultEndpointsProtocol=https;AccountName=henriquestorage001;AccountKey=eOSkiZ1oQlx1i2+zfHpTc73Q4PWn7WwrMn7urcsCzApCNj2W2fMc279gIY1PyGC/AOQKzDujfSA6fp2ntb0xdg==;EndpointSuffix=core.windows.net");
            var blobClient = cloundStorageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("fotos1");
            await container.CreateIfNotExistsAsync();
            var blob = container.GetBlockBlobReference(Guid.NewGuid().ToString());
            await blob.UploadFromStreamAsync(reader);
            var uri = blob.Uri.ToString();
            return uri;
        }
    }
}
