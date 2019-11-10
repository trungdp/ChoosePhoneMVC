using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class UserDAO
    {
        private OnlineShopDbContext db = null;

        public UserDAO()
        {
            db = new OnlineShopDbContext();
        }

        public long Insert(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
            return user.ID;
        }

        public User FindByName(string userName)
        {
            return db.Users.SingleOrDefault(x=>x.UserName == userName);
        }

        public bool Login(string userName, string password)
        {
            var result = db.Users.Count(x => x.UserName == userName && x.Password == password);
            return result > 0;
        }
    }
}
