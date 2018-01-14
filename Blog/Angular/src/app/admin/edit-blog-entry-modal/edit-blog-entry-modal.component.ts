import { Component, OnInit, Input } from '@angular/core';
import { BlogEntry } from '../../blog/models/blog-entry';
import { NgbModalRef, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { BlogService } from '../../blog/blog.service';

@Component({
  selector: 'app-edit-blog-entry-modal',
  templateUrl: './edit-blog-entry-modal.component.html',
  styleUrls: ['./edit-blog-entry-modal.component.scss']
})
export class EditBlogEntryModalComponent implements OnInit {

  @Input() blogEntry: BlogEntry;

  private modalRef: NgbModalRef;
  public updateIsBusy: boolean;
  public blogEntryTitle: string;
  public blogEntryBody: string;
  public blogEntryCommentsDisabled: boolean;

  constructor(private modalService: NgbModal,
              private blogService: BlogService) { }

  ngOnInit() {
  }

  open(content) {
    this.blogEntryTitle = this.blogEntry.title;
    this.blogEntryBody = this.blogEntry.body;
    this.blogEntryCommentsDisabled = this.blogEntry.commentsDisabled;
    this.modalRef = this.modalService.open(content);
  }

  public updateBlogEntry() {
    this.updateIsBusy = true;
    this.blogEntry.title = this.blogEntryTitle;
    this.blogEntry.body = this.blogEntryBody;
    this.blogEntry.commentsDisabled = this.blogEntryCommentsDisabled;
    this.blogService.updateBlogEntry(this.blogEntry).subscribe(res => {
      this.updateIsBusy = true;
      this.modalRef.close();
    });
  }

}
