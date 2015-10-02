using GeekQuiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GeekQuiz.Controllers
{
    public class TriviaController : ApiController
    {
        private TriviaContext db = new TriviaContext();
        // GET api/trivia
        public TriviaQuestion Get()
        {
            var userId = User.Identity.Name;

            TriviaQuestion nextQuestion = NextQuestion(userId);

            if (nextQuestion == null)
            {
                return null;
            }

            return nextQuestion;
        }

        private TriviaQuestion NextQuestion(string userId)
        {
            var lastQuestionId = db.TriviaAnswers
                .Where(a => a.UserId == userId)
                .GroupBy(a => a.QuestionId)
                .Select(g => new { QuestionId = g.Key, Count = g.Count() })
                .OrderByDescending(q => new { q.Count, QuestionId = q.QuestionId })
                .Select(q => q.QuestionId)
                .FirstOrDefault();

            var questionsCount = db.TriviaQuestions.Count();

            var nextQuestionId = (lastQuestionId % questionsCount) + 1;
            return db.TriviaQuestions.Find(nextQuestionId);
        }

        // POST api/Trivia
        public bool Post(TriviaAnswer answer)
        {
            if (!ModelState.IsValid)
            {
                return false;
            }

            answer.UserId = User.Identity.Name;

            var isCorrect = this.Store(answer);
            return isCorrect;
        }

        private bool Store(TriviaAnswer answer)
        {
            this.db.TriviaAnswers.Add(answer);

            this.db.SaveChanges();
            var selectedOption = this.db.TriviaOptions.FirstOrDefault(o => o.Id == answer.OptionId
                && o.QuestionId == answer.QuestionId);

            return selectedOption.IsCorrect;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
