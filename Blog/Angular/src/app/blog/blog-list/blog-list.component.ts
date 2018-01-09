import { Component, OnInit } from '@angular/core';
import { BlogService } from '../blog.service';
import { BlogEntry } from '../models/blog-entry';
import { Observable } from 'rxjs/Observable';
import { BlogEntryFull } from '../models/blog-entry-full';

@Component({
  selector: 'app-blog-list',
  templateUrl: './blog-list.component.html',
  styleUrls: ['./blog-list.component.scss']
})
export class BlogListComponent implements OnInit {

    public blogEntries: Observable<BlogEntry[]>;

    constructor(private data: BlogService) { }

    ngOnInit() {
      this.blogEntries = this.data.list();
    }
}
