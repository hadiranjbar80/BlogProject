﻿using BlogProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Service.QuestionService
{
    public interface IQuestionService
    {
        Task<List<Question>> GetQuestionsAsync();
        Task<List<Question>> GetLastCreatedQuestionsAsync();
        Task<List<Question>> GetQuestionsByArticleIdAsync(int articleId);
        Task<List<Question>> GetQuestionByUserIdAsync(string userId);
        Task<List<Question>> GetLastFiveQuestionsAsync();
        Task<Question> GetQuestionByIdAsync(int id); 
        Task CreateNewQuestionAsync(Question question);
        Task UpdateQuestion(Question question);
        Task DeleteQuestionAsync(Question question);
        Task<List<Question>> SearchQuestionAsync(string q);
    }
}
