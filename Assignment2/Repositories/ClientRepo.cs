using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment2.Data;
using Assignment2.ViewModels;

namespace Assignment2.Repositories
{
    public class ClientRepo
    {
        ApplicationDbContext db;

        public ClientRepo(ApplicationDbContext context)
        {
            db = context;
        }


        public bool Create(string LN, string FN, string email)
        {
            Client client = new Client()
            {
                lastName = LN,
                firstName = FN,
                email = email
            };

            db.Clients.Add(client);
            db.SaveChanges();

            return true;
        }

        public bool Update(int clientId, string LN, string FN)
        {

            Client client = db.Clients.Where(c => c.ID == clientId).FirstOrDefault();
            client.lastName = LN;
            client.firstName = FN;

            db.SaveChanges();

            return true;
        }
    }
}
