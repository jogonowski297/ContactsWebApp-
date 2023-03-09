using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NetPC.Data;
using NetPC.Models.Domain;

namespace NetPC.Controllers
{
    [Route("contact/[controller]")]
    [ApiController]
    public class ContactController : Controller
    {
        private readonly ILogger<ContactController> _logger;
        private readonly ApplicationDbContext applicationDbContrext;


        public ContactController(ILogger<ContactController> logger, ApplicationDbContext applicationDbContrext)
        {
            _logger = logger;
            this.applicationDbContrext = applicationDbContrext;
        }


        [HttpGet]
        public async Task<IActionResult> Contacts()
        {
            //Pobieranie wszystkich kontaktów z bazy danych
            var contacts = await applicationDbContrext.Contacts.ToListAsync();

            return View(contacts);
        }

        [Authorize]
        [HttpGet("view/{id}")]
        public async Task<IActionResult> ViewContact(int id)
        {

            var member = await applicationDbContrext.Contacts.FirstOrDefaultAsync(x => x.Id == id);

            // Pobieranie kategorii z bazy danych
            var optionsCategory = await applicationDbContrext.Categories
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToListAsync();

            var modelCategory = new Option()
            {
                Options = optionsCategory
            };
            // ------------------------------------


            // Pobieranie podkategorii z bazy danych
            var optionsSubcategory = await applicationDbContrext.Subcategories
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToListAsync();

            var modelSubcategory = new Option()
            {
                Options = optionsSubcategory
            };
            // ------------------------------------

            if (member != null)
            {

                var contact = new Contact()
                {
                    Id = member.Id,
                    FirstName = member.FirstName,
                    LastName = member.LastName,
                    Email = member.Email,
                    Password = member.Password,
                    Category = member.Category,
                    Subcategory = member.Subcategory,
                    PhoneNumber = member.PhoneNumber,
                    DateOfBrith = member.DateOfBrith
                };

                var viewModel = new MultipleViews()
                {
                    Category = modelCategory,
                    Subcategory = modelSubcategory,
                    Contact = contact
                };


                return await Task.Run(() => View("ViewContact", viewModel));

            }

            return View(member);
        }


        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContact(int id)
        {
            //Sprawdzanie czy istnieje kontakt o Id=id
            var member = await applicationDbContrext.Contacts.FirstOrDefaultAsync(x => x.Id == id);

            // Pobieranie kategorii z bazy danych
            var optionsCategory = await applicationDbContrext.Categories
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToListAsync();

            var modelCategory = new Option()
            {
                Options = optionsCategory
            };

            // ------------------------------------


            // Pobieranie podkategorii z bazy danych
            var optionsSubcategory = await applicationDbContrext.Subcategories
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToListAsync();

            var modelSubcategory = new Option()
            {
                Options = optionsSubcategory
            };
            // ------------------------------------

            if (member != null)
            {

                var contact = new Contact()
                {
                    Id = member.Id,
                    FirstName = member.FirstName,
                    LastName = member.LastName,
                    Email = member.Email,
                    Password = member.Password,
                    Category = member.Category,
                    Subcategory = member.Subcategory,
                    PhoneNumber = member.PhoneNumber,
                    DateOfBrith = member.DateOfBrith
                };

                var viewModel = new MultipleViews()
                {
                    Category = modelCategory,
                    Subcategory = modelSubcategory,
                    Contact = contact
                };


                return await Task.Run(() => View("Contact", viewModel));

            }

            return View(member);
        }

        [Authorize]
        [HttpPost("editcontact/{id}")]
        public async Task<IActionResult> EditContact([FromForm] MultipleViews multipleViews)
        {
            //Sprawdzanie czy istnieje kontakt o Id=Id
            var member = await applicationDbContrext.Contacts.FindAsync(multipleViews.Contact.Id);


            //Sprawdzanie czy email istnieje w bazie danych
            bool emailExists = await applicationDbContrext.Contacts.AnyAsync(x => x.Email == multipleViews.Contact.Email && x.Id != multipleViews.Contact.Id);
            if (emailExists)
            {
                ModelState.AddModelError(nameof(multipleViews.Contact.Email), "Ten adres e-mail jest już używany");
                return View("Error", multipleViews);
            }

            if (member != null)
            {
                member.FirstName = multipleViews.Contact.FirstName;
                member.LastName = multipleViews.Contact.LastName;
                member.Email = multipleViews.Contact.Email;
                member.Category = multipleViews.Category.SelectedOption;
                if (multipleViews.Category.SelectedOption == "7")
                {
                    member.Subcategory = null;
                }
                else if (multipleViews.Category.SelectedOption == "8")
                {
                    member.Subcategory = multipleViews.Contact.Subcategory;
                }
                else
                {
                    member.Subcategory = multipleViews.Subcategory.SelectedOption;
                }
                member.PhoneNumber = multipleViews.Contact.PhoneNumber;
                member.DateOfBrith = multipleViews.Contact.DateOfBrith;

                await applicationDbContrext.SaveChangesAsync();

                return RedirectToAction("Contacts");
            }

            return RedirectToAction("Contacts");
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            //Sprawdzanie czy istnieje kontakt o Id=id
            var member = await applicationDbContrext.Contacts.FindAsync(id);

            if (member != null)
            {
                applicationDbContrext.Contacts.Remove(member);
                await applicationDbContrext.SaveChangesAsync();

                return RedirectToAction("Contacts");
            }

            return RedirectToAction("Contacts");
        }

        [Authorize]
        [HttpGet("add")]
        public async Task<IActionResult> AddContact()
        {

            // Pobieranie kategorii z bazy danych
            var optionsCategory = await applicationDbContrext.Categories
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToListAsync();

            var modelCategory = new Option()
            {
                Options = optionsCategory
            };

            // ------------------------------------


            // Pobieranie podkategorii z bazy danych
            var optionsSubcategory = await applicationDbContrext.Subcategories
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToListAsync();

            var modelSubcategory = new Option()
            {
                Options = optionsSubcategory
            };
            // ------------------------------------


            var viewModel = new MultipleViews()
            {
                Category = modelCategory,
                Subcategory = modelSubcategory
            };


            return await Task.Run(() => View(viewModel));

        }


        [Authorize]
        [HttpPost("addcontact")]
        public async Task<IActionResult> AddContact([FromForm] MultipleViews multipleViews)
        {
            if (ModelState.IsValid)
            {
                //Sprawdzanie email istneieje w bazie danych
                bool emailExists = await applicationDbContrext.Contacts.AnyAsync(x => x.Email == multipleViews.Contact.Email);
                if (emailExists)
                {
                    ModelState.AddModelError(nameof(multipleViews.Contact.Email), "Ten adres e-mail jest już używany");
                    return View("Error", multipleViews);
                }


                var contact = new Contact()
                {
                    FirstName = multipleViews.Contact.FirstName,
                    LastName = multipleViews.Contact.LastName,
                    Email = multipleViews.Contact.Email,
                    Password = multipleViews.Contact.Password,
                    Category = multipleViews.Category.SelectedOption,
                    Subcategory = multipleViews.Category.SelectedOption,
                    PhoneNumber = multipleViews.Contact.PhoneNumber,
                    DateOfBrith = multipleViews.Contact.DateOfBrith
                };

                await applicationDbContrext.Contacts.AddAsync(contact);
                
                await applicationDbContrext.SaveChangesAsync();
                
                
                return RedirectToAction("Contacts");
            }
            
            return RedirectToAction("Index", "Home");

        }


    }
}
