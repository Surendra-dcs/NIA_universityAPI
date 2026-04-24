using NIAUNIVERSITYPANELAPI.Models;

namespace NIAUNIVERSITYPANELAPI.Service
{
    public interface ISubjectService
    {
        string SaveSubject(SubjectModel model);
        List<SubjectModel> GetSubjects();
        List<Course> GetCoursesByProgram(int programId);
        SubjectModel GetSubjectById(int id);
        string DeleteSubject(int id);
    }
}
