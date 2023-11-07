using FiorelloBackend.ViewModels;

namespace FiorelloBackend.Services.Interfaces
{
    public interface ILayoutService
    {
       HeaderVM GetHeaderDatas();
       FooterVM GetFooterDatas();
    }
}
