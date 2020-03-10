using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WallMessage.Models;
using WallMessage.Services;

namespace WallMessage.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly MessageManager _messageManager;

        public HomeController(UserManager<User> userManager, MessageManager messageManager)
        {
            _userManager = userManager;
            _messageManager = messageManager;
        }
        public async Task<IActionResult> Index()
        {
            var messageList = await _messageManager.GetAll();
            return View(messageList);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Message model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                ModelState.AddModelError("", "Błąd dodawania wiadomości");
                return View(model);
            }

            model.User = user;

            await _messageManager.Add(model);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return BadRequest();

            var message = await _messageManager.Get(id, user);

            if (message == null)
                return BadRequest();

            return View(message);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [FromForm] Message model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return BadRequest();

            model.Id = id;
            model.User = user;

            await _messageManager.Edit(model);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return BadRequest();

            var message = await _messageManager.Get(id, user);

            if (message == null)
                return BadRequest();

            return View(message);
        }

        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return BadRequest();

            await _messageManager.Delete(id, user);

            return RedirectToAction("Index");
        }
    }
}