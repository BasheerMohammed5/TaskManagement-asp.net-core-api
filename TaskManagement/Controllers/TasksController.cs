//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using TaskManagement.Models;
//using System.Threading.Tasks;
//using System.Linq;


//namespace TaskManagement.Controllers
//{
//    public class TasksController : Controller
//    {
//        private readonly TaskContext _context;

//        public TasksController(TaskContext context)
//        {
//            _context = context;
//        }

//        // GET: Tasks
//        public async Task<IActionResult> Index()
//        {
//            return View(await _context.Tasks.ToListAsync());
//        }

//        // GET: Tasks/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var task = await _context.Tasks
//                .FirstOrDefaultAsync(m => m.TaskId == id);
//            if (task == null)
//            {
//                return NotFound();
//            }

//            return View(task);
//        }

//        // GET: Tasks/Create
//        public IActionResult Create()
//        {
//            return View();
//        }

//        // POST: Tasks/Create
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("TaskId,Title,Description,DueDate,IsCompleted")] TaskManagement.Models.Task task)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(task);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            return View(task);
//        }

//        // GET: Tasks/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var task = await _context.Tasks.FindAsync(id);
//            if (task == null)
//            {
//                return NotFound();
//            }
//            return View(task);
//        }

//        // POST: Tasks/Edit/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("TaskId,Title,Description,DueDate,IsCompleted")] TaskManagement.Models.Task task)
//        {
//            if (id != task.TaskId)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(task);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!TaskExists(task.TaskId))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            return View(task);
//        }

//        // GET: Tasks/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var task = await _context.Tasks
//                .FirstOrDefaultAsync(m => m.TaskId == id);
//            if (task == null)
//            {
//                return NotFound();
//            }

//            return View(task);
//        }

//        // POST: Tasks/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var task = await _context.Tasks.FindAsync(id);
//            _context.Tasks.Remove(task);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool TaskExists(int id)
//        {
//            return _context.Tasks.Any(e => e.TaskId == id);
//        }
//    }
//}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace TaskManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly TaskContext _context;

        public TasksController(TaskContext context)
        {
            _context = context;
        }

        // GET: api/Tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Task>>> GetTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Task>> GetTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return task;
        }

        // POST: api/Tasks
        [HttpPost]
        public async Task<ActionResult<Models.Task>> CreateTask(Models.Task task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTask), new { id = task.TaskId }, task);
        }

        // PUT: api/Tasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, Models.Task task)
        {
            if (id != task.TaskId)
            {
                return BadRequest();
            }

            _context.Entry(task).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.TaskId == id);
        }
    }
}
