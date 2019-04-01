using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using EBBS.Data.IDAO;

namespace EBBS.Data.DAO
{
    public class SecurityQuestionDAO : ISecurityQuestionDAO
    {
        private EBBSEntities db;

        public SecurityQuestionDAO()
        {
            db = new EBBSEntities();
        }

        public IList<SecurityQuestion> GetAllSecurityQuestions()
        {

            //IQueryable<SecurityQuestion> _securityQuestions;
            //_securityQuestions = from sq 
            //                     in db.SecurityQuestion
            //                     select sq;
            //return _securityQuestions.ToList<SecurityQuestion>();

           return db.SecurityQuestion.ToList();

        }

        
        public void InsertSecurityQuestion(SecurityQuestion newSecurityQuestion)
        {
            try
            {
                if (db.SecurityQuestion == null)
                {
                    throw new ArgumentNullException("newSecurityQuestion");
                }

                this.db.SecurityQuestion.Add(newSecurityQuestion);
                this.db.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                string errorMessage = "";
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {

                    foreach (var validationError in validationErrors.ValidationErrors)
                    {

                        errorMessage += string.Format("Property: {0} Error: {1}",
                                            validationError.PropertyName, validationError.ErrorMessage) +
                                        Environment.NewLine;
                    }
                }

                throw new Exception(errorMessage, dbEx);
            }
        }

        public SecurityQuestion GetSecurityQuestionById(int id)
        {
            //SecurityQuestion sq = db.SecurityQuestion.Where(x => x.qId == id).FirstOrDefault();
            //return sq;

            SecurityQuestion singleRecord = db.SecurityQuestion.SingleOrDefault(x => x.qId == id);
            return singleRecord;
        }

        public void UpdateSecurityQuestion(SecurityQuestion editSecurityQuestion)
        {
            try
            {
                if (db.SecurityQuestion == null)
                {
                    throw new ArgumentNullException("editSecurityQuestion");
                }

                SecurityQuestion securityQuestion = GetSecurityQuestionById(editSecurityQuestion.qId);
                securityQuestion.question = editSecurityQuestion.question;
                this.db.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                string errorMessage = "";
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {

                    foreach (var validationError in validationErrors.ValidationErrors)
                    {

                        errorMessage += string.Format("Property: {0} Error: {1}",
                                            validationError.PropertyName, validationError.ErrorMessage) +
                                        Environment.NewLine;
                    }
                }

                throw new Exception(errorMessage, dbEx);
            }
        }

        public void DeleteSecurityQuestion(SecurityQuestion deleteSecurityQuestion)
        {
            try
            {
                if (db.SecurityQuestion == null)
                {
                    throw new ArgumentNullException("deleteSecurityQuestion");
                }

                SecurityQuestion securityQuestion = GetSecurityQuestionById(deleteSecurityQuestion.qId);

                this.db.SecurityQuestion.Remove(securityQuestion);

                this.db.SaveChanges();

            }

            catch (DbEntityValidationException dbEx)
            {
                string errorMessage = "";

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessage += Environment.NewLine + string.Format("Property: {0} Error: {1}",
                                            validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                throw new Exception(errorMessage, dbEx);
            }
        }

        public bool UniqueRole(string secQuestion)
        {
            var result = true;
            var uniqueSecurityQuestion = db.SecurityQuestion.FirstOrDefault(x => x.question == secQuestion);

            if(uniqueSecurityQuestion != null)
            {
                result = true;
            }
            else
            {
                result = false;
            }

            return result;

            
        }
    }
}
