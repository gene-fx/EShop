using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Exceptions
{
    public class BasketNotFoundException : NotFoundException
    {
        public BasketNotFoundException(string UserName) : base(UserName)
        {
            
        }
    }
}
