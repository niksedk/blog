import { Component, OnInit } from '@angular/core';
import { BlogEntryFull } from '../models/blog-entry-full';
import { ActivatedRoute } from '@angular/router';
import { BlogService } from '../blog.service';

@Component({
  selector: 'app-blog-entry-with-comments',
  templateUrl: './blog-entry-with-comments.component.html',
  styleUrls: ['./blog-entry-with-comments.component.scss']
})
export class BlogEntryWithCommentsComponent implements OnInit {

  urlFriendlyId: string;
  public blogEntryWithComments: BlogEntryFull;

  constructor(private route: ActivatedRoute, private data: BlogService) {
    this.route.params.subscribe(res => {
        this.urlFriendlyId = res.urlFriendlyId;
        this.data.getFullBlogEntry(this.urlFriendlyId).subscribe(full => {
           this.blogEntryWithComments = full;
        });
      });
   }

  ngOnInit() {
  }

}
