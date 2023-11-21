using MaksBooks.DataAccess.Repository.IRepository;
using MaksBooks.Models;
using Microsoft.AspNetCore.Mvc;

namespace MaksBookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Product product = new Product();
            if (id == null)
            {
                return View(product);
            }

            product = _unitOfWork.Product.Get(id.GetValueOrDefault());
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        //use http post to define the post action method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Product product)
        {
            if (ModelState.IsValid) // check all validations
            {
                if (product.Id == 0)
                {
                    _unitOfWork.Product.Add(product);
                }
                else
                {
                    _unitOfWork.Product.Update(product);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));  // to see all categories
            }
            return View(product);
        }

        // To call all apis

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            //return NotFound();
            var allObj = _unitOfWork.Product.GetAll();
            return Json(new { data = allObj });
        }


        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Product.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Delete while deleting" });
            }
            _unitOfWork.Product.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete successful" });
        }
        #endregion
    }
}
