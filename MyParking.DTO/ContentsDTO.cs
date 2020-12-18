using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyParking.DTO
{
    public class ContentsDTO
    {
        public int ID { get; set; }
        public string HTMLContent { get; set; }
        public string Description { get; set; }
        public bool CanEdit { get; set; }
        public int Seq { get; set; }
    }
    public class ShoppingDTO
    {
        public Customer Customer { get; set; }
        public ContentsDTO WebSite { get; set; }
        public decimal Price { get; set; }
        public int Qty { get; set; }
        public decimal GetTotal()
        {
            return Price * Qty;
        }

        public static ShoppingDTO InitDTO()
        {
            var customer = new Customer
            {
                Name = "Jimmy Wu",
                Age = 46,
                MobileNo = "1888888888"
            };
            var web = new ContentsDTO
            {
                ID = 1,
                HTMLContent = @"<p>Welcome to the Expense Manager:</p>
< p > This program will make it easier and faster to submit expense reports.All you need to do to get started is: < br />
  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; select<strong> New Expense Report</ strong ></ p >
              < p > It will guide you through to the Expense Summary page, where you will enter all your expenses(in any order) by selecting from the&nbsp;< br />
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; < strong > Add An Expense Item </ strong > from a drop down menu </ p >
                                   < p > The program will provide further instructions as you go along.</ p >
                                      < p > You will receive an e-mail when your Reimbursement is ready.Or if your manager is returning a report to you for changes.Meanwhile you can look up the status of your reports any time under Expense Report Status.This includes reports you have not submitted yet.</ p >
                                        < p > If you need help, or have suggestions, please call Kimberly McGill, phone number 714 - 427 - 3759.</ p >
                                        < p > &nbsp;</ p > ",
                Description = "Introduction",
                CanEdit = true,
                Seq = 1
            };
            return new ShoppingDTO
            {
                Customer = customer,
                WebSite = web,
                Price = (decimal)180.00,
                Qty = 50
            };
        }
    }
    public class Customer
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string MobileNo { get; set; }
    }
}
