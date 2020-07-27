//
//  PendentViewController.swift
//  DocsRevision
//
//  Created by Eduardo-Forlogic on 19/01/20.
//  Copyright © 2020 Eduardo. All rights reserved.
//

import UIKit

class PendentViewController: UITableViewController {

    var dados: [Document] = [];
    
    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view.
    }
    
    override func viewDidAppear(_ animated: Bool) {
        LoadData();
        tableView.reloadData()
    }
    
    func LoadData(){
        var document: Document
         
        document = Document(id: "d", name: "fee", fullName: "Fee.doc", creationDate: NSDate.now as NSDate, exte: "doc", creatorId: "fff", creatorName: "Eduardo", revisionId: "ss", revision: Revision(id: "dd", documentId: "ff", numRevision: 1, revisorId: "ff", revisor: User(id: "FF", name: "aa", email: "AD"), approverId: "SS", approver: User(id: "S", name: "Edk", email: "ddd"), status: EStatus.Ok, revisionDate: NSDate.now as NSDate, expirationDate: NSDate.now as NSDate, deadlineDate: NSDate.now as NSDate, isObsolete: false))
         
         dados.append(document)
         
         document = Document(id: "d", name: "fee", fullName: "Fee.xls", creationDate: NSDate.now as NSDate, exte: "xls", creatorId: "fff", creatorName: "Eduardo", revisionId: "ss", revision: Revision(id: "dd", documentId: "ff", numRevision: 1, revisorId: "ff", revisor: User(id: "FF", name: "aa", email: "AD"), approverId: "SS", approver: User(id: "S", name: "Edk", email: "ddd"), status: EStatus.Ok, revisionDate: NSDate.now as NSDate, expirationDate: NSDate.now as NSDate, deadlineDate: NSDate.now as NSDate, isObsolete: false))
         
         dados.append(document)
        
         document = Document(id: "d", name: "fee", fullName: "Fee.pdf", creationDate: NSDate.now as NSDate, exte: "pdf", creatorId: "fff", creatorName: "Eduardo", revisionId: "ss", revision: Revision(id: "dd", documentId: "ff", numRevision: 1, revisorId: "ff", revisor: User(id: "FF", name: "aa", email: "AD"), approverId: "SS", approver: User(id: "S", name: "Edk", email: "ddd"), status: EStatus.Ok, revisionDate: NSDate.now as NSDate, expirationDate: NSDate.now as NSDate, deadlineDate: NSDate.now as NSDate, isObsolete: false))
                
         dados.append(document)
    }

    override func numberOfSections(in tableView: UITableView) -> Int {
        return 1;
    }
    
    override func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return dados.count;
    }
    
    override func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let doc = dados[indexPath.row];
        
        let cel = "pendentCell"
        let celula = tableView.dequeueReusableCell(withIdentifier: cel, for: indexPath) as! PendentCell
        
        celula.NameFullPent.text = doc.FullName
        celula.CreatorNamePent.text = doc.CreatorName
        celula.NumRevisionPent.text = "Revisão:" + String(doc.Revision.NumRevision)
        celula.DocImagePen.image = UIImage(named: doc.Extension)
        
        return celula
    }
}
