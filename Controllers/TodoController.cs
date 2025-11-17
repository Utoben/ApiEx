using Microsoft.AspNetCore.Mvc;
using ApiEx.Models;

namespace ApiEx.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private static List<TodoItem> _items = new();
        private static int _nextId = 1;

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_items);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item = _items.FirstOrDefault(x => x.Id == id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TodoItem item)
        {
            item.Id = _nextId++;
            _items.Add(item);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] TodoItem update)
        {
            var item = _items.FirstOrDefault(x => x.Id == id);
            if (item == null) return NotFound();

            item.Title = update.Title;
            item.IsDone = update.IsDone;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _items.FirstOrDefault(x => x.Id == id);
            if (item == null) return NotFound();

            _items.Remove(item);
            return NoContent();
        }
    }
}
