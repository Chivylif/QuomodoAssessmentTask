using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuomodoAssessmentTask.DTOs.Requests;
using QuomodoAssessmentTask.DTOs.Response;
using QuomodoAssessmentTask.Services.DatabaseServices;
using QuomodoAssessmentTask.Services.ServerServices;

namespace QuomodoAssessmentTask.Controllers
{
    [Route("api/folder")]
    [ApiController]
    public class FolderController : ControllerBase
    {
        private readonly IFolderServices _service;
        private readonly IFolderServicesServer _serverService;

        public FolderController(IFolderServices service, IFolderServicesServer serverService)
        {
            _service = service;
            _serverService = serverService;
        }

        [HttpPost]
        [Route("create/{folderName}")]
        public async Task<IActionResult> CreateFolder(string folderName)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(folderName))
                {
                    return BadRequest("Folder Name cannot be empty");
                }

                //This creates the folder on the server
                var result = await _serverService.CreateFolder(folderName);

                if (result)
                {
                    //This creates the folder on the database
                    var res = await _service.CreateFolder(folderName);
                    return Ok(res);
                }
                else
                {
                    return BadRequest("Something went arong");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("create/subfolder")]
        public async Task<IActionResult> CreateSubFolder([FromBody] CreateSubFolderRequest request)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(request.Name))
                {
                    return BadRequest("Folder Name cannot be empty");
                }

                //This creates the folder on the server
                var result = await _serverService.CreateSubFolder(request);

                if (result)
                {
                    //This creates the folder on the database
                    var res = await _service.CreateSubFolder(request);
                    return Ok(res);
                }
                else
                {
                    return BadRequest("Something went arong");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("rename")]
        public async Task<IActionResult> RenameFolder([FromBody] RenameFolderRequest request)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(request.NewName) || String.IsNullOrWhiteSpace(request.OldName))
                {
                    return BadRequest("Folder Name cannot be empty");
                }

                //This renames the folder on the server
                var result = await _serverService.RenameFolder(request);
                
                if (result)
                {
                    //Renames the folder on the database
                    var res = await _service.RenameFolder(request);
                    return res == true ? Ok("Folder renamed successfully") : BadRequest("Folder renaming failed");
                }
                else
                {
                    return BadRequest("Something went arong");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteFolder([FromBody] DeleteFolderRequest request)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(request.Name))
                {
                    return BadRequest("Folder Name cannot be empty");
                }
                //Deletes folder from the server
                var result = await _serverService.DeleteFolder(request);
                if (result)
                {
                    //Deletes the folder from the database
                    var res = await _service.DeleteFolder(request);
                    return res == true ? Ok("Folder deleted successfully") : BadRequest("Folder deletion failed");
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

        [HttpGet]
        [Route("get-folder-content")]
        public async Task<IActionResult> GetFolderContents([FromQuery] GetFolderContentsRequest request)
        {
            try
            {
                var res = new GetFolderContentResponse();

                //Gets the folder contents from the server
                res = await _serverService.GetFolderContents(request);
                if (res.FolderCount == 0 && res.FileCount == 0)
                {
                    //Gets the folder contents from the database
                    res = await _service.GetFolderContents(request.FolderId);
                    return Ok(res);
                }

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
