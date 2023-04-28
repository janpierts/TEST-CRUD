using Microsoft.AspNetCore.Mvc;
using TEST_CRUD.Datos;
using TEST_CRUD.Models;

namespace TEST_CRUD.Controllers
{
    public class G_empleadosController : Controller
    {
        empleadosDatos _empleadosDatos = new empleadosDatos();
        public IActionResult Listar_Personal()
        {
            var oLista = _empleadosDatos.Listar();
            return View(oLista);
        }
        public IActionResult FGuardar_Personal()
        {
			var oRoles = _empleadosDatos.ListarR();
            return View();
        }
        [HttpPost]
        public IActionResult FGuardar_Personal(empleadosModel oGuardarP)
        {
            var save = _empleadosDatos.Guardar(oGuardarP);
            if (save)
                return RedirectToAction("Listar_Personal");
            else
                return View();
        }
        public IActionResult FEditar_Personal(int I_ID)
        {
            var oID_Roles = _empleadosDatos.ObtenerPId(I_ID);
            return View(oID_Roles);
        }

        [HttpPost]
        public IActionResult FEditar_Personal(empleadosModel oI_ID)
        {
            if (!ModelState.IsValid)
                return View();

            var up = _empleadosDatos.Editar(oI_ID);

            if (up)
                return RedirectToAction("Listar_Personal");
            else
                return View();
        }
        
        public IActionResult Eliminar_Personal(int I_ID)
        {
            //METODO SOLO DEVUELVE LA VISTA
            var oDI = _empleadosDatos.ObtenerPId(I_ID);
            return View(oDI);
        }

        [HttpPost]
        public IActionResult Eliminar_Personal(empleadosModel odi)
        {

            var down = _empleadosDatos.Eliminar(odi.empleadoId);

            if (down)
                return RedirectToAction("Listar_Personal");
            else
                return View();
        }
    }
}
 
