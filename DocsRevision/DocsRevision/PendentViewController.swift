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
