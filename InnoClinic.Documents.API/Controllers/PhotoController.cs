using InnoClinic.Documents.Application.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.Documents.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PhotoController : ControllerBase
{
    private readonly IPhotoService _photoService;

    public PhotoController(IPhotoService photoService)
    {
        _photoService = photoService;
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult> CreatePhotoAsync(IFormFile photo)
    {
        return Ok(await _photoService.CreatePhotoAsync(photo));
    }

    [AllowAnonymous]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetPhotoByIdAsync(string id)
    {
        var fileStream = await _photoService.GetPhotoByIdAsync(id);

        return File(fileStream, "application/octet-stream", id.ToString());
    }

    [HttpPut("{id:guid}")]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult> UpdatePhotoAsync(IFormFile photo, string id)
    {
        await _photoService.UpdatePhotoAsync(photo, id);

        return Ok();
    }
}