using Microsoft.AspNetCore.Http;
using Vacancy.Domain.Entities.Assetss;

namespace Vacancy.Service.Interfaces.Assetss;

public interface IAssetsService
{
    Task<Assets> CreateAsync(IFormFile file, string fieldname);

    Task<bool> DeleteAsync(string path);
}
