using Microsoft.AspNetCore.Mvc;
using MvcCoreApiCrudDepartamentos.Models;
using MvcCoreApiCrudDepartamentos.Services;

namespace MvcCoreApiCrudDepartamentos.Controllers
{
    public class DepartamentosController : Controller
    {
        private ServiceDepartamentos service;
        public DepartamentosController(ServiceDepartamentos service)
        {
            this.service = service;
        }
        public async Task<IActionResult> Index()
        {
            List<Departamento> data = await this.service.GetDepartamentosAsync();
            return View(data);
        }
        public async Task<IActionResult> Details
            (int id)
        {
            Departamento data = await this.service.FindDepartamentoAsync(id);
            return View(data);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create
            (Departamento departamento)
        {
            await this.service.InsertDepartamentoAsync(departamento.IdDepartamento,
                departamento.Nombre, departamento.Localidad);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit
            (int id)
        {
            Departamento departamento = await this.service.FindDepartamentoAsync(id);
            return View(departamento);
        }
        [HttpPost]
        public async Task<IActionResult> Edit
            (Departamento departamento)
        {
            await this.service.UpdateDepartamentoAsync(departamento.IdDepartamento,
                departamento.Nombre, departamento.Localidad);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete
            (int id)
        {
            await this.service.DeleteDepartamento(id);
            return RedirectToAction("Index");
        }
        public IActionResult Cliente()
        {

            return View();
        }
    }
}
