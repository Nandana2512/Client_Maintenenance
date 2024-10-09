using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client_Maintenance.GUI;
using Client_Maintenance.VALIDATION;
using Client_Maintenance.DAL;

namespace Client_Maintenance.BLL
{
    public  class Clients
    {
        private int clientNumber;
        private string lastName;
        private string firstName;       
        private string phoneNumber;
        private string email;


        public int ClientNumber { get => clientNumber; set => clientNumber = value; }
       
        public string LastName { get => lastName; set => lastName = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string Email { get => email; set => email = value; }

        public Clients()
        {
            clientNumber = 1111;
            lastName = string.Empty;
            firstName = string.Empty;            
            phoneNumber = string.Empty;
            email = string.Empty;

        }


        public void SaveClient(Clients cli)
        {
            ClientDB.SaveRecord(cli);

        }


        public List<Clients> GetClientList()
        {
            return ClientDB.GetAllRecords();
        }

        public Clients SearchClient(int cliNum)
        {
            return ClientDB.SearchRecord(cliNum);

        }

        public List<Clients> SearchClient(string input)
        {
            return ClientDB.SearchRecord(input);
        }

        public bool IsUniqueClientNumber(int cliNum) => ClientDB.IsUniqueClientNumber(cliNum);
    }
}
