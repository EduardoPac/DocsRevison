import Foundation

class Document{
    var Id: String
    var Name: String
    var CreationDate: NSDate
    var CreationId: String
    var RevisionId: String
    var Revision: Revision
    
    init(
        id: String,
        name: String,
        creationDate: NSDate,
        creationId: String,
        revisionId: String,
        revision: Revision) {
        self.Id=id
        self.Name=name
        self.CreationDate=creationDate
        self.CreationId=creationId
        self.RevisionId=revisionId
        self.Revision=revision
    }
}