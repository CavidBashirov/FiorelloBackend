using FiorelloBackend.ViewModels;

namespace FiorelloBackend.Services.Interfaces
{
    public interface ISliderInfoService
    {
        Task<SliderInfoVM> GetAsync();
    }
}
