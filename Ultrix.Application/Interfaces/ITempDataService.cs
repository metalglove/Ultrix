using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Threading.Tasks;

namespace Ultrix.Application.Interfaces
{
    public interface ITempDataService
    {
        Task UpdateTempDataAsync(ITempDataDictionary tempData, int userId);
    }
}
