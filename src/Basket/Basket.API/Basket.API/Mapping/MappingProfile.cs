﻿using AutoMapper;
using Basket.API.Models;
using Basket.API.Models.Dto;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Basket.API.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ShoppingCart, ShoppingCartDto>();
        CreateMap<ShoppingCartDto, ShoppingCart>();
    }
}