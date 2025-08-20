using Aris3._0.Infrastructure.Data.Context;
using Aris3._0Fe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Net;
using System.Text;
namespace Aris3._0Fe.Controllers
{
    public class FilmController : Controller
    {
        private readonly ArisDbContext dbContext;

        public FilmController(ArisDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult Index(string id, string posterurl, string name, string original_name, string thumburl)
        {
            var episodeArray= new List<Episode>();

            if (id != null)
            {
                var film = dbContext.Films.FirstOrDefault(f => f.Id == id);
                if (film != null)
                {
                    film.View += 1;
                    dbContext.SaveChanges();
                }

                episodeArray = dbContext.Episodes.Where(e=>e.Server.Film.Id == id)
                                 .Include(s => s.Server)
                                     .ThenInclude(s => s.Film)
                                      .ThenInclude(f=>f.Countries)
                                 .Include(s => s.Server)
                                     .ThenInclude(s => s.Film)
                                      .ThenInclude(f => f.Categories)
                                .Include(s => s.Server)
                                     .ThenInclude(s => s.Film)
                                      .ThenInclude(f => f.Countries)
                                .Include(s => s.Server)
                                     .ThenInclude(s => s.Film)
                                      .ThenInclude(f => f.Actors)
                                 .ToList();
            }

            return View(episodeArray);
        }



        [HttpGet]
        public async Task<IActionResult> Search(string searchQuery, Guid id,string category)
        {
            if (searchQuery != null && id == Guid.Empty)
            {
                string RemoveDiacritics(string text)
                {
                    if (string.IsNullOrEmpty(text)) return text;
                    var normalized = text.Normalize(NormalizationForm.FormD);
                    var sb = new StringBuilder();
                    foreach (var c in normalized)
                    {
                        if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                            sb.Append(c);
                    }
                    return sb.ToString().Normalize(NormalizationForm.FormC).ToLower();
                }

                List<Film> films = await dbContext.Films.ToListAsync();

                List<Film> key;

                if (!string.IsNullOrWhiteSpace(searchQuery) && searchQuery.Length >= 3)
                {
                    string keywordStart = RemoveDiacritics(searchQuery.Substring(0, 3));

                    key = films.Where(f => !string.IsNullOrEmpty(f.Name) &&
                                           RemoveDiacritics(f.Name).StartsWith(keywordStart))
                               .ToList();

                    if (key.Count >= 1)
                    {
                        ViewBag.Count = key.Count;
                        ViewBag.TuKhoa = searchQuery;
                        return View(key);
                    }
                }
                else
                {
                    key = films;
                    ViewBag.Count = key.Count;
                    ViewBag.TuKhoa = searchQuery;
                    return View(key);
                }

                return NotFound("Không tìm thấy phim.");
            }
            else if (string.IsNullOrWhiteSpace(searchQuery) && id == Guid.Empty && !string.IsNullOrEmpty(category))
            {
                var decodedCategory = WebUtility.UrlDecode(category).Trim();

                var films = await dbContext.Films
                                           .Include(f => f.Categories)
                                           .Where(f => f.Categories.Any(c => c.Name.ToLower() == decodedCategory.ToLower()))
                                           .ToListAsync();
                var cate = dbContext.categories
                                    .FirstOrDefault(c => c.Name.ToLower() == decodedCategory.ToLower());
                ViewBag.Country = cate?.Name;
                return View(films);
            }
            else
            {
                var films = await dbContext.Films
                                           .Include(f => f.Countries)
                                           .Where(f => f.Countries.Any(c => c.Id == id))
                                           .ToListAsync();

                var cotry = dbContext.Countries.FirstOrDefault(c => c.Id == id);
                ViewBag.Country = cotry?.Name;
                return View(films);
            }
        }
    }
}


