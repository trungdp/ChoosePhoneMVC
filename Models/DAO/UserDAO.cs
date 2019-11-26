using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
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

        public bool Update(User user)
        {
            //try
            //{
                
            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            var updated = db.Users.Find(user.ID);
            updated.ModifiedDate = DateTime.Now;
            updated.UserName = user.UserName;
            updated.Phone = user.Phone;
            updated.Email = user.Email;
            updated.Address = user.Address;
            updated.Status = user.Status;
            db.SaveChanges();
            return true;
        }

        public User Detail(int ID) {
            return db.Users.Find(ID);
        }
        public IEnumerable<User> ListAllPaging(int page, int pageSize)
        {
            return db.Users.OrderByDescending(item=>item.ID).ToPagedList(page,pageSize);
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

        public bool Delete(int id)
        {
            try
            {
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
