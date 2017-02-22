using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sample_aspnet_core_api
{
    public interface ICustomerModule
    {
        void Add(Customer item);
        IEnumerable<Customer> GetAll();
        Customer Find(int id);
        void Remove(int id);
        void Update(Customer item);
    }

    public class CustomerModule : ICustomerModule
    {
        private readonly CustomerContext _context;

        public CustomerModule(CustomerContext context)
        {
            _context = context;
            Add(new Customer { Name = "First Customer" });
        }

        public IEnumerable<Customer> GetAll()
        {
            return _context.Customers.ToList();
        }

        public void Add(Customer item)
        {
            _context.Customers.Add(item);
            _context.SaveChanges();
        }

        public Customer Find(int id)
        {
            return _context.Customers.FirstOrDefault(t => t.Id == id);
        }

        public void Remove(int id)
        {
            var entity = _context.Customers.First(t => t.Id == id);
            _context.Customers.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Customer item)
        {
            _context.Customers.Update(item);
            _context.SaveChanges();
        }

    }
}
