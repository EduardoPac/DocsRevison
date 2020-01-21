using System;

namespace DocsRevision.Models
{
    public class Document
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NameFull { get; set; }
        public string Extension { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreatorId { get; set; }
        public string CreatorName { get; set; }
        public string RevisionId { get; set; }
        public string RevisorName { get; set; }
        public Revision CurrentRevision { get; set; }
        public Revision LastRevision { get; set; }


        public string RevisionFormated { get => "Revisão " + CurrentRevision.NumRevision; }
        public string CreatedInFormated { get => "Criado em " + CreationDate.ToString("dd/MM/yyyy"); }
        public string CreatedForFormated { get => "Criado por " + CreatorName; }
        public string RevisorFormated { get => "Revisor " + RevisorName; }
        public string IsObsolet { get => CurrentRevision.IsObsolete ? "Obsoleto: Sim" : "Obsoleto: Não"; }
        public bool IsPendent { get => CurrentRevision.Status == Enum.EStatus.Pending ? false : true; }
        public bool IsNotPendent => !IsPendent;
        public bool IsRevisor { get => CurrentRevision.RevisorId == App.User.Id ? true : false; }
    }
}
