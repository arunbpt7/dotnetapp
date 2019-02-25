using PDFTestCore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace PDFTestCore.Controllers
{
    public class EFormsController:Controller
    {
        private EformsContext _context;

        public EFormsController(EformsContext context)
        {
            _context = context;
        }

         public IActionResult Index()
        {
            // return View(_context.Ll30Snippets.ToList());
            return View(_context.Ll30Forms.ToList());
        }

    }
}