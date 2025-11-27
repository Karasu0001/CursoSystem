using CursoSystem.Models;
using CursoSystem.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CursoSystem.Services
{
    public interface ICourseComponentService
    {
        Task<List<CourseComponent>> GetByCourseAsync(int courseId);
        Task<CourseComponent?> GetByIdAsync(int id);
        Task<List<CourseComponent>> GetChildrenAsync(int parentId);
        Task<CourseComponent> CreateAsync(ComponentCreateDto dto);
        Task<bool> UpdateAsync(int id, ComponentUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
