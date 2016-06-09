using MyWeddingSystem.Models.Context;
using MyWeddingSystem.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace MyWeddingSystem.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        public override string ApplicationName
        {
            get
            {
                return this.GetType().Assembly.GetName().Name.ToString();
            }
            set
            {
                this.ApplicationName = this.GetType().Assembly.GetName().Name.ToString();
            }
        }

        public override bool RoleExists(string roleName)
        {
            return true;
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            return true;
        }

        public override string[] GetAllRoles()
        {
            return new string[0];
        }

        public override string[] GetUsersInRole(string roleName)
        {
            return new string[0];
        }

        public override string[] GetRolesForUser(string username)
        {
            UserRepository userRepository = new UserRepository();
            return userRepository.GetGroupByLogin(username);
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            return new string[0];
        }

        public override void CreateRole(string roleName)
        {
            return;
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            return true;
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            return;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            return;
        }
    }
}