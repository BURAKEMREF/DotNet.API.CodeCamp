using Microsoft.AspNetCore.Http;

namespace WebApiProject.Models
{
    public class RequestMail
    {
        public Guid? TableRowId { get; set; }

        public string? Subject { get; set; }

        public int MailType { get; set; }

        public string? MailDisplayName { get; set; }

        public Recipients Recipients { get; set; } = new Recipients();

        public List<FormFile> AttachmentList { get; set; } = new List<FormFile>();

        public string? MailTemplateName { get; set; } = string.Empty;

        public ProfileUpdateDemandStructure ProfileUpdateDemandStructure { get; set; } = new ProfileUpdateDemandStructure();

        public OtherMailTemplate OtherMailTemplate { get; set; } = new OtherMailTemplate();
    }

    public class Recipients
    {
        public List<string> ListofEmailTo { get; set; } = new List<string>();

        public List<string> ListOfEmailCC { get; set; } = new List<string>();

        public List<string> ListOfEmailBCC { get; set; } = new List<string>();
    }

    public class RecipientsCombined
    {
        public string ListofEmailTo { get; set; } = string.Empty;

        public string ListOfEmailCC { get; set; } = string.Empty;

        public string ListOfEmailBCC { get; set; } = string.Empty;
    }

    public class OtherMailTemplate
    {
    }

    public enum MailStatus
    {
        WaitingToBeSend = 0,
        SentSuccessfully = 1,
        SentFailed = 2
    }

    public static class MailConsts
    {
        #region Mail Templates

        public const string VirtualHRResignationDemandMailTemplateName = "101IKResignationDemandMailTemplate.html";
        public const string OtherMailTemplateName = "OtherMailTemplate.html";
        public const string VirtualHRProfileUpdateMailTemplateName = "101IKProfileUpdateDemandMailTemplate.html";
        public const string VirtualHRProfileNotificationMailTemplateName = "101IKProfileNotificationMailTemplate.html";
        public const string VirtualHRSurveyAssignedMailTemplateName = "101IKSurveyMailAssignedTemplate.html";

        #endregion
    }
}