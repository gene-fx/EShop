﻿namespace CatalogAPI.Exceptions
{
    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(Guid Id) : base("Product", Id) 
        { }

        public ProductNotFoundException(string message) : base(message)
        {
            
        }
    }
}
