using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PARCIAL1A.Models;

namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        private readonly LibroContex _autoresContex;


        public AutoresController(LibroContex autoresCont)
        {
            _autoresContex = autoresCont;

        }
        //Peticiones

        //Peticiones

        ///Mostrar todo GET
        [HttpGet]
        [Route("OBTENER_AUTORES_TODO")]

        public IActionResult Get()
        {
            List<Autores> listadoAutores = (from e in _autoresContex.Autores
                                           select e).ToList();
            if (listadoAutores.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoAutores);

        }




        //Peticion para agregar un equipo
        [HttpPost]
        [Route("AGREGARAUTOR")]
        public IActionResult save_equipo([FromBody] Autores autor)
        {
            try
            {
                _autoresContex.Autores.Add(autor);
                _autoresContex.SaveChanges();
                return Ok(autor);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        //Peticion para actualizar un registro
        [HttpPut]
        [Route("Actualizarautores/{id}")]
        public IActionResult update_reg(int id, [FromBody] Autores AUTORUPDATE)
        {

            //Buscar el registro que se desea modificar
            //Contener en el objeto equiposelection
            Autores? autorselect = (from e in _autoresContex.Autores
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
                autorselect.Nombre = AUTORUPDATE.Nombre;

                //Marcamos el registro modificado
                //Enviar modificaciones a la base de datos

                _autoresContex.Entry(autorselect).State = EntityState.Modified;
                _autoresContex.SaveChanges();
                return Ok(AUTORUPDATE);

            }

        }
        //Eliminar un registro
        [HttpDelete]
        [Route("Deleteautor/{id}")]
        public IActionResult delete_product(int id)
        {
            //Obtener el registro que se desea eliminar
            Autores? autorselect = (from e in _autoresContex.Autores
                                        where e.id == id
                                        select e).FirstOrDefault();

            //Verificamos si existe
            if (autorselect == null)
            {
                return NotFound();
            }
            else
            {
                //si existe ejecutamos la accion de eliminar
                _autoresContex.Autores.Attach(autorselect);
                _autoresContex.Autores.Remove(autorselect);
                _autoresContex.SaveChanges();
                return Ok("Se a eliminado el registro \n" + autorselect + "Nombre: " + autorselect.Nombre );


            }

        }

        //Filtrado de un registro
        [HttpGet]
        [Route("Buscar/{id}")]
        public IActionResult search_ref(int id)
        {

            //Buscar el registro con la consulta
            Autores? autorselect = (from e in _autoresContex.Autores
                                        where e.id == id
                                        select e).FirstOrDefault();


            //Verificar si existe
            if (autorselect == null)
            {
                return NotFound();
            }
            else
            {
                return Ok("Busqueda realizada con exito\n " + "Nombre" + autorselect.Nombre );

            }
        }
        [HttpGet]
        [Route("PostsPorLibro/{tituloLibro}")]
        public IActionResult postsPorLibro(string tituloLibro)
        {

            var resultados = from l in _autoresContex.Libros
                             join al in _autoresContex.AutorLibro on l.Id equals al.LibroId
                             join a in _autoresContex.Autores on al.AutorId equals a.id
                             join p in _autoresContex.Posts on a.id equals p.AutorId
                             where l.Titulo == tituloLibro
                             select new
                             {
                                 TituloLibro = l.Titulo,
                                 NombreAutor = a.Nombre,
                                 Contenido = p.Contenido,
                                 FechaPublicacion = p.FechaPublicacion
                             };


            if (!resultados.Any())
            {
                return NotFound();
            }


            return Ok(resultados);
        }

        //Filtrado de un registro
        [HttpGet]
        [Route("BuscaAutor/{autor}")]
        public IActionResult buscaautor(string autor)
        {


            var resultados = from a in _autoresContex.Autores
                             join al in _autoresContex.AutorLibro on a.id equals al.AutorId
                             join l in _autoresContex.Libros on al.LibroId equals l.Id
                             join p in _autoresContex.Posts on a.id equals p.AutorId
                             where a.Nombre == autor
                             select new
                             {
                                 NombreAutor = a.Nombre,
                                 TituloLibro = l.Titulo,
                                 Contenido = p.Contenido,
                                 FechaPublicacion = p.FechaPublicacion,
                                 Orden = al.Orden
                             };

            // Verificar si existen resultados
            if (!resultados.Any())
            {
                return NotFound();
            }

            // Devolver los resultados
            return Ok(resultados);
        }


        [HttpGet]
        [Route("BuscaAutorUltimos20/{autor}")]
        public IActionResult buscaAutorUltimos20(string autor)
        {

            var resultados = (from a in _autoresContex.Autores
                              join p in _autoresContex.Posts on a.id equals p.AutorId
                              where a.Nombre == autor
                              orderby p.FechaPublicacion descending
                              select new
                              {
                                  NombreAutor = a.Nombre,
                                  Contenido = p.Contenido,
                                  FechaPublicacion = p.FechaPublicacion
                              }).Take(20);

            // Verificar si existen resultados
            if (!resultados.Any())
            {
                return NotFound();
            }

            // Devolver los últimos 20 posts del autor
            return Ok(resultados);
        }

       
       
    }



}
