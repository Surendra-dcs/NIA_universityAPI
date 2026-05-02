using NIAUNIVERSITYPANELAPI.Models;

namespace NIAUNIVERSITYPANELAPI.Service
{
    public interface IExamService
    {
        string SaveExam(ExamModel model);
        List<ExamModel> GetExams();
        ExamModel GetExamById(int id);
        string DeleteExam(int id);

        List<Rolllist> GetRolllist();
        List<TabulationRegisterModel> GetTabulationRegister(int? examId = null, string? rollNo = null);
        List<Resultlist> Getresultlist();
        List<ReEvaluationModel> GetReEvaluationList(); 
    }
}
