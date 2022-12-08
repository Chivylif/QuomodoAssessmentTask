using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QuomodoAssessmentTask.DTOs.Requests;
using QuomodoAssessmentTask.Models;
using QuomodoAssessmentTask.Services.DatabaseServices;
using QuomodoAssessmentTask.Services.ServerServices;

namespace QuomodoAssessmentTask.Controllers
{
    [Route("api/upload")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IUploadServices _service;
        private readonly IUploadServicesServer _serverService;

        public UploadController(IUploadServices service, IUploadServicesServer serverService)
        {
            _service = service;
            _serverService = serverService;
        }

        /// <summary>
        /// Uploads a file to the server and database
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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

                //Uploads file to the server
                var url = await _serverService.UploadFile(request);
                
                //Uploads to the database
                if (!url.IsNullOrEmpty())
                {
                    var res = await _service.UploadFile(request, url);
                    return Ok(res);
                }

                return BadRequest("Upload failed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Gets all files from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("get-all-files")]
        public async Task<IActionResult> GetAllFiles()
        {
            try
            {
                var res = await _service.GetAllFiles();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes files from the server and database
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete-file")]
        public async Task<IActionResult> DeleteFile([FromBody] DeleteFileRequest request)
        {
            try
            {               
                if (String.IsNullOrWhiteSpace(request.FileName))
                {
                    return BadRequest("File Name cannot be empty");
                }

                //Deletes file from the server
                var result = await _serverService.DeleteFile(request);

                if (result)
                {
                    //Deletes file from the database
                    var res = await _service.DeleteFile(request);
                    return Ok("File was deleted successfully");
                }
                else
                {
                    return BadRequest("Something went wrong");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
