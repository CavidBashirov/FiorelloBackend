using FiorelloBackend.Areas.Admin.ViewModels.Slider;
using FiorelloBackend.Models;

namespace FiorelloBackend.ViewModels
{
    public class HomeVM
    {
        public List<SliderVM> Sliders { get; set; }
        public SliderInfoVM SliderInfo { get; set; }
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
    }
}
