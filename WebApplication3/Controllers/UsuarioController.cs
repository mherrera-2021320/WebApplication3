using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;
using WebApplication3.Filters;

namespace WebApplication3.Controllers
{
    public class UsuarioController : Controller
    {
        private SQLModels db = new SQLModels();

        // GET: Usuario
        
        [AuthorizeUser(idOperacion: 9)]
        public async Task<ActionResult> Index()
        {
            var usuario = db.usuario.Include(u => u.rol);
            return View(await usuario.ToListAsync());
        }

        // GET: Usuario/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuario usuario = await db.usuario.FindAsync(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            ViewBag.rolId = new SelectList(db.rol, "codigo", "nombre");
            return View();
        }

        // POST: Usuario/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "codigo,nombre_completo,correo,username,password,rolId")] usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.usuario.Add(usuario);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.rolId = new SelectList(db.rol, "codigo", "nombre", usuario.rolId);
            return View(usuario);
        }

        // GET: Usuario/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuario usuario = await db.usuario.FindAsync(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.rolId = new SelectList(db.rol, "codigo", "nombre", usuario.rolId);
            return View(usuario);
        }

        // POST: Usuario/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "codigo,nombre_completo,correo,username,password,rolId")] usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.rolId = new SelectList(db.rol, "codigo", "nombre", usuario.rolId);
            return View(usuario);
        }

        // GET: Usuario/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuario usuario = await db.usuario.FindAsync(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            usuario usuario = await db.usuario.FindAsync(id);
            db.usuario.Remove(usuario);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
