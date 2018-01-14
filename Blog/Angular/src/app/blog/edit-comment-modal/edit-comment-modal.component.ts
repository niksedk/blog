import { Component, OnInit, Input } from '@angular/core';
import { NgbModal, ModalDismissReasons, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { BlogComment } from '../models/blog-comment';
import { BlogService } from '../blog.service';

@Component({
  selector: 'app-edit-comment-modal',
  templateUrl: './edit-comment-modal.component.html',
  styleUrls: ['./edit-comment-modal.component.scss']
})
export class EditCommentModalComponent implements OnInit {

  @Input() comment: BlogComment;

  public closeResult: string;
  private modalRef: NgbModalRef;
  public updateIsBusy: boolean;
  public commentBody: string;

  constructor(private modalService: NgbModal,
              private blogService: BlogService) { }

  ngOnInit() {
  }

  open(content) {
    this.commentBody = this.comment.body;
    this.modalRef = this.modalService.open(content);
    this.modalRef.result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    });
  }

  public updateComment() {
    this.updateIsBusy = true;
    this.comment.body = this.commentBody;
    this.blogService.updateComment(this.comment).subscribe(res => {
      this.updateIsBusy = true;
      this.modalRef.close();
    });
  }

}
