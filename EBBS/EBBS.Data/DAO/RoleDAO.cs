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
    public class RoleDao : IRoleDao
    {
        private EbbSEntities context;

        public RoleDao()
        {
            context=new EbbSEntities();
            
        }

        public IList<Role> GetAllRoles()
        {
            return context.Role.ToList();

        }

        public Role RoleById(int id)
        {
            Role roleType = context.Role.SingleOrDefault(x => x.rId == id);
            return roleType;
        }

        public void InsertRole(Role newRole)
        {

            Role role = new Role();
            role.roleName = newRole.roleName;
            context.Role.Add(role);
            context.SaveChanges();
        }


        public void UpdateRole(Role editRole)
        {
            Role roleById = RoleById(editRole.rId);
            roleById.roleName = editRole.roleName;
            context.SaveChanges();
        }


        public void DeleteRole(Role deleteRole)
        {
            try
            {
                if (context.Role == null)
                {
                    throw new ArgumentNullException("deleteRole");
                }

                Role roleById = RoleById(deleteRole.rId);

                this.context.Role.Remove(roleById);

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

        public bool UniqueRole(string uniqueRole)
        {
            var result = true;
            var roleByUnique = context.Role.FirstOrDefault(x=>x.roleName== uniqueRole);

            if (roleByUnique != null)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public IEnumerable<Role> RoleIeNum
        {
            get { return context.Role; }
        }
    }
}
