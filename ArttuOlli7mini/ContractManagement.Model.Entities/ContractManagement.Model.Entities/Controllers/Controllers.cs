using System;
using ContractManagement.Model.Entities;
using ContractManagement.Model.DAL;

namespace ContractManagement.Controller
{
    public class ContractController
    {
        private ContractDAL contractDAL = new ContractDAL();

        public int CreateContract(string companyName, int creatorId)
        {
            Contract contract = new Contract
            {
                Company_name = companyName,
                The_Creator = creatorId,
                Created_date = DateTime.Now,
                Approved = false,
                Sent_to_external = false
            };
            return contractDAL.CreateContract(contract);
        }
    }
}