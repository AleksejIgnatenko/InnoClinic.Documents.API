using InnoClinic.Documents.Core.Models.NotificationModels;
using iTextSharp.text.pdf;
using System.Text.Json;
using System.Text;

namespace InnoClinic.Documents.Application.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly HttpClient _httpClient;

        public DocumentService()
        {
            _httpClient = new HttpClient();
        }

        public byte[] CreateAppointmentResultDocument(
            string date,
            string patientFullName,
            string dateOfBirth,
            string doctorFullName,
            string specialization,
            string medicalServiceName,
            string complaints,
            string conclusion,
            string diagnosis,
            string recommendations)
        {
            var document = new iTextSharp.text.Document();
            var memoryStream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
            document.Open();

            PdfPTable table = new PdfPTable(2); 
            table.AddCell("Patient");
            table.AddCell(patientFullName);
            table.AddCell("Date of Birth");
            table.AddCell(dateOfBirth);
            table.AddCell("Doctor");
            table.AddCell(doctorFullName);
            table.AddCell("Specialization");
            table.AddCell(specialization);
            table.AddCell("Medical Service Name");
            table.AddCell(medicalServiceName);
            table.AddCell("Complaints");
            table.AddCell(complaints);
            table.AddCell("Conclusion");
            table.AddCell(conclusion);
            table.AddCell("Diagnosis");
            table.AddCell(diagnosis);
            table.AddCell("Recommendations");
            table.AddCell(recommendations);

            document.Add(table);
            document.Close();

            var pdfBytes = memoryStream.ToArray();

            return pdfBytes;
        }

        public async Task CreateAndSendAppointmentResultDocumentToEmailAsync(
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
            string email)
        {
            var document = new iTextSharp.text.Document();
            var memoryStream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
            document.Open();

            PdfPTable table = new PdfPTable(2);
            table.AddCell("Patient");
            table.AddCell(patientFullName);
            table.AddCell("Date of Birth");
            table.AddCell(dateOfBirth);
            table.AddCell("Doctor");
            table.AddCell(doctorFullName);
            table.AddCell("Specialization");
            table.AddCell(specialization);
            table.AddCell("Medical Service Name");
            table.AddCell(medicalServiceName);
            table.AddCell("Complaints");
            table.AddCell(complaints);
            table.AddCell("Conclusion");
            table.AddCell(conclusion);
            table.AddCell("Diagnosis");
            table.AddCell(diagnosis);
            table.AddCell("Recommendations");
            table.AddCell(recommendations);

            document.Add(table);
            document.Close();

            var pdfBytes = memoryStream.ToArray();
            var requestPayload = new SendAppointmentResultDocumentRequest(pdfBytes, email);

            var jsonContent = JsonSerializer.Serialize(requestPayload);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            await _httpClient.PostAsync("http://innoclinic_notification_api:8080/api/Notification/send-appointment-result-document", content);
        }
    }
}