import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { BlogEntry } from './models/blog-entry';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { BlogEntryFull } from './models/blog-entry-full';
import { BlogComment } from './models/blog-comment';

@Injectable()
export class BlogService {
  private baseUrl = 'http://localhost:54882/api/blog'; // TODO: move to json setting

  constructor(private http: Http) {}

  list(): Observable<BlogEntry[]> {
    console.log('calling GET ' + this.baseUrl + '/list');

    return this.http.get(this.baseUrl).map(res => <BlogEntry[]>res.json());

    // "class" way of doing this
    // return this.http.get(this.baseUrl + "/list").map(res => {
    //   return res.json().map(item => {
    //     return new BlogEntry {
    //       item.title,
    //       item.body,
    //       item.email,
    //       item.userId,
    //       item.name,
    //       item.created
    //     };
    //   });
    // });
  }

  getFullBlogEntry(friendlyId: string): Observable<BlogEntryFull> {
    console.log('calling GET ' + this.baseUrl + '?urlFriendlyId=' + friendlyId);
    return this.http.get(this.baseUrl + '/' + friendlyId).map(res => <BlogEntryFull>res.json());
  }

  deleteBlogEntry(blogEntryId: number) {
    console.log('calling DELETE ' + this.baseUrl + '/' + blogEntryId);
    return this.http.delete(this.baseUrl + '/' + blogEntryId);
  }

  addComment(comment: BlogComment): Observable<BlogComment> {
    console.log('calling POST ' + this.baseUrl + '/' + comment.blogEntryId + '/comments');
    return this.http.post(this.baseUrl + '/' + comment.blogEntryId + '/comments', comment)
                          .map(res => <BlogComment>res.json());
  }

  addBlogEntry(blogEntry: BlogEntry): Observable<BlogEntry> {
    console.log('calling POST ' + this.baseUrl);
    return this.http.post(this.baseUrl, blogEntry)
      .map(res => <BlogEntry>res.json());
  }

  deleteComment(blogEntryId: number, blogCommentId: number) {
    console.log('calling DELETE ' + this.baseUrl + '/' + blogEntryId + '/comments/' + blogCommentId);
    return this.http.delete(this.baseUrl + '/' + blogEntryId + '/comments/' + blogCommentId);
  }

}
