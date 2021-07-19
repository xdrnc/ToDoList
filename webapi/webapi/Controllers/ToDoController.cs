using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using webapi.Models;

namespace webapi.Controllers
{
    public class ToDoController : ApiController
    {
        // Allow CORS for all origins. (Caution!)
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        // GET
        /// <summary>
        /// this gets all the current to-do lists
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult GetAllToDoItems()
        {
            IList<ToDoItemViewModel> toDoItems = null;
            using (var data = new GCUK_ToDoItemDBEntities())
            {
                toDoItems = data.ToDoItems
                            .Select(t => new ToDoItemViewModel()
                            {
                                Id = t.id,
                                Description = t.description,
                                Priority = t.priority
                            }).ToList();
                
                if(toDoItems.Count == 0)
                {
                    return NotFound();
                }
                return Ok(toDoItems);
            }
        }


        [EnableCors(origins: "*", headers: "*", methods: "*")]
        // POST
        /// <summary>
        /// this creates new to-do list, accepting the description and the priority
        /// </summary>
        /// <param name="toDoItemViewModel"></param>
        /// <returns></returns>
        public IHttpActionResult CreateNewToDoItem(ToDoItemViewModel toDoItemViewModel)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Invalid data of the new to-do item. Please contact the support team.");
            }

            using (var data = new GCUK_ToDoItemDBEntities())
            {
                data.ToDoItems.Add(new ToDoItem()
                {
                    description = toDoItemViewModel.Description,
                    priority = toDoItemViewModel.Priority
                });

                data.SaveChanges();
            }

            return Ok("New to-do item has been created successfully :)");
        }


        [EnableCors(origins: "*", headers: "*", methods: "*")]
        // PUT
        /// <summary>
        /// this modifies the current to-do list, requires the id indicating which record we want to modify, as well as the modified description and/or priority.
        /// </summary>
        /// <param name="toDoItemViewModel"></param>
        /// <returns></returns>
        [HttpPut] //alextest post clashes with put
        public IHttpActionResult ModifyToDoItem(ToDoItemViewModel toDoItemViewModel)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Invalid data of the new to-do item. Please contact the support team.");
            }

            using (var data = new GCUK_ToDoItemDBEntities())
            {
                var existingToDoItem = data.ToDoItems.Where(t => t.id == toDoItemViewModel.Id).FirstOrDefault();

                if (existingToDoItem != null)
                {
                    existingToDoItem.description = toDoItemViewModel.Description;
                    existingToDoItem.priority = toDoItemViewModel.Priority;

                    data.SaveChanges();
                }
                else return NotFound();
            }

            return Ok("to-do item is modified successfully :)");
        }


        [EnableCors(origins: "*", headers: "*", methods: "*")]
        // DELETE
        /// <summary>
        /// This deletes the to-do list based on the id entered.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult DeleteToDoItem(int id)
        {
            if(id <= 0)
            {
                return BadRequest("The id for the todo item is not valid");
            }

            using (var data = new GCUK_ToDoItemDBEntities())
            {
                var existingToDoItem = data.ToDoItems.Where(t => t.id == id).FirstOrDefault();

                data.Entry(existingToDoItem).State = System.Data.Entity.EntityState.Deleted;

                data.SaveChanges();
            }

                return Ok("Selected to-do list of ID: " + id + " has been deleted successfully");
        }
    }
}
