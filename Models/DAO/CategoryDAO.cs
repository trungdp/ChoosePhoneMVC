using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class CategoryDAO
    {
        private OnlineShopDbContext db = null;

        public CategoryDAO()
        {
            db = new OnlineShopDbContext();
        }

        public long Insert(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
            return category.ID;
        }

        public bool Update(Category user)
        {
            try
            {
                var updated = db.Categories.Find(user.ID);
                updated.ModifiedDate = DateTime.Now;
                updated.Name = user.Name;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Category Detail(int ID)
        {
            return db.Categories.Find(ID);
        }
        public IEnumerable<Category> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Category> model = db.Categories;
            if (!String.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.OrderByDescending(item => item.ID).ToPagedList(page, pageSize);
        }

        public Category FindByName(string Name)
        {
            return db.Categories.SingleOrDefault(x => x.Name == Name);
        }


        public bool Delete(int id)
        {
            try
            {
                var category = db.Categories.Find(id);
                db.Categories.Remove(category);
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
