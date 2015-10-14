using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using PagedList;
using PagedList.Mvc;

namespace WebApplication1.Controllers
{
    public class BlogPostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BlogPosts
        public ActionResult Index(int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(db.Posts.OrderByDescending(p => p.Created).ToPagedList(pageNumber, pageSize));
        }

        // GET: BlogPosts/Details/5
        public ActionResult Details( string slug)
        {
            if (String.IsNullOrWhiteSpace(slug))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.Posts.FirstOrDefault(p => p.Slug == slug);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // GET: BlogPosts/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: BlogPosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,Created,Updated,Title,Slug,Body,MediaURL,Published")] BlogPost blogPost)
        {
            if (ModelState.IsValid)
            {
                var Slug = StringUtilities.URLFriendly(blogPost.Title);
                if (String.IsNullOrWhiteSpace(Slug))
                {
                    ModelState.AddModelError("Title", "Invalid title.");
                    return View(blogPost);
                }
                if(db.Posts.Any(p => p.Slug == Slug))
                {
                    ModelState.AddModelError("Title", "The title must be unique.");
                    return View(blogPost);
                }
                blogPost.Slug = Slug;
                blogPost.Created = System.DateTimeOffset.Now;
                db.Posts.Add(blogPost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blogPost);
        }

        // GET: BlogPosts/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.Posts.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // POST: BlogPosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(BlogPost blogPost)
        {
            if (ModelState.IsValid)
            {
                blogPost.Updated = System.DateTimeOffset.Now;
                db.Posts.Attach(blogPost);
                db.Entry(blogPost).Property("Body").IsModified = true;
                db.Entry(blogPost).Property("Updated").IsModified = true;
                db.Entry(blogPost).Property("MediaURL").IsModified = true;
                db.Entry(blogPost).Property("Published").IsModified = true;
                //db.Entry(blogPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blogPost);
        }

        // GET: BlogPosts/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.Posts.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // POST: BlogPosts/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogPost blogPost = db.Posts.Find(id);
            db.Posts.Remove(blogPost);
            db.SaveChanges();
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

        public ActionResult Login()
        {
            return View();
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddComment(Comment newComment)
        {
            var post = db.Posts.Find( newComment.PostID );

            if (ModelState.IsValid)
            {
                newComment.Created = System.DateTimeOffset.Now;
                newComment.AuthorId = User.Identity.GetUserId();
                db.Comments.Add(newComment);
                db.SaveChanges();
            }
            return RedirectToAction("Details", "BlogPosts", new { Slug = post.Slug});       
        }

        [Authorize(Roles = "Admin, Moderator")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult DeleteComment(int ID)
        {
            var toBeDeleted = db.Comments.Find(ID);
            var Slug = toBeDeleted.Post.Slug;

            db.Comments.Remove(toBeDeleted);
            db.SaveChanges();
            return RedirectToAction("Details", "BlogPosts", new { Slug });
        }

        [Authorize(Roles = "Admin, Moderator")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditComment(int ID, string Body)
        {

            var comment = db.Comments.Find(ID);
            comment.Body = Body;
            comment.Updated = System.DateTimeOffset.Now;
            db.Entry(comment).Property("Body").IsModified = true;
            db.Entry(comment).Property("Updated").IsModified = true;
            db.SaveChanges();
            return RedirectToAction("Details", "BlogPosts", new { Slug = comment.Post.Slug });
        }
    }

    
}
