using Medix.Data;
using Medix.Models;
using Medix.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Medix.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var viewModel = new DashboardViewModel
            {
                UserName = currentUser.UserName,
                TotalUnidades = await _context.UnidadesMedicas.CountAsync(),
                UnidadesAtivas = await _context.UnidadesMedicas.CountAsync(u => u.Status == StatusUnidade.Ativa),
                UnidadesInativas = await _context.UnidadesMedicas.CountAsync(u => u.Status == StatusUnidade.Inativa),
                UnidadesSuspensas = await _context.UnidadesMedicas.CountAsync(u => u.Status == StatusUnidade.Suspensa),
                AtividadeRecente = await _context.UnidadesMedicas
                                        .OrderByDescending(u => u.DataCadastro)
                                        .Take(5)
                                        .ToListAsync(),
                StatusDistribution = new Dictionary<string, int>
                {
                    { "Ativas", await _context.UnidadesMedicas.CountAsync(u => u.Status == StatusUnidade.Ativa) },
                    { "Inativas", await _context.UnidadesMedicas.CountAsync(u => u.Status == StatusUnidade.Inativa) },
                    { "Suspensas", await _context.UnidadesMedicas.CountAsync(u => u.Status == StatusUnidade.Suspensa) },
                    { "Em Teste", await _context.UnidadesMedicas.CountAsync(u => u.Status == StatusUnidade.EmTeste) }
                }
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}