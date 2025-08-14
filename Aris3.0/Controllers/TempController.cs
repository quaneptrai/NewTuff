using Aris3._0.Application.DTOs;
using Aris3._0.Application.Interface.Repositories;
using Aris3._0.Application.Interface.Service;
using Aris3._0.Domain.Entities;
using Aris3._0.Infrastructure.Data.Context;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Nodes;
namespace Aris3._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TempController : ControllerBase
    {
        private readonly ArisDbContext dbContext;

        public TempController(ArisDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet("{count}")]
        public async Task<IActionResult> UpdateCategoryTempFilm(int count)
        {
            var results = new List<object>();
            var options = new ChromeOptions();
            options.AddArgument("start-maximized");
            using var driver = new ChromeDriver(options);

            var keywords = await dbContext.Films
                .OrderBy(f => f.Id)
                .Take(count)
                .ToListAsync();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

            foreach (var film in keywords)
            {
                var filmResult = new FilmResultDto
                {
                    FilmName = film.Name,
                    CategoryTemps = new List<string>()
                };

                try
                {
                    driver.Navigate().GoToUrl("https://www.rophim.me/phimhay");

                    // Wait until input is clickable
                    var input = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("main-search")));

                    // Clear input safely
                    input.SendKeys(Keys.Control + "a");
                    input.SendKeys(Keys.Backspace);

                    // Type keyword
                    foreach (char c in film.Name)
                    {
                        input.SendKeys(c.ToString());
                    }
                    input.SendKeys(Keys.Enter);

                    // Click search button if exists
                    try
                    {
                        var searchButton = driver.FindElement(By.CssSelector(
                            "button[type='submit'], input[type='submit'], [role='search'] button, .search-button"
                        ));
                        searchButton.Click();
                    }
                    catch { }

                    // Wait for first thumbnail link
                    var firstLink = wait.Until(ExpectedConditions.ElementToBeClickable(
                        By.CssSelector(".row-content .sw-item a.v-thumbnail")
                    ));
                    firstLink.Click();

                    // Wait 5 seconds for detail page
                    Thread.Sleep(5000);

                    // Extract categories
                    var tagLinks = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(
                        By.CssSelector(".detail-more .hl-tags:nth-of-type(2) a.tag-topic")
                    ));

                    foreach (var tag in tagLinks)
                    {
                        filmResult.CategoryTemps.Add(tag.Text.Trim());
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing film '{film.Name}': {ex.Message}");
                }

                results.Add(filmResult);
            }
            driver.Quit();
            return Ok(results);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCategoryAndFilm()
        {
            var count = await dbContext.Films.CountAsync();
            var keywords = await dbContext.Films
                .OrderBy(f => f.Id)
                .Take(count)
                .ToListAsync();

            var results = new List<FilmResultDto>();

            var filmNumber = results.Count();
            var firstHalf = keywords.Take(count / 2).ToList();
            var secondHalf = keywords.Skip(count / 2).ToList();

            var task1 = Task.Run(() => ProcessFilmsAsync(firstHalf));
            var task2 = Task.Run(() => ProcessFilmsAsync(secondHalf));

            var allResults = await Task.WhenAll(task1, task2);

            foreach (var r in allResults)
                results.AddRange(r);

            foreach (var result in results)
            {
                var film = await dbContext.Films
                    .Include(f => f.Categories)
                    .FirstOrDefaultAsync(f => f.Name == result.FilmName);
                if (film == null) continue;

                foreach (var categoryName in result.CategoryTemps)
                {
                    // Skip if film already has this category
                    string normalizedName = categoryName.Trim().ToLower();

                    // Check if film already has category (normalize names)
                    if (film.Categories.Any(c => c.Name.Trim().ToLower() == normalizedName))
                        continue;

                    // Check if category exists in DB
                    var category = await dbContext.CategoryTemps
                        .FirstOrDefaultAsync(c => c.Name.Trim().ToLower() == normalizedName);
                    if (category == null)
                    {
                        category = new CategoryTemp
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = categoryName,
                            Slug = categoryName.ToLower().Replace(" ", "-")
                        };
                        await dbContext.CategoryTemps.AddAsync(category);
                    }
                    film.CategoryTemps.Add(category);
                    if (!film.CategoryTemps.Any(c => c.Id == category.Id))
                        film.CategoryTemps.Add(category);
                }
            }
            await dbContext.SaveChangesAsync();

            return Ok(new { message = "Films updated with categories successfully", updatedCount = results.Count, data = results });
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
                    CategoryTemps = new List<string>()
                };

                try
                {
                    driver.Navigate().GoToUrl("https://www.rophim.me/phimhay");

                    var input = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("main-search")));
                    input.SendKeys(Keys.Control + "a");
                    input.SendKeys(Keys.Backspace);

                    foreach (char c in film.Name)
                        input.SendKeys(c.ToString());

                    input.SendKeys(Keys.Enter);

                    try
                    {
                        var shortWait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
                        var firstLink = shortWait.Until(ExpectedConditions.ElementToBeClickable(
                            By.CssSelector(".row-content .sw-item a.v-thumbnail")
                        ));
                        firstLink.Click();
                    }
                    catch (WebDriverTimeoutException)
                    {
                        Console.WriteLine($"Timeout: Could not find thumbnail for '{film.Name}'. Skipping.");
                        continue;
                    }
                    Thread.Sleep(5000); // or use explicit wait
                    var tagLinks = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(
                        By.CssSelector(".detail-more .hl-tags:nth-of-type(2) a.tag-topic")
                    ));
                    foreach (var tag in tagLinks)
                    {
                        var categortempName = tag.Text.Trim();

                        // Check if category already exists in the database
                        var existsInDb = await dbContext.CategoryTemps.AnyAsync(c => c.Name == categortempName);

                        if (!filmResult.CategoryTemps.Contains(categortempName) && !existsInDb)
                            filmResult.CategoryTemps.Add(categortempName);
                    }
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
        [HttpPost("temps-to-category")]
        public async Task<IActionResult> PutTempToCategory()
        {
            // Get distinct temp category names
            var distinctTemps = await dbContext.CategoryTemps
                                              .Select(c => c.Name)
                                              .Distinct()
                                              .ToListAsync();

            foreach (var item in distinctTemps)
            {
                // Check if category already exists
                bool exists = await dbContext.categories.AnyAsync(f => f.Name == item);
                if (exists)
                    continue;

                // Add new category
                var newCate = new Category
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = item,
                    Slug = item.ToLower().Replace(" ", "-")
                };
                dbContext.categories.Add(newCate);
            }

            await dbContext.SaveChangesAsync();

            return Ok(new { message = "Temp categories inserted successfully" });
        }
    }
}
