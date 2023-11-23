using AutoMapper;
using FiorelloBackend.Data;
using FiorelloBackend.Models;
using FiorelloBackend.Services.Interfaces;
using FiorelloBackend.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace FiorelloBackend.Services
{
    public class SliderInfoService : ISliderInfoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public SliderInfoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SliderInfoVM> GetAsync()
        {
            var data = await _context.SliderInfos.FirstOrDefaultAsync();

            SliderInfoVM sliderInfo = _mapper.Map<SliderInfoVM>(data);

            //var entity = _mapper.Map<SliderInfo>(sliderInfo);

            //_mapper.Map(data, sliderInfo);

            return sliderInfo; 
        }
    }
}
