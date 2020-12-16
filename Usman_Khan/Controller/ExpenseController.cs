using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Usman_Khan.Models;
using Microsoft.EntityFrameworkCore;


namespace Usman_Khan.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {


        private ExpenseDatabase _database = new ExpenseDatabase();


        [HttpGet]
        public IEnumerable<Expense> Get()
        {
            return _database.Expenses;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _database.Expenses.FindAsync(id);
            if (item == null)
                return NotFound(new { error = "id is not found" });
            else
                return Ok(item);
        }

        /*    [HttpGet("{id}/{year}/{month}")]
            public async Task<IActionResult> Get(int id, int year, int month)
            {
                var item = await _database.Items.FindAsync(id);
                if (item == null)
                    return NotFound(new { error = "id is not found" });
                else
                    return Ok(item);
            } */


        [HttpPost]
        public async Task<IActionResult> Post(Expense expItem)
        {
            _database.Expenses.Add(expItem);
            await _database.SaveChangesAsync();
            return Ok(expItem);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(int id, Expense expItems)
        {
            if (id != expItems.Id)
            {
                return BadRequest();
            }

            var expItem = await _database.Expenses.FindAsync(id);
            if (expItem == null)
            {
                return NotFound();
            }

            expItem.Description = expItems.Description;
            expItem.Amount = expItems.Amount;
            expItem.Date = expItems.Date;
            expItem.UserId = expItems.UserId;

            try
            {
                await _database.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!TodoItemExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _database.Expenses.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _database.Expenses.Remove(todoItem);
            await _database.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoItemExists(int id) => _database.Expenses.Any(e => e.Id == id);

    }
}
