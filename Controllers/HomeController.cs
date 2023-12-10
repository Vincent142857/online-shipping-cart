﻿using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineShoppingCart.Core.UnitOfWork;
using OnlineShoppingCart.Models;
using OnlineShoppingCart.Models.DTOs;

namespace OnlineShoppingCart.Controllers;

public class HomeController : Controller
{
    protected readonly ILogger<HomeController> _logger;
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IMapper _mapper;

    public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _unitOfWork.Products.GetAll("Inventories,Images,Feedbacks");
        var productDtoList = products!.Select(p => _mapper.Map<ProductDto>(p)).OrderByDescending(x => x.CreateAt).ToList();

        // var productDtoList = _mapper.Map<List<ProductDto>>(products);
        return View(productDtoList);
    }

    [HttpGet("/about")]
    public IActionResult About()
    {
        return View();
    }

    [HttpGet("/policy")]
    public IActionResult Policy()
    {
        return View();
    }

    [HttpGet("/collection/{id?}")]
    public async Task<IActionResult> Shop(string? id = null)
    {
        var products = await _unitOfWork.Products.GetAll("Inventories,Images,Feedbacks");
        var productDtoList = products!.Select(p => _mapper.Map<ProductDto>(p)).OrderByDescending(x => x.CreateAt).ToList();
        if (id == null)
        {
            return View(productDtoList);
        }

        var productDto = productDtoList.Where(x => x.CategoryId == id).ToList();
        if (productDto != null && productDto.Count > 0)
        {
            return View(productDto);
        }

        var prodSearch = productDtoList.Where(x => x.Name!.ToLower().Contains(id.ToLower())).ToList();
        if (prodSearch != null && prodSearch.Count > 0)
        {
            return View(prodSearch);
        }

        return View(productDtoList);
    }




    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpGet]
    public async Task<IActionResult> Search()
    {
        var products = await _unitOfWork.Products.GetAll("Inventories,Images,Feedbacks");
        var productDtoList = products!.Select(p => _mapper.Map<ProductDto>(p)).OrderByDescending(x => x.CreateAt).ToList();

        string value = string.Empty!;
        value = JsonConvert.SerializeObject(productDtoList, Formatting.Indented, new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        });

        return Json(value);
    }

}
