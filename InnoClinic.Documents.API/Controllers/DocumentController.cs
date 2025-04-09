using InnoClinic.Documents.Application.Services;
using InnoClinic.Documents.Core.Models.AppointmentResultModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.Documents.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DocumentController : ControllerBase
{
    private readonly IDocumentService _documentService;

    public DocumentController(IDocumentService documentService)
    {
        _documentService = documentService;
    }

    [HttpPost]
    public IActionResult CreateAppointmentResultDocument([FromBody] CreateDocumentAppointmentResultRequest createDocumentAppointmentResultRequest)
    {
        byte[] pdfBytes = _documentService.CreateAppointmentResultDocument(createDocumentAppointmentResultRequest.Date, createDocumentAppointmentResultRequest.PatientFullName, createDocumentAppointmentResultRequest.DateOfBirth, createDocumentAppointmentResultRequest.DoctorFullName, createDocumentAppointmentResultRequest.Specialization, createDocumentAppointmentResultRequest.MedicalServiceName, createDocumentAppointmentResultRequest.Complaints, createDocumentAppointmentResultRequest.Conclusion, createDocumentAppointmentResultRequest.Diagnosis, createDocumentAppointmentResultRequest.Recommendations);

        MemoryStream memoryStream = new MemoryStream(pdfBytes);
        return File(memoryStream, "application/pdf", "appointment_result_document.pdf");
    }

    [HttpPost("create-and-send-appointment-result-document-to-email")]
    public async Task<IActionResult> CreateAndSendAppointmentResultDocumentToEmailAsync([FromBody] CreateAndSendAppointmentResultDocumentToEmailRequest createAndSendAppointmentResultDocumentToEmailRequest)
    {
        await _documentService.CreateAndSendAppointmentResultDocumentToEmailAsync(createAndSendAppointmentResultDocumentToEmailRequest.Date, createAndSendAppointmentResultDocumentToEmailRequest.PatientFullName, createAndSendAppointmentResultDocumentToEmailRequest.DateOfBirth, createAndSendAppointmentResultDocumentToEmailRequest.DoctorFullName, createAndSendAppointmentResultDocumentToEmailRequest.Specialization, createAndSendAppointmentResultDocumentToEmailRequest.MedicalServiceName, createAndSendAppointmentResultDocumentToEmailRequest.Complaints, createAndSendAppointmentResultDocumentToEmailRequest.Conclusion, createAndSendAppointmentResultDocumentToEmailRequest.Diagnosis, createAndSendAppointmentResultDocumentToEmailRequest.Recommendations, createAndSendAppointmentResultDocumentToEmailRequest.Email);

        return Ok();
    }
}