using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Thrima.WebApi.Common;

[ApiController]
[Route("/api/[controller]")]
public abstract class ApiController : Controller
{
}
