﻿using Project.ENTITIES.Models;

namespace Project.COREMVC.Areas.Admin.Models.Products.PageVMS
{
    public class CreateProductPageVM
    {
        //Refactor Domain Entity'leri düzenleyin
        public List<Category> Categories { get; set; }
        public Product Product { get; set; }

    }
}
