﻿namespace MVC.Services.Interfaces;

public interface IBaseService
{
    Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true);
}