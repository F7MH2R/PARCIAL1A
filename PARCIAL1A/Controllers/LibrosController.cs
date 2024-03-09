using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PARCIAL1A.Models;

namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly LibroContex _aLibroLContex;


        public LibrosController(LibroContex LibroCont)
        {
            _aLibroLContex = LibroCont;

        }

        //Peticiones

        //Peticiones

        ///Mostrar todo GET
        [HttpGet]
        [Route("OBTENER_AUTORES_TODO")]

        public IActionResult Get()
        {
            List<Libros> listadoLibro = (from e in _aLibroLContex.Libros
                                               select e).ToList();
            if (listadoLibro.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoLibro);

        }




        //Peticion para agregar un equipo
        [HttpPost]
        [Route("AGREGARAUTOR")]
        public IActionResult save_equipo([FromBody] AutorLibro autorl)
        {
            try
            {
                _aLibroLContex.AutorLibros.Add(autorl);
                _aLibroLContex.SaveChanges();
                return Ok(autorl);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);


            }

        }

        //Peticion para actualizar un registro
        [HttpPut]
        [Route("Actualizarautores/{id}")]
        public IActionResult update_reg(int id, [FromBody] AutorLibro AUTORUPDATE)
        {

            //Buscar el registro que se desea modificar
            //Contener en el objeto equiposelection
            AutorLibro? autorselect = (from e in _aLibroLContex.AutorLibros
                                       where e.AutorId == id
                                       select e).FirstOrDefault();

            //Verificar que si existe el registro con el id correspondiente

            //Si se encuentra modificar

            if (autorselect == null)
            {
                return NotFound();
            }
            else
            {
                autorselect.LibroId = AUTORUPDATE.LibroId;
                autorselect.Orden = AUTORUPDATE.Orden;

                //Marcamos el registro modificado
                //Enviar modificaciones a la base de datos

                _aLibroLContex.Entry(autorselect).State = EntityState.Modified;
                _aLibroLContex.SaveChanges();
                return Ok(AUTORUPDATE);

            }

        }
        //Eliminar un registro
        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult delete_product(int id)
        {
            //Obtener el registro que se desea eliminar
            AutorLibro? autorselect = (from e in _aLibroLContex.AutorLibros
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
                _aLibroLContex.AutorLibros.Attach(autorselect);
                _aLibroLContex.AutorLibros.Remove(autorselect);
                _aLibroLContex.SaveChanges();
                return Ok("Se a eliminado el registro \n" + autorselect + "AutorId: " + autorselect.AutorId);


            }

        }

        //Filtrado de un registro
        [HttpGet]
        [Route("Buscar/{id}")]
        public IActionResult search_ref(int id)
        {

            //Buscar el registro con la consulta
            AutorLibro? autorselect = (from e in _aLibroLContex.AutorLibros
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
