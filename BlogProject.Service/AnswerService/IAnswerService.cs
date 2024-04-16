using BlogProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Service.AnswerService
{
    public interface IAnswerService
    {
        Task<List<Answer>> GetAnswerByQuestionIdAsync(int questionId);
        Task CreateNewAnswerAsync(Answer answer);
        Task UpdateAnswerAsync(Answer answer);
        Task DeleteAnswerAsync(Answer answer);
    }
}
