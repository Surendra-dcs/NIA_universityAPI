using Microsoft.Data.SqlClient;
using NIAUNIVERSITYPANELAPI.Models;
using System.Data;
using Dapper;

namespace NIAUNIVERSITYPANELAPI.Service
{
    public class ExaminerService
    {
        private readonly string _connectionString;
        private readonly DbHelper _db;

        public ExaminerService(IConfiguration configuration, DbHelper db)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _db = db;
        }

        public async Task<int> Save(ExaminerModel model)
        {
            using SqlConnection con = new SqlConnection(_connectionString);

            var param = new DynamicParameters(model);
            param.Add("@Action", model.ExaminerID == 0 ? "INSERT" : "UPDATE");

            var result = await con.QuerySingleAsync<int>(
                "SP_Examiner_CRUD",
                param,
                commandType: CommandType.StoredProcedure
            );

            return result;
        }

        public async Task<List<ExaminerModel>> GetAll()
        {
            using SqlConnection con = new SqlConnection(_connectionString);

            return (await con.QueryAsync<ExaminerModel>(
                "SP_Examiner_CRUD",
                new { Action = "SELECT" },
                commandType: CommandType.StoredProcedure
            )).ToList();
        }

        public async Task<int> DeleteExaminerAsync(int id)
        {
            SqlParameter[] prm =
            {
            new SqlParameter("@Action", "DELETE"),
            new SqlParameter("@ExaminerID", id)
            };
            await _db.ExecuteDataAsync("SP_Examiner_CRUD", prm);
            return 1;
        }

        public async Task<ExaminerModel> GetById(int id)
        {
            using SqlConnection con = new SqlConnection(_connectionString);

            return await con.QueryFirstOrDefaultAsync<ExaminerModel>(
                "SP_Examiner_CRUD",
                new { Action = "GETBYID", ExaminerID = id },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<dynamic>> GetColleges()
        {
            using SqlConnection con = new SqlConnection(_connectionString);

            return await con.QueryAsync(
                "SELECT Id, Name FROM Colleges"
            );
        }

        public async Task<IEnumerable<dynamic>> GetCourses()
        {
            using SqlConnection con = new SqlConnection(_connectionString);

            return await con.QueryAsync(
                "SELECT course_Id, course_name FROM tbl_CourseMaster"
            );
        }

        public async Task<IEnumerable<dynamic>> GetSubjects(int courseId)
        {
            using SqlConnection con = new SqlConnection(_connectionString);

            return await con.QueryAsync(
                "SELECT subject_id, subject_name FROM tbl_MasterSubject WHERE course_id=@courseId",
                new { courseId }
            );
        }

        public async Task<IEnumerable<dynamic>> GetExams(int courseId)
        {
            using SqlConnection con = new SqlConnection(_connectionString);

            return await con.QueryAsync(
                "SELECT exam_id, exam_name FROM tbl_ExamMaster WHERE course_id=@courseId",
                new { courseId }
            );
        }

        public async Task<bool> AssignPaper(AssignPaperModel model)
        {
            using SqlConnection con = new SqlConnection(_connectionString);

            var rows = await con.ExecuteAsync(
                @"INSERT INTO AssignExaminerPaper
          (ExaminerId, CollegeId, CourseId, ExamId, SubjectId, PaperType)
          VALUES (@ExaminerId, @CollegeId, @CourseId, @ExamId, @SubjectId, @PaperType)",
                model
            );

            return rows > 0;
        }

    }
}
