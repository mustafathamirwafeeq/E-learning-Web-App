using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ELearning.Models;
using System.Configuration;

namespace ELearning.Controllers
{
    public class EvaluationsController : ApiController
    {
        private GreenLightFeaturesEntities db = new GreenLightFeaturesEntities();

        // GET: api/Evaluations
        public IQueryable<Evaluation> GetEvaluations()
        {
            return db.Evaluation;
        }

        // GET: api/Evaluations/5
        [ResponseType(typeof(Evaluation))]
        public async Task<IHttpActionResult> GetEvaluation(int id)
        {
            Evaluation evaluation = await db.Evaluation.FindAsync(id);
            if (evaluation == null)
            {
                return NotFound();
            }

            return Ok(evaluation);
        }

        // PUT: api/Evaluations/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEvaluation(int id, Evaluation evaluation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != evaluation.Id)
            {
                return BadRequest();
            }

            db.Entry(evaluation).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EvaluationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Evaluations
        /// <summary>
        /// Add new evalution in to database
        /// </summary>
        [ResponseType(typeof(Evaluation))]
        public async Task<IHttpActionResult> PostEvaluation(Evaluation evaluation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!ExceedEvaluations(evaluation.StudentId, evaluation.ExamId))
            {
                db.Evaluation.Add(evaluation);
                await db.SaveChangesAsync();

                return CreatedAtRoute("DefaultApi", new { id = evaluation.Id }, evaluation);
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.NotAcceptable);
            }
        }

        // DELETE: api/Evaluations/5
        [ResponseType(typeof(Evaluation))]
        public async Task<IHttpActionResult> DeleteEvaluation(int id)
        {
            Evaluation evaluation = await db.Evaluation.FindAsync(id);
            if (evaluation == null)
            {
                return NotFound();
            }

            db.Evaluation.Remove(evaluation);
            await db.SaveChangesAsync();

            return Ok(evaluation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EvaluationExists(int id)
        {
            return db.Evaluation.Count(e => e.Id == id) > 0;
        }

        /// <summary>
        /// Check the count of existing evalutions for provided student and exam
        /// </summary>
        private bool ExceedEvaluations(int studentId, int examId)
        {
            string limit = ConfigurationManager.AppSettings["ExamTakenLimit"];
            return db.Evaluation.Count(e => e.StudentId == studentId && e.ExamId == examId) >= Convert.ToInt32(limit);
        }
    }
}