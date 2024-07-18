using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vacancy.Data.IRepositories.Assetss;
using Vacancy.Domain.Entities.Assetss;
using Vacancy.Service.Exceptions;
using Vacancy.Service.Helpers;
using Vacancy.Service.Interfaces.Assetss;

namespace Vacancy.Service.Services.Assetss;

public class AssetsService : IAssetsService
{
    private readonly IAssetsRepository _assetsRepo;

    public AssetsService(IAssetsRepository assetsRepo)
    {
        _assetsRepo = assetsRepo;
    }

    public async Task<Assets> CreateAsync(IFormFile file, string fieldName)
    {
        var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);
        var folderPath = Path.Combine(WebHostEnvironmentHelper.WebRootPath, fieldName);
        var rootPath = Path.Combine(WebHostEnvironmentHelper.WebRootPath, fieldName, fileName);

        if(!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        await using (var stream = new FileStream(rootPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
            await stream.FlushAsync();
            stream.Close();
        }

        var newAsset = new Assets
        {
            Name = fileName,
            Path = Path.Combine(fieldName, fileName).Replace('\\','/'),
            Extention = Path.GetExtension(file.FileName),
            Size = file.Length,
            Type = file.ContentType,
        };

        var assets = await _assetsRepo.CreateAsync(newAsset);
        return assets;
    }

    public async Task<bool> DeleteAsync(string path)
    {
        var asset = await _assetsRepo
            .GetAllAsQueryable()
            .FirstOrDefaultAsync(x => x.Path == path);

        if (asset == null)
            throw new VacancyException(404, $"Asset not found with path : {path}");

        var result = await _assetsRepo.DeleteAsync(asset.Id);

        if (result)
        {
            var rootPath = Path.Combine(WebHostEnvironmentHelper.WebRootPath, path);
            if (File.Exists(rootPath))
                File.Delete(rootPath);
        }

        return result ? true : false;
    }
}
