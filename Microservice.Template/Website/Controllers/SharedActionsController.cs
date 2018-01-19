namespace Controllers
{
    using System;
    using System.Linq;
    using System.ComponentModel;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Olive.Entities;
    using Olive.Mvc;
    using Microsoft.AspNetCore.Http;
    using Olive;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Olive.Microservices;

    public class SharedActionsController : BaseController
    {
        [Route("error")]
        public async Task<ActionResult> Error() => await View("error");

        [Route("error/404")]
        public new async Task<ActionResult> NotFound() => await View("error-404");

        [HttpPost, Route("file/upload")]
        [Authorize]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public ActionResult UploadTempFileToServer(IFormFile[] files)
        {
            // Note: This will prevent uploading of all unsafe files defined at Blob.UnsafeExtensions
            // If you need to allow them, then comment it out.
            if (Blob.HasUnsafeFileExtension(files[0].FileName))
                return Json(new { Error = "Invalid file extension." });

            // var file = Request.Files[0];
            var path = System.IO.Path.Combine(FileUploadService.GetFolder(Guid.NewGuid().ToString()).FullName, files[0].FileName.ToSafeFileName());
            if (path.Length >= 260)
                return Json(new { Error = "File name length is too long." });

            return Json(new FileUploadService().TempSaveUploadedFile(files[0]));
        }

        [Route("file/download")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task<ActionResult> DownloadFile()
        {
            var path = Request.QueryString.ToString().TrimStart('?');
            var accessor = await FileAccessor.Create(path, User);
            if (!accessor.IsAllowed()) return new UnauthorizedResult();

            if (accessor.Blob.IsMedia())
                return await RangeFileContentResult.From(accessor.Blob);
            else return await File(accessor.Blob);
        }

        [Route("temp-file/{key}")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Task<ActionResult> DownloadTempFile(string key)
        {
            return TempFileService.Download(key);
        }
        
        [Route("/Login")]
        public async Task<ActionResult> Login()
        {
            return Redirect(Microservice.Url("auth"));
        }
    }
}