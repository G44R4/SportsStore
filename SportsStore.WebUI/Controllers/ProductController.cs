using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;

        //We want 4 products per page
        public int PageSize = 4;
 
        public ProductController(IProductRepository productRepository)
        {
            this.repository = productRepository;
        }
        public ViewResult List(string category, int page = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = repository.Products
                                     .Where(p => category == null || p.Category == category)
                                     .OrderBy(p => p.ProductID)
                                     .Skip((page - 1) * PageSize)
                                     .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ? repository.Products.Count() : repository.Products.Where (e => e.Category == category).Count()
                },
                CurrentCategory = category
            };
            return View(model);

             /* return View(repository.Products
                                      .OrderBy(p => p.ProductID)    //order by primary key
                                      .Skip((page -1) * PageSize)   //skip over products occuring before the start current pg
                                      .Take(PageSize));             //take only no. of specified products
             */
             
        }

        public FileContentResult GetImage(int productId)
        {
            Product prod = repository.Products
                                     .FirstOrDefault(p => p.ProductID == productId);
            if (prod != null)
            {
                return File(prod.ImageData, prod.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}

