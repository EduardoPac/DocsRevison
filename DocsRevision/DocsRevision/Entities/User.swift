import Foundation

class User{
    var Id: String
    var Name:String
    var Email:String
    
    init(id: String, name: String, email: String) {
        self.Id = id
        self.Name = name
        self.Email = email
    }
}
