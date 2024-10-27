global using Application.Features.Towns.Commands.CreateTown;
global using Application.Features.Towns.Commands.UpdateTown;
global using Application.Features.Towns.Commands.DeleteTown;
global using Application.Features.Towns.Queries.GetTownById;
global using Application.Features.Towns.Queries.GetAllTowns;
global using Application.Features.Addresses.Commands.DeleteAddress;
global using Application.Features.Addresses.Commands.UpdateAddress;
global using Application.Features.Addresses.Queries.GetAddressById;
global using Application.Features.Addresses.Queries.GetAllAddresses;
global using Application.Features.Addresses.Commands.CreateAddress;
global using Application.Features.Employees.Commands.CreateEmployee;
global using Application.Features.Employees.Commands.UpdateEmployee;
global using Application.Features.Employees.Commands.DeleteEmployee;
global using Application.Features.Employees.Queries.GetEmployeeById;
//global using Application.Features.Employees.Queries.GetAllEmployees;
global using Domain.Models;
global using MediatR;
global using Microsoft.AspNetCore.Mvc;
global using System.Threading.Tasks;
global using System;
global using System.Reflection;
global using System.Collections.Generic;
global using Infrastructure.DbContexts;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Infrastructure.Repositories;
global using Infrastructure.IRepositories;
