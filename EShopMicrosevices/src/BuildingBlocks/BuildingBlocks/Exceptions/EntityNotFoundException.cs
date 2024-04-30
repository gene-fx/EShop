using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Exceptions
{
    public class EntityNotFoundException : NotFoundException
    {
        public EntityNotFoundException(string message) : base(message)
        {
        }
    }
}
