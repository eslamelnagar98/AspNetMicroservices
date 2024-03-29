﻿global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Net;
global using System.Reflection;
global using System.Text.Json;
global using System.Threading.Tasks;
global using Ardalis.GuardClauses;
global using Basket.API.Configurations;
global using Basket.API.Entities;
global using Basket.API.GrpcServices;
global using Basket.API.Options;
global using Basket.API.Repositories;
global using Discount.Grpc.Protos;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Options;
global using NLog;
global using NLog.Web;
global using StackExchange.Redis;
