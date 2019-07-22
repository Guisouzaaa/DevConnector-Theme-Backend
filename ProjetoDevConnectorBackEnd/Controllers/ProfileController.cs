using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DevConnectorBackEnd.Models;
using DevConnectorBackEnd.Models.ViewModels;

namespace DevConnectorBackEnd.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<UsuarioModel> userManager;

        public ProfileController(UserManager<UsuarioModel> _userManager)
        {
            userManager = _userManager;
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public async Task<IActionResult> ViewProfile(string username)
        {
            UsuarioModel usuario = await userManager.FindByNameAsync(username);
            if(usuario != null)
            {
                return View(usuario);
            }
            else
            {
                return Redirect("GetProfiles");
            }

        }

        [AllowAnonymous]
        public IActionResult GetProfiles()
        { 
            return View(userManager.Users.ToList());
        }

        public JsonResult GetEducation()
        {
            string id = userManager.GetUserId(User);
            return Json(EducationModel.GetEducation(id));
        }

        public IActionResult AddEducation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEducation(EducationViewModel edc)
        {
            if (ModelState.IsValid)
            {
                edc.FromDate = DateTime.Parse(edc.FromDate).ToString("yyyy MMMM");
                edc.ToDate = DateTime.Parse(edc.ToDate).ToString("yyyy MMMM");
                edc.UserId = userManager.GetUserId(User);

                if (EducationModel.AddEducation(edc) == 1)
                {
                    return Redirect("Dashboard");
                }
            }

            return View(edc);
        }

        [HttpPost]
        public JsonResult DeleteEducation(int id)
        {
            return Json(EducationModel.DeleteEducation(id));
        }

        public IActionResult AddExperience()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddExperience(ExpViewModel exp)
        {
            if (ModelState.IsValid)
            {
                exp.FromDate = DateTime.Parse(exp.FromDate).ToString("yyyy MMMM");
                exp.ToDate = DateTime.Parse(exp.ToDate).ToString("yyyy MMMM");
                exp.UserId = userManager.GetUserId(User);

                if (ExpModel.AddExp(exp) == 1)
                {
                    return Redirect("Dashboard");
                }
            }
 
            return View(exp);
        }

        public JsonResult GetExperiences()
        {
            string id = userManager.GetUserId(User);
            return Json(ExpModel.ListarExp(id));
        }

        [HttpPost]
        public JsonResult DeleteExperience(int id)
        {
            return Json(ExpModel.DeleteExp(id));
        }

        public async Task<IActionResult> EditProfile()
        {
            UsuarioModel usuario = await userManager.FindByIdAsync(userManager.GetUserId(User));
            UsuarioViewModel usu = new UsuarioViewModel();
            usu.ProfessionalStatus = usuario.ProfessionalStatus;
            usu.Bio = usuario.Bio;
            usu.Location = usuario.Location;
            usu.Company = usuario.Company;
            usu.Website = usuario.Website;
            usu.Skills = usuario.Skills;
            usu.GithubUsername = usuario.GithubUsername;
            usu.TwitterUrl = usuario.TwitterUrl;
            usu.YoutubeUrl = usuario.YoutubeUrl;
            usu.FacebookUrl = usuario.FacebookUrl;
            usu.LinkedinUrl = usuario.LinkedinUrl;
            usu.InstagramUrl = usuario.InstagramUrl;

            return View(usu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(UsuarioViewModel usuario)
        {
            if (ModelState.IsValid)
            {
                UsuarioModel usu = await userManager.FindByIdAsync(userManager.GetUserId(User));
                usu.ProfessionalStatus = usuario.ProfessionalStatus;
                usu.Bio = usuario.Bio;
                usu.Location = usuario.Location;
                usu.Company = usuario.Company;
                usu.Website = usuario.Website;
                usu.Skills = usuario.Skills;
                usu.GithubUsername = usuario.GithubUsername;
                usu.TwitterUrl = usuario.TwitterUrl;
                usu.YoutubeUrl = usuario.YoutubeUrl;
                usu.FacebookUrl = usuario.FacebookUrl;
                usu.LinkedinUrl = usuario.LinkedinUrl;
                usu.InstagramUrl = usuario.InstagramUrl;

                var resultado = await userManager.UpdateAsync(usu);
                if (resultado.Succeeded)
                {
                    return Redirect("Dashboard");
                }
                else
                {
                    ModelState.AddModelError("", "Erro ao atualizar usuario");
                    return View(usuario);
                }
            }
            return View(usuario);
        }

        public async Task<IActionResult> GetPosts()
        {
            UsuarioModel usu = await userManager.FindByIdAsync(userManager.GetUserId(User));
            return View(usu);
        }

        public JsonResult GetAllPosts()
        {
            return Json(PostsModel.getAllPosts());
        }

        public IActionResult OpenPost(int id)
        {
            if(PostsModel.getPostById(id) != null)
            {
                return View(PostsModel.getPostById(id));
            }
            else
            {
                return Redirect("GetPosts");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPost([FromBody]PostsViewModel post)
        {
            if (ModelState.IsValid)
            {
                post.UserId = userManager.GetUserId(User);
                post.UserName = userManager.GetUserName(User);
                post.Likes = 0;
                post.Dislikes = 0;

                return Json(PostsModel.AddPost(post));
            }
            else
            {
                ModelState.AddModelError("", "Erro");
                return Redirect("GetPosts");
            }
        }

        [HttpPost]
        public JsonResult AddLike([FromBody]PostsViewModel post)
        {
            return Json(PostsModel.UpdatePostLikes(post));
        }

        [HttpPost]
        public JsonResult AddDislike([FromBody]PostsViewModel post)
        {
            return Json(PostsModel.UpdatePostDislikes(post));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddComment([FromBody]CommentsViewModel comment)
        {
            if (ModelState.IsValid)
            {
                comment.UserId = userManager.GetUserId(User);
                comment.UserName = userManager.GetUserName(User);
                return Json(CommentsModel.AddComment(comment));
            }
            else
            {
                ModelState.AddModelError("", "Erro");
                return Redirect("OpenPost");
            }

        }

        public JsonResult GetComments(int id)
        {
            return Json(CommentsModel.getCommentById(id));
        }

    }
}