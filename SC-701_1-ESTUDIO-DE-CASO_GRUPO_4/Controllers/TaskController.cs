using Caso1.Persistence.Contracts.Repositories;
using Caso1.Persistence.Contracts;
using Caso1.Persistence.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Caso1.Domain.Entities;
using SC_701_1_ESTUDIO_DE_CASO_GRUPO_4.DTOs.List;
using SC_701_1_ESTUDIO_DE_CASO_GRUPO_4.DTOs.Create;
using Caso1.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SC_701_1_ESTUDIO_DE_CASO_GRUPO_4.Controllers
{
    public class TaskController : Controller
    {
        private readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;
        private readonly IRepository<Tarea> _tareaRepository;
        private readonly IRepository<Usuario> _usuarioRepository;

        public TaskController(IUnitOfWork<ApplicationDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _tareaRepository = unitOfWork.Repository<Tarea>();
            _usuarioRepository = unitOfWork.Repository<Usuario>();

        }
        [HttpGet]
        public IActionResult Index()
        {
            var tareas = _tareaRepository.GetAll().ToList();
            var tareasDTO = new List<TareaDTO>();

            foreach (var tarea in tareas)
            {
                var usuario = _usuarioRepository.Get(u => u.Id == tarea.Usuario);

                var usuarioDTO = new UserDTO { Id = usuario.Id, Name = usuario.Name };

                tareasDTO.Add(new TareaDTO
                {
                    Id = tarea.Id,
                    Fecha = tarea.Fecha,
                    Asunto = tarea.Asunto,
                    Completado = tarea.Completado,
                    Esfuerzo = tarea.Esfuerzo,
                    Users = new List<UserDTO> { usuarioDTO }
                });
            }

            return View(tareasDTO);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var usuarios = _usuarioRepository.GetAll().ToList();
            var inputT = new InputT { InputTarea = new InputTarea(), InputUser = new InputUser() };

            ViewBag.Usuarios = new SelectList(usuarios, "Id", "Name");

            return View(inputT);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(InputT input)
        {
            if (input.InputTarea.Asunto != null && input.InputTarea.Esfuerzo != null && input.InputUser.id != 0 && input.InputUser.id != null)
            {
                var usuario = _usuarioRepository.Get(u => u.Id == input.InputUser.id);

                var tarea = new Tarea
                {
                    Fecha = input.InputTarea.Fecha,
                    Asunto = input.InputTarea.Asunto,
                    Completado = false,
                    Esfuerzo = input.InputTarea.Esfuerzo,
                    Usuario = usuario.Id
                };

                _tareaRepository.Insert(tarea);
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }

            return View(input);
        }
    }
}
