import { Component, OnInit } from '@angular/core';
import { BlogService } from '../../blog/blog.service';
import { Observable } from 'rxjs/Observable';
import { BlogEntry } from '../../blog/models/blog-entry';
import 'rxjs/add/operator/filter';
import 'rxjs/add/operator/map';

@Component({
  selector: 'app-blog-manage',
  templateUrl: './blog-manage.component.html',
  styleUrls: ['./blog-manage.component.scss']
})
export class BlogManageComponent implements OnInit {

  public blogEntries: Observable<BlogEntry[]>;

  constructor(private blogService: BlogService) { 
    this.resetNewBlogEntry();
  }

  public newBlogEntry: BlogEntry;

  private resetNewBlogEntry() {
    this.newBlogEntry = {
      email: '',
      blogEntryId: 0,
      body: '',
      created: null,
      name: '',
      commentCount: 0,
      title: '',
      urlFriendlyId: '',
      userId: 0,
      commentsDisabled: false
    };
  }

  ngOnInit() {
    this.blogEntries = this.blogService.list();
  }

  public deleteBlogEntry(blogEntry: BlogEntry) {
    this.blogService.deleteBlogEntry(blogEntry.blogEntryId)
    .subscribe(res => {
      console.log('blogEntry deleted ' + blogEntry.blogEntryId);
      this.blogEntries = this.blogEntries
      .map(blogEntries => blogEntries.filter(b => b.blogEntryId !== blogEntry.blogEntryId));
    });
  }

  public addBlogEntry() {
    this.blogService.addBlogEntry(this.newBlogEntry)
    .subscribe(res => {
      console.log('blogEntry added ' + this.newBlogEntry.title);
      this.blogEntries = this.blogService.list();
    });
  }  

}
