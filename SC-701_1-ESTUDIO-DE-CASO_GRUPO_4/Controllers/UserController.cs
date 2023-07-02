using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Caso1.Domain.Entities;
using Caso1.Persistence.Contracts;
using Caso1.Persistence.Contracts.Repositories;
using Caso1.Persistence.DbContexts;
using SC_701_1_ESTUDIO_DE_CASO_GRUPO_4.DTOs.Create;
using SC_701_1_ESTUDIO_DE_CASO_GRUPO_4.DTOs.List;

namespace SC_701_1_ESTUDIO_DE_CASO_GRUPO_4.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;
        private readonly IRepository<Usuario> _repository;
        public UserController(IUnitOfWork<ApplicationDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.Repository<Usuario>();
        }

        [HttpGet]
        [Route("/api/Users")]
        public JsonResult GetAll()
        {
            return
                new JsonResult(
                    new
                    {
                        success = true,
                        data = _repository.GetAll().ToList().ConvertAll
                        (s => new UserDTO { Id = s.Id, Name = s.Name })
                    }
                );
        }

        [HttpPost]
        [Route("/api/Users/create")]
        public JsonResult Create([FromBody] UserDTO model)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(new { success = false, error = "" });
            }

            _repository.Insert(new Usuario { Name = model.Name });
            _unitOfWork.Save();

            return new JsonResult(new { success = true, error = "" });
        }

        [HttpDelete]
        [Route("/api/Users/delete/{id}")]
        public JsonResult Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(new { success = false, error = "" });
            }

            _repository.Delete(new Usuario { Id = id });
            _unitOfWork.Save();

            return new JsonResult(new { success = true, error = "" });
        }

        [HttpPut]
        [Route("/api/Users/update/{id}")]
        public JsonResult Update([FromRoute] int id, UserDTO model)
        {
            var existingIngredient = _repository.GetAll().First
                        (s => s.Id == id);
            if (!ModelState.IsValid)
            {
                return new JsonResult(new { success = false, error = "" });
            }

            existingIngredient.Name = model.Name;
            _repository.Update(existingIngredient);
            _unitOfWork.Save();

            return new JsonResult(new { success = true, error = "" });
        }
    }
}
