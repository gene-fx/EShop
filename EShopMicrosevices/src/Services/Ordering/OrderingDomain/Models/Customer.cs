using OrderingDomain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingDomain.Models
{
    public class Customer : Entity<Guid>
    {
        public string Name { get; private set; } = default!;

        public string Email { get; private set; } = default!;
    }
}
