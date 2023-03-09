using Microsoft.AspNetCore.Mvc.Rendering;


namespace NetPC.Models.Domain
{
    public class Option
    {
        public List<SelectListItem>? Options { get; set; }
        public string? SelectedOption { get; set; }

    }
}
