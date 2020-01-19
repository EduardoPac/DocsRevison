import Foundation

class Document{
    var Id: String
    var Name: String
    var FullName: String
    var CreationDate: NSDate
    var Extension: String
    var DocType : EType
    var CreatorId: String
    var CreatorName: String
    var RevisionId: String
    var Revision: Revision
    
    init(
        id: String,
        name: String,
        fullName : String,
        creationDate: NSDate,
        exte: String,
        creatorId: String,
        creatorName: String,
        docType : EType,
        revisionId: String,
        revision: Revision) {
        self.Id=id
        self.Name=name
        self.FullName = fullName
        self.DocType = docType
        self.CreationDate=creationDate
        self.Extension = exte
        self.CreatorId=creatorId
        self.CreatorName=creatorName
        self.RevisionId=revisionId
        self.Revision=revision
    }
}

enum EType {
    case pdf
    case doc
    case ppt
    case xls
}
