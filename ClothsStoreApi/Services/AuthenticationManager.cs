using ClothsStore.BL.Models;
using ClothsStore.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothsStore.Api.Services
{
    public class AuthenticationManager
    {
        private DesignTimeDbContextFactory _ctxFactory;

        public AuthenticationManager()
        {
            _ctxFactory = new DesignTimeDbContextFactory();
        }
        public User AuthenticateUser(User user)
        {
            if (user.Username != null && user.HashedPassword != null)
            {
                var args = new String[] { "" };
                using (var context = _ctxFactory.CreateDbContext(args))
                {
                    var users = context.User.ToList<User>();
                    IEnumerable<User> filteringQuery =
                        from usr in users
                        where usr.Username.Trim() == user.Username.Trim()
                        && usr.HashedPassword.Trim() == user.HashedPassword.Trim()
                        select usr;

                    var filteredUser = filteringQuery.FirstOrDefault();

                    return filteredUser;
                }
            }
            return null;
        }

        internal bool checkUserExists(string username)
        {
            var args = new String[] { "" };
            using (var context = _ctxFactory.CreateDbContext(args))
            {
                var users = context.User.ToList<User>();
                IEnumerable<User> filteringQuery =
                    from usr in users
                    where usr.Username.Trim() == username.Trim()
                    select usr;

                return filteringQuery.ToList().Count > 0;
            }
        }
    }
}
