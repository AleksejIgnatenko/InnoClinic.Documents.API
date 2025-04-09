namespace InnoClinic.Documents.Core.Models.AppointmentResultModels;

public record CreateDocumentAppointmentResultRequest(
    string Date,
    string PatientFullName,
    string DateOfBirth,
    string DoctorFullName,
    string Specialization,
    string MedicalServiceName,
    string Complaints,
    string Conclusion,
    string Diagnosis,
    string Recommendations);