using CursoSystem.Models.DTOs;
using CursoSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace CursoSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseComponentsController : ControllerBase
    {
        private readonly ICourseComponentService _service;

        public CourseComponentsController(ICourseComponentService service)
        {
            _service = service;
        }

        [HttpGet("course/{courseId}")]
        public async Task<IActionResult> GetByCourse(int courseId)
        {
            try
            {
                var components = await _service.GetByCourseAsync(courseId);
                return Ok(components);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener componentes del curso: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var comp = await _service.GetByIdAsync(id);
                if (comp == null) return NotFound();
                return Ok(comp);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener componente: {ex.Message}");
            }
        }

        [HttpGet("children/{parentId}")]
        public async Task<IActionResult> GetChildren(int parentId)
        {
            try
            {
                var children = await _service.GetChildrenAsync(parentId);
                return Ok(children);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener componentes hijos: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ComponentCreateDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var created = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.ComponentId }, created);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear componente: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ComponentUpdateDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var updated = await _service.UpdateAsync(id, dto);
                if (!updated) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar componente: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _service.DeleteAsync(id);
                if (!deleted) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar componente: {ex.Message}");
            }
        }
    }
}