using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PARCIAL1A.Models;

namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorLibroController : ControllerBase
    {
        private readonly LibroContex _autoresLContex;


        public AutorLibroController(LibroContex autoreslCont)
        {
            _autoresLContex = autoreslCont;

        }

        //Peticiones

        //Peticiones

        ///Mostrar todo GET
        [HttpGet]
        [Route("OBTENER_AUTORES_TODO_AUTORL")]

        public IActionResult GetAutoL()
        {
            List<AutorLibro> LISTAl = (from e in _autoresLContex.AutorLibro
                                            select e).ToList();
            if (LISTAl.Count == 0)
            {
                return NotFound();
            }
            return Ok(LISTAl);

        }




        //Peticion para agregar un equipo
        [HttpPost]
        [Route("AGREGARAUTOR")]
        public IActionResult save_equipo([FromBody] AutorLibro autorl)
        {
            try
            {
                _autoresLContex.AutorLibro.Add(autorl);
                _autoresLContex.SaveChanges();
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
            AutorLibro? autorselect = (from e in _autoresLContex.AutorLibro
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

                _autoresLContex.Entry(autorselect).State = EntityState.Modified;
                _autoresLContex.SaveChanges();
                return Ok(AUTORUPDATE);

            }

        }
        //Eliminar un registro
        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult delete_product(int id)
        {
            //Obtener el registro que se desea eliminar
            AutorLibro? autorselect = (from e in _autoresLContex.AutorLibro
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
                _autoresLContex.AutorLibro.Attach(autorselect);
                _autoresLContex.AutorLibro.Remove(autorselect);
                _autoresLContex.SaveChanges();
                return Ok("Se a eliminado el registro \n" + autorselect + "AutorId: " + autorselect.AutorId);


            }

        }

        //Filtrado de un registro
        [HttpGet]
        [Route("Buscar/{id}")]
        public IActionResult search_ref(int id)
        {

            //Buscar el registro con la consulta
            AutorLibro? autorselect = (from e in _autoresLContex.AutorLibro
                                       where e.AutorId == id
                                    select e).FirstOrDefault();


            //Verificar si existe
            if (autorselect == null)
            {
                return NotFound();
            }
            else
            {
                return Ok("Busqueda realizada con exito\n " + "AutorId" + autorselect.AutorId+ " \nLibroId" + autorselect.LibroId);

            }


        }
    }
}
