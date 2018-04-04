﻿namespace Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Olive.Mvc;

    public class SharedActionsController : BaseController
    {
        [Route("error")]
        public async Task<ActionResult> Error() => await View("error");

        [Route("error/404")]
        public async Task<ActionResult> NotFound404() => await View("error-404");

        [HttpPost, Authorize, Route("upload")]
        public async Task<IActionResult> UploadTempFileToServer(IFormFile[] files)
        {
            return Json(await new FileUploadService().TempSaveUploadedFile(files[0]));
        }

        [Route("file")]
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
        public Task<ActionResult> DownloadTempFile(string key) => TempFileService.Download(key);
    }
}