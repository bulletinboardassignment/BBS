using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EBBS.Data;
using EBBS.Data.IDAO;


namespace EBBS.Data.DAO
{
    public class RoleDAO : IRoleDAO
    {
        private EBBSEntities context;

        public RoleDAO()
        {
            context=new EBBSEntities();
            
        }

        public IList<Role> GetAllRoles()
        {
            return context.Role.ToList();

        }

        public Role RoleById(int id)
        {
            //Role roleType = context.Role.Where(x => x.rId == id).FirstOrDefault();
            //return roleType;

            Role roleType = context.Role.SingleOrDefault(x => x.rId == id);
            return roleType;
        }

        public void InsertRole(Role newRole)
        {

            Role _Role = new Role();
            _Role.roleName = newRole.roleName;
            context.Role.Add(_Role);
            context.SaveChanges();
        }

        //public void Edit(Role editRole)
        //    {
               

        //            try
        //            {
        //                if (context.Role == null)
        //                {
        //                    throw new ArgumentNullException("editRole");
        //                }

        //                Role role = RoleById(editRole.rId);
        //                role.roleName = editRole.roleName;
        //                this.context.SaveChanges();
        //            }
        //            catch (DbEntityValidationException dbEx)
        //            {
        //                string errorMessage = "";
        //                foreach (var validationErrors in dbEx.EntityValidationErrors)
        //                {

        //                    foreach (var validationError in validationErrors.ValidationErrors)
        //                    {

        //                        errorMessage += string.Format("Property: {0} Error: {1}",
        //                                            validationError.PropertyName, validationError.ErrorMessage) 
        //                                        + Environment.NewLine;
        //                    }
        //                }

        //                throw new Exception(errorMessage, dbEx);
        //            }

        //}


        public void UpdateRole(Role editRole)
        {
            Role role = RoleById(editRole.rId);
            role.roleName = editRole.roleName;
            context.SaveChanges();
        }


        public void DeleteRole(Role deRole)
        {
            try
            {
                if (context.Role == null)
                {
                    throw new ArgumentNullException("delRole");
                }

                Role roleBy = RoleById(deRole.rId);

                this.context.Role.Remove(roleBy);

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

        public bool UniqueRole(string roleName)
        {
            var result = true;
            var roleUnique = context.Role.FirstOrDefault(x=>x.roleName==roleName);

            if (roleUnique != null)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        //public void Save(Role role)
        //{
        //    if (role.rId == 0)
        //    {

        //        Role _Role = new Role();
        //        _Role.roleName = role.roleName;
        //        context.Role.Add(_Role);
        //        context.SaveChanges();

        //    }
        //    else
        //    {
        //        Role dbEntry = context.Role.Find(role.rId);
        //        if (dbEntry != null)
        //        {
        //            dbEntry.rId = role.rId;
        //            dbEntry.roleName = role.roleName;
        //            context.SaveChanges();
        //            role.rId = dbEntry.rId;
        //        }
        //    }
        //}
    }
}
