namespace InnoClinic.Documents.Core.Models.NotificationModels;

public record SendAppointmentResultDocumentRequest(
    byte[] PdfBytes,
    string Email
);