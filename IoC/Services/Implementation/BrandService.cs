using System.Linq.Expressions;
using AutoMapper;
using Domain.Models;
using Infrastructure.DTO;
using Infrastructure.IRepository;
using IoC.Services.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IoC.Services.Implementation;

public class BrandService : IBrandService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _webHost;
    public BrandService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment webHost)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _webHost = webHost;
    }

    public async Task<IEnumerable<BrandDTO>> GetAllAsync( Expression<Func<Brand,bool>>? filter = null)
    {
        IEnumerable<Brand> brands = null;
     
           brands  = await _unitOfWork.Brands.GetAllAsync(filter);
       
        return _mapper.Map<IEnumerable<BrandDTO>>(brands);
    }
    public  Task<SelectList> GetSelectListAsync(Expression<Func<Brand,bool>>? filter = null)
    {
        var brandList =  GetAllAsync(filter).GetAwaiter().GetResult().Select(u => new SelectListItem
        {
            Text = u.Name,
            Value = u.Id.ToString()
        });
        return Task.FromResult(new SelectList(brandList, "Value", "Text"));
    }
    public async Task<BrandDTO?> GetByIdAsync(int id)
    {
        var brand = await _unitOfWork.Brands.GetByIdAsync(id);
        return _mapper.Map<BrandDTO?>(brand);
    }

    public async Task AddAsync(BrandDTO brandDto)
    {
        var brand = _mapper.Map<Brand>(brandDto);
        
        await _unitOfWork.Brands.AddAsync(brand);
        await _unitOfWork.CompleteAsync();
    }

    public async Task UpdateAsync(BrandDTO brandDto)
    {
        var brand = await _unitOfWork.Brands.GetByIdAsync(brandDto.Id);
        
        if (brand == null) return;
         _mapper.Map(brandDto, brand);
         
        _unitOfWork.Brands.Update(brand);
        await _unitOfWork.CompleteAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var brand = await _unitOfWork.Brands.GetByIdAsync(id);
        if (brand != null)
        {
            _unitOfWork.Brands.Remove(brand);
            await _unitOfWork.CompleteAsync();
        }
    }

 
}
