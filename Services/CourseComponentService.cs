using CursoSystem.Data;
using CursoSystem.Models;
using CursoSystem.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoSystem.Services
{
    public class CourseComponentService : ICourseComponentService
    {
        private readonly ApplicationDbContext _context;

        public CourseComponentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CourseComponent>> GetByCourseAsync(int courseId)
        {
            return await _context.CourseComponents
                .Where(c => c.CourseId == courseId && c.ParentId == null)
                .Include(c => c.Children)
                .ToListAsync();
        }

        public async Task<CourseComponent?> GetByIdAsync(int id)
        {
            return await _context.CourseComponents
                .Include(c => c.Children)
                .FirstOrDefaultAsync(c => c.ComponentId == id);
        }

        public async Task<List<CourseComponent>> GetChildrenAsync(int parentId)
        {
            return await _context.CourseComponents
                .Where(c => c.ParentId == parentId)
                .ToListAsync();
        }

        public async Task<CourseComponent> CreateAsync(ComponentCreateDto dto)
        {
            var component = new CourseComponent
            {
                CourseId = dto.CourseId,
                ParentId = dto.ParentId,
                Name = dto.Name,
                Type = dto.Type,
                Content = dto.Content,
                Position = dto.Position,
                DurationMinutes = dto.DurationMinutes
            };

            _context.Add(component);
            await _context.SaveChangesAsync();
            return component;
        }

        public async Task<bool> UpdateAsync(int id, ComponentUpdateDto dto)
        {
            var component = await _context.CourseComponents.FindAsync(id);
            if (component == null)
                return false;

            component.Name = dto.Name;
            component.Type = dto.Type;
            component.Content = dto.Content;
            component.Position = dto.Position;
            component.DurationMinutes = dto.DurationMinutes;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var comp = await _context.CourseComponents.FindAsync(id);
            if (comp == null)
                return false;

            _context.CourseComponents.Remove(comp);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
