using Microsoft.AspNetCore.Mvc;
using Misha_Infotech_Machine_Test.Models;
using Newtonsoft.Json;

namespace Misha_Infotech_Machine_Test.Controllers
{
	public class UserController : Controller
	{
		private readonly HttpClient _client;
		private readonly IConfiguration configuration;
		public UserController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
		{
			this.configuration = configuration;
			_client = httpClientFactory.CreateClient();
			_client.BaseAddress = new Uri(configuration.GetSection("ApiAddress").Value!);
		}

        public async Task<IActionResult> Index()
        {
            List<User> users = new();

            try
            {
                var response = await _client.GetAsync("UserAPI/GetUsers");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    users = JsonConvert.DeserializeObject<List<User>>(json) ?? new List<User>();
                    foreach (var user in users)
                    {
                        if (!string.IsNullOrEmpty(user.Image) && !user.Image.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                        {
                            user.Image = $"{Request.Scheme}://{Request.Host}/{user.Image.TrimStart('/')}";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error: {ex.Message}");
            }

            return View(users);
        }

        public IActionResult CreateUser()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> CreateUser(User user)
		{
			if (user.Photo != null)
			{
				var fileName = $"{Guid.NewGuid()}{Path.GetExtension(user.Photo.FileName)}";
				var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
				if (!Directory.Exists(uploads))
					Directory.CreateDirectory(uploads);

				var filePath = Path.Combine(uploads, fileName);
				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{
					await user.Photo.CopyToAsync(fileStream);
				}
				user.Image = $"/uploads/{fileName}";
			}
            var request = await _client.PostAsJsonAsync("UserAPI/CreateUsers", user);
			if (request.IsSuccessStatusCode)
			{

			}
			else
			{
				ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
				return View();
			}
			return Ok();
		}
		[HttpGet]
		public async Task<IActionResult> GetCities(int stateId)
		{
            List<Cities> cities = new();
            var req = await _client.GetAsync("UserAPI/GetCities?StateId=" + stateId);
			if (req.IsSuccessStatusCode)
			{
				var response = await req.Content.ReadAsStringAsync();
				cities = JsonConvert.DeserializeObject<List<Cities>>(response);
			}
			return Ok(cities);
		}
		[HttpGet]
		public async Task<IActionResult> GetStates()
		{
            List<States> states = new();
            var req = await _client.GetAsync("UserAPI/GetStates");
			if (req.IsSuccessStatusCode)
			{
				var response = await req.Content.ReadAsStringAsync();
                states = JsonConvert.DeserializeObject<List<States>>(response);
			}
			return Ok(states);
		}
	}
}
