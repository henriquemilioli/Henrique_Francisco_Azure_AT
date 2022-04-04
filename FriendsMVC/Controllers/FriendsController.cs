using FriendsMVC.Data;
using FriendsMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsMVC.Controllers
{
    public class FriendsController : Controller
    {
        private readonly AppDbContext _context;

        public FriendsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Friends
        public ActionResult Index()
        {
            var friends = new List<Friends>();

            var connectionString = "Server=tcp:milioliserver.database.windows.net,1433;Initial Catalog=TP03;Persist Security Info=False;User ID=milioli85;Password=Francisco85!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            using (var connection = new SqlConnection(connectionString))
            {
                var storagep = "GetFriends";
                var sqlCommand = new SqlCommand(storagep, connection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                try
                {
                    connection.Open();

                    using (var reader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            var friend = new Friends
                            {
                                FriendId = (int)reader["FriendId"],
                                Name = reader["Name"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                PhoneNumber = reader["PhoneNumber"].ToString(),
                                Country = reader["Country"].ToString(),
                                Birth = (DateTime)reader["Birth"],
                                FriendsPhoto = reader["FriendsPhoto"].ToString(),
                                Email = reader["Email"].ToString(),

                            };
                            friends.Add(friend);
                        }
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
            return View(friends);
        }

        // GET: Friends/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friends = await _context.Friends
                .FirstOrDefaultAsync(m => m.FriendId == id);
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
        public ActionResult Create(Friends friend)
        {
            if (ModelState.IsValid)
            {
                var connectionString = "Server=tcp:milioliserver.database.windows.net,1433;Initial Catalog=TP03;Persist Security Info=False;User ID=milioli85;Password=Francisco85!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

                using (var connection = new SqlConnection(connectionString))
                {
                    var storagep = "AddFriends";
                    var sqlCommand = new SqlCommand(storagep, connection);

                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@Name", friend.Name);
                    sqlCommand.Parameters.AddWithValue("@LastName", friend.LastName);
                    sqlCommand.Parameters.AddWithValue("@Email", friend.Email);
                    sqlCommand.Parameters.AddWithValue("@Birth", friend.Birth);
                    sqlCommand.Parameters.AddWithValue("@PhoneNumber", friend.PhoneNumber);
                    sqlCommand.Parameters.AddWithValue("@Country", friend.Country);
                    sqlCommand.Parameters.AddWithValue("@FriendsPhoto", friend.FriendsPhoto);


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
            return View(friend);
        }

        // GET: Friends/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friend = await _context.Friends.FindAsync(id);
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
        public IActionResult Edit(int id, Friends friend)
        {
            string connectionString = "Server=tcp:milioliserver.database.windows.net,1433;Initial Catalog=TP03;Persist Security Info=False;User ID=milioli85;Password=Francisco85!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string consult = "EditFriend";
                SqlCommand sqlCommand = new SqlCommand(consult, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@FriendId", friend.FriendId);
                sqlCommand.Parameters.AddWithValue("@Name", friend.Name);
                sqlCommand.Parameters.AddWithValue("@LastName", friend.LastName);
                sqlCommand.Parameters.AddWithValue("@Email", friend.Email);
                sqlCommand.Parameters.AddWithValue("@Birth", friend.Birth);
                sqlCommand.Parameters.AddWithValue("@PhoneNumber", friend.PhoneNumber);
                sqlCommand.Parameters.AddWithValue("@Country", friend.Country);
                sqlCommand.Parameters.AddWithValue("@FriendsPhoto", friend.FriendsPhoto);
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

            var friend = await _context.Friends
                .FirstOrDefaultAsync(m => m.FriendId == id);
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
                string consult = "DeleteFriend";
                SqlCommand cmd = new SqlCommand(consult, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FriendId", id);
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

        private bool FriendsExists(int id)
        {
            return _context.Friends.Any(e => e.FriendId == id);
        }
    }
}
