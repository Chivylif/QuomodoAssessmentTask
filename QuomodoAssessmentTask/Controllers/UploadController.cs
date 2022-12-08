using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuomodoAssessmentTask.DTOs.Requests;
using QuomodoAssessmentTask.Services.DatabaseServices;

namespace QuomodoAssessmentTask.Controllers
{
    [Route("api/upload")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IUploadServices _service;

        public UploadController(IUploadServices service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> UploadFile([FromForm] UploadFilesRequest request)
        {
            try
            {
                if (request.Files == null)
                {
                    return BadRequest("File cannot be empty");
                }

                var url = await UploadFileToServer(request.Files);
                var res = await _service.UploadFile(request, url);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteFile([FromBody] DeleteFileRequest request)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(request.FileName))
                {
                    return BadRequest("File Name cannot be empty");
                }

                var res = await _service.DeleteFile(request);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private async Task<string> UploadFileToServer(IFormFile file)
        {
            var fileName = Path.GetFileName(file.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }
    }
}
