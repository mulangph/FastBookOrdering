using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;


        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<CoverType> objCoverTypeList = _unitOfWork.CoverType.GetAll();

            return View(objCoverTypeList);
        }


        //GET
        public IActionResult Create()
        {
            return View();
        }


        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType obj)
        {


            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Cover Type added succesfully.";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Edit(int? Id)
        {

            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            var coverTypeFromDB = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == Id);

            if (coverTypeFromDB == null)
            {
                return NotFound();
            }

            return View(coverTypeFromDB);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType obj)
        {


            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Cover Type updated succesfully.";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //DELETE
        public IActionResult Delete(int? Id)
        {

            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            var coverTypeFromDB = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == Id);

            if (coverTypeFromDB == null)
            {
                return NotFound();
            }

            return View(coverTypeFromDB);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? Id)
        {

            var coverTypeFromDB = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == Id);

            if (coverTypeFromDB == null)
            {
                return NotFound();
            }

            _unitOfWork.CoverType.Remove(coverTypeFromDB);
            _unitOfWork.Save();
            TempData["success"] = "Cover Type deleted succesfully.";
            return RedirectToAction("Index");


        }

    }
}
