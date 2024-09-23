using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cart_API_Lab.Models;

namespace Cart_API_Lab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {

        private static List<CartItem> items = new List<CartItem>()
        {
            new CartItem(){ id=1, product="Tomato", price = 2, quantity = 1 },
            new CartItem(){ id=2, product="Orange", price = 1, quantity = 3 },
            new CartItem(){ id=3, product="Apple", price = 1.25, quantity = 4 }
        };

        private static int nextId = 4;
        //All, MaxPrice, Prefix, Page
        [HttpGet("")]
        
        public IActionResult MaxPrice(double? price, string? product, int? id)
        {

            List<CartItem> result = new();
            //Max Price
            //curl -K 'GET' \
            //'http://localhost:5195/api/Cart?price=1' \
            //-H 'accept: */*'

            if (price != null )
            {
                for (int i = 0; i < items.Count; i++)
                {
                    if (price >= items[i].price)
                    {
                        result.Add(items[i]);
                    }
                }
                return Ok(result);
            }
            //Prefix
            //curl -K 'GET' \
            //'http://localhost:5195/api/Cart?product=ap' \
            //-H 'accept: */*'

            if (product != null)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].product.Substring(0,3).ToLower().Contains(product.ToLower()))
                    {
                        result.Add(items[i]);
                    }
                }

                return Ok(result);
            }
            //Page
            //curl -K 'GET' \
            // 'http://localhost:5195/api/Cart?id=3' \
            // -H 'accept: */*'
            if (id != null)
            {
                for (int i = 0; i < id; i++)
                {
                    
                        result.Add(items[i]);
                 
                }
                return Ok(result);
            }


            return Ok(items);

        }

        //Id
        //curl -K 'GET' \
        //'http://localhost:5195/api/Cart/2' \
        //-H 'accept: */*'

        [HttpGet("{id}")]
        public IActionResult GetId(int id)
        {
            List<CartItem> result = items;

            int index = items.FindIndex(e => e.id == id);
            if (index == -1)
            {
                return NotFound("Item not found");
            }
            else
            {
                return Ok(result[index]);
            }

        }


        //Add New Item
    //    curl -K 'POST' \
    // 'http://localhost:5195/api/Cart' \
    //-H 'accept: */*' \
    //-H 'Content-Type: application/json' \
    //-d '{
    //"id": 6,
    //"product": "Mayo",
    //"price": 4,
    //"quantity": 3}'

        [HttpPost()]

        public IActionResult AddItem([FromBody] CartItem newItem)
        {
            newItem.id = nextId;
            items.Add(newItem);
            nextId++;
            return Created($"/api/Employees/{newItem.id}", newItem);
        }

        //Update
    //    curl -K 'PUT' \
    //'http://localhost:5195/api/Cart/2' \
    // -H 'accept: */*' \
    // -H 'Content-Type: application/json' \
    // -d '{
    // "id": 2,
    //"product": "pizza",
    //"price": 5,
    //"quantity": 2
    // }'

        [HttpPut("{id}")]

        public IActionResult UpdateItem(int id, [FromBody] CartItem updatedItem)
        {
            if (id != updatedItem.id) { return BadRequest(); }
            if (items.Any(e => e.id == id) == false) { return NotFound(); }

            int index = items.FindIndex(e => e.id == id);
            items[index] = updatedItem;
            return Ok(updatedItem);

        }


        //DELETE
        //      curl -K 'DELETE' \
        //'http://localhost:5195/api/Cart/2' \
        //-H 'accept: */*'

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            int index = items.FindIndex(e => e.id == id);
            if (index == -1)
            {
                return NotFound("Item not found");
            }
            else
            {
                items.RemoveAt(index);
                return NoContent();

            }
        }


    }
}
