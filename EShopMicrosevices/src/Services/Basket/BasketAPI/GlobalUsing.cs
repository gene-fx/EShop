﻿global using BasketAPI.Data.Repository;
global using BasketAPI.Data.Repository.IRepository;
global using BasketAPI.Models;
global using BuildingBlocks.Behaviors;
global using BuildingBlocks.CQRS;
global using BuildingBlocks.Exceptions;
global using BuildingBlocks.Exceptions.Handler;
global using Carter;
global using FluentValidation;
global using Marten;
global using Mapster;
global using MediatR;
global using System.Linq.Expressions;
global using Microsoft.Extensions.Caching.Distributed;
global using System.Text.Json;
namespace BasketAPI;