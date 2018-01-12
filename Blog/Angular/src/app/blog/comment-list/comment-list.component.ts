import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { BlogEntryFull } from '../models/blog-entry-full';
import { BlogService } from '../blog.service';
import { BlogComment } from '../models/blog-comment';
import { BlogEntryComponent } from '../blog-entry/blog-entry.component';
import { TokenService } from '../../account/token.service';

@Component({
  selector: 'app-comment-list',
  templateUrl: './comment-list.component.html',
  styleUrls: ['./comment-list.component.scss']
})
export class CommentListComponent implements OnInit {
  @Input() blogEntryWithComments: BlogEntryFull;

  public newComment: BlogComment;
  public hasValidToken: boolean;
  public isAdministrator: boolean;
  public currentUserId: number;
  public currentName: string;

  constructor(private blogService: BlogService, tokenService: TokenService) {
    this.hasValidToken = tokenService.hasValidToken(); 
    this.isAdministrator = tokenService.isAdministrator();
    this.currentUserId = tokenService.getTokenUserId();
    this.currentName = tokenService.getTokenName();
  }

  ngOnInit() {
    this.resetNewComment();
  }

  private resetNewComment() {
    this.newComment = {
      email: '',
      blogCommentId: 0,
      blogEntryId: this.blogEntryWithComments.blogEntryId,
      body: '',
      created: null,
      createdBy: null,
      modified: null,
      name: ''
    };
  }

  public addComment() {
    this.blogService.addComment(this.newComment).subscribe(res => {
      this.blogEntryWithComments.comments.push(res);
      this.resetNewComment();
    });
  }

  public deleteComment(commentId: number) {
    this.blogService.deleteComment(commentId)
    .subscribe(res => {
      this.blogEntryWithComments.comments = this.blogEntryWithComments.comments
      .filter((c) => c.blogCommentId !== commentId);
    });
  }

  public editComment(comment: BlogComment) {
    alert('edit');
  }  

}
