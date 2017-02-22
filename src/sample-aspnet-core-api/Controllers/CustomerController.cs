using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace sample_aspnet_core_api.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly ICustomerModule _customerModule;

        public CustomerController(ICustomerModule customerModule)
        {
            _customerModule = customerModule;
        }

        #region snippet_GetAll
        [HttpGet]
        public IEnumerable<Customer> GetAll()
        {
            return _customerModule.GetAll();
        }

        [HttpGet("{id}", Name = "GetCustomer")]
        public IActionResult GetById(int id)
        {
            var item = _customerModule.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        #endregion
        #region snippet_Create
        [HttpPost]
        public IActionResult Create([FromBody] Customer model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            _customerModule.Add(model);

            return CreatedAtRoute("GetTodo", new { id = model.Id }, model);
        }
        #endregion

        #region snippet_Update
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Customer model)
        {
            if (model == null || model.Id != id)
            {
                return BadRequest();
            }

            var customer = _customerModule.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            customer.Name = model.Name;

            _customerModule.Update(customer);
            return new NoContentResult();
        }
        #endregion

        #region snippet_Delete
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var todo = _customerModule.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _customerModule.Remove(id);
            return new NoContentResult();
        }
        #endregion
    }
}
