import Foundation

class Revision{
    var Id: String
    var DocumentId: String
    var NumRevision: Int
    var RevisorId: String
    var Revisor: User
    var ApproverId: String
    var Approver: User
    var Status: EStatus
    var RevisionDate: NSDate
    var ExpirationDate: NSDate
    var DeadlineDate: NSDate
    var IsObsolete: Bool
    
    init (
        id: String,
        documentId:String
        numRevision: Int,
        revisorId: String,
        revisor: User,
        approverId: String,
        approver: User,
        status: EStatus,
        revisionDate: NSDate,
        expirationDate: NSDate,
        deadlineDate: NSDate,
        isObsolete: Bool)
    {
        self.Id = id
        self.DocumentId = documentId
        self.NumRevision = numRevision
        self.RevisorId = revisorId
        self.Revisor = revisor
        self.ApproverId = approverId
        self.Approver = approver
        self.Status = status
        self.RevisionDate = revisionDate
        self.ExpirationDate = expirationDate
        self.DeadlineDate = deadlineDate
        self.IsObsolete = isObsolete
    }
}

enum EStatus {
    case Ok
    case Pending
    case Aproved
    case Reproved
    case Obsolete
    case Removed
}
