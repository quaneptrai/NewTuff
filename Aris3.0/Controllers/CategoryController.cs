    using Aris3._0.Application.DTOs;
    using Aris3._0.Application.Interface.Repositories;
    using Aris3._0.Application.Interface.Service;
    using Aris3._0.Domain.Entities;
    using Aris3._0.Infrastructure.Data.Context;
    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json.Linq;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Support.UI;
    using SeleniumExtras.WaitHelpers;
    using System.Runtime.InteropServices;
using System.Threading.Tasks;

    namespace Aris3._0.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class CategoryController : ControllerBase
        {
            private readonly HttpClient client;
            private readonly ArisDbContext dbContext;
            private readonly IMapper mapper;
            private readonly ICategoryService categoryRepository;

            public CategoryController(HttpClient client, ArisDbContext dbContext, IMapper mapper,ICategoryService categoryRepository)
            {
                this.client = client;
                this.dbContext = dbContext;
                this.mapper = mapper;
                this.categoryRepository = categoryRepository;
            }
            [HttpGet]
            public async Task<IActionResult> GetAllNewUpdateCategory()
            {
               var cate = await categoryRepository.FetchCategoriesFromApiAsync();
               return Ok(cate);
            }
            [HttpPost]
            public async Task<IActionResult> UpdateCategoryAndFilm()
            {
                var films = await dbContext.Films
                    .Include(f => f.Categories)
                    .OrderBy(f => f.Id)
                    .ToListAsync();
            int half = films.Count / 2;
            var firstHalf = films.Take(half).ToList();
            var secondHalf = films.Skip(half).ToList();
            var t1 = Task.Run(() => ProcessFilmsAsync(firstHalf));
            var t2 = Task.Run(() => ProcessFilmsAsync(secondHalf));
            var results = await Task.WhenAll(t1, t2);
            var allResults = results[0].Concat(results[1]).ToList();
            foreach (var result in allResults)
                {

                    var film = await dbContext.Films
                        .Include(f => f.Categories)
                        .FirstOrDefaultAsync(f => f.Name == result.FilmName);

                    if (film == null) continue;

                    foreach (var categoryName in result.Category)
                    {
                        var categorySlug = Slugify(categoryName);
                        if (film.Categories.Any(c => c.Slug == categorySlug))
                            continue;

                        var category = dbContext.Categories
                                                .Local
                                                .FirstOrDefault(c => c.Slug == categorySlug) 
                                                ?? await dbContext.Categories.FirstOrDefaultAsync(c => c.Slug == categorySlug);   
                        if (category == null)
                        {
                            category = new Category
                            {
                                Id = Guid.NewGuid().ToString(),
                                Name = categoryName.Trim(),
                                Slug = categorySlug
                            };
                            dbContext.Categories.Add(category);
                        }
                        if (dbContext.Entry(category).State == EntityState.Detached)
                            dbContext.Categories.Attach(category);

                        if (!film.Categories.Any(c => c.Slug == categorySlug))
                            film.Categories.Add(category);
                    }
            }
            await dbContext.SaveChangesAsync();
            return Ok(new
                {
                    message = "Films updated with categories successfully",
                    updatedCount = allResults.Count,
                    data = allResults
            });
            }
        private string Slugify(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return "";

            // Normalize and remove diacritics
            var normalized = text.Normalize(System.Text.NormalizationForm.FormD);
            var sb = new System.Text.StringBuilder();
            foreach (var c in normalized)
            {
                var category = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c);
                if (category != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    // Keep letters and digits only, replace others with space
                    if (char.IsLetterOrDigit(c))
                        sb.Append(c);
                    else
                        sb.Append(' ');
                }
            }

            // Convert to lowercase, trim, and replace spaces with hyphens
            var slug = sb.ToString()
                         .Normalize(System.Text.NormalizationForm.FormC)
                         .ToLower()
                         .Trim();

            // Replace multiple spaces with single hyphen
            slug = System.Text.RegularExpressions.Regex.Replace(slug, @"\s+", "-");

            return slug;
        }

        private async Task<List<FilmResultDto>> ProcessFilmsAsync(List<Film> films)
            {
                var results = new List<FilmResultDto>();
                var options = new ChromeOptions();
                options.AddArgument("start-maximized");

                using var driver = new ChromeDriver(options);
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

                foreach (var film in films)
                {
                    var filmResult = new FilmResultDto
                    {
                        FilmName = film.Name,
                        Category = new List<string>()
                    };

                    try
                    {
                        driver.Navigate().GoToUrl("https://www.rophim.me/phimhay");

                        var input = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("main-search")));

                        // Clear previous input
                        input.Clear();
                        input.SendKeys(film.Name);
                        wait.Until(driver => input.GetAttribute("value") == film.Name);
                        input.SendKeys(Keys.Enter);

                        // Wait for search results
                        var firstLink = new WebDriverWait(driver, TimeSpan.FromSeconds(5))
                            .Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".row-content .sw-item a.v-thumbnail")));

                        firstLink.Click();

                    // Wait for tag elements to load
                    var tagLinks = new WebDriverWait(driver, TimeSpan.FromSeconds(15)) // longer than 5s
      .Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(
          By.CssSelector(".detail-more .hl-tags:nth-of-type(2) a.tag-topic")
      ));
                    foreach (var tag in tagLinks)
                        {
                            var categoryName = tag.Text.Trim();
                            var categorySlug = Slugify(categoryName);

                            // Avoid duplicates in the list
                            if (!filmResult.Category.Any(c => Slugify(c) == categorySlug))
                                filmResult.Category.Add(categoryName);
                        }
                    }
                    catch (WebDriverTimeoutException)
                    {
                        Console.WriteLine($"Timeout: Could not find search results for '{film.Name}'. Skipping.");
                    }
                    catch (StaleElementReferenceException)
                    {
                        Console.WriteLine($"Stale element for film '{film.Name}', skipping.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing film '{film.Name}': {ex.Message}");
                    }

                    results.Add(filmResult);
                }

                driver.Quit();
                return results;
            }
        }
    }
