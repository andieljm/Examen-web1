using Caso1.Domain.Entities;
using Caso1.Persistence.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SC_701_1_ESTUDIO_DE_CASO_GRUPO_4.Controllers
{
    [ApiController]
    public class TareaController : Controller
    {
        private readonly IApplicationDbContext _dbContext;

        public TareaController(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("/api/tareas/")]
        public JsonResult Get()
        {
            var products =
            _dbContext.Tareas.Include(i => i.Usuarios).ToList();

            var response =
                products.ConvertAll(p => new DTOs.List.TareaDTO
                {
                    Id = p.Id,
                    Fecha = p.Fecha,
                    Asunto = p.Asunto,
                    Completado = p.Completado,
                    Esfuerzo = p.Esfuerzo,
                    Users = p.Usuarios.ConvertAll(i => new DTOs.List.UserDTO { Id = i.Id, Name = i.Name })
                });
            
            return new JsonResult(new { success = true, data = response });
        }

        [HttpPost]
        [Route("/api/tareas/create")]
        public JsonResult Create([FromBody] DTOs.Create.InputTarea product)
        {
            var existingProduct = _dbContext.Tareas.FirstOrDefault(s => s.Asunto == product.Asunto);
            if (existingProduct != null)
                return new JsonResult(new { success = false, error = "There is an existing product with the same name." });

            var newProduct = new Tarea { Fecha = product.Fecha, Asunto = product.Asunto, Completado = product.Completado, Esfuerzo = product.Esfuerzo, };
            _dbContext.Tareas.Add(newProduct);
            _dbContext.save();


            return new JsonResult(new { success = true });
        }



        [HttpPut]
        [Route("/api/tareas/update/{id}")]
        public JsonResult Update([FromRoute] int id, DTOs.Create.InputTarea product)
        {
            var existingProduct = _dbContext.Tareas.FirstOrDefault(s => s.Id == id);
            if (existingProduct == null)
                return new JsonResult(new { success = false, error = "There is no existing product with the provided id." });

            existingProduct.Asunto = product.Asunto;
            _dbContext.Tareas.Update(existingProduct);
            _dbContext.save();


            return new JsonResult(new { success = true });
        }
    }
}
