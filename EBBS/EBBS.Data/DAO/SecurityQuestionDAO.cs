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
    public class SecurityQuestionDao : ISecurityQuestionDao
    {
        private EbbSEntities context;

        public SecurityQuestionDao()
        {
            context = new EbbSEntities();
        }

        public IList<SecurityQuestion> GetAllSecurityQuestions()
        {

           return context.SecurityQuestion.ToList();

        }

        

        public void InsertSecurityQuestion(SecurityQuestion newSecurityQuestion)
        {
            try
            {
                if (context.SecurityQuestion == null)
                {
                    throw new ArgumentNullException("newSecurityQuestion");
                }

                this.context.SecurityQuestion.Add(newSecurityQuestion);
                this.context.SaveChanges();
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
         
            SecurityQuestion singleRecord = context.SecurityQuestion.SingleOrDefault(x => x.qId == id);
            return singleRecord;
        }

        public void UpdateSecurityQuestion(SecurityQuestion editSecurityQuestion)
        {
            try
            {
                if (context.SecurityQuestion == null)
                {
                    throw new ArgumentNullException("editSecurityQuestion");
                }

                SecurityQuestion securityQuestion = GetSecurityQuestionById(editSecurityQuestion.qId);
                securityQuestion.question = editSecurityQuestion.question;
                this.context.SaveChanges();
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
                if (context.SecurityQuestion == null)
                {
                    throw new ArgumentNullException("deleteSecurityQuestion");
                }

                SecurityQuestion securityQuestion = GetSecurityQuestionById(deleteSecurityQuestion.qId);

                this.context.SecurityQuestion.Remove(securityQuestion);

                this.context.SaveChanges();

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

        public bool UniqueRole(string securityQuestion)
        {
            var result = true;
            var uniqueSecurityQuestion = context.SecurityQuestion.FirstOrDefault(x => x.question == securityQuestion);

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

        public List<SecurityQuestion> GetMySQs()
        {
            return context.SecurityQuestion.ToList();
        }

        public IEnumerable<SecurityQuestion> SqIeNum
        {
            get { return context.SecurityQuestion; }
        }
    }
}
