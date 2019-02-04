using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ImageSizeConverter.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ImageSizeConverter.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        [Required]
        public bool HeightOrWidth { get; set; }

        [BindProperty]
        [Required]
        public int Size { get; set; }

        [BindProperty]
        [Required]
        public string RootFolder { get; set; }

        [BindProperty]
        [Required]
        public string TargetFolder { get; set; }

        public void OnPost()
        {
            try
            {
                FileConverter.ConvertAllImages(RootFolder, TargetFolder, Size, HeightOrWidth);
                ViewData["message"] = "All files converted successfully!";
            }
            catch (Exception)
            {
                ViewData["message"] = "Please check if folders are correct";
            }               
        }
    }
}
