using Domain.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using SQLite;
using System.Reflection;
using BusinessLogic.Interfaces;

namespace ApiDevBP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {        
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService;

        public UsersController(ILogger<UsersController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        /// <summary>
        /// Crea un nuevo usuario.
        /// </summary>
        /// <param name="user">Los datos del usuario a crear.</param>
        /// <returns>Devuelve un mensaje de éxito si el usuario es creado.</returns>
        [HttpPost]
        public async Task<IActionResult> SaveUser(UserModel user)
        {
            await _userService.Add(user);
            return Ok(true);
        }

        /// <summary>
        /// Obtiene todos los usuarios.
        /// </summary>
        /// <returns>Devuelve una lista de todos los usuarios.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _userService.GetAll();
            return Ok(data);
        }

        /// <summary>
        /// Obtiene un usuario por su ID.
        /// </summary>
        /// <param name="id">El ID del usuario a obtener.</param>
        /// <returns>Devuelve los datos del usuario si se encuentra, de lo contrario, devuelve un 404 Not Found.</returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var data = await _userService.GetById(id);
                return Ok(data);
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Actualiza un usuario existente.
        /// </summary>
        /// <param name="user">Los datos actualizados del usuario.</param>
        /// <returns>Devuelve un 204 No Content si la actualización es exitosa, o un 404 Not Found si el usuario no se encuentra.</returns>
        [HttpPut]
        public async Task<IActionResult> Update(UserModel user)
        {
            var updated = await _userService.Update(user);
            if (!updated)
            {
                return NotFound("El usuario no fue encontrado.");
            }

            return NoContent(); 
        }

        /// <summary>
        /// Elimina un usuario por su ID.
        /// </summary>
        /// <param name="id">El ID del usuario a eliminar.</param>
        /// <returns>Devuelve un 204 No Content si la eliminación es exitosa, o un 404 Not Found si el usuario no se encuentra.</returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _userService.Delete(id);
            if (!deleted)
            {
                return NotFound("El usuario no fue encontrado.");
            }

            return NoContent(); // 204 No Content
        }

    }
}
