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
        public Client GetOneByEmail(string email)
        {
            var user = db.Clients.Where(c => c.email == email).FirstOrDefault();

            return user;
        }
        public bool isExist (string email)
        {
            var isRegister = GetOneByEmail(email);

            if (isRegister != null)
                return true;
            return false;
        }

        public bool Create(string UN, string LN, string FN, string email)
        {
            Client client = new Client()
            {
                userName = UN,
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
