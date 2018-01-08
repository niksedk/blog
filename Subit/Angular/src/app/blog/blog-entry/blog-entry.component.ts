import { Component, OnInit, Input } from '@angular/core';
import { BlogEntry } from '../models/blog-entry';

@Component({
  selector: 'app-blog-entry',
  templateUrl: './blog-entry.component.html',
  styleUrls: ['./blog-entry.component.scss']
})
export class BlogEntryComponent implements OnInit {
  @Input() blogEntry: BlogEntry;
  @Input() displayCommentsLink: boolean;

  constructor() { }

  ngOnInit() {
  }

}
