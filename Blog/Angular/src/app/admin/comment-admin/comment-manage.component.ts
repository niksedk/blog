import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { BlogService } from '../../blog/blog.service';
import { BlogComment } from '../../blog/models/blog-comment';

@Component({
  selector: 'app-comment-manage',
  templateUrl: './comment-manage.component.html',
  styleUrls: ['./comment-manage.component.scss']
})
export class CommentManageComponent implements OnInit {

  public comments: Observable<BlogComment[]>;

  constructor(private blogService: BlogService) {
    this.comments = blogService.listComments();
  }

  ngOnInit() {
  }

  public deleteComment(comment: BlogComment) {
    this.blogService.deleteComment(comment.blogCommentId)
      .subscribe(res => {
        this.comments = this.comments.map(u => u.filter(b => b.blogCommentId !== comment.blogCommentId));
      });
  }

}
