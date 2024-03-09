using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PARCIAL1A.Models;

namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly LibroContex _aPostsContex;


        public PostsController(LibroContex LibroCont)
        {
            _aPostsContex = LibroCont;

        }

        //Peticiones

        //Peticiones

        ///Mostrar todo GET
        [HttpGet]
        [Route("OBTENER_POSTS_TODO")]

        public IActionResult Get()
        {
            List<Post> listadoPosts = (from e in _aPostsContex.Posts
                                         select e).ToList();
            if (listadoPosts.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoPosts);

        }




        //Peticion para agregar un posts
        [HttpPost]
        [Route("AGREGAR POSTS")]
        public IActionResult save_equipo([FromBody] Post postl)
        {
            try
            {
                _aPostsContex.Posts.Add(postl);
                _aPostsContex.SaveChanges();
                return Ok(postl);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);


            }

        }

        //Peticion para actualizar un registro
        [HttpPut]
        [Route("ActualizarPost/{id}")]
        public IActionResult update_reg(int id, [FromBody] AutorLibro AUTORUPDATE)
        {

            //Buscar el registro que se desea modificar
            //Contener en el objeto equiposelection
            Post? autorselect = (from e in _aPostsContex.Posts
                                 where e.id == id
                                       select e).FirstOrDefault();

            //Verificar que si existe el registro con el id correspondiente

            //Si se encuentra modificar

            if (autorselect == null)
            {
                return NotFound();
            }
            else
            {
                autorselect.id = AUTORUPDATE.LibroId;

                //Marcamos el registro modificado
                //Enviar modificaciones a la base de datos

                _aPostsContex.Entry(autorselect).State = EntityState.Modified;
                _aPostsContex.SaveChanges();
                return Ok(AUTORUPDATE);

            }

        }
        //Eliminar un registro
        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult delete_product(int id)
        {
            //Obtener el registro que se desea eliminar
            AutorLibro? autorselect = (from e in _aPostsContex.AutorLibros
                                       where e.AutorId == id
                                       select e).FirstOrDefault();

            //Verificamos si existe
            if (autorselect == null)
            {
                return NotFound();
            }
            else
            {
                //si existe ejecutamos la accion de eliminar
                _aPostsContex.AutorLibros.Attach(autorselect);
                _aPostsContex.AutorLibros.Remove(autorselect);
                _aPostsContex.SaveChanges();
                return Ok("Se a eliminado el registro \n" + autorselect + "AutorId: " + autorselect.AutorId);


            }

        }

        //Filtrado de un registro
        [HttpGet]
        [Route("Buscar/{id}")]
        public IActionResult search_ref(int id)
        {

            //Buscar el registro con la consulta
            AutorLibro? autorselect = (from e in _aPostsContex.AutorLibros
                                       where e.AutorId == id
                                       select e).FirstOrDefault();


            //Verificar si existe
            if (autorselect == null)
            {
                return NotFound();
            }
            else
            {
                return Ok("Busqueda realizada con exito\n " + "AutorId" + autorselect.AutorId + " \nLibroId" + autorselect.LibroId);

            }
        }
    }
}
    

