namespace Autoshop.Web.Models
{
    using System;

    public class NavigationViewModel
    {
        public int CurrentPage { get; set; }

        public int TotalCount { get; set; }

        public int PerPage { get; set; }

        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PerPage);

        public int Next => CurrentPage >= TotalPages ? CurrentPage : CurrentPage + 1;

        public int Previous => CurrentPage <= 1 ? 1 : CurrentPage - 1;
    }
}
