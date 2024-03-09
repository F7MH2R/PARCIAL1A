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
        [Route("OBTENER_AUTORES_TODOLibros")]

        public IActionResult GetL()
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
        public IActionResult save_equipo([FromBody] Libros autorl)
        {
            try
            {
                _aLibroLContex.Libros.Add(autorl);
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
        public IActionResult update_reg(int id, [FromBody] Libros AUTORUPDATE)
        {

            //Buscar el registro que se desea modificar
            //Contener en el objeto equiposelection
            Libros? autorselect = (from e in _aLibroLContex.Libros
                                   where e.Id == id
                                       select e).FirstOrDefault();

            //Verificar que si existe el registro con el id correspondiente

            //Si se encuentra modificar

            if (autorselect == null)
            {
                return NotFound();
            }
            else
            {
                autorselect.Titulo = AUTORUPDATE.Titulo;
             

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
            Libros? autorselect = (from e in _aLibroLContex.Libros
                                   where e.Id == id
                                       select e).FirstOrDefault();

            //Verificamos si existe
            if (autorselect == null)
            {
                return NotFound();
            }
            else
            {
                //si existe ejecutamos la accion de eliminar
                _aLibroLContex.Libros.Attach(autorselect);
                _aLibroLContex.Libros.Remove(autorselect);
                _aLibroLContex.SaveChanges();
                return Ok("Se a eliminado el registro \n" + autorselect + "Titulo: " + autorselect.Titulo);


            }

        }

        //Filtrado de un registro
        [HttpGet]
        [Route("Buscar/{id}")]
        public IActionResult search_ref(int id)
        {

            //Buscar el registro con la consulta
            Libros? autorselect = (from e in _aLibroLContex.Libros
                                       where e.Id == id
                                       select e).FirstOrDefault();


            //Verificar si existe
            if (autorselect == null)
            {
                return NotFound();
            }
            else
            {
                return Ok("Busqueda realizada con exito\n " + "ID" + autorselect.Id + " \nLibro Titulo:" + autorselect.Titulo);

            }
        }
    }
}
