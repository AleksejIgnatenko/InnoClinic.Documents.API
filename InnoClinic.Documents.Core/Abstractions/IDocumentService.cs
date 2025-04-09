namespace InnoClinic.Documents.Application.Services;

public interface IDocumentService
{
    byte[] CreateAppointmentResultDocument(
        string date,
        string patientFullName,
        string dateOfBirth,
        string doctorFullName,
        string specialization,
        string medicalServiceName,
        string complaints,
        string conclusion,
        string diagnosis,
        string recommendations);

    Task CreateAndSendAppointmentResultDocumentToEmailAsync(
        string date,
        string patientFullName,
        string dateOfBirth,
        string doctorFullName,
        string specialization,
        string medicalServiceName,
        string complaints,
        string conclusion,
        string diagnosis,
        string recommendations,
        string email);
}