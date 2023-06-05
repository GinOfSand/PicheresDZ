using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picheres1
{
   
    internal class ADBService
    {
       

        public List<Image> PicherImage()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.images.ToList();
            }
        }
        public Image AddPcihers(Image im)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.images.Add(im);
                db.SaveChanges();
                return im;
            }
        }
        public void RemovePichers(string name)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Image i = db.images.FirstOrDefault(x => x.Name == name);
                db.images.Remove(i);
                db.SaveChanges();
            }
        }
        public void RenamePichers(string oldname,string newName)
        {
            using(ApplicationDbContext db = new ApplicationDbContext()) { 
                Image i = db.images.FirstOrDefault(p=>p.Name == oldname);

                i.Name = newName;
                
                db.SaveChanges();
            }
        }
        public byte[] SearchImage(string name)
        {
            using (ApplicationDbContext db = new ApplicationDbContext()) 
            {
                Image im = db.images.FirstOrDefault(p => p.Name == name);
                byte[] map = im.Data.ToArray();
                return map;
            }
        }

    }
}
