using BlogApp.Data;
using BlogApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Controllers
{
    public class PostController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;
        private readonly string[] _allowedExtension = { ".jpg", ".jpeg", ".png" };

        public PostController(AppDbContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;
        }

        // For the Index action, we will retrieve all posts from the database and pass them to the view. We will also retrieve all categories to display in a dropdown for filtering posts by category.
        [HttpGet]
        public IActionResult Index(int? categoryId)
        {
            var postQuery = _context.Posts.Include(p => p.Category).AsQueryable();

            if (categoryId.HasValue)
            {
                postQuery = postQuery.Where(p => p.CategoryId == categoryId);
            }
            var posts = postQuery.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            return View(posts);
        }

        // For the Create action, we will display a form for creating a new post. We will also retrieve all categories to display in a dropdown for selecting the category of the post.
        [HttpGet]
        public IActionResult Create()
        {
            var postViewModel = new PostViewModel();

            postViewModel.Categories = _context.Categories
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
                .ToList();

            return View(postViewModel);
        }

        // For the Create action, we will handle the form submission for creating a new post. We will validate the model, check the file extension of the uploaded image, and save the post to the database if everything is valid.
        [HttpPost]
        public async Task<IActionResult> Create(PostViewModel postViewModel)
        {
            if (ModelState.IsValid)
            {
                var inputFileExtesion = Path.GetExtension(postViewModel.FeatureImage.FileName).ToLower();
                bool isAllowed = _allowedExtension.Contains(inputFileExtesion);

                if (!isAllowed)
                {
                    ModelState.AddModelError("", "Invalid Image Format. Allowed Format are .jpg, .jpeg, and .png");
                    return View(postViewModel);
                }

                postViewModel.Post.FeatureImagePath = await UploadFiletoFolder(postViewModel.FeatureImage);
                postViewModel.Post.PublishedDate = DateTime.Now;
                await _context.Posts.AddAsync(postViewModel.Post);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            postViewModel.Categories = _context.Categories
               .Select(c => new SelectListItem
               {
                   Value = c.Id.ToString(),
                   Text = c.Name
               })
               .ToList();

            return View(postViewModel);
        }

        private async Task<string> UploadFiletoFolder(IFormFile file)
        {
            var inputFileExtension = Path.GetExtension(file.FileName);
            var fileName = Guid.NewGuid().ToString() + inputFileExtension;
            var wwwRootPath = _webHostEnviroment.WebRootPath;
            var imagesFolderPath = Path.Combine(wwwRootPath, "images");

            if (!Directory.Exists(imagesFolderPath))
            {
                Directory.CreateDirectory(imagesFolderPath);
            }

            var filePath = Path.Combine(imagesFolderPath, fileName);

            try
            {
                await using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

            }
            catch (Exception ex)
            {
                return "Error Uploading Image:" + ex.Message;
            }
            return "/images/" + fileName;
        }
    }
}